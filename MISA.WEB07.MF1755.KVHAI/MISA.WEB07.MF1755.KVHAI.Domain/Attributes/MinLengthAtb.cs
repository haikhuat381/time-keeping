using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WEB07.MF1755.KVHAI.Domain.Resource;
using System.Resources;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public class MinLengthAtb : ValidationAttribute
    {
        public int MinLength { get; }

        public MinLengthAtb(int minLength)
        {
            MinLength = minLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is Array arrayValue)
            {
                var count = arrayValue.Length;
                if (count >= MinLength)
                {
                    return ValidationResult.Success;
                }
            }

            // Sử dụng ResourceManager để lấy thông báo từ tài nguyên
            var resourceManager = new ResourceManager(typeof(ResourceVN));
            var errorMessage = resourceManager.GetString("Error_MinArrayLength");

            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                errorMessage = "{0} có độ dài tối thiểu là {1}.";
            }

            // Format thông báo lỗi với tên trường và độ dài tối thiểu
            errorMessage = string.Format(errorMessage, validationContext.DisplayName, MinLength);

            return new ValidationResult(errorMessage);
        }
    }
}
