using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public interface IOvertimeValidate
    {
        /// <summary>
        /// Kiểm tra date đã hợp lệ với nghiệp vụ 
        /// </summary>
        /// <param name="overtime">Đơn đăng ký làm thêm</param>
        /// <exception cref="BadRequestException">Lỗi</exception>
        /// <returns></returns>
        public void CheckValidateBusiness(Overtime overtime);

        /// <summary>
        /// Kiểm tra Danh sách nhân viên làm thêm đã có hay chưa
        /// </summary>
        /// <param name="overtime">Đơn đăng ký làm thêm</param>
        /// <exception cref="BadRequestException">Lỗi</exception>
        /// <returns></returns>
        public void CheckOvertimeEmployees(Overtime overtime);
    }
}
