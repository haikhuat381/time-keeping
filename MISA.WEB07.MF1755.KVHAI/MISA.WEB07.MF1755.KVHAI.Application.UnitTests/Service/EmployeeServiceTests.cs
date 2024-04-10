using AutoMapper;
using MISA.WEB07.MF1755.KVHAI.Domain;
using MISA.WEB07.MF1755.KVHAI.Infrastructure;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Application.UnitTests
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
        public IEmployeeValidate EmployeeValidate { get; set; }
        public EmployeeService EmployeeService { get; set; }
        public IMapper Mapper { get; set; }

        /// <summary>
        /// setup
        /// </summary>
        [SetUp]
        public void SetUp() 
        {
            EmployeeRepository = Substitute.For<IEmployeeRepository>();
            EmployeeValidate = Substitute.For<IEmployeeValidate>();
            DepartmentRepository = Substitute.For<IDepartmentRepository>();
            Mapper = Substitute.For<IMapper>();
            EmployeeService = Substitute.For<EmployeeService>(EmployeeRepository, EmployeeValidate, DepartmentRepository, Mapper);
        }


        // Test Insert

        /// <summary>
        /// UnitTests InSertAsync with EmployeeId not null
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task InSertAsync_EmptyEmployeeId_IdNotNull() 
        {
            //Arrange
            var employeeCreateDto = new EmployeeCreateDto();
            var employee = new Employee()
            {
                EmployeeId = Guid.Empty
            };

            EmployeeService.MapCreateDtoToEntity(employeeCreateDto).Returns(employee);

            //Act
            var employeeDto = await EmployeeService.InSertAsync(employeeCreateDto);

            //Assert
            Assert.That(employee.EmployeeId, Is.Not.EqualTo(Guid.Empty));
        }

        /// <summary>
        /// UnitTests InSertAsync with CreatedBy and ModifiedBy not null
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task InSertAsync_EmployeeAuditNull_EmployeeAuditNotNull()
        {
            //Arrange
            var employeeCreateDto = new EmployeeCreateDto();
            var employee = new Employee()
            {
                EmployeeId = Guid.Empty
            };

            EmployeeService.MapCreateDtoToEntity(employeeCreateDto).Returns(employee);

            //Act
            var employeeDto = await EmployeeService.InSertAsync(employeeCreateDto);

            //Assert
            Assert.That(employee.CreatedBy, Is.EqualTo("KVH"));
            Assert.That(employee.ModifiedBy, Is.EqualTo("KVH"));
        }

        /// <summary>
        /// UnitTests InSertAsync Success
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task InsertAsync_ValidInput_Success()
        {
            //Arrange
            var employeeCreateDto = new EmployeeCreateDto();
            var employee = new Employee()
            {
                EmployeeId = Guid.Empty
            };

            EmployeeService.MapCreateDtoToEntity(employeeCreateDto).Returns(employee);

            //Act
            var employeeDto = await EmployeeService.InSertAsync(employeeCreateDto);

            //Assert
            await EmployeeService.Received(1).ValidateCreateBusiness(employee);
            await EmployeeRepository.Received(1).InSertAsync(employee);
        }

        /// <summary>
        /// UnitTests ValidateCreateBusiness Success
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task ValidateCreateBusiness_ValidInput_Success()
        {
            //Arrange
            var employee = new Employee
            {
                DepartmentId = Guid.NewGuid(),
                EmployeeCode = "NV9876",
            };

            var department = new Department
            {
                DepartmentId = (Guid)employee.DepartmentId
            };

            DepartmentRepository.FindAsync((Guid)employee.DepartmentId).Returns(department);
            EmployeeService.When(x => x.ValidateCreateBusiness(employee)).CallBase();
            //Act
            await EmployeeService.ValidateCreateBusiness(employee);

            //Assert
            await DepartmentRepository.Received(1).FindAsync((Guid)employee.DepartmentId);
            await EmployeeValidate.Received(1).CheckEmployeeExistAsync(null, employee);
        }

        /// <summary>
        /// UnitTests ValidateCreateBusiness ThrowException
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task ValidateCreateBusiness_InvalidDepartment_ThrowException()
        {
            //Arrange
            var employee = new Employee
            {
                DepartmentId = Guid.NewGuid(),
                EmployeeCode = "NV9876",
            };

            DepartmentRepository.FindAsync((Guid)employee.DepartmentId).Returns((Department)null);
            EmployeeService.When(x => x.ValidateCreateBusiness(employee)).CallBase();

            //Act and Assert
            Assert.ThrowsAsync<BadRequestException>(async () => await EmployeeService.ValidateCreateBusiness(employee));
            await DepartmentRepository.Received(1).FindAsync((Guid)employee.DepartmentId);
        }


        // Test Update

        /// <summary>
        /// UnitTests UpdateAsync Success
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task UpdateAsync_ValidInput_Success()
        {
            //Arrange
            var employeeUpdateDto = new EmployeeUpdateDto();

            var employee = new Employee();
            EmployeeRepository.GetAsync(employeeUpdateDto.EmployeeId).Returns(employee);

            var newEmployee = new Employee();
            EmployeeService.MapUpdateDtoToEntity(employeeUpdateDto, employee).Returns(newEmployee);

            //Act
            var result = await EmployeeService.UpdateAsync(employeeUpdateDto.EmployeeId, employeeUpdateDto);

            //Assert
            await EmployeeRepository.Received(1).GetAsync(employeeUpdateDto.EmployeeId);
            await EmployeeService.Received(1).ValidateUpdateBusiness(newEmployee);
            await EmployeeRepository.Received(1).UpdateAsync(employeeUpdateDto.EmployeeId, newEmployee);
        }

        /// <summary>
        /// UnitTests UpdateAsync with ModifiedBy not null
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task UpdateAsync_EmployeeAuditNull_EmployeeAuditNotNull()
        {
            //Arrange
            var employeeUpdateDto = new EmployeeUpdateDto();
            var employee = new Employee();
            EmployeeRepository.GetAsync(employeeUpdateDto.EmployeeId).Returns(employee);
            var newEmployee = new Employee();

            EmployeeService.MapUpdateDtoToEntity(employeeUpdateDto, employee).Returns(newEmployee);

            //Act
            var result = await EmployeeService.UpdateAsync(employeeUpdateDto.EmployeeId, employeeUpdateDto);

            //Assert
            Assert.That(newEmployee.ModifiedBy, Is.EqualTo("KVH"));
        }

        /// <summary>
        /// UnitTests ValidateUpdateBusiness Success
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task ValidateUpdateBusiness_ValidInput_Success()
        {
            //Arrange
            var employee = new Employee
            {
                DepartmentId = Guid.NewGuid(),
                EmployeeId = Guid.NewGuid(),
                EmployeeCode = "NV9876",
            };

            var department = new Department
            {
                DepartmentId = (Guid)employee.DepartmentId
            };

            DepartmentRepository.FindAsync((Guid)employee.DepartmentId).Returns(department);
            EmployeeService.When(x => x.ValidateUpdateBusiness(employee)).CallBase();

            //Act
            await EmployeeService.ValidateUpdateBusiness(employee);

            //Assert
            await DepartmentRepository.Received(1).FindAsync((Guid)employee.DepartmentId);
            await EmployeeValidate.Received(1).CheckEmployeeExistAsync(employee.EmployeeId, employee);
        }

        /// <summary>
        /// UnitTests ValidateUpdateBusiness ThrowException
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task ValidateUpdateBusiness_InvalidDepartment_ThrowException()
        {
            //Arrange
            var employee = new Employee
            {
                DepartmentId = Guid.NewGuid(),
                EmployeeCode = "NV9876",
            };

            DepartmentRepository.FindAsync((Guid)employee.DepartmentId).Returns((Department)null);
            EmployeeService.When(x => x.ValidateUpdateBusiness(employee)).CallBase();

            //Act and Assert
            Assert.ThrowsAsync<BadRequestException>(async () => await EmployeeService.ValidateUpdateBusiness(employee));
            await DepartmentRepository.Received(1).FindAsync((Guid)employee.DepartmentId);
        }

        // Test Delete

        /// <summary>
        /// UnitTests DeleteAsync Success
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task DeleteAsync_ValidInput_Success()
        {
            //Arrange
            var employeeId = Guid.NewGuid();
            var employee = new Employee();
            EmployeeRepository.GetAsync(employeeId).Returns(employee);

            //Act
            var result = await EmployeeService.DeleteAsync(employeeId);

            //Assert
            await EmployeeRepository.Received(1).GetAsync(employeeId);
            await EmployeeRepository.Received(1).DeleteAsync(employee);
        }

        /// <summary>
        /// UnitTests DeleteMultipleAsync Success
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task DeleteMultipleAsync_ValidInput_Success()
        {
            //Arrange
            List<Guid> ids = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            };

            List<Employee> employees = new List<Employee> 
            {
                new Employee(),
                new Employee(),
                new Employee()
            };
            EmployeeRepository.GetByListIdAsync(ids).Returns(employees);
            //Act
            var result = await EmployeeService.DeleteMultipleAsync(ids);

            //Assert
            await EmployeeRepository.Received(1).GetByListIdAsync(ids);
            await EmployeeRepository.Received(1).DeleteMultipleAsync(employees);
        }

        /// <summary>
        /// UnitTests DeleteMultipleAsync Empty in ids
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task DeleteMultipleAsync_EmptyIds_Fail()
        {
            //Arrange
            List<Guid> ids = new List<Guid>();

            List<Employee> employees = new List<Employee>();
            EmployeeRepository.GetByListIdAsync(ids).Returns(employees);

            //Act
            var result = await EmployeeService.DeleteMultipleAsync(ids);

            //Assert
            Assert.That(result, Is.EqualTo(0));
        }


        // Test Read

        /// <summary>
        /// UnitTests GetNewEmployeeCodeAsync Success
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task GetNewEmployeeCodeAsync_ValidInput_Success()
        {
            //Arrange
            var newCode = "NV9999";

            EmployeeRepository.GetNewEmployeeCodeAsync().Returns(newCode);

            //Act
            var result = await EmployeeService.GetNewEmployeeCodeAsync();

            //Assert
            EmployeeRepository.Received(1).GetNewEmployeeCodeAsync();
        }

        /// <summary>
        /// UnitTests FindAsync Success
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task FindAsync_ValidInput_Success()
        {
            //Arrange
            var employeeId = Guid.NewGuid();
            var employee = new Employee();
            var employeeDto = new EmployeeDto();

            EmployeeRepository.FindAsync(employeeId).Returns(employee);
            EmployeeService.MapEntityToDto(employee).Returns(employeeDto);

            //Act
            var result = await EmployeeService.FindAsync(employeeId);

            //Assert
            EmployeeRepository.Received(1).FindAsync(employeeId);
        }

        /// <summary>
        /// UnitTests GetAsync Success
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task GetAsync_ValidInput_Success()
        {
            //Arrange
            var employeeId = Guid.NewGuid();
            var employee = new Employee();
            var employeeDto = new EmployeeDto();

            EmployeeRepository.GetAsync(employeeId).Returns(employee);
            EmployeeService.MapEntityToDto(employee).Returns(employeeDto);

            //Act
            var result = await EmployeeService.GetAsync(employeeId);

            //Assert
            EmployeeRepository.Received(1).GetAsync(employeeId);

        }

        /// <summary>
        /// UnitTests GetAll Success
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task GetAllAsync_ValidInput_Success()
        {
            //Arrange
            List<Employee> employees = new List<Employee>();
            List<EmployeeDto> employeeDtos = new List<EmployeeDto>();

            EmployeeRepository.GetAllAsync().Returns(employees);

            //Act
            var result = await EmployeeService.GetAllAsync();

            //Assert
            EmployeeRepository.Received(1).GetAllAsync();

        }

        /// <summary>
        /// UnitTests GetFiltersAsync Success
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task GetFiltersAsync_ValidInput_Success()
        {
            //Arrange
            var employeeSearchPaging = new EmployeeSearchPaging();
            var employeeFilterResult = new EmployeeFilterResult();
            var employeeFilterResultDto = new EmployeeFilterResultDto();

            EmployeeRepository.GetFiltersAsync(employeeSearchPaging, false).Returns(employeeFilterResult);
            EmployeeService.MapFilterResultDtoToFilterResult(employeeFilterResult).Returns(employeeFilterResultDto);

            //Act
            var result = await EmployeeService.GetFiltersAsync(employeeSearchPaging);

            //Assert
            EmployeeRepository.Received(1).GetFiltersAsync(employeeSearchPaging, false);
        }

    }
}
