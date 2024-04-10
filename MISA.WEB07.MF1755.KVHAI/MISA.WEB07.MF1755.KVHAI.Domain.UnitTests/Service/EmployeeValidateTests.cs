using MISA.WEB07.MF1755.KVHAI.Application;
using MISA.WEB07.MF1755.KVHAI.Infrastructure;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain.UnitTests
{
    public class EmployeeValidateTests
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public EmployeeValidate EmployeeValidate { get; set; }

        /// <summary>
        /// Setup
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            EmployeeRepository = Substitute.For<IEmployeeRepository>();
            EmployeeValidate = Substitute.For<EmployeeValidate>(EmployeeRepository);
        }

        /// <summary>
        /// UnitTests CheckEmployeeExistAsync Success
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task CheckEmployeeExistAsync_NotExistEmployee_Success()
        {
            // Arrrge
            var employeeId = Guid.NewGuid();
            var employee = new Employee
            {
                EmployeeCode = "NV0001"
            };
            EmployeeRepository.IsExistEmployeeAsync(employee.EmployeeId, employee.EmployeeCode).Returns(false);

            //Act
            await EmployeeValidate.CheckEmployeeExistAsync(employee.EmployeeId, employee);

            //Assert
            await EmployeeRepository.Received(1).IsExistEmployeeAsync(employee.EmployeeId, employee.EmployeeCode);
        }

        /// <summary>
        /// UnitTests CheckEmployeeExistAsync ThrowException
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task CheckEmployeeExistAsync_ExistEmployee_ThrowException()
        {
            // Arrrge
            var employeeId = Guid.NewGuid();
            var employee = new Employee
            {
                EmployeeCode = "NV0002"
            };
            EmployeeRepository.IsExistEmployeeAsync(employee.EmployeeId, employee.EmployeeCode).Returns(true);

            //Act and Assert

            Assert.ThrowsAsync<ConflictException>(async () =>  await EmployeeValidate.CheckEmployeeExistAsync(employee.EmployeeId, employee));

            await EmployeeRepository.Received(1).IsExistEmployeeAsync(employee.EmployeeId, employee.EmployeeCode);
        }
    }
}
