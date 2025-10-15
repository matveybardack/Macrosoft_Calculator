using ClassLibraryCalculater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCalculater
{
    internal interface ICalculate
    {
        // Определение экземпляра класса
        Calculate Calculator { get; }

        // Сложение
        double Add(double left, double right);

        // Вычитание
        double Subtract(double left, double right);

        // Умножение
        double Multiply(double left, double right);

        // Деление
        double Divide(double left, double right);

        // Изменение знака числа
        double Negate(double value);
    }
}
