using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public class ConflictException : Exception
    {
        /// <summary>
        /// Mã code nội bộ
        /// </summary>
        public ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Thông báo lỗi cho người dùng
        /// </summary>
        public string? UserMsg { get; set; }
        public ConflictException() { }
        public ConflictException(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        public ConflictException(string message) : base(message)
        {
        }

        public ConflictException(string message, ErrorCode errorCode) : base(message)
        {
            ErrorCode = errorCode;
            UserMsg = message;
        }
    }
}
