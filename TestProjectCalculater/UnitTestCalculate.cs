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
    }
}