using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public interface IEntity
    {
        /// <summary>
        /// Lấy id
        /// </summary>
        /// <returns>id</returns>
        public Guid GetId();

        /// <summary>
        /// set id
        /// </summary>
        /// <param name="id"></param>
        public void SetId(Guid id);
    }
}
