using MISA.WEB07.MF1755.KVHAI.Domain;
using MISA.WEB07.MF1755.KVHAI.Domain.Resource;
using MISA.WEB07.MF1755.KVHAI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    public class UserService : BaseService<User, UserDto, UserCreateDto, UserUpdateDto, UserSearchPaging, UserFilterResultDto, UserFilterResult>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDateFormatRepository _dateFormatRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IDateFormatRepository dateFormatRepository, IMapper mapper) : base(userRepository)
        {
            _userRepository = userRepository;
            _dateFormatRepository = dateFormatRepository;
            _mapper = mapper;
        }
        public override User MapCreateDtoToEntity(UserCreateDto createDto)
        {
            throw new NotImplementedException();
        }

        public override UserDto MapEntityToDto(User user)
        {
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public override UserFilterResultDto MapFilterResultDtoToFilterResult(UserFilterResult entityFilterResult)
        {
            throw new NotImplementedException();
        }

        public override User MapUpdateDtoToEntity(UserUpdateDto userUpdateDto, User user)
        {
            userUpdateDto.UserId = user.UserId;
            var newUser = _mapper.Map(userUpdateDto, user);

            return newUser;
        }

        public override async Task ValidateUpdateBusiness(User user)
        {

            //Check DateFormat
            var isCheckDateFormat = await _dateFormatRepository.FindAsync((Guid)user.DateFormatId);
            if (isCheckDateFormat == null)
            {
                throw new BadRequestException(ResourceVN.Error_DateFormatInvalid, ErrorCode.Invalid);
            }
        }
    }
}
