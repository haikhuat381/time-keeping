using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    public class BaseFilterResultDto<TEntityDto>
    {
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Danh sách dữ liệu
        /// </summary>
        public List<TEntityDto> Data { get; set; }

    }
}
