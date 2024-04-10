using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public class OvertimeValidate : IOvertimeValidate
    {
        public readonly IOvertimeRepository _overtimeRepository;

        public OvertimeValidate(IOvertimeRepository overtimeRepository)
        {
            _overtimeRepository = overtimeRepository;
        }

        /// <summary>
        /// Kiểm tra validate nghiệp vụ
        /// </summary>
        /// <param name="overtime">Đơn đăng ký làm thêm</param>
        /// <exception cref="BadRequestException">Lỗi</exception>
        /// <returns></returns>
        public void CheckValidateBusiness(Overtime overtime)
        {
            var messageError = "";
            if(overtime.FromDate > overtime.ToDate)
            {
                messageError = Resource.ResourceVN.Error_FromDateBiggerTodateOvertime;
            }

            if (overtime.BreakTimeFrom.HasValue && !overtime.BreakTimeTo.HasValue)
            {
                if(overtime.BreakTimeFrom < overtime.FromDate)
                {
                    messageError = Resource.ResourceVN.Error_BreakTimeFromLessFromDateOvertime;
                    messageError += ", ";
                }
                messageError += Resource.ResourceVN.Error_BreakTimeToOvertimeRequired;
            }

            if (overtime.BreakTimeTo.HasValue && !overtime.BreakTimeFrom.HasValue)
            {
                messageError = Resource.ResourceVN.Error_BreakTimeFromOvertimeRequired;
                if (overtime.BreakTimeTo > overtime.ToDate)
                {
                    messageError += ", ";
                    messageError += Resource.ResourceVN.Error_BreakTimeToBiggerToDateOvertime;
                }
            }

            if (overtime.BreakTimeTo.HasValue && overtime.BreakTimeFrom.HasValue)
            {
                if (overtime.BreakTimeFrom < overtime.FromDate)
                {
                    messageError = Resource.ResourceVN.Error_BreakTimeFromLessFromDateOvertime;
                }
                if (overtime.BreakTimeTo < overtime.BreakTimeFrom)
                {
                    messageError = Resource.ResourceVN.Error_BreakTimeToLessBreakTimeFromOvertime;
                }

                if (overtime.ToDate < overtime.BreakTimeTo)
                {
                    messageError = Resource.ResourceVN.Error_ToDateLessBreakTimeToOvertime;
                }

            }
            if ((overtime.OverTimeInWorkingShiftId != OverTimeInWorkingShift.DayOff || !overtime.OverTimeInWorkingShiftId.HasValue) && !overtime.WorkingShiftId.HasValue)
            {
                messageError = Resource.ResourceVN.Error_WorkingShiftOvertimeRequired;
            }

            if(messageError != "")
            {
                throw new BadRequestException(messageError, ErrorCode.Date);
            }

        }

        /// <summary>
        /// Kiểm tra đã có danh sách nhân viên làm thêm hay chưa
        /// </summary>
        /// <param name="overtime">Đơn đăng ký làm thêm</param>
        /// <exception cref="BadRequestException">Lỗi</exception>
        /// <returns></returns>
        public void CheckOvertimeEmployees(Overtime overtime)
        {
            if (overtime.OvertimeEmployees.Count == 0)
            {
                throw new BadRequestException(Resource.ResourceVN.Error_OvertimeEmployeesRequired, ErrorCode.ArrayEmpty);
            }
        }
    }
}
