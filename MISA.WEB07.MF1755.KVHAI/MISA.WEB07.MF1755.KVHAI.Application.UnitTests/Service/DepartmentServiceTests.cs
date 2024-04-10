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
    public class DepartmentServiceTests
    {
        public IDepartmentRepository DepartmentRepository { get; set; }
        public DepartmentService DepartmentService { get; set; }
        public IMapper Mapper { get; set; }

        /// <summary>
        /// Setup
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            DepartmentRepository = Substitute.For<IDepartmentRepository>();
            Mapper = Substitute.For<IMapper>();
            DepartmentService = Substitute.For<DepartmentService>(DepartmentRepository, Mapper);
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
            var departmentId = Guid.NewGuid();
            var department = new Department();
            var departmentDto = new DepartmentDto();

            DepartmentRepository.FindAsync(departmentId).Returns(department);
            DepartmentService.MapEntityToDto(department).Returns(departmentDto);

            //Act
            var result = await DepartmentService.FindAsync(departmentId);

            //Assert
            DepartmentRepository.Received(1).FindAsync(departmentId);
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
            var departmentId = Guid.NewGuid();
            var department = new Department();
            var departmentDto = new DepartmentDto();

            DepartmentRepository.GetAsync(departmentId).Returns(department);
            DepartmentService.MapEntityToDto(department).Returns(departmentDto);

            //Act
            var result = await DepartmentService.GetAsync(departmentId);

            //Assert
            DepartmentRepository.Received(1).GetAsync(departmentId);

        }

        /// <summary>
        /// UnitTests GetAllAsync Success
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task GetAllAsync_ValidInput_Success()
        {
            //Arrange
            List<Department> departments = new List<Department>();
            List<DepartmentDto> departmentDtos = new List<DepartmentDto>();

            DepartmentRepository.GetAllAsync().Returns(departments);

            //Act
            var result = await DepartmentService.GetAllAsync();

            //Assert
            DepartmentRepository.Received(1).GetAllAsync();

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
            var departmentSearchPaging = new DepartmentSearchPaging();
            var departmentFilterResult = new DepartmentFilterResult();
            var departmentFilterResultDto = new DepartmentFilterResultDto();

            DepartmentRepository.GetFiltersAsync(departmentSearchPaging, false).Returns(departmentFilterResult);
            DepartmentService.MapFilterResultDtoToFilterResult(departmentFilterResult).Returns(departmentFilterResultDto);

            //Act
            var result = await DepartmentService.GetFiltersAsync(departmentSearchPaging);

            //Assert
            DepartmentRepository.Received(1).GetFiltersAsync(departmentSearchPaging, false);
        }


    }
}
