//using MISA.WEB07.MF1755.KVHAI.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public interface IBaseRepository<TEntity, TEntitySearchPaging, TEntityFilterResult>
    {
        /// <summary>
        /// Lấy danh sách theo phân trang, lọc và tìm kiếm
        /// </summary>
        /// <returns>Mảng danh sách</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        Task<TEntityFilterResult> GetFiltersAsync(TEntitySearchPaging entitySearchPaging, bool? isNoPaging);


        /// <summary>
        /// Hàm lấy một theo id
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Có - trả về, Không có - trả về null</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        Task<TEntity?> FindAsync(Guid id);

        /// <summary>
        /// Hàm lấy một theo id
        /// </summary>
        /// <param name="id">ID</param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>Có - trả về, Không có - trả về Exception</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        Task<TEntity> GetAsync(Guid id);

        /// <summary>
        /// Hàm danh sach tu danh sach id
        /// </summary>
        /// <param name="id">ID</param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>Có - trả về, Không có - trả về Exception</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        Task<List<TEntity>> GetByListIdAsync(List<Guid> ids);

        /// <summary>
        /// Hàm lấy một theo id
        /// </summary>
        /// <param name=""></param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>Có - trả về, Không có - trả về Exception</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Tạo
        /// </summary>
        /// <param name="entity">Thông tin</param>
        /// <returns>Sô bản ghi thành công</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        Task<int> InSertAsync(TEntity entity);


        /// <summary>
        /// Tạo nhiều
        /// </summary>
        /// <param name="entities">Thông tin</param>
        /// <returns>Sô bản ghi thành công</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        Task<int> InSertMultipleAsync(List<TEntity> entities);

        /// <summary>
        /// Sửa
        /// </summary>
        /// <param name="entity">Thông tin</param>
        /// <returns>1 - thành công, 0 - thất bại</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        Task<int> UpdateAsync(Guid id, TEntity entity);

        /// <summary>
        /// Sửa nhiều
        /// </summary>
        /// <param name="entities">Thông tin</param>
        /// <returns>1 - thành công, 0 - thất bại</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        Task<int> UpdateMultipleAsync(List<TEntity> entities);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="entity">Thông tin</param>
        /// <returns>1 - thành công, 0 - thất bại</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        Task<int> DeleteAsync(TEntity entity);

        /// <summary>
        /// Xóa nhiều
        /// </summary>
        /// <param name="entities">Danh sách thông tin</param>
        /// <returns>thành công - số bản ghi xóa thành công, thất bại - 0</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        Task<int> DeleteMultipleAsync(List<TEntity> entities);
    }
}
