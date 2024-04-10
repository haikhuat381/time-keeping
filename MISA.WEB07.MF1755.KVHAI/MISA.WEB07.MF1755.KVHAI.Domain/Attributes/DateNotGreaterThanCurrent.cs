using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    /// <summary>
    /// Attribute kiểm tra ngày
    /// </summary>
    public class DateNotGreaterThanCurrent : ValidationAttribute
    {
        /// <summary>
        /// Hàm kiểm tra so sánh với ngày hiện tại
        /// </summary>
        /// <param name="value">ngày cần kiểm tra</param>
        /// <returns>true - false</returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            if (value is DateTime date)
            {
                // Lấy ngày hiện tại
                var currentDate = DateTime.Now;

                // So sánh ngày truyền vào với ngày hiện tại
                if (date <= currentDate)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
