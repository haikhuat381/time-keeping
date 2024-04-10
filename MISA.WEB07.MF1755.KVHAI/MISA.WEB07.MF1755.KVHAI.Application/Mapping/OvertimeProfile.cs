using AutoMapper;
using MISA.WEB07.MF1755.KVHAI.Domain;
using MISA.WEB07.MF1755.KVHAI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Application
{
    public class OvertimeProfile : Profile
    {
        /// <summary>
        /// mapper of overtime
        /// </summary>
        public OvertimeProfile()
        {
            CreateMap<Overtime, OvertimeDto>();
            CreateMap<OvertimeCreateDto, Overtime>();
            CreateMap<OvertimeUpdateDto, Overtime>();
            CreateMap<Overtime, OvertimeExportExcelDto>();
        }
    }
}
