using AutoMapper;
using MISA.WEB07.MF1755.KVHAI.Domain;
using MISA.WEB07.MF1755.KVHAI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    public class EmployeeProfile : Profile
    {
        /// <summary>
        /// mapper of employee
        /// </summary>
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
            CreateMap<Employee, EmployeeExportExcelDto>();
        }
    }
}
