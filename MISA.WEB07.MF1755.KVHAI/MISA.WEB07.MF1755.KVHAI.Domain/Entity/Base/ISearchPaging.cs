using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public interface ISearchPaging<TEntityFilter, TEntitySort>
    {
        public int GetPageIndex();
        public int GetPageSize();
        public string GetKeyword();
        public int? GetStatus();
        public List<Guid>? GetDepartmentIds();
        public List<Guid>? GetIds();
        public List<TEntityFilter>? GetFilters();
        public List<TEntitySort>? GetSorts();
    }
}
