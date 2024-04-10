using Dapper;
using MISA.WEB07.MF1755.KVHAI.Application;
using MISA.WEB07.MF1755.KVHAI.Domain;
using MISA.WEB07.MF1755.KVHAI.Domain.Resource;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Infrastructure
{
    public class DepartmentRepository : BaseRepository<Department, DepartmentSearchPaging, DepartmentFilterResult, DepartmnetFilter, DepartmentSort>, IDepartmentRepository
    {
        public DepartmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public override string CreateSqlFilters(DepartmentSearchPaging departmentSearchPaging, string filterSql, string sortSql, bool? isNoPaging)
        {
            throw new NotImplementedException();
        }

        public override DepartmentFilterResult GetDataFilters(int totalRecords, List<Department> data)
        {
            throw new NotImplementedException();
        }
    }
}
