using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB07.MF1755.KVHAI;
using MySqlConnector;
using Dapper;
using MISA.WEB07.MF1755.KVHAI.Application;
using MISA.WEB07.MF1755.KVHAI.Domain;

namespace MISA.WEB07.MF1755.KVHAI.Controllers
{
    public class DepartmentsController : BaseReadOnlyController<DepartmentDto, DepartmentSearchPaging, DepartmentFilterResultDto>
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService) : base(departmentService)

        {
            _departmentService = departmentService;
        }
    }
}
