using Dapper;
using MISA.WEB07.MF1755.KVHAI.Application;
using MISA.WEB07.MF1755.KVHAI.Domain;
using MISA.WEB07.MF1755.KVHAI.Domain.Resource;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Infrastructure
{
    public class DateFormatRepository : BaseRepository<DateFormat, DateFormatSearchPaging, DateFormatFilterResult, DateFormatFilter, DateFormatSort>, IDateFormatRepository
    {
        public DateFormatRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public override string CreateSqlFilters(DateFormatSearchPaging dateFormatSearchPaging, string filterSql, string sortSql, bool? isNoPaging)
        {
            throw new NotImplementedException();
        }

        public override DateFormatFilterResult GetDataFilters(int totalRecords, List<DateFormat> data)
        {
            throw new NotImplementedException();
        }
    }
}
