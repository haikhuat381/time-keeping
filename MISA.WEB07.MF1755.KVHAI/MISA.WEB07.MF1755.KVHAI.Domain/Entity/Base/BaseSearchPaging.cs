using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public class BaseSearchPaging<TEntityFilter, TEntitySort> : ISearchPaging<TEntityFilter, TEntitySort>
    {
        /// <summary>
        /// Số bản ghi trên một trang
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Số trang
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Từ khóa tìm kiếm
        /// </summary>
        public string? Keyword { get; set; }


        /// <summary>
        /// Số bản ghi trên một trang
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// Danh sách đơn vị
        /// </summary>
        public List<Guid>? DepartmentIds { get; set; }

        /// <summary>
        /// Danh sách định danh
        /// </summary>
        public List<Guid>? Ids { get; set; }

        /// <summary>
        /// Mảng các bộ lộc
        /// </summary>
        public List<TEntityFilter>? Filters { get; set; }

        /// <summary>
        /// Mảng các sắp xếp
        /// </summary>
        public List<TEntitySort>? Sorts { get; set; }

        public string GetKeyword() => Keyword;

        public int GetPageIndex() => PageIndex;

        public int GetPageSize() => PageSize;

        public int? GetStatus() => Status;

        public List<Guid>? GetDepartmentIds() => DepartmentIds;
        public List<Guid>? GetIds() => Ids;

        public List<TEntityFilter> GetFilters() => Filters;

        public List<TEntitySort>? GetSorts() => Sorts;
    }
}
