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
    public class OvertimeServiceTests
    {
        private IOvertimeRepository _overtimeRepository { get; set; }
        private IOvertimeValidate _overtimeValidate { get; set; }
        private OvertimeService _overtimeService { get; set; }
        private IMapper _mapper { get; set; }

        /// <summary>
        /// setup
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _overtimeRepository = Substitute.For<IOvertimeRepository>();
            _overtimeValidate = Substitute.For<IOvertimeValidate>();
            _mapper = Substitute.For<IMapper>();
            _overtimeService = Substitute.For<OvertimeService>(_overtimeRepository, _overtimeValidate, _mapper);
        }

        // Test Insert

        /// <summary>
        /// UnitTests InSertAsync with OvertimeId not null
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task InSertAsync_EmptyOvertimeId_IdNotNull()
        {
            //Arrange
            var overtimeCreateDto = new OvertimeCreateDto();
            var overtime = new Overtime()
            {
                OvertimeId = Guid.Empty
            };

            _overtimeService.MapCreateDtoToEntity(overtimeCreateDto).Returns(overtime);

            //Act
            var overtimeDto = await _overtimeService.InSertAsync(overtimeCreateDto);

            //Assert
            Assert.That(overtime.OvertimeId, Is.Not.EqualTo(expected: Guid.Empty));
        }


        /// <summary>
        /// UnitTests InSertAsync with CreatedBy and ModifiedBy not null
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task InSertAsync_OvertimeAuditNull_OvertimeAuditNotNull()
        {
            //Arrange
            var overtimeCreateDto = new OvertimeCreateDto();
            var overtime = new Overtime()
            {
                OvertimeId = Guid.Empty
            };

            _overtimeService.MapCreateDtoToEntity(overtimeCreateDto).Returns(overtime);

            //Act
            var overtimeDto = await _overtimeService.InSertAsync(overtimeCreateDto);

            //Assert
            Assert.That(overtime.CreatedBy, Is.EqualTo("KVH"));
            Assert.That(overtime.ModifiedBy, Is.EqualTo("KVH"));
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
            var overtimeCreateDto = new OvertimeCreateDto();
            var overtime = new Overtime()
            {
                OvertimeId = Guid.Empty
            };

            _overtimeService.MapCreateDtoToEntity(overtimeCreateDto).Returns(overtime);

            //Act
            var overtimeDto = await _overtimeService.InSertAsync(overtimeCreateDto);

            //Assert
            await _overtimeService.Received(1).ValidateCreateBusiness(overtime);
            await _overtimeRepository.Received(1).InSertAsync(overtime);
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
            var overtime = new Overtime();

            _overtimeService.When(x => x.ValidateCreateBusiness(overtime)).CallBase();
            //Act
            await _overtimeService.ValidateCreateBusiness(overtime);

            //Assert
            _overtimeValidate.Received(1).CheckValidateBusiness(overtime);
            _overtimeValidate.Received(1).CheckOvertimeEmployees(overtime);
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
            var overtimeUpdateDto = new OvertimeUpdateDto();

            var overtime = new Overtime();
            _overtimeRepository.GetAsync(overtimeUpdateDto.OvertimeId).Returns(overtime);

            var newOvertime = new Overtime();
            _overtimeService.MapUpdateDtoToEntity(overtimeUpdateDto, overtime).Returns(newOvertime);

            //Act
            var result = await _overtimeService.UpdateAsync(overtimeUpdateDto.OvertimeId, overtimeUpdateDto);

            //Assert
            await _overtimeRepository.Received(1).GetAsync(overtimeUpdateDto.OvertimeId);
            await _overtimeService.Received(1).ValidateUpdateBusiness(newOvertime);
            await _overtimeRepository.Received(1).UpdateAsync(overtimeUpdateDto.OvertimeId, newOvertime);
        }

        /// <summary>
        /// UnitTests UpdateAsync with ModifiedBy not null
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task UpdateAsync_OvertimeAuditNull_OvertimeAuditNotNull()
        {
            //Arrange
            var overtimeUpdateDto = new OvertimeUpdateDto();
            var overtime = new Overtime();
            _overtimeRepository.GetAsync(overtimeUpdateDto.OvertimeId).Returns(overtime);
            var newOvertime = new Overtime();

            _overtimeService.MapUpdateDtoToEntity(overtimeUpdateDto, overtime).Returns(newOvertime);

            //Act
            var result = await _overtimeService.UpdateAsync(overtimeUpdateDto.OvertimeId, overtimeUpdateDto);

            //Assert
            Assert.That(newOvertime.ModifiedBy, Is.EqualTo("KVH"));
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
            var overtime = new Overtime();

            _overtimeService.When(x => x.ValidateUpdateBusiness(overtime)).CallBase();

            //Act
            await _overtimeService.ValidateUpdateBusiness(overtime);

            //Assert
            _overtimeValidate.Received(1).CheckValidateBusiness(overtime);
            _overtimeValidate.Received(1).CheckOvertimeEmployees(overtime);
        }

        /// <summary>
        /// UnitTests ApproveRequestAsync Success with status = 1 (duyệt)
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task ApproveRequestAsync_ValidInput_Success()
        {
            var status = 1;
            List<Guid> ids = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            };

            List<Overtime> overtimes = new List<Overtime>
            {
                new Overtime(),
                new Overtime(),
                new Overtime()
            };
            _overtimeRepository.GetByListIdAsync(ids).Returns(overtimes);
            //Act
            var result = await _overtimeService.ApproveRequestAsync(status, ids);

            //Assert
            await _overtimeRepository.Received(1).GetByListIdAsync(ids);
            await _overtimeRepository.Received(1).ApproveRequestAsync(status, overtimes);
            //Assert.That(result, Is.EqualTo(ids.Count));
        }


        /// <summary>
        /// UnitTests ApproveRequestAsync Fail
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task ApproveRequestAsync_InValidInput_Fail()
        {
            var status = 2;
            List<Guid> ids = new List<Guid>();

            List<Overtime> overtimes = new List<Overtime>();
            _overtimeRepository.GetByListIdAsync(ids).Returns(overtimes);
            //Act
            var result = await _overtimeService.ApproveRequestAsync(status, ids);

            //Assert
            Assert.That(result, Is.EqualTo(0));
        }

        ///// <summary>
        ///// UnitTests OvertimeExportToExcelAsync Success with status = 1 (duyệt)
        ///// </summary>
        ///// <returns></returns>
        ///// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task OvertimeExportToExcelAsync_ValidInput_Success()
        {
            // Arrange
            var overtimeSearchPaging = new OvertimeSearchPaging();
            var overtimes = new List<Overtime>();

            var overtimeFilterResult = new OvertimeFilterResult
            {
                Data = overtimes,
            };
            var overtimeExportExcel = new OvertimeExportExcel();

            _overtimeRepository.GetFiltersAsync(overtimeSearchPaging, true).Returns(overtimeFilterResult);
            _overtimeRepository.OvertimeExportToExcelAsync(Arg.Any<List<OvertimeExportExcelDto>>())
                .Returns(overtimeExportExcel);

            _overtimeRepository.OvertimeExportToExcelAsync(Arg.Any<List<OvertimeExportExcelDto>>()).Returns(overtimeExportExcel);

            // Act
            var result = await _overtimeService.OvertimeExportToExcelAsync(overtimeSearchPaging);

            // Assert
            _overtimeRepository.Received(1).OvertimeExportToExcelAsync(Arg.Any<List<OvertimeExportExcelDto>>());


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
            var overtimeId = Guid.NewGuid();
            var overtime = new Overtime();
            _overtimeRepository.GetAsync(overtimeId).Returns(overtime);

            //Act
            var result = await _overtimeService.DeleteAsync(overtimeId);

            //Assert
            await _overtimeRepository.Received(1).GetAsync(overtimeId);
            await _overtimeRepository.Received(1).DeleteAsync(overtime);
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

            List<Overtime> overtimes = new List<Overtime>
            {
                new Overtime(),
                new Overtime(),
                new Overtime()
            };
            _overtimeRepository.GetByListIdAsync(ids).Returns(overtimes);
            //Act
            var result = await _overtimeService.DeleteMultipleAsync(ids);

            //Assert
            await _overtimeRepository.Received(1).GetByListIdAsync(ids);
            await _overtimeRepository.Received(1).DeleteMultipleAsync(overtimes);
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

            List<Overtime> overtimes = new List<Overtime>();
            _overtimeRepository.GetByListIdAsync(ids).Returns(overtimes);

            //Act
            var result = await _overtimeService.DeleteMultipleAsync(ids);

            //Assert
            Assert.That(result, Is.EqualTo(0));
        }


        // Test Read

        /// <summary>
        /// UnitTests FindAsync Success
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task FindAsync_ValidInput_Success()
        {
            //Arrange
            var overtimeId = Guid.NewGuid();
            var overtime = new Overtime();
            var overtimeDto = new OvertimeDto();

            _overtimeRepository.FindAsync(overtimeId).Returns(overtime);
            _overtimeService.MapEntityToDto(overtime).Returns(overtimeDto);

            //Act
            var result = await _overtimeService.FindAsync(overtimeId);

            //Assert
            _overtimeRepository.Received(1).FindAsync(overtimeId);
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
            var overtimeId = Guid.NewGuid();
            var overtime = new Overtime();
            var overtimeDto = new OvertimeDto();

            _overtimeRepository.GetAsync(overtimeId).Returns(overtime);
            _overtimeService.MapEntityToDto(overtime).Returns(overtimeDto);

            //Act
            var result = await _overtimeService.GetAsync(overtimeId);

            //Assert
            _overtimeRepository.Received(1).GetAsync(overtimeId);

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
            List<Overtime> overtimes = new List<Overtime>();
            List<OvertimeDto> overtimeDtos = new List<OvertimeDto>();

            _overtimeRepository.GetAllAsync().Returns(overtimes);

            //Act
            var result = await _overtimeService.GetAllAsync();

            //Assert
            _overtimeRepository.Received(1).GetAllAsync();

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
            var overtimeSearchPaging = new OvertimeSearchPaging();
            var overtimeFilterResult = new OvertimeFilterResult();
            var overtimeFilterResultDto = new OvertimeFilterResultDto();

            _overtimeRepository.GetFiltersAsync(overtimeSearchPaging, false).Returns(overtimeFilterResult);
            _overtimeService.MapFilterResultDtoToFilterResult(overtimeFilterResult).Returns(overtimeFilterResultDto);

            //Act
            var result = await _overtimeService.GetFiltersAsync(overtimeSearchPaging);

            //Assert
            _overtimeRepository.Received(1).GetFiltersAsync(overtimeSearchPaging, false);
        }

    }
}
