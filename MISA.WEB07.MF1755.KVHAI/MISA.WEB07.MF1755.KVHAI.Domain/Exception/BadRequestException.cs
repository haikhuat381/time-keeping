﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public class BadRequestException : Exception
    {
        /// <summary>
        /// Mã code nội bộ
        /// </summary>
        public ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Thông báo lỗi cho người dùng
        /// </summary>
        public string? UserMsg { get; set; }
        public BadRequestException() { }
        public BadRequestException(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, ErrorCode errorCode) : base(message)
        {
            ErrorCode = errorCode;
            UserMsg = message;
        }
    }
}