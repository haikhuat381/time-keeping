using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public class User : BaseEntity, IEntity
    {
        /// <summary>
        /// Định danh người dùng
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Tên người dùng
        /// </summary>
        public String? UserName { get; set; }

        /// <summary>
        /// Định danh kiểu format
        /// </summary>
        public Guid? DateFormatId { get; set; }

        /// <summary>
        /// Lấy ra UserId
        /// </summary>
        /// <returns></returns>
        public Guid GetId()
        {
            return UserId;
        }

        /// <summary>
        /// set giá trị cho UserId
        /// </summary>
        /// <returns></returns>
        public void SetId(Guid id)
        {
            UserId = id;
        }
    }
}
