using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using MISA.WEB07.MF1755.KVHAI.Model;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    /// <summary>
    /// Interface tương tác với Repository của Employee
    /// </summary>
    public interface IEmployeeRepository : IBaseRepository<Employee, EmployeeSearchPaging, EmployeeFilterResult>
    {

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        Task<string> GetNewEmployeeCodeAsync();

        /// <summary>
        /// Check đã tồn tại Mã nhân viên hay chưa
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>true - có, false - chưa có</returns>
        Task<bool> IsExistEmployeeAsync(Guid? employeeId, string employeeCode);

        /// <summary>
        /// Xuất excel
        /// </summary>
        /// <param name="employeeSearchPaging">Điều kiện lọc, tìm kiếm</param>
        /// <returns>File Excel Danh sách nhân viên</returns>
        //Task<EmployeeExportExcel> EmployeeExportToExcelAsync(EmployeeSearchPaging employeeSearchPaging);
        Task<EmployeeExportExcel> EmployeeExportToExcelAsync(List<EmployeeExportExcelDto> employeeExportExcelDto);
    }
}
