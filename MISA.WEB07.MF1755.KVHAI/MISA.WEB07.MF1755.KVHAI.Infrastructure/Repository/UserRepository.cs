using MISA.WEB07.MF1755.KVHAI.Application;
using MISA.WEB07.MF1755.KVHAI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Infrastructure
{
    public class UserRepository : BaseRepository<User, UserSearchPaging, UserFilterResult, UserFilter, UserSort>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override string CreateSqlFilters(UserSearchPaging userSearchPaging, string filterSql, string sortSql, bool? isNoPaging)
        {
            throw new NotImplementedException();
        }

        public override UserFilterResult GetDataFilters(int totalRecords, List<User> data)
        {
            throw new NotImplementedException();
        }
    }
}
