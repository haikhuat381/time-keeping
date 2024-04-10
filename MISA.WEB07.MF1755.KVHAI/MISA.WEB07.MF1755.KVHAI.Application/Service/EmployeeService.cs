using MISA.WEB07.MF1755.KVHAI.Domain;
using MISA.WEB07.MF1755.KVHAI.Domain.Resource;
using MISA.WEB07.MF1755.KVHAI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    public class EmployeeService : BaseService<Employee, EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto, EmployeeSearchPaging, EmployeeFilterResultDto, EmployeeFilterResult> , IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeValidate _employeeValidate;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper; 

        public EmployeeService(IEmployeeRepository employeeRepository, IEmployeeValidate employeeValidate, IDepartmentRepository departmentRepository, IMapper mapper) : base( employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeValidate = employeeValidate;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        public async Task<string> GetNewEmployeeCodeAsync ()
        {
            var result = await _employeeRepository.GetNewEmployeeCodeAsync();
            return result;
        }

        /// <summary>
        /// Chuyển EmployeeCreateDto sang Employee
        /// </summary>
        /// <param name="employeeCreateDto"></param>
        /// <returns></returns>
        public override Employee MapCreateDtoToEntity(EmployeeCreateDto employeeCreateDto)
        {
            var employee = _mapper.Map<Employee>(employeeCreateDto);
            employee.EmployeeId = Guid.NewGuid();

            return employee;
        }

        public override async Task ValidateCreateBusiness(Employee employee)
        {
            // Check department
            var isCheckDepartment = await _departmentRepository.FindAsync((Guid)employee.DepartmentId);
            if (isCheckDepartment == null)
            {
                throw new BadRequestException(ResourceVN.Error_DepartmentInvalid, ErrorCode.Invalid);
            }

            //Check trung code
            await _employeeValidate.CheckEmployeeExistAsync(null, employee);
        }

        /// <summary>
        /// Chuyển EmployeeUpdateDto sang Employee
        /// </summary>
        /// <param name="employeeUpdateDto"></param>
        /// <returns></returns>
        public override Employee MapUpdateDtoToEntity(EmployeeUpdateDto employeeUpdateDto, Employee employee)
        {

            employeeUpdateDto.EmployeeId = employee.EmployeeId;
            var newEmployee = _mapper.Map(employeeUpdateDto, employee);

            return newEmployee;
        }

        public override async Task ValidateUpdateBusiness(Employee employee)
        {   

            // Check department
            var isCheckDepartment = await _departmentRepository.FindAsync((Guid)employee.DepartmentId);
            if (isCheckDepartment == null)
            {
                throw new BadRequestException(ResourceVN.Error_DepartmentInvalid, ErrorCode.Invalid);
            }
            //Check trung code
            await _employeeValidate.CheckEmployeeExistAsync(employee.EmployeeId, employee);
        }


        /// <summary>
        /// Chuyển Employee sang EmployeeDto
        /// </summary>
        /// <param name="employee">Nhân viên</param>
        /// <returns></returns>
        public override EmployeeDto MapEntityToDto(Employee employee)
        {
            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }


        /// <summary>
        /// Chuyển EmployeeFilterResult sang EmployeeFilterResultDto
        /// </summary>
        /// <param name="employeeFilterResult"></param>
        /// <returns>EmployeeFilterResultDto</returns>
        public override EmployeeFilterResultDto MapFilterResultDtoToFilterResult(EmployeeFilterResult employeeFilterResult)
        {
            var employeeDtos = employeeFilterResult.Data.Select( employee => MapEntityToDto(employee)).ToList();

            //var dtoList = await Task.WhenAll(tasks);

            //var employeeDtos = dtoList.ToList();

            var result = new EmployeeFilterResultDto()
            {
                TotalRecords = employeeFilterResult.TotalRecords,
                Data = employeeDtos.ToList()
            };
            return result;

        }


        /// <summary>
        /// Xuất Excel
        /// </summary>
        /// <param name="employeeSearchPaging">Điều kiện lọc, tìm kiếm</param>
        /// <returns></returns>
        public async Task<EmployeeExportExcel> EmployeeExportToExcelAsync(EmployeeSearchPaging employeeSearchPaging)
        {
            var employeeFilterResult = await _employeeRepository.GetFiltersAsync(employeeSearchPaging, true);

            var employeeExportExcelTasks = employeeFilterResult.Data.Select(async (employee, index) => await MapEmployeeToEmployeeExportExcel(employee, index));

            var employeeExportExcels = await Task.WhenAll(employeeExportExcelTasks);
            var data = employeeExportExcels.ToList();

            var result = await _employeeRepository.EmployeeExportToExcelAsync(data);
            return result;
        }


        /// <summary>
        /// Chuyển đổi Employee sang EmployeeExportExcelDto
        /// </summary>
        /// <param name="employee">Nhân viên</param>
        /// <param name="index">indexx</param>
        /// <returns><EmployeeExportExcelDto/returns>
        private async Task<EmployeeExportExcelDto> MapEmployeeToEmployeeExportExcel(Employee employee, int index)
        {
            var dateOfBirthFormat = "";
            var identityDateFormat = "";
            var genderFormat = "";

            if (employee.DateOfBirth.HasValue)
            {
                dateOfBirthFormat = employee.DateOfBirth.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                dateOfBirthFormat = string.Empty;
            }


            if (employee.IdentityDate.HasValue)
            {
                identityDateFormat = employee.IdentityDate.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                identityDateFormat = string.Empty;
            }


            if (employee.Gender == GenderEnum.Male)
            {
                genderFormat = "Nam";
            }
            else if (employee.Gender == GenderEnum.Female)
            {
                genderFormat = "Nữ";
            }
            else if (employee.Gender == GenderEnum.Other)
            {
                genderFormat = "Khác";
            }
            else
            {
                genderFormat = "";
            }

            var employeeExportExcel = _mapper.Map<EmployeeExportExcelDto>(employee);
            employeeExportExcel.NumericalOrder = index + 1;
            employeeExportExcel.DateOfBirth = dateOfBirthFormat;
            employeeExportExcel.IdentityDate = identityDateFormat;
            employeeExportExcel.Gender = genderFormat;

            return employeeExportExcel;
        }
    }
}
