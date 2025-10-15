using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using ClassLibraryCalculater;

namespace WpfAppCalculater
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CalculatorService _calculator = new CalculatorService();
        private readonly CultureInfo _culture = CultureInfo.CurrentCulture;

        private string _currentInput = "0";
        private double? _storedValue = null;
        private string _pendingOperator = null;
        private bool _justCalculated = false;
        private bool _isFractionInput = false;
        private string _numeratorPart = "";
        private string _denominatorPart = "";

        public MainWindow()
        {
            InitializeComponent();
            UpdateDisplay();
        }

        /// <summary>
        /// Обновление экрана калькулятора
        /// </summary>
        private void UpdateDisplay()
        {
            if (Display == null)
                return;

            if (_pendingOperator != null && !_justCalculated)
                if (_storedValue.HasValue)
                {
                    string displayRightOperand = _currentInput;
                    if (displayRightOperand.StartsWith("-") && displayRightOperand != "0")
                    {
                        displayRightOperand = $"({displayRightOperand})";
                    }

                    Display.Text = string.Format(_culture, "{0} {1} {2}",
                        _storedValue.Value, _pendingOperator, displayRightOperand);
                    return;
                }

            // Отображаем дробь в специальном формате
            if (_isFractionInput && _currentInput.Contains("/"))
            {
                string[] parts = _currentInput.Split('/');
                if (parts.Length == 2)
                {
                    Display.Text = $"{parts[0]}\n―\n{parts[1]}";
                    return;
                }
            }

            Display.Text = _currentInput;
        }

        private void OnDigitClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string digit = Convert.ToString(button.Content, _culture);

                if (_justCalculated)
                {
                    _currentInput = "0";
                    _justCalculated = false;
                    _isFractionInput = false;
                    _numeratorPart = "";
                    _denominatorPart = "";
                }

                if (_isFractionInput)
                {
                    // Ввод в знаменатель
                    _denominatorPart += digit;
                    _currentInput = _numeratorPart + "/" + _denominatorPart;
                }
                else
                {
                    // Обычный ввод
                    if (_currentInput == "0")
                    {
                        _currentInput = digit;
                    }
                    else
                    {
                        _currentInput += digit;
                    }
                }

                UpdateDisplay();
            }
        }

        private void OnDecimalClick(object sender, RoutedEventArgs e)
        {
            string sep = _culture.NumberFormat.NumberDecimalSeparator;

            if (_justCalculated)
            {
                _currentInput = "0";
                _justCalculated = false;
                _isFractionInput = false;
                _numeratorPart = "";
                _denominatorPart = "";
            }

            if (_isFractionInput)
            {
                // Десятичная точка для знаменателя
                if (!_denominatorPart.Contains(sep))
                {
                    _denominatorPart += sep;
                    _currentInput = _numeratorPart + "/" + _denominatorPart;
                    UpdateDisplay();
                }
            }
            else
            {
                // Обычный ввод
                if (!_currentInput.Contains(sep))
                {
                    _currentInput += sep;
                    UpdateDisplay();
                }
            }
        }

        /// <summary>
        /// Нажатие на знак операции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOperatorClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string op = Convert.ToString(button.Content, _culture);

                if (double.TryParse(_currentInput, NumberStyles.Float, _culture, out double value))
                {
                    if (_storedValue.HasValue && _pendingOperator != null && !_justCalculated)
                    {
                        if (_currentInput == "0")
                        {
                            _pendingOperator = op;
                            UpdateDisplay();
                            return;
                        }
                        else
                        {
                            Compute(value);
                        }
                    }
                    else
                    {
                        _storedValue = value;
                    }

                    _pendingOperator = op;
                    _currentInput = "0";
                    _justCalculated = false;
                    UpdateDisplay();
                }
            }
        }

        /// <summary>
        /// Надатие на знак "равно" 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEqualsClick(object sender, RoutedEventArgs e)
        {
            if (!_storedValue.HasValue || string.IsNullOrEmpty(_pendingOperator))
            {
                return;
            }

            if (!double.TryParse(_currentInput, NumberStyles.Float, _culture, out double right))
            {
                return;
            }

            double left = _storedValue.Value;

            try
            {
                double result = Compute(right);

                string historyEntry = string.Format(_culture, "{0} {1} {2} = {3}",
                    left, _pendingOperator, right, result);
                HistoryList?.Items.Insert(0, historyEntry);

                _currentInput = result.ToString(_culture);
                _storedValue = null;
                _pendingOperator = null;
                _justCalculated = true;
                UpdateDisplay();
            }
            catch (DivideByZeroException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                ClearAll();
            }
        }

        private void NegateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Если есть ожидающая операция (значит работаем с правым операндом)
                if (_pendingOperator != null && !_justCalculated)
                {
                    // Если текущий ввод уже содержит отрицательное число
                    if (_currentInput.StartsWith("-") && _currentInput != "0")
                    {
                        // Убираем минус
                        _currentInput = _currentInput.Substring(1);
                        if (string.IsNullOrEmpty(_currentInput)) _currentInput = "0";
                    }
                    else if (_currentInput == "0")
                    {
                        // Для нуля просто ставим минус
                        _currentInput = "-";
                    }
                    else
                    {
                        // Добавляем минус в начало
                        _currentInput = "-" + _currentInput;
                    }
                }
                else
                {
                    // Работаем с левым операндом или одиночным числом
                    if (double.TryParse(_currentInput, NumberStyles.Float, _culture, out double currentValue))
                    {
                        double result = _calculator.Negate(currentValue);
                        _currentInput = result.ToString(_culture);
                    }
                }

                UpdateDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnFractionClick(object sender, RoutedEventArgs e)
        {
            if (_isFractionInput)
            {
                // Завершение ввода дроби
                if (!string.IsNullOrEmpty(_numeratorPart) && !string.IsNullOrEmpty(_denominatorPart))
                {
                    if (double.TryParse(_numeratorPart, NumberStyles.Float, _culture, out double numerator) &&
                        double.TryParse(_denominatorPart, NumberStyles.Float, _culture, out double denominator) &&
                        denominator != 0)
                    {
                        double fractionValue = numerator / denominator;
                        _currentInput = fractionValue.ToString(_culture);
                    }
                    else
                    {
                        _currentInput = "0";
                    }
                }

                _isFractionInput = false;
                _numeratorPart = "";
                _denominatorPart = "";
            }
            else
            {
                // Начинаем ввод дроби
                _isFractionInput = true;
                _numeratorPart = _currentInput == "0" ? "" : _currentInput;
                _denominatorPart = "";
                _currentInput = _numeratorPart + "/";
            }

            UpdateDisplay();
        }

        /// <summary>
        /// Вычисление операции
        /// </summary>
        /// <param name="right">правый операнд</param>
        /// <returns></returns>
        private double Compute(double right)
        {
            if (!_storedValue.HasValue || string.IsNullOrEmpty(_pendingOperator))
            {
                return right;
            }

            double left = _storedValue.Value;
            double result;

            switch (_pendingOperator)
            {
                case "+":
                    result = _calculator.Add(left, right);
                    break;
                case "-":
                    result = _calculator.Subtract(left, right);
                    break;
                case "*":
                    result = _calculator.Multiply(left, right);
                    break;
                case "/":
                    result = _calculator.Divide(left, right);
                    break;
                default:
                    result = right;
                    break;
            }

            _storedValue = result;
            _currentInput = "0";
            return result;
        }

        /// <summary>
        /// Нажатие на "С"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClearClick(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            _currentInput = "0";
            _storedValue = null;
            _pendingOperator = null;
            _justCalculated = false;
            _isFractionInput = false;
            _numeratorPart = "";
            _denominatorPart = "";
            UpdateDisplay();
        }
    }
}
