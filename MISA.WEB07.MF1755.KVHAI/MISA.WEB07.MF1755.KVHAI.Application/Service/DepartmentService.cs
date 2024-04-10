using AutoMapper;
using Microsoft.VisualBasic;
using MISA.WEB07.MF1755.KVHAI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    public class DepartmentService : BaseReadOnlyService<Department, DepartmentDto, DepartmentSearchPaging, DepartmentFilterResultDto, DepartmentFilterResult>, IDepartmentService
    {
        private readonly IMapper _mapper;
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper) : base(departmentRepository)
        {
            _mapper = mapper;
        }

        public override DepartmentDto MapEntityToDto(Department department)
        {
            //var departmentDto = new DepartmentDto()
            //{
            //    DepartmentId = department.DepartmentId,
            //    DepartmentCode = department.DepartmentCode,
            //    DepartmentName = department.DepartmentName,
            //    ModifiedBy = department.ModifiedBy,
            //    ModifiedDate = department.ModifiedDate,
            //    CreatedBy = department.CreatedBy,
            //    CreatedDate = department.CreatedDate,
            //};

            //return departmentDto;
            var departmentDto = _mapper.Map<DepartmentDto>(department);

            return departmentDto;
        }

        public override DepartmentFilterResultDto MapFilterResultDtoToFilterResult(DepartmentFilterResult entityFilterResult)
        {
            throw new NotImplementedException();
        }

    }
}
