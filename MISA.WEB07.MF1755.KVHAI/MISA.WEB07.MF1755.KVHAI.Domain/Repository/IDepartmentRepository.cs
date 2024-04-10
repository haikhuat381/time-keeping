using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    /// <summary>
    /// Interface tương tác với Repository của Department
    /// </summary>
    public interface IDepartmentRepository : IBaseRepository<Department, DepartmentSearchPaging, DepartmentFilterResult>
    {
    }
}
