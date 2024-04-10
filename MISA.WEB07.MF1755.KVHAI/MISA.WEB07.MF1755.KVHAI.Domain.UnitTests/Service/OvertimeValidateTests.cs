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
    [TestFixture]
    public class OvertimeValidateTests
    {
        private IOvertimeRepository _overtimeRepository { get; set; }
        private OvertimeValidate _overtimeValidate { get; set; }

        /// <summary>
        /// Setup
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _overtimeRepository = Substitute.For<IOvertimeRepository>();
            _overtimeValidate = Substitute.For<OvertimeValidate>(_overtimeRepository);
        }

        /// <summary>
        /// UnitTests CheckValidateBusiness Date Invalid  -> BadRequestException
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [TestCase("2023-11-12T00:00:00.000Z", null, null, "2023-11-10T00:00:00.000Z")]
        [TestCase("2023-11-12T00:00:00.000Z", "2023-11-13T00:00:00.000Z", null, "2023-11-15T00:00:00.000Z")]
        [TestCase("2023-11-12T00:00:00.000Z", "2023-11-10T00:00:00.000Z", null, "2023-11-15T00:00:00.000Z")]
        [TestCase("2023-11-12T00:00:00.000Z", null, "2023-11-12T00:00:00.000Z", "2023-11-15T00:00:00.000Z")]
        [TestCase("2023-11-12T00:00:00.000Z", null, "2023-11-16T00:00:00.000Z", "2023-11-15T00:00:00.000Z")]

        [TestCase("2023-11-12T00:00:00.000Z", "2023-11-11T00:00:00.000Z", "2023-11-14T00:00:00.000Z", "2023-11-15T00:00:00.000Z")]
        [TestCase("2023-11-12T00:00:00.000Z", "2023-11-11T00:00:00.000Z", "2023-11-09T00:00:00.000Z", "2023-11-15T00:00:00.000Z")]
        [TestCase("2023-11-12T00:00:00.000Z", "2023-11-11T00:00:00.000Z", "2023-11-14T00:00:00.000Z", "2023-11-13T00:00:00.000Z")]
        public async Task CheckValidateBusiness_InValidDate_ThrowException(string fromDate, string? breakTimeFrom, string? breakTimeTo, string toDate)
        {
            var overtime = new Overtime
            {
                FromDate = DateTime.Parse(fromDate),
                ToDate = DateTime.Parse(toDate),
                BreakTimeFrom = string.IsNullOrEmpty(breakTimeFrom) ? null : DateTime.Parse(breakTimeFrom),
                BreakTimeTo = string.IsNullOrEmpty(breakTimeTo) ? null : DateTime.Parse(breakTimeTo)
            };

            //Act
            //Assert
            Assert.ThrowsAsync<BadRequestException>(async () => _overtimeValidate.CheckValidateBusiness(overtime));
        }

        /// <summary>
        /// UnitTests CheckValidateBusiness with OverTimeInWorkingShiftId !=4 (DayOff) -> BadRequestException
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task CheckValidateBusiness_InValidOverTimeInWorkingShiftId_ThrowException()
        {
            var overtime = new Overtime
            {
                OverTimeInWorkingShiftId = OverTimeInWorkingShift.AfterShift,
                WorkingShiftId = null
            };

            //Act
            //Assert
            Assert.ThrowsAsync<BadRequestException>(async () => _overtimeValidate.CheckValidateBusiness(overtime));
        }


        /// <summary>
        /// UnitTests CheckOvertimeEmployees Success -> BadRequestException
        /// </summary>
        /// <returns></returns>
        /// CreateBy: KVH (25/09/2023)
        [Test]
        public async Task CheckOvertimeEmployees_InValidOvertimeEmployees_ThrowException()
        {
            List<Employee> employees = new List<Employee>();
            var overtime = new Overtime
            {
                OvertimeEmployees = employees
            };

            //Act
            //Assert
            Assert.ThrowsAsync<BadRequestException>(async () => _overtimeValidate.CheckOvertimeEmployees(overtime));
        }
    }
}
