using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using MISA.WEB07.MF1755.KVHAI.Domain.Resource;

namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public class ErrorMessages
    {
        private static readonly ResourceManager _resourceManager = new ResourceManager(typeof(ResourceVN));
        public static string GetErrorMessages(string resourceName)
        {
            return _resourceManager.GetString(resourceName);
        }
    }
}
