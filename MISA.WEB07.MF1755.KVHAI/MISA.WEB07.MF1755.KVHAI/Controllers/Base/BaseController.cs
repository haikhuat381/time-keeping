using Microsoft.AspNetCore.Mvc;
using MISA.WEB07.MF1755.KVHAI.Application;

namespace MISA.WEB07.MF1755.KVHAI
{
    public class BaseController<TDto, TCreateDto, TUpdateDto, TEntitySearchPaging, TEntityFilterResultDto> : BaseReadOnlyController<TDto, TEntitySearchPaging, TEntityFilterResultDto>
    {
        protected readonly IBaseService<TDto, TCreateDto, TUpdateDto, TEntitySearchPaging, TEntityFilterResultDto> BaseService;
        public BaseController(IBaseService<TDto, TCreateDto, TUpdateDto, TEntitySearchPaging, TEntityFilterResultDto> baseService) : base(baseService)
        {
            BaseService = baseService;
        }

        /// <summary>
        /// Xóa một
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>StatusCode 200</returns>
        /// CreatedBy: KVHAI (09/09/2023)

        #region Delete
        [HttpDelete("{id}")]
        public async Task<int> DeleteAsync(Guid id)
        {
            var result = await BaseService.DeleteAsync(id);
            return result;
        }
        #endregion


        /// <summary>
        /// Xóa nhiều
        /// </summary>
        /// <param name="ids">Danh sách Id</param>
        /// <returns>StatusCode 200</returns>
        /// CreatedBy: KVHAI (09/09/2023)

        #region DeleteMultiple
        [HttpDelete]
        public async Task<int> DeleteMultipleAsync([FromBody] List<Guid> ids)
        {
            var result = await BaseService.DeleteMultipleAsync(ids);
            return result;
        }
        #endregion


        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="createDto">Thông tin</param>
        /// <returns>StatusCode 201</returns>
        /// CreatedBy: KVHAI (09/09/2023)

        #region Insert
        [HttpPost]
        public async Task<IActionResult> InSertAsync(TCreateDto createDto)
        {
            var result = await BaseService.InSertAsync(createDto);
            if (result > 0)
            {
                return StatusCode(StatusCodes.Status201Created, result);
            }
            return BadRequest(result);

        }
        #endregion



        /// <summary>
        /// Sửa
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="updateDto">Thông tin</param>
        /// <returns>StatusCode 200</returns>
        /// CreatedBy: KVHAI (11/09/2023)

        #region Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, TUpdateDto updateDto)
        {
            //employeeUpdateDto.EmployeeId = id;
            var result = await BaseService.UpdateAsync(id, updateDto);
            if (result > 0)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion
    }
}
