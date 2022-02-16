using System;
using System.Collections.Generic;
using System.Text;
using SODP.Model;
using SODP.UI.Services;
using Xunit;

namespace Tests.UITests.ServiceTests
{
    public class PaginationCalculatorTests
    {

        [Theory]
        [InlineData(1, 5, 1)]
        [InlineData(2, 5, 1)]
        [InlineData(2, 5, 2)]
        [InlineData(3, 5, 1)]
        [InlineData(3, 5, 2)]
        [InlineData(3, 5, 3)]
        [InlineData(9, 5, 1)]
        [InlineData(9, 5, 2)]
        [InlineData(9, 5, 3)]
        [InlineData(9, 5, 4)]
        [InlineData(9, 5, 5)]
        [InlineData(9, 5, 6)]
        [InlineData(9, 5, 7)]
        [InlineData(9, 5, 8)]
        [InlineData(9, 5, 9)]
        [InlineData(10, 5, 1)]
        [InlineData(10, 5, 2)]
        [InlineData(10, 5, 3)]
        [InlineData(10, 5, 4)]
        [InlineData(10, 5, 5)]
        [InlineData(11, 5, 1)]
        [InlineData(11, 5, 2)]
        [InlineData(11, 5, 3)]
        [InlineData(11, 5, 4)]
        [InlineData(11, 5, 5)]
        [InlineData(12, 5, 1)]
        [InlineData(12, 5, 2)]
        [InlineData(12, 5, 3)]
        [InlineData(12, 5, 4)]
        [InlineData(12, 5, 5)]
        public void CalculatorShouldByReturnLeft2(int total, int width, int current)
        {
            // Arrange
            var calculator = new PaginationCalculator(total, width, current);
            // Act
            var left = calculator.Left;
            var right = calculator.Right;
            // Assert
            Assert.Equal(2, left);
        }

        [Theory]
        [InlineData(1, 5, 1)]
        [InlineData(2, 5, 1)]
        [InlineData(2, 5, 2)]
        [InlineData(3, 5, 1)]
        [InlineData(3, 5, 2)]
        [InlineData(3, 5, 3)]
        [InlineData(9, 5, 1)]
        [InlineData(9, 5, 2)]
        [InlineData(9, 5, 3)]
        [InlineData(9, 5, 4)]
        [InlineData(9, 5, 5)]
        [InlineData(9, 5, 6)]
        [InlineData(9, 5, 7)]
        [InlineData(9, 5, 8)]
        [InlineData(9, 5, 9)]
        [InlineData(10, 5, 1)]
        [InlineData(10, 5, 2)]
        [InlineData(10, 5, 3)]
        [InlineData(10, 5, 4)]
        [InlineData(10, 5, 5)]
        [InlineData(11, 5, 1)]
        [InlineData(11, 5, 2)]
        [InlineData(11, 5, 3)]
        [InlineData(11, 5, 4)]
        [InlineData(11, 5, 5)]
        public void CalculatorShouldByReturnRight7(int total, int width, int current)
        {
            // Arrange
            var calculator = new PaginationCalculator(total, width, current);
            // Act
            var left = calculator.Left;
            var right = calculator.Right;
            // Assert
            Assert.Equal(7, right);
        }
    }
}
