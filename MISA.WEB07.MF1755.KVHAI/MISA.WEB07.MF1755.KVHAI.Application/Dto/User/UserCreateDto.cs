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
    public class UserCreateDto : BaseDto
    {
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
