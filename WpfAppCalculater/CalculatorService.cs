using ClassLibraryCalculater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCalculater
{
    internal class CalculatorService : ICalculate
    {
        public Calculate Calculator { get; }

        public double Add(double left, double right)
        {
            return Calculator.Add(left, right);
        }

        public double Subtract(double left, double right)
        {
            return Calculator.Subtract(left, right);
        }

        public double Multiply(double left, double right)
        {
            return Calculator.Multiply(left, right);
        }

        public double Divide(double left, double right)
        {
            return Calculator.Divide(left, right);
        }

        public double Negate(double value)
        {
            return Calculator.Negate(value);
        }


        public CalculatorService() {
            Calculator = new Calculate();
        }
    }
}
