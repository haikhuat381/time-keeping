using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB07.MF1755.KVHAI;
using MySqlConnector;
using Dapper;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MISA.WEB07.MF1755.KVHAI.Application;
using MISA.WEB07.MF1755.KVHAI.Domain;

namespace MISA.WEB07.MF1755.KVHAI.Controllers
{
    public class EmployeesController : BaseController<EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto, EmployeeSearchPaging, EmployeeFilterResultDto>
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <param></param>
        /// <returns>mã nhân viên lớn nhất + 1, nếu chưa có thì trả về "NV1"</returns>
        /// CreatedBy: KVHAI (09/09/2023)

        #region GetNewCode
        [HttpGet("NewCode")]
        public async Task<string> GetNewEmployeeCodeAsync()
        {
            var newEmployeeCode = await _employeeService.GetNewEmployeeCodeAsync();
            return newEmployeeCode;
        }
        #endregion

        /// <summary>
        /// Xuất Excel
        /// </summary>
        /// <param name="employeeFilter">Điều kiện lọc, tìm kiếm</param>
        /// <returns>file excel</returns>
        /// CreatedBy: KVHAI (18/09/2023)
        [HttpPost("ExportExcel")]
        public async Task<IActionResult> EmployeeExportToExcelAsync(EmployeeSearchPaging employeeFilter)
        {
            var result = await _employeeService.EmployeeExportToExcelAsync(employeeFilter);
            return File(result.File, result.ExcelMimeType, result.FileName);
        }

    }
}
