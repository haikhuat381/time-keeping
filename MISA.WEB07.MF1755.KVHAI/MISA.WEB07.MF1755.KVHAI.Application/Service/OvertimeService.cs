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
    public class OvertimeService : BaseService<Overtime, OvertimeDto, OvertimeCreateDto, OvertimeUpdateDto, OvertimeSearchPaging, OvertimeFilterResultDto, OvertimeFilterResult>, IOvertimeService
    {
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly IOvertimeValidate _overtimeValidate;
        private readonly IMapper _mapper;

        public OvertimeService(IOvertimeRepository overtimeRepository, IOvertimeValidate overtimeValidate, IMapper mapper) : base(overtimeRepository)
        {
            _overtimeRepository = overtimeRepository;
            _overtimeValidate = overtimeValidate;
            _mapper = mapper;
        }

        /// <summary>
        /// Phê duyêt: duyệt hoặc từ chối
        /// </summary>
        /// <param name="status">Trạng thái update</param>
        /// <param name="ids">Danh sách id các đơn update</param>
        /// <returns>số đơn update thành công</returns>
        /// CreatedBy: KVHAI (18/09/2023)
        public async Task<int> ApproveRequestAsync(int status, List<Guid> ids)
        {
            var entities = await _overtimeRepository.GetByListIdAsync(ids);
            if (entities != null && entities.Any())
            {
                var result = await _overtimeRepository.ApproveRequestAsync(status, entities);
                return result;
            }
            return 0;
        }


        /// <summary>
        /// Chuyển OvertimeCreateDto sang Overtime
        /// </summary>
        /// <param name="overtimeCreateDto"></param>
        /// <returns>Overtime</returns>
        /// CreatedBy:  KVHAI(20/10/2023)
        public override Overtime MapCreateDtoToEntity(OvertimeCreateDto overtimeCreateDto)
        {
            var overtime = _mapper.Map<Overtime>(overtimeCreateDto);
            overtime.OvertimeId = Guid.NewGuid();

            return overtime;
        }

        /// <summary>
        /// Validate nghiệp vụ
        /// </summary>
        /// <param name="overtime"></param>
        /// CreatedBy:  KVHAI(20/10/2023)
        public override async Task ValidateCreateBusiness(Overtime overtime)
        {
            // Check date + mảng danh sách nhân viên làm thêm
            _overtimeValidate.CheckValidateBusiness(overtime);
            _overtimeValidate.CheckOvertimeEmployees(overtime);
        }

        /// <summary>
        /// Validate nghiệp vụ
        /// </summary>
        /// <param name="overtime"></param>
        /// CreatedBy:  KVHAI(20/10/2023)
        public override async Task ValidateUpdateBusiness(Overtime overtime)
        {
            // Check date + mảng danh sách nhân viên làm thêm
            _overtimeValidate.CheckValidateBusiness(overtime);
            _overtimeValidate.CheckOvertimeEmployees(overtime);
        }

        /// <summary>
        /// Chuyển Overtime sang OvertimeDto
        /// </summary>
        /// <param name="overtime">Nhân viên</param>
        /// <returns></returns>
        public override OvertimeDto MapEntityToDto(Overtime overtime)
        {
            var overtimeDto = _mapper.Map<OvertimeDto>(overtime);

            return overtimeDto;
        }

        /// <summary>
        /// Chuyển OvertimeFilterResult sang OvertimeFilterResultDto
        /// </summary>
        /// <param name="overtimeFilterResult"></param>
        /// <returns>OvertimeFilterResultDto</returns>
        public override OvertimeFilterResultDto MapFilterResultDtoToFilterResult(OvertimeFilterResult overtimeFilterResult)
        {
            var overtimeDtos = overtimeFilterResult.Data.Select( overtime => MapEntityToDto(overtime)).ToList();

            var result = new OvertimeFilterResultDto()
            {
                TotalRecords = overtimeFilterResult.TotalRecords,
                Data = overtimeDtos.ToList()
            };
            return result;
        }


        /// <summary>
        /// Chuyển OvertimeUpdateDto sang Overtime
        /// </summary>
        /// <param name="overtimeUpdateDto"></param>
        /// <param name="overtime"></param>
        /// <returns></returns>
        public override Overtime MapUpdateDtoToEntity(OvertimeUpdateDto overtimeUpdateDto, Overtime overtime)
        {
            overtimeUpdateDto.OvertimeId = overtime.OvertimeId;
            var newOvertime = _mapper.Map(overtimeUpdateDto, overtime);

            return newOvertime;
        }


        /// <summary>
        /// Xuất Excel
        /// </summary>
        /// <param name="overtimeSearchPaging">Điều kiện lọc, tìm kiếm</param>
        /// <returns></returns>
        public async Task<OvertimeExportExcel> OvertimeExportToExcelAsync(OvertimeSearchPaging overtimeSearchPaging)
        {
            var overtimeFilterResult = await _overtimeRepository.GetFiltersAsync(overtimeSearchPaging, true);

            var data = overtimeFilterResult.Data.Select( (overtime) => MapOvertimeToOvertimeExportExcel(overtime)).ToList();

            List<OvertimeExportExcelDto> processedList = new List<OvertimeExportExcelDto>();
            List<string> processedIds = new List<string>();

            var orderNumber = 1;
            for (int i = 0; i < data.Count; i++)
            {
                var item = data[i];
                if(processedIds.Count == 0)
                {
                    processedIds.Add(item.OvertimeId);
                    //item.OvertimeId = (i + 1).ToString();
                    item.OvertimeId = orderNumber.ToString();
                    processedList.Add(item);
                    orderNumber ++;
                } else
                {
                    var check = processedIds.Contains( item.OvertimeId);
                    if(check)
                    {
                        item.OvertimeId = null;
                        //item.OvertimeId = (i + 1).ToString();
                        item.FullName = null;
                        item.EmployeeCode = null;
                        item.DepartmentName = null;
                        item.CareerTitles = null;
                        item.ApplyDate = null;
                        item.OverTimeInWorkingShiftName = null;
                        item.FromDate = null;
                        item.ToDate = null;
                        item.WorkingShiftName = null;
                        item.Reason = null;
                        item.ApprovalName = null;
                        item.RelationShipNames = null;
                        item.Note = null;
                        item.Status = null;
                        item.OvertimeEmployeeCode = item.OvertimeEmployeeCode;
                        item.OvertimeEmployeeFullName = item.OvertimeEmployeeFullName;
                        processedList.Add(item);
                    }
                    else
                    {
                        processedIds.Add(item.OvertimeId);
                        //item.OvertimeId = (i + 1).ToString();
                        //processedList.Add(item);
                        item.OvertimeId = orderNumber.ToString();
                        processedList.Add(item);
                        orderNumber++;
                    }

                }
            }
            var result = await _overtimeRepository.OvertimeExportToExcelAsync(processedList);
            return result;
        }

        /// <summary>
        /// Chuyển đổi Overtime sang OvertimeExportExcelDto
        /// </summary>
        /// <param name="overtime">Nhân viên</param>
        /// <param name="index">indexx</param>
        /// <returns><OvertimeExportExcelDto/returns>
        public OvertimeExportExcelDto MapOvertimeToOvertimeExportExcel(Overtime overtime)
        {
            var statusFormat = "";
            if (overtime.Status == Status.Approved)
            {
                statusFormat = ResourceVN.Status_Approved;
            }
            else if (overtime.Status == Status.Pending)
            {
                statusFormat = ResourceVN.Status_Pending;
            }
            else if (overtime.Status == Status.Refuse)
            {
                statusFormat = ResourceVN.Status_Refuse;
            }
            else
            {
                statusFormat = "";
            }

            var overtimeExportExcel = _mapper.Map<OvertimeExportExcelDto>(overtime);
            overtimeExportExcel.ApplyDate = overtime.ApplyDate.Value.ToString("dd/MM/yyyy HH:mm:ss");
            overtimeExportExcel.FromDate = overtime.FromDate.Value.ToString("dd/MM/yyyy HH:mm:ss");
            overtimeExportExcel.ToDate = overtime.ToDate.Value.ToString("dd/MM/yyyy HH:mm:ss");
            overtimeExportExcel.Status = statusFormat;

            return overtimeExportExcel;
        }
    }
}
