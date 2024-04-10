using MISA.WEB07.MF1755.KVHAI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    /// <summary>
    /// Interface tương tác với Service của DateFormat
    /// </summary>
    public interface IDateFormatService : IBaseReadOnlyService<DateFormatDto, DateFormatSearchPaging, DateFormatFilterResultDto>
    {
    }
}
