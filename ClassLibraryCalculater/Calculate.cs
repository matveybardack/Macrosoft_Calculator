using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryCalculater
{
    public class Calculate
    {
        public double Add(double leftOperand, double rightOperand)
        {
            return leftOperand + rightOperand;
        }

        public double Subtract(double leftOperand, double rightOperand)
        {
            return leftOperand - rightOperand;
        }

        public double Multiply(double leftOperand, double rightOperand)
        {
            return leftOperand * rightOperand;
        }

        public double Divide(double dividend, double divisor)
        {
            if (Math.Abs(divisor) < double.Epsilon)
            {
                throw new DivideByZeroException("Деление на ноль невозможно.");
            }

            return dividend / divisor;
        }
    }
}
