using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public class DateFormat : BaseEntity, IEntity
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

        /// <summary>
        /// Lấy ra DateFormatId
        /// </summary>
        /// <returns></returns>
        public Guid GetId()
        {
            return DateFormatId;
        }

        /// <summary>
        /// set DateFormatId
        /// </summary>
        /// <param name="id"></param>
        public void SetId(Guid id)
        {
            DateFormatId = id;
        }
    }
}
