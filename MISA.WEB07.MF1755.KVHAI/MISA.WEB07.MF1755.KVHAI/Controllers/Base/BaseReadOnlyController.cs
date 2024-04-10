using Microsoft.AspNetCore.Mvc;
using MISA.WEB07.MF1755.KVHAI.Application;
using MISA.WEB07.MF1755.KVHAI.Domain;

namespace MISA.WEB07.MF1755.KVHAI
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseReadOnlyController<TDto, TEntitySearchPaging, TEntityFilterResultDto> : ControllerBase
    {
        protected readonly IBaseReadOnlyService<TDto, TEntitySearchPaging, TEntityFilterResultDto> BaseReadOnlyService;

        public BaseReadOnlyController(IBaseReadOnlyService<TDto, TEntitySearchPaging, TEntityFilterResultDto> baseReadOnlyService)
        {
            BaseReadOnlyService = baseReadOnlyService;
        }

        /// <summary>
        /// Lấy theo id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Thông tin</returns>
        /// CreatedBy: KVHAI (09/09/2023)

        #region GetById
        [HttpGet("{id}")]
        public async Task<TDto> GetEmployeeAsync(Guid id)
        {
            var result = await BaseReadOnlyService.GetAsync(id);
            return result;
        }
        #endregion

        [HttpGet()]
        public async Task<List<TDto>> GetAllAsync()
        {
            var result = await BaseReadOnlyService.GetAllAsync();
            return result;
        }


        /// <summary>
        /// Lấy danh sách theo phân trang cộng tìm kiếm
        /// </summary>
        /// <param name="entitySearchPaging">phân trang, lọc, tìm kiếm</param>
        /// <returns>Danh sách nhân theo điều kiện tìm kiếm trên</returns>
        /// CreatedBy: KVHAI (11/09/2023)

        #region GetEmployeesFilters
        [HttpPost("Filter")]
        public async Task<TEntityFilterResultDto> GetFiltersAsync(TEntitySearchPaging entitySearchPaging)
        {
            var result = await BaseReadOnlyService.GetFiltersAsync(entitySearchPaging);
            return result;
        }
        #endregion
    }
}
