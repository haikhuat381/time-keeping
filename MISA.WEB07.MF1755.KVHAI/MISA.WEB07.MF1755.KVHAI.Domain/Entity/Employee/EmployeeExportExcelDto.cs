using MISA.WEB07.MF1755.KVHAI.Domain;
using System;
using System.ComponentModel;

namespace MISA.WEB07.MF1755.KVHAI.Model
{
    public class EmployeeExportExcelDto
    {
        /// <summary>
        /// Số thứ tự
        /// </summary>
        [DisplayName("STT")]
        public int? NumericalOrder { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [DisplayName("Mã nhân viên")]
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [DisplayName("Tên nhân viên")]
        public string? FullName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [DisplayName("Ngày sinh")]
        public string? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>

        [DisplayName("Giới tính")]
        public string? Gender { get; set; }

        /// <summary>
        /// Số căn cước công dân
        /// </summary>
        [DisplayName("Số CCCD")]
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        [DisplayName("Ngày cấp")]
        public string? IdentityDate { get; set; }


        /// <summary>
        /// Nơi cấp
        /// </summary>
        [DisplayName("Nơi cấp")]
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        [DisplayName("Mã đơn vị")]
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        [DisplayName("Tên đơn vị")]
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        [DisplayName("Chức danh")]
        public string? CareerTitles { get; set; }

        /// <summary>
        /// Điện thoại di động
        /// </summary>
        [DisplayName("Điện thoại di động")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Điện thoại cố định
        /// </summary>
        [DisplayName("Điện thoại cố định")]
        public string? LandlinePhone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [DisplayName("Email")]
        public string? Email { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        [DisplayName("Số tài khoản ngân hàng")]
        public string? BankNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [DisplayName("Tên ngân hàng")]
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh
        /// </summary>
        [DisplayName("Chi nhánh")]
        public string? BankBranch { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [DisplayName("Địa chỉ")]
        public string? Address { get; set; }
    }
}
