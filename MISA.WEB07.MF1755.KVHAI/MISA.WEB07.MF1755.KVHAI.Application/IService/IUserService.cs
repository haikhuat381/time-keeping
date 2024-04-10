using MISA.WEB07.MF1755.KVHAI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    /// <summary>
    /// Interface tương tác với Service của User
    /// </summary>
    public interface IUserService : IBaseService<UserDto, UserCreateDto, UserUpdateDto, UserSearchPaging, UserFilterResultDto>
    {
    }
}
