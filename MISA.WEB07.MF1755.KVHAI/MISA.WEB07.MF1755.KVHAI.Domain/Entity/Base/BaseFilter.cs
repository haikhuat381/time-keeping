using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public class BaseFilter : IFilter
    {

        /// <summary>
        /// key trường lọc
        /// </summary>
        public string KeyCodition { get; set; }

        /// <summary>
        /// từ khóa lọc
        /// </summary>
        public string? KeywordCodition { get; set; }

        /// <summary>
        /// tên trường lọc
        /// </summary>
        public string? NameCodition { get; set; }

        /// <summary>
        /// tên loại lọc
        /// </summary>
        public string? TextTypeCodition { get; set; }

        /// <summary>
        /// kiểu lọc
        /// </summary>
        public Filter TypeCodition { get; set; }

        public string GetKeyCodition() => KeyCodition;

        public string? GetKeywordCodition() => KeywordCodition;

        public Filter GetTypeCodition() => TypeCodition;
    }
}
