namespace TestProjectCalculater
{
    public class UnitTestCalculate
    {
        [Fact]
        public void Add_TwoNumbers_ReturnsSum()
        {
            var calc = new ClassLibraryCalculater.Calculate();
            var result = calc.Add(77, 23);
            Assert.Equal(100, result);
        }

        [Fact]
        public void Subtract_TwoNumbers_ReturnsDifference()
        {
            var calc = new ClassLibraryCalculater.Calculate();
            var result = calc.Subtract(77, 23);
            Assert.Equal(54, result);
        }

        [Fact]
        public void Multiply_TwoNumbers_ReturnsProduct()
        {
            var calc = new ClassLibraryCalculater.Calculate();
            var result = calc.Multiply(7, 6);
            Assert.Equal(42, result);
        }

        [Fact]
        public void Divide_TwoNumbers_ReturnsQuotient()
        {
            var calc = new ClassLibraryCalculater.Calculate();
            var result = calc.Divide(84, 2);
            Assert.Equal(42, result);
        }

        [Fact]
        public void Divide_ByZero_Throws()
        {
            var calc = new ClassLibraryCalculater.Calculate();
            Assert.Throws<DivideByZeroException>(() => calc.Divide(10, 0));
        }

        [Fact]
        public void Add_DecimalNumbers_ReturnsSum()
        {
            var calc = new ClassLibraryCalculater.Calculate();
            var result = calc.Add(2.5, 3.7);
            Assert.Equal(6.2, result);
        }

        [Fact]
        public void Subtract_DecimalNumbers_ReturnsSum()
        {
            var calc = new ClassLibraryCalculater.Calculate();
            var result = calc.Subtract(3.1, 4.5);
            Assert.Equal(-1.4, result);
        }

        [Fact]
        public void Multiply_DecimalNumbers_ReturnsSum()
        {
            var calc = new ClassLibraryCalculater.Calculate();
            var result = calc.Multiply(1.8, 3.14);
            Assert.Equal(5.652, result);
        }

        [Fact]
        public void Divide_DecimalNumbers_ReturnsQuotient()
        {
            var calc = new ClassLibraryCalculater.Calculate();
            var result = calc.Divide(5.5, 2);
            Assert.Equal(2.75, result);
        }

        [Fact]
        public void Add_PositiveAndNegative_ReturnsSum()
        {
            var calc = new ClassLibraryCalculater.Calculate();
            var result = calc.Add(10, -3);
            Assert.Equal(7, result);
        }

        [Fact]
        public void Add_TwoNegativeNumbers_ReturnsSum()
        {
            var calc = new ClassLibraryCalculater.Calculate();
            var result = calc.Add(-4, -6);
            Assert.Equal(-10, result);
        }

        [Fact]
        public void Subtract_PositiveAndNegative_ReturnsDifference()
        {
            var calc = new ClassLibraryCalculater.Calculate();
            var result = calc.Subtract(10, -3);
            Assert.Equal(13, result);
        }

        [Fact]
        public void Subtract_NegativeFromNegative_ReturnsDifference()
        {
            var calc = new ClassLibraryCalculater.Calculate();
            var result = calc.Subtract(-5, -8);
            Assert.Equal(3, result);
        }

        [Fact]
        public void Multiply_TwoNegativeNumbers_ReturnsPositive()
        {
            var calc = new ClassLibraryCalculater.Calculate();
            var result = calc.Multiply(-4, -3);
            Assert.Equal(12, result);
        }

        [Fact]
        public void Multiply_NegativeAndPositive_ReturnsNegative()
        {
            var calc = new ClassLibraryCalculater.Calculate();
            var result = calc.Multiply(-4, 5);
            Assert.Equal(-20, result);
        }

        [Fact]
        public void Divide_NegativeByPositive_ReturnsNegative()
        {
            var calc = new ClassLibraryCalculater.Calculate();
            var result = calc.Divide(-15, 3);
            Assert.Equal(-5, result);
        }

        [Fact]
        public void Divide_NegativeByNegative_ReturnsPositive()
        {
            var calc = new ClassLibraryCalculater.Calculate();
            var result = calc.Divide(-16, -4);
            Assert.Equal(4, result);
        }

        [Fact]
        public void Negate_NegativeDecimal_ReturnsPositive()
        {
            var calc = new ClassLibraryCalculater.Calculate();
            var result = calc.Negate(-3.14);
            Assert.Equal(3.14, result);
        }

        [Fact]
        public void Negate_PositiveDecimal_ReturnsNegative()
        {
            var calc = new ClassLibraryCalculater.Calculate();
            var result = calc.Negate(2.71);
            Assert.Equal(-2.71, result);
        }
    }
}