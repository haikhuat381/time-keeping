using MISA.WEB07.MF1755.KVHAI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    public abstract class BaseReadOnlyService<TEntity, TDto, TEntitySearchPaging, TEntityFilterResultDto, TEntityFilterResult> : IBaseReadOnlyService<TDto, TEntitySearchPaging, TEntityFilterResultDto>
    {
        protected readonly IBaseRepository<TEntity, TEntitySearchPaging, TEntityFilterResult> BaseRepository;

        protected BaseReadOnlyService(IBaseRepository<TEntity, TEntitySearchPaging, TEntityFilterResult> baseRepository)
        {
            BaseRepository = baseRepository;
        }

        /// <summary>
        /// Hàm lấy một theo id
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Có - trả về, Không có - trả về null</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        public async Task<TDto?> FindAsync(Guid id)
        {
            var entity = await BaseRepository.FindAsync(id);
            var result = MapEntityToDto(entity);
            return result;
        }

        /// <summary>
        /// Hàm lấy tất cả 
        /// </summary>
        /// <param name=""></param>
        /// <returns>Có - trả về, Không có - trả về Exception</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        public async Task<List<TDto>> GetAllAsync()
        {
            var entities = await BaseRepository.GetAllAsync();
 
            var result = entities.Select( entity => MapEntityToDto(entity)).ToList();
            //var dtoList = await Task.WhenAll(tasks);
            //var result = dtoList.ToList();

            return result;
        }


        /// <summary>
        /// Hàm lấy một theo id
        /// </summary>
        /// <param name="id">ID</param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>Có - trả về, Không có - trả về Exception</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        public async Task<TDto> GetAsync(Guid id)
        {
            var entity = await BaseRepository.GetAsync(id);

            var result = MapEntityToDto(entity);

            return result;

        }

        /// <summary>
        /// Lấy danh sách theo phân trang, lọc và tìm kiếm
        /// </summary>
        /// <returns>Mảng danh sách</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        public async Task<TEntityFilterResultDto> GetFiltersAsync(TEntitySearchPaging entitySearchPaging)
        {
            var entity = await BaseRepository.GetFiltersAsync(entitySearchPaging, false);
            var result = MapFilterResultDtoToFilterResult(entity);
             
            return result;
        }

        /// <summary>
        /// Chuyển Entity sang Dto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>TDto</returns>
        /// CreatedBy:  KVHAI(20/09/2023)
        public abstract TDto MapEntityToDto(TEntity entity);

        /// <summary>
        /// Chuyển TEntityFilterResult sang TEntityFilterResultDto
        /// </summary>
        /// <param name="entityFilterResult"></param>
        /// <returns>TEntityFilterResultDto</returns>
        public abstract TEntityFilterResultDto MapFilterResultDtoToFilterResult(TEntityFilterResult entityFilterResult);
    }
}
