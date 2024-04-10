using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    public class DateFormatDto : BaseDto
    {
        /// <summary>
        /// Định danh kiểu format
        /// </summary>
        public Guid DateFormatId { get; set; }

        /// <summary>
        /// Tên kiểu format
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Loại format
        /// </summary>
        public string? Format { get; set; }
    }
}
