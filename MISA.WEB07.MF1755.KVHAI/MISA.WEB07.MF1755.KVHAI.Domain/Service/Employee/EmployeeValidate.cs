using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    /// <summary>
    /// Validate nghiệp vụ
    /// </summary>
    public class EmployeeValidate : IEmployeeValidate
    {
        public readonly IEmployeeRepository _employeeRepository;

        public EmployeeValidate(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Kiểm tra mã nhân viên đã tồn tại hay chưa(TH1: Tất cả, TH2: Tất cả trừ chính nó)
        /// </summary>
        /// <param name="employeeId">Id nhân viên</param>
        /// <param name="employee">Nhân viên</param>
        /// <returns>có - true, ko - false </returns>
        /// <exception cref="ConflictException"></exception>
        public async Task<bool> CheckEmployeeExistAsync(Guid? employeeId, Employee employee)
        {
            var isExistEmployee = await _employeeRepository.IsExistEmployeeAsync(employeeId, employee.EmployeeCode);
            if (isExistEmployee)
            {
                throw new ConflictException( Resource.ResourceVN.Error_EmployeeCodeExists, ErrorCode.DuplicateCode);
            }
            return isExistEmployee;
        }

    }
}
