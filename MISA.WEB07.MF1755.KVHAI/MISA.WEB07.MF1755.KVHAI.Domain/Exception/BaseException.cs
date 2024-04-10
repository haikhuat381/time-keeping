using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public class BaseException
    {
        /// <summary>
        /// mã lỗi
        /// </summary>
        public int? ErrorCode { get; set; }

        /// <summary>
        /// Thông báo cho dev
        /// </summary>
        public string? DevMsg { get; set; }

        /// <summary>
        /// Thông báo cho người dùng
        /// </summary>
        public object? UserMsg { get; set; }

        /// <summary>
        /// Thông tin chi tiết về mã lỗi
        /// </summary>
        public string? MoreInfo { get; set; }

        /// <summary>
        /// Mã để tra cứu thông tin log
        /// </summary>
        public string? TraceId { get; set; }

        public object? Errors { get; set; }

        public override string ToString()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return JsonSerializer.Serialize(this, options);
        }
    }
}
