using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    /// <summary>
    /// Interface tương tác với Service Validaet của Employee
    /// </summary>
    public interface IEmployeeValidate
    {
        /// <summary>
        /// Kiểm tra Mã nhân viên đã tồn tại hay chưa
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <exception cref="ConflictException">Nếu tồn tại</exception>
        /// <returns></returns>
        Task<bool> CheckEmployeeExistAsync(Guid? employeeId, Employee employee);
    }
}
