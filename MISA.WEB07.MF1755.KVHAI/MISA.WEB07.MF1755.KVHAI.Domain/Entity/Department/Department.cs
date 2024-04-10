namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public class Department : BaseEntity, IEntity
    {
        /// <summary>
        /// Định danh đơn vị
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Định danh đơn vị cha
        /// </summary>
        public Guid? DepartmentParentId { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string? DepartmentName { get; set; }

        ///// <summary>
        ///// Người tạo
        ///// </summary>
        //public string? CreatedBy { get; set; }

        ///// <summary>
        ///// Ngày tạo
        ///// </summary>
        //public DateTime? CreatedDate { get; set; }

        ///// <summary>
        ///// Ngày sửa đổi
        ///// </summary>
        //public string? ModifiedBy { get; set; }

        /// <summary>
        /// Lấy ra DepartmentId
        /// </summary>
        /// <returns></returns>
        public Guid GetId()
        {
            return DepartmentId;
        }

        /// <summary>
        /// set DepartmentId
        /// </summary>
        /// <param name="id"></param>
        public void SetId(Guid id)
        {
            DepartmentId = id;
        }
    }
}
