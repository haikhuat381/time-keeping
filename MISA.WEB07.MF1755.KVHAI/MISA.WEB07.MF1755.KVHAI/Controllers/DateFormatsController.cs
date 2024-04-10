using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB07.MF1755.KVHAI;
using MySqlConnector;
using Dapper;
using MISA.WEB07.MF1755.KVHAI.Application;
using MISA.WEB07.MF1755.KVHAI.Domain;

namespace MISA.WEB07.MF1755.KVHAI.Controllers
{

    public class DateFormatsController : BaseReadOnlyController<DateFormatDto, DateFormatSearchPaging, DateFormatFilterResultDto>
    {
        private readonly IDateFormatService _dateFormatService;

        public DateFormatsController(IDateFormatService dateFormatService) : base(dateFormatService)

        {
            _dateFormatService = dateFormatService;
        }
    }
}
