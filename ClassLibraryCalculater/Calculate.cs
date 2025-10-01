using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryCalculater
{
    // Калькулятор
    public class Calculate
    {
        /// <summary>
        /// Операция сложения
        /// </summary>
        /// <param name="leftOperand">левое слагаемое</param>
        /// <param name="rightOperand">правое слагаемое</param>
        /// <returns>сумма</returns>
        public double Add(double leftOperand, double rightOperand)
        {
            return leftOperand + rightOperand;
        }

        /// <summary>
        /// Операция вычитания
        /// </summary>
        /// <param name="leftOperand">уменьшаемое</param>
        /// <param name="rightOperand">вычитаемое</param>
        /// <returns>разность</returns>
        public double Subtract(double leftOperand, double rightOperand)
        {
            return leftOperand - rightOperand;
        }

        /// <summary>
        /// Операция умножения
        /// </summary>
        /// <param name="leftOperand">левый множитель</param>
        /// <param name="rightOperand">правый множитель</param>
        /// <returns>умножение</returns>
        public double Multiply(double leftOperand, double rightOperand)
        {
            return leftOperand * rightOperand;
        }

        /// <summary>
        /// Операция деления
        /// </summary>
        /// <param name="dividend">делимое</param>
        /// <param name="divisor">делитель</param>
        /// <returns>частное</returns>
        /// <exception cref="DivideByZeroException">Делитель равен 0</exception>
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
