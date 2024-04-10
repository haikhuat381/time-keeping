using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public enum Status
    {
        /// <summary>
        /// Đã duyệt
        /// </summary>
        Approved = 1,

        /// <summary>
        /// Chờ duyệt
        /// </summary>
        Pending = 2,

        /// <summary>
        /// Từ chối
        /// </summary>
        Refuse = 3,
    }
}
