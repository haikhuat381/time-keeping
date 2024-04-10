using MISA.WEB07.MF1755.KVHAI.Domain;
using MISA.WEB07.MF1755.KVHAI.Domain.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    public class OvertimeCreateDto : BaseDto
    {

        /// <summary>
        /// Ngày nộp đơn
        /// </summary>
        [RequiredAtb("Error_ApplyDateOvertimeRequired")]
        public DateTime? ApplyDate { get; set; }

        /// <summary>
        /// Ngày làm thêm từ
        /// </summary>
        [RequiredAtb("Error_FromDateOvertimeRequired")]
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Ngày làm thêm đến
        /// </summary>
        [RequiredAtb("Error_ToDateOvertimeRequired")]
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Thời điểm làm thêm
        /// </summary>
        [RequiredAtb("Error_OverTimeInWorkingShiftOvertimeRequired")]
        public OverTimeInWorkingShift? OverTimeInWorkingShiftId { get; set; }


        /// <summary>
        /// Ca áp dụng
        /// </summary>
        //[RequiredAtb("Error_WorkingShiftOvertimeRequired")]
        public WorkingShift? WorkingShiftId { get; set; }

        /// <summary>
        /// Lý do
        /// </summary>
        [RequiredAtb("Error_ReasonOvertimeRequired")]
        public string? Reason { get; set; }

        /// <summary>
        /// Dịnh dang người duyệt
        /// </summary>
        [RequiredAtb("Error_ApprovalOvertimeRequired")]
        public Guid? ApprovalId { get; set; }

        /// <summary>
        /// Tên người duyệt
        /// </summary>
        public string? ApprovalName { get; set; }


        /// <summary>
        /// Định danh nhân viên
        /// </summary>
        [RequiredAtb("Error_EmployeeOvertimeRequired")]
        public Guid? EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public String? EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public String? FullName { get; set; }

        /// <summary>
        /// Định danh đơn vị
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Chức danh, Vị trí
        /// </summary>
        public string? CareerTitles { get; set; }

        /// <summary>
        /// Danh sách id người liên quan
        /// </summary>
        public string? RelationShipIds { get; set; }

        /// <summary>
        /// Danh sách tên người liên quan
        /// </summary>
        public string? RelationShipNames { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        [RequiredAtb("Error_StatusOvertimeRequired")]
        public Status? Status { get; set; }

        /// <summary>
        /// Id thời điểm làm thêm
        /// </summary>
        public string? OverTimeInWorkingShiftName { get; set; }

        /// <summary>
        /// Id ca áp dụng
        /// </summary>
        public string? WorkingShiftName { get; set; }

        /// <summary>
        /// Nghỉ giữa ca từ
        /// </summary>
        //[RequiredAtb("Error_BreakTimeDateOvertimeRequired")]
        public DateTime? BreakTimeFrom { get; set; }

        /// <summary>
        /// Nghỉ giữa ca đến
        /// </summary>
        //[RequiredAtb("Error_BreakTimeToOvertimeRequired")]
        public DateTime? BreakTimeTo { get; set; }

        /// <summary>
        /// Id loại làm thêm
        /// </summary>
        [RequiredAtb("Error_OvertimeTypeRequired")]
        public OvertimeType? OvertimeTypeId { get; set; }

        /// <summary>
        /// Tên loại làm thêm
        /// </summary>
        public string? OvertimeTypeName { get; set; }

        /// <summary>
        /// Danh sách nhân viên làm thêm
        /// </summary>
        //[MinLengthAtb(1)]
        //[DisplayName("Danh sách nhân viên làm thêm")]
        public List<Employee> OvertimeEmployees { get; set; }


        /// <summary>
        /// Mã nhân viên làm thêm
        /// </summary>
        [DisplayName("Mã nhân viên làm thêm")]

        public string? OvertimeEmployeeCode { get; set; }

        /// <summary>
        /// Nhân viên làm thêm
        /// </summary>
        [DisplayName("Nhân viên làm thêm")]

        public string? OvertimeEmployeeFullName { get; set; }
    }
}
