using MISA.WEB07.MF1755.KVHAI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    /// <summary>
    /// Interface tương tác với Service của Overtime
    /// </summary>
    public interface IOvertimeService : IBaseService<OvertimeDto, OvertimeCreateDto, OvertimeUpdateDto, OvertimeSearchPaging, OvertimeFilterResultDto>
    {
        /// <summary>
        /// Xuất Excel
        /// </summary>
        /// <param name="overtimeSearchPaging">Điều kiện lọc, tìm kiếm</param>
        /// <returns></returns>
        Task<OvertimeExportExcel> OvertimeExportToExcelAsync(OvertimeSearchPaging overtimeSearchPaging);

        /// <summary>
        /// Phê duyêt: duyệt hoặc từ chối
        /// </summary>
        /// <param name="status">Trạng thái update</param>
        /// <param name="ids">Danh sách id các đơn update</param>
        /// <returns>số đơn update thành công</returns>
        /// CreatedBy: KVHAI (18/09/2023)
        Task<int> ApproveRequestAsync(int status, List<Guid> ids);
    }
}
