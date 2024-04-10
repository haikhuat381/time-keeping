using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    public class DepartmentDto : BaseDto
    {
        /// <summary>
        /// Định danh đơn vị
        /// </summary>
        public Guid? DepartmentId { get; set; }

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
    }
}
