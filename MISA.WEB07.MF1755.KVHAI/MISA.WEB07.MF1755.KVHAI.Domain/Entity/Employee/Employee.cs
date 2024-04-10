//using MISA.WEB07.MF1755.KVHAI.Domain;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public class Employee : BaseEntity, IEntity
    {
        /// <summary>
        /// Định danh nhân viên
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public String? EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public String? FullName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public GenderEnum? Gender { get; set; }

        /// <summary>
        /// Định danh đơn vị
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        public String? CareerTitles { get; set; }

        /// <summary>
        /// Số căn cước công dân
        /// </summary>
        public String? IdentityNumber { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        public String? IdentityPlace { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public String? Address { get; set; }

        /// <summary>
        /// Điện thoại di động
        /// </summary>
        public String? PhoneNumber { get; set; }

        /// <summary>
        /// Điện thoại cố định
        /// </summary>
        public String? LandlinePhone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public String? Email { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public String? BankNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public String? BankName { get; set; }

        /// <summary>
        /// Chi nhánh
        /// </summary>
        public String? BankBranch { get; set; }

        /// <summary>
        /// Lấy ra EmployeeId
        /// </summary>
        /// <returns></returns>
        public Guid GetId()
        {
            return EmployeeId;
        }

        /// <summary>
        /// set lại EmployeeId
        /// </summary>
        /// <param name="id"></param>
        public void SetId(Guid id)
        {
            EmployeeId = id;
        }
    }
}
