using MISA.WEB07.MF1755.KVHAI.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    public class UserDto : BaseDto
    {
        /// <summary>
        /// Định danh người dùng
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Tên người dùng
        /// </summary>
        public String? UserName { get; set; }

        /// <summary>
        /// Định danh kiểu format
        /// </summary>
        public Guid? DateFormatId { get; set; }
    }
}
