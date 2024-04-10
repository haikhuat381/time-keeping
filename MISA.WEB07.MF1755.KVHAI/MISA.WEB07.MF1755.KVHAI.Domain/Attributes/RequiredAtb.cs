using MISA.WEB07.MF1755.KVHAI.Domain.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public class RequiredAtb : RequiredAttribute
    {

        //public RequiredAtb(string errorMessage)
        //{
        //    ErrorMessageResourceType = typeof(ResourceVN);
        //    ErrorMessageResourceName = errorMessage;
        //}

        //public override bool IsValid(object value)
        //{
        //    return value != null && !string.IsNullOrWhiteSpace(value.ToString());
        //}
        public RequiredAtb(string errorMessage) : base()
        {
            ErrorMessageResourceType = typeof(ResourceVN); // Loại tài nguyên mặc định
            ErrorMessageResourceName = errorMessage; // Tên thông báo lỗi
        }
    }
}
