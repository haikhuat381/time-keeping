using MISA.WEB07.MF1755.KVHAI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    /// <summary>
    /// Interface tương tác với Service của Employee
    /// </summary>
    public interface IEmployeeService : IBaseService<EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto, EmployeeSearchPaging, EmployeeFilterResultDto>
    {

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        Task<string> GetNewEmployeeCodeAsync();

        /// <summary>
        /// Xuất Excel
        /// </summary>
        /// <param name="employeeSearchPaging">Điều kiện lọc, tìm kiếm</param>
        /// <returns></returns>
        Task<EmployeeExportExcel> EmployeeExportToExcelAsync(EmployeeSearchPaging employeeSearchPaging);
    }
}
