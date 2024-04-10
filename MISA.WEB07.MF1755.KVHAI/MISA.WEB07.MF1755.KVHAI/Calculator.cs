using System.Text.RegularExpressions;

namespace MISA.WEB07.MF1755.KVHAI
{
    public class Calculator
    {
        /// <summary>
        /// Hàm cộng hai số nguyên
        /// </summary>
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 2</param>
        /// <returns>Tổng hai số nguyên</returns>
        /// CreatedBy: KVHAI (12/09/2023)
        public int Add(int x, int y) 
        { 
            return x + y; 
        }


        /// <summary>
        /// Tính tổng các số nguyên trong chuối
        /// </summary>
        /// <param name="input">Một chuỗi gồm các giá trị phân tách với nhau bởi dấu phẩy</param>
        /// <returns>
        /// Chuỗi rỗng - Trả về 0
        /// Chuỗi chứa giá trị âm, các giá trị ko phải số,.. - ThrowException
        /// Chuỗi hợp lệ - Trả về tổng các số nguyên
        /// </returns>
        /// CreatedBy: KVHAI (12/09/2023)
        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return 0;
            }

            input = input.Replace(" ", "");
            string[] numbers = input.Split(',');

            int sum = 0;
            string negativeNumbers = "";

            foreach (string number in numbers)
            {
                if (string.IsNullOrWhiteSpace(number))
                {
                    continue;
                }

                if (int.TryParse(number.Trim(), out int value))
                {
                    sum += value;
                    if (value < 0)
                    {
                        if (string.IsNullOrEmpty(negativeNumbers))
                        {
                            negativeNumbers = number;
                        }
                        else
                        {
                            negativeNumbers += $", {number}";
                        }
                    }
                }
                else
                {
                    throw new Exception("Đầu vào không đúng định dạng");
                }
            }

            if (!string.IsNullOrEmpty(negativeNumbers))
            {
                throw new Exception($"Không chấp nhận toán tử âm: {negativeNumbers}");
            }

            return sum;
        }


        /// <summary>
        /// Hàm trừ hai số nguyên
        /// </summary>
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 2</param>
        /// <returns>Hiệu hai số nguyên</returns>
        /// CreatedBy: KVHAI (12/09/2023)
        public int Sub(int x, int y)
        { 
            return x - y; 
        }


        /// <summary>
        /// Hàm nhân hai số nguyên
        /// </summary>
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 2</param>
        /// <returns>Tíhc hai số nguyên</returns>
        /// CreatedBy: KVHAI (12/09/2023)
        public int Mul(int x, int y)
        {
            return x * y;
        }


        /// <summary>
        /// Hàm chia hai số nguyên
        /// </summary>
        /// <param name="x">Toán hạng 1</param>
        /// <param name="y">Toán hạng 2</param>
        /// <returns>
        /// phép chia hợp lệ - trả về giá trị của phép chia
        /// phép chia không hợp lệ(chia cho 0) - ThrowException
        /// </returns>
        /// CreatedBy: KVHAI (12/09/2023)
        public double Div(int x, int y) 
        { 
            if(y == 0)
            {
                throw new Exception("Không được chia cho 0");
            }    
            return x / (double)y;
        }

    }
}
