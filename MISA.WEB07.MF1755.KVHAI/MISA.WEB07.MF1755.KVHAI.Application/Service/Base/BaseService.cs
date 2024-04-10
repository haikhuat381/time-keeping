using MISA.WEB07.MF1755.KVHAI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    public abstract class BaseService<TEntity, TDto, TCreateDto, TUpdateDto, TEntitySearchPaging, TEntityFilterResultDto, TEntityFilterResult> : BaseReadOnlyService<TEntity, TDto, TEntitySearchPaging, TEntityFilterResultDto, TEntityFilterResult> , IBaseService<TDto, TCreateDto, TUpdateDto, TEntitySearchPaging, TEntityFilterResultDto> where TEntity : IEntity
    {
        protected BaseService(IBaseRepository<TEntity, TEntitySearchPaging, TEntityFilterResult> baseRepository) : base(baseRepository)
        {
        }

        /// <summary>
        /// Tạo
        /// </summary>
        /// <param name="createDto">Thông tin</param>
        /// <returns>Sô bản ghi thành công</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        public async Task<int> InSertAsync(TCreateDto createDto)
        {
            var entity = MapCreateDtoToEntity(createDto);

            if (entity.GetId() == Guid.Empty)
            {
                entity.SetId(Guid.NewGuid());
            }
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.CreatedDate ??= DateTime.Now;
                baseEntity.CreatedBy ??= "KVH";
                baseEntity.ModifiedDate ??= DateTime.Now;
                baseEntity.ModifiedBy ??= "KVH";

            }

            await ValidateCreateBusiness(entity);
            var result = await BaseRepository.InSertAsync(entity);
            return result;
        }

        /// <summary>
        /// Sửa
        /// </summary>
        /// <param name="updateDto">Thông tin</param>
        /// <returns>1 - thành công, 0 - thất bại</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        public async Task<int> UpdateAsync(Guid id, TUpdateDto updateDto)
        {
            var entity = await BaseRepository.GetAsync(id);
            var newEntity = MapUpdateDtoToEntity(updateDto, entity);
            if (newEntity is BaseEntity baseEntity)
            {
                baseEntity.ModifiedDate ??= DateTime.Now;
                baseEntity.ModifiedBy ??= "KVH";

            }
            await ValidateUpdateBusiness(newEntity);
            var result = await BaseRepository.UpdateAsync(id, newEntity);
            return result;
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="id">Thông tin</param>
        /// <returns>1 - thành công, 0 - thất bại</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await BaseRepository.GetAsync(id);
            var result = await BaseRepository.DeleteAsync(entity);
            return result;
        }

        /// <summary>
        /// Xóa nhiều
        /// </summary>
        /// <param name="ids">Danh sách id</param>
        /// <returns>thành công - số bản ghi xóa thành công, thất bại - 0</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        public async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            var entities = await BaseRepository.GetByListIdAsync(ids);
            if (entities != null && entities.Any())
            {
                var result = await BaseRepository.DeleteMultipleAsync(entities);
                return result;
            }
            return 0;
        }

        /// <summary>
        /// Chuyển TCreateDto sang TEntity
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns>TEntity</returns>
        /// CreatedBy:  KVHAI(20/09/2023)
        public abstract TEntity MapCreateDtoToEntity(TCreateDto createDto);

        /// <summary>
        /// Validate khi thêm mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task ValidateCreateBusiness(TEntity entity)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Chuyển TUpdateDto sang TEntity
        /// </summary>
        /// <param name="updateDto"></param>
        /// <returns>TEntity</returns>
        /// CreatedBy:  KVHAI(20/09/2023)
        public abstract TEntity MapUpdateDtoToEntity(TUpdateDto updateDto, TEntity entity);

        /// <summary>
        /// Validate khi sửa
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task ValidateUpdateBusiness(TEntity entity)
        {
            await Task.CompletedTask;
        }

    }
}
