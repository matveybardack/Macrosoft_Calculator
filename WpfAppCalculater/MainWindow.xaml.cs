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
            {
                return;
            }

            if (_pendingOperator != null && !_justCalculated)
            {
                if (_storedValue.HasValue)
                {
                    if (_currentInput == "0")
                    {
                        Display.Text = string.Format(_culture, "{0}{1}", _storedValue.Value, _pendingOperator);
                    }
                    else
                    {
                        Display.Text = string.Format(_culture, "{0}{1}{2}", _storedValue.Value, _pendingOperator, _currentInput);
                    }
                    return;
                }
            }

            Display.Text = _currentInput;
        }

        /// <summary>
        /// Нажатие на число
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDigitClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string digit = Convert.ToString(button.Content, _culture);

                if (_justCalculated)
                {
                    _currentInput = "0";
                    _justCalculated = false;
                }

                if (_currentInput == "0")
                {
                    _currentInput = digit;
                }
                else
                {
                    _currentInput += digit;
                }

                UpdateDisplay();
            }
        }

        /// <summary>
        /// Нажатие на запятую
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDecimalClick(object sender, RoutedEventArgs e)
        {
            string sep = _culture.NumberFormat.NumberDecimalSeparator;

            if (_justCalculated)
            {
                _currentInput = "0";
                _justCalculated = false;
            }

            if (!_currentInput.Contains(sep))
            {
                _currentInput += sep;
                UpdateDisplay();
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
                        Compute(value);
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
            UpdateDisplay();
        }
    }
}
