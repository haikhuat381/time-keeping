using MISA.WEB07.MF1755.KVHAI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    public interface IBaseReadOnlyService<TDto, TEntitySearchPaging, TEntityFilterResultDto>
    {
        /// <summary>
        /// Lấy danh sách theo phân trang, lọc và tìm kiếm
        /// </summary>
        /// <returns>Mảng danh sách</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        Task<TEntityFilterResultDto> GetFiltersAsync(TEntitySearchPaging entitySearchPaging);

        /// <summary>
        /// Hàm lấy một theo id
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Có - trả về, Không có - trả về null</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        Task<TDto?> FindAsync(Guid id);

        /// <summary>
        /// Hàm lấy một theo id
        /// </summary>
        /// <param name="id">ID</param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>Có - trả về, Không có - trả về Exception</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        Task<TDto> GetAsync(Guid id);

        /// <summary>
        /// Hàm lấy tất cả 
        /// </summary>
        /// <param name=""></param>
        /// <returns>Có - trả về, Không có - trả về Exception</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        Task<List<TDto>> GetAllAsync();

    }
}
