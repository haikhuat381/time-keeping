using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    public interface IBaseService<TDto, TCreateDto, TUpdateDto, TEntitySearchPaging, TEntityFilterResultDto> : IBaseReadOnlyService<TDto, TEntitySearchPaging, TEntityFilterResultDto>
    {
        /// <summary>
        /// Tạo
        /// </summary>
        /// <param name="createDto">Thông tin</param>
        /// <returns>Sô bản ghi thành công</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        Task<int> InSertAsync(TCreateDto createDto);

        /// <summary>
        /// Sửa
        /// </summary>
        /// <param name="updateDto">Thông tin</param>
        /// <returns>1 - thành công, 0 - thất bại</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        Task<int> UpdateAsync(Guid id, TUpdateDto updateDto);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="id">Thông tin</param>
        /// <returns>1 - thành công, 0 - thất bại</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// Xóa nhiều
        /// </summary>
        /// <param name="ids">Danh sách id</param>
        /// <returns>thành công - số bản ghi xóa thành công, thất bại - 0</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        Task<int> DeleteMultipleAsync(List<Guid> ids);
    }
}
