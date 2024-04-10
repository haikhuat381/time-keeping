using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using MISA.WEB07.MF1755.KVHAI.Model;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public interface IOvertimeRepository : IBaseRepository<Overtime, OvertimeSearchPaging, OvertimeFilterResult>
    {
        /// <summary>
        /// Xuất excel
        /// </summary>
        /// <param name="overtimeSearchPaging">Điều kiện lọc, tìm kiếm</param>
        /// <returns>File Excel Danh sách nhân viên</returns>
        Task<OvertimeExportExcel> OvertimeExportToExcelAsync(List<OvertimeExportExcelDto> overtimeExportExcelDto);

        /// <summary>
        /// Phê duyêt: duyệt hoặc từ chối
        /// </summary>
        /// <param name="status">Trạng thái update</param>
        /// <param name="overtimes">Danh sách các đơn update</param>
        /// <returns>số đơn update thành công</returns>
        /// CreatedBy: KVHAI (18/09/2023)
        Task<int> ApproveRequestAsync(int status, List<Overtime> overtimes);
    }
}
