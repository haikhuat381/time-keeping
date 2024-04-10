using MISA.WEB07.MF1755.KVHAI.Domain;
using MISA.WEB07.MF1755.KVHAI.Domain.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    public class EmployeeCreateDto : BaseDto
    {
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [MaxLength(20, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_EmployeeCodeMaxLength")]
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_EmployeeCodeRequired")]
        public String? EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [MaxLength(100, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_FullNameMaxLength")]
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_FullNameRequired")]
        public String? FullName { get; set; }
            
        /// <summary>
        /// Định danh đơn vị
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_DepartmentRequired")]
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [DateNotGreaterThanCurrent(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_DateOfBirthInvalid")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public GenderEnum? Gender { get; set; }


        /// <summary>
        /// Chức danh
        /// </summary>
        [MaxLength(255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "CareerTitles")]
        public String? CareerTitles { get; set; }

        /// <summary>
        /// Số căn cước công dân
        /// </summary>
        [MaxLength(25, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_IdentityNumberMaxLength")]

        public String? IdentityNumber { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        [MaxLength(255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_IdentityPlaceMaxLength")]
        public String? IdentityPlace { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        [DateNotGreaterThanCurrent(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_IdentityDateInvalid")]
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [MaxLength(255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_AddressMaxLength")]
        public String? Address { get; set; }

        /// <summary>
        /// Điện thoại di động
        /// </summary>
        [MaxLength(50, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_PhoneNumberMaxLength")]
        public String? PhoneNumber { get; set; }

        /// <summary>
        /// Điện thoại cố định
        /// </summary>
        [MaxLength(50, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_LandlinePhoneMaxLength")]
        public String? LandlinePhone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [MaxLength(50, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_EmailMaxLength")]
        [EmailAddress(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_EmailInvalid")]
        
        public String? Email { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        [MaxLength(25, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_BankNumberMaxLength")]
        public String? BankNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [MaxLength(255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_BankNameMaxLength")]
        public String? BankName { get; set; }

        /// <summary>
        /// Chi nhánh
        /// </summary>
        [MaxLength(255, ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = "Error_BankBranchMaxLength")]
        public String? BankBranch { get; set; }

    }
}
