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
    public class OvertimesController : BaseController<OvertimeDto, OvertimeCreateDto, OvertimeUpdateDto, OvertimeSearchPaging, OvertimeFilterResultDto>
    {
        private readonly IOvertimeService _overtimeService;

        public OvertimesController(IOvertimeService overtimeService) : base(overtimeService)
        {
            _overtimeService = overtimeService;
        }

        /// <summary>
        /// Xuất Excel
        /// </summary>
        /// <param name="overtimeFilter">Điều kiện lọc, tìm kiếm</param>
        /// <returns>file excel</returns>
        /// CreatedBy: KVHAI (18/09/2023)
        [HttpPost("ExportExcel")]
        public async Task<IActionResult> OvertimeExportToExcelAsync(OvertimeSearchPaging overtimeFilter)
        {
            var result = await _overtimeService.OvertimeExportToExcelAsync(overtimeFilter);
            return File(result.File, result.ExcelMimeType, result.FileName);
        }

        /// <summary>
        /// Phê duyêt: duyệt hoặc từ chối
        /// </summary>
        /// <param name="status">Trạng thái update</param>
        /// <param name="ids">Danh sách id các đơn update</param>
        /// <returns>số đơn update thành công</returns>
        /// CreatedBy: KVHAI (18/09/2023)

        [HttpPost("ApproveRequest")]
        public async Task<IActionResult> ApproveRequestAsync(int status, List<Guid> ids)
        {
            var result = await _overtimeService.ApproveRequestAsync(status, ids);
            return Ok(result);
        }

    }
}
