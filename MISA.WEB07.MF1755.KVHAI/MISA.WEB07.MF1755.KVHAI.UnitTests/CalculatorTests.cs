using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.UnitTests
{
    [TestFixture]
    public class CalculatorTests
    {

        /// <summary>
        /// Tính tổng số trong một chuỗi
        /// Unit test với input hợp lệ
        /// </summary>
        /// <param name="input">Chuỗi đầu vào hợp lệ</param>
        /// <param name="expectedResult">Kết quả mong đợi</param>
        /// CreatedBy: KVHAI (12/09/2023)

        [TestCase("", 0)]
        [TestCase("5", 5)]
        [TestCase(" 1,2, 3", 6)]
        [TestCase("1, 5, 3", 9)]
        [TestCase("10  , 2  ,3  ", 15)]
        [TestCase("10,2,,4,", 16)]
        public void Add_ValidString_SumDigits(string input, int expectedResult)
        {
            //Arrange
            var calculator = new Calculator();

            //Act
            var actualResult = calculator.Add(input);

            //Asset
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }


        /// <summary>
        /// Tính tổng số trong một chuỗi
        /// Unit test với input không hợp lệ
        /// </summary>
        /// <param name="input">Chuỗi đầu vào không hợp lệ</param>
        /// <param name="expectedResult">Thông báo lỗi tương ứng</param>
        /// CreatedBy: KVHAI (12/09/2023)

        [TestCase("a", "Đầu vào không đúng định dạng")]
        [TestCase("1,5,3, d ", "Đầu vào không đúng định dạng")]
        [TestCase("1;2,4", "Đầu vào không đúng định dạng")]
        [TestCase("1a; 2 ; 3", "Đầu vào không đúng định dạng")]
        [TestCase("1,2, a, 3", "Đầu vào không đúng định dạng")]
        [TestCase("1,-2 , a, 5", "Đầu vào không đúng định dạng")]
        [TestCase("1,-3,3, -5", "Không chấp nhận toán tử âm: -3, -5")]
        public void Add_InvalidString_ThrowException(string input, string expectedResult)
        {
            //Arrange
            var calculator = new Calculator();

            //Act
            //Asset
            try
            {
                calculator.Add(input);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(expectedResult));
            }
        }


        /// <summary>
        /// Tính tổng 2 số
        /// Unit test hợp lệ
        /// </summary>
        /// <param name="x">Số nguyên thứ nhất</param>
        /// <param name="y">Số nguyên thứ hai</param>
        /// <param name="expecttedResult">Kết quả mong muốn</param>
        /// CreatedBy: KVHAI (12/09/2023)

        [TestCase(1, 2, 3)]
        [TestCase(2, 4, 6)]
        [TestCase(2, -7, -5)]
        public void Add_ValidInput_Sum2Digit(int x, int y, int expecttedResult)
        {
            // Arrange
            var calculator = new Calculator();

            //Act
            var actualResult = calculator.Add(x, y);

            //Assert

            Assert.That(actualResult, Is.EqualTo(expecttedResult));
        }


        /// <summary>
        /// Tính hiệu 2 số
        /// Unit test hợp lệ
        /// </summary>
        /// <param name="x">Số nguyên thứ nhất</param>
        /// <param name="y">Số nguyên thứ hai</param>
        /// <param name="expecttedResult">Kết quả mong muốn</param>
        /// CreatedBy: KVHAI (12/09/2023)

        [TestCase(1, 2, -1)]
        [TestCase(2, 4, -2)]
        [TestCase(2, -7, 9)]
        public void Sub_ValidInput_Sub2Digit(int x, int y, int expecttedResult)
        {
            // Arrange
            var calculator = new Calculator();

            //Act
            var actualResult = calculator.Sub(x, y);

            //Assert
            Assert.That(actualResult, Is.EqualTo(expecttedResult));
        }

        /// <summary>
        /// Tính tích 2 số
        /// Unit test hợp lệ
        /// </summary>
        /// <param name="x">Số nguyên thứ nhất</param>
        /// <param name="y">Số nguyên thứ hai</param>
        /// <param name="expecttedResult">Kết quả mong muốn</param>
        /// CreatedBy: KVHAI (12/09/2023)

        [TestCase(1, 2, 2)]
        [TestCase(2, 4, 8)]
        [TestCase(2, -7, -14)]
        public void Mul_ValidInput_Mul2Digit(int x, int y, int expecttedResult)
        {
            // Arrange
            var calculator = new Calculator();

            //Act
            var actualResult = calculator.Mul(x, y);

            //Assert
            Assert.That(actualResult, Is.EqualTo(expecttedResult));
        }


        /// <summary>
        /// Chia 2 số
        /// Unit test hợp lệ
        /// </summary>
        /// <param name="x">Số nguyên thứ nhất</param>
        /// <param name="y">Số nguyên thứ hai</param>
        /// <param name="expecttedResult">Kết quả mong muốn</param>
        /// CreatedBy: KVHAI (12/09/2023)

        [TestCase(1, 2, 1 / (double)2)]
        [TestCase(2, 3, 2 / (double)3)]
        public void Div_ValidInput_Div2Digit(int x, int y, double expecttedResult)
        {
            // Arrange
            var calculator = new Calculator();

            //Act
            var actualResult = calculator.Div(x, y);

            //Assert
            Assert.That(Math.Abs(actualResult - expecttedResult), Is.LessThan(10e-6));
        }


        /// <summary>
        /// Hàm chia
        /// Unit test với phép chia không hợp lệ
        /// </summary>
        /// CreatedBy: KVHAI (12/09/2023)

        [Test]
        public void Div_DivideByZero_ThrowException()
        {
            // Arrange
            var x = 2;
            var y = 0;
            var exceptionMsg = "Không được chia cho 0";

            var calculator = new Calculator();

            //Act
            //Assert
            try
            {
                var actualResult = calculator.Div(x, y);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo(exceptionMsg));
            }
        }
    }
}
