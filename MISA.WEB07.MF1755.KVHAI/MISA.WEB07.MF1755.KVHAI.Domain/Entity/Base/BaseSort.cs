using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public class BaseSort : ISort
    {
        /// <summary>
        /// key trường sắp xếp
        /// </summary>
        public string? KeySort { get; set; }

        /// <summary>
        /// kiểu sắp xếp
        /// </summary>
        public string? TypeSort { get; set; }

        public string? GetKeySort() => KeySort;

        public string? GetTypeSort() => TypeSort;

    }
}
