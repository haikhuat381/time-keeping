using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public class OvertimeExportExcelDto
    {
        /// <summary>
        /// Định danh đăng ký làm thêm
        /// </summary>
        //public string? OvertimeId { get; set; }
        /// <summary>
        /// Số thứ tự
        /// </summary>
        [DisplayName("STT")]
        public string? OvertimeId { get; set; }

        //public int? NumericalOrder { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [DisplayName("Mgười nộp đơn")]
        public string? FullName { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [DisplayName("Mã nhân viên")]

        public string? EmployeeCode { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        [DisplayName("Đơn vị công tác")]

        public string? DepartmentName { get; set; }

        /// <summary>
        /// Chức danh, Vị trí
        /// </summary>
        [DisplayName("Vị trí công việc")]

        public string? CareerTitles { get; set; }

        /// <summary>
        /// Ngày nộp đơn
        /// </summary>
        [DisplayName("Ngày nộp đơn")]

        public string? ApplyDate { get; set; }


        /// <summary>
        /// Thời điểm làm thêm
        /// </summary>
        [DisplayName("Thời điểm làm thêm")]
        public string? OverTimeInWorkingShiftName { get; set; }

        /// <summary>
        /// Ngày làm thêm từ
        /// </summary>
        [DisplayName("Làm thêm từ")]

        public string? FromDate { get; set; }

        /// <summary>
        /// Ngày làm thêm đến
        /// </summary>
        [DisplayName("Làm thêm đến")]

        public string? ToDate { get; set; }


        /// <summary>
        /// Ca áp dụng
        /// </summary>
        [DisplayName("Ca áp dụng")]

        public string? WorkingShiftName { get; set; }

        /// <summary>
        /// Lý do
        /// </summary>
        [DisplayName("Lý do làm thêm")]

        public string? Reason { get; set; }

        /// <summary>
        /// Người duyệt
        /// </summary>
        [DisplayName("Người duyệt")]
        public string? ApprovalName { get; set; }


        /// <summary>
        /// Danh sách tên người liên quan
        /// </summary>
        [DisplayName("Người liên quan")]

        public string? RelationShipNames { get; set; }


        /// <summary>
        /// Ghi chú
        /// </summary>
        [DisplayName("Ghi chú")]

        public string? Note { get; set; }


        /// <summary>
        /// Trạng thái
        /// </summary>
        [DisplayName("Trạng thái")]

        public string? Status { get; set; }


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
        



        ///// <summary>
        ///// Danh sách nhân viên làm thêm
        ///// </summary>
        //[DisplayName("Mã nhân viên")]

        //public List<Employee> OvertimeEmployees { get; set; }
    }
}
