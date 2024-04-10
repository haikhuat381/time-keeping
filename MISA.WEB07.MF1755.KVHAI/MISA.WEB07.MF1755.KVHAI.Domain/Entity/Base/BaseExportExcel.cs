using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public class BaseExportExcel
    {

        /// <summary>
        /// File dữ liệu
        /// </summary>
        public byte[]? File { get; set; }

        /// <summary>
        /// Loại Mime Excel
        /// </summary>
        public string? ExcelMimeType { get; set; }

        /// <summary>
        /// Tên file
        /// </summary>
        public string? FileName { get; set; }
    }
}
