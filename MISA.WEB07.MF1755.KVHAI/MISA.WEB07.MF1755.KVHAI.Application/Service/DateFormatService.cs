using AutoMapper;
using MISA.WEB07.MF1755.KVHAI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    public class DateFormatService : BaseReadOnlyService<DateFormat, DateFormatDto, DateFormatSearchPaging, DateFormatFilterResultDto, DateFormatFilterResult>, IDateFormatService
    {
        private readonly IMapper _mapper;
        public DateFormatService(IDateFormatRepository dateFormatRepository, IMapper mapper) : base(dateFormatRepository)
        {
            _mapper = mapper;
        }
        public override DateFormatDto MapEntityToDto(DateFormat dateFormat)
        {
            //var dateFormatDto = new DateFormatDto()
            //{
            //    DateFormatId = dateFormat.DateFormatId,
            //    Name = dateFormat.Name,
            //    Format = dateFormat.Format,
            //    ModifiedBy = dateFormat.ModifiedBy,
            //    ModifiedDate = dateFormat.ModifiedDate,
            //    CreatedBy = dateFormat.CreatedBy,
            //    CreatedDate = dateFormat.CreatedDate,
            //};

            //return dateFormatDto;
            var dateFormatDto = _mapper.Map<DateFormatDto>(dateFormat);

            return dateFormatDto;
        }

        public override DateFormatFilterResultDto MapFilterResultDtoToFilterResult(DateFormatFilterResult entityFilterResult)
        {
            throw new NotImplementedException();
        }
    }
}
