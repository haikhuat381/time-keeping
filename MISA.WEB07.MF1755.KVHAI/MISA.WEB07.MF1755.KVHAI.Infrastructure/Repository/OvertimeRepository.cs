using Dapper;
using MISA.WEB07.MF1755.KVHAI.Domain;
using MISA.WEB07.MF1755.KVHAI.Domain.Resource;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using MISA.WEB07.MF1755.KVHAI.Model;
using MISA.WEB07.MF1755.KVHAI.Application;
using AutoMapper;
using OfficeOpenXml.Sorting;
using static Dapper.SqlMapper;
using System.Resources;
using static System.Net.Mime.MediaTypeNames;


namespace MISA.WEB07.MF1755.KVHAI.Infrastructure
{
    public class OvertimeRepository : BaseRepository<Overtime, OvertimeSearchPaging, OvertimeFilterResult, OvertimeFilter, OvertimeSort>, IOvertimeRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        public OvertimeRepository(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Phê duyêt: duyệt hoặc từ chối
        /// </summary>
        /// <param name="status">Trạng thái update</param>
        /// <param name="overtimes">Danh sách các đơn update</param>
        /// <returns>số đơn update thành công</returns>
        /// CreatedBy: KVHAI (18/09/2023)
        public async Task<int> ApproveRequestAsync(int status, List<Overtime> overtimes)
        {
            var ids = overtimes.Select(entity => entity.GetId()).ToList();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Status", status);
            parameters.Add("OvertimeIds", ids);

            var sql = "UPDATE overtime SET Status = @Status WHERE OvertimeId IN @OvertimeIds";

            var result = await _unitOfWork.Connection.ExecuteAsync(sql, parameters, transaction: UnitOfWork.Transaction);

            return result;
        }


        /// <summary>
        /// Lấy câu truy vấn
        /// </summary>
        /// <param name="overtimeSearchPaging">overtimeSearchPaging</param>
        /// <param name="filterSql">lọc</param>
        /// <param name="sortSql">sắp xếp</param>
        /// <param name="isNoPaging"> có phân trang hay ko</param>
        /// <returns>string sql</returns>
        public override string CreateSqlFilters(OvertimeSearchPaging overtimeSearchPaging, string filterSql, string sortSql, bool? isNoPaging)
        {
            var sql = "";
            if (isNoPaging == true)
            {
                sql += @"
                        SELECT SQL_CALC_FOUND_ROWS o.*, e.EmployeeCode AS OvertimeEmployeeCode, e.FullName AS OvertimeEmployeeFullName
                          FROM overtime o 
                          JOIN overtimedetail od ON o.OvertimeId = od.OvertimeId 
                          JOIN employee e ON od.EmployeeId = e.EmployeeId
                        WHERE (@Keyword IS NULL 
                               OR o.EmployeeCode LIKE @Keyword 
                               OR o.FullName LIKE @Keyword)
                               AND (@Status IS NULL OR Status = @Status)
                               "
                        + filterSql
                        + " ORDER BY o.ModifiedDate DESC, o.OvertimeId;"
                        + "SELECT FOUND_ROWS() AS TotalRecords;";

            }
            else
            {
                sql += @"
                            SELECT SQL_CALC_FOUND_ROWS *
                            FROM overtime o
                            WHERE (@Keyword IS NULL 
                                   OR EmployeeCode LIKE @Keyword 
                                   OR FullName LIKE @Keyword)
                                   AND (@Status IS NULL OR Status = @Status)
                                   " + filterSql;

                if (!string.IsNullOrWhiteSpace(sortSql))
                {
                    sql += sortSql;
                }
                else
                {
                    sql += @" ORDER BY ModifiedDate DESC";
                }

                sql += " LIMIT @PageSize OFFSET @Offset ; " +
                       "SELECT FOUND_ROWS() AS TotalRecords;";

            }
            return sql;
        }

        /// <summary>
        /// Lấy data đã lọc
        /// </summary>
        /// <param name="totalRecords">Tổng</param>
        /// <param name="data"> mảng giá trị</param>
        /// CreatedBy:  KVHAI(20/09/2023)
        public override OvertimeFilterResult GetDataFilters(int totalRecords, List<Overtime> data)
        {
            var result = new OvertimeFilterResult()
            {
                TotalRecords = totalRecords,
                Data = data
            };
            return result;
        }

        public override async Task<Overtime> GetAsync(Guid id)
        {
            var entity = await FindAsync(id);
            if (entity == null)
            {
                var nameMessage = $"Error_{TableName}NotFound";

                var resourceManager = new ResourceManager(typeof(ResourceVN));
                var message = resourceManager.GetString(nameMessage);
                throw new NotFoundException(message, ErrorCode.NotFound);
            }
            else
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("OvertimeId", id);

                string combinedSql = @"
                       SELECT e.*, d.DepartmentName, d.DepartmentCode
                       FROM employee e
                       JOIN overtimedetail od ON e.EmployeeId = od.EmployeeId
                       JOIN department d ON e.DepartmentId = d.DepartmentId
                       WHERE od.OvertimeId = @OvertimeId";

                var combinedResult = await UnitOfWork.Connection.QueryAsync<Employee>(combinedSql, parameters, transaction: UnitOfWork.Transaction);
                var employees = combinedResult.ToList();
                entity.OvertimeEmployees = employees;
                return entity;

            }
        }

        /// <summary>
        /// tạo một
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>1- thành công, 0 - thất bại</returns>
        /// CreatedBy:  KVHAI(20/09/2023)

        public override async Task<int> InSertAsync(Overtime overtime)
        {
            var sql = $"Proc_{TableName}_Insert";

            DynamicParameters parameters = new DynamicParameters();

            foreach (var property in overtime.GetType().GetProperties())
            {
                if (property.Name != "OvertimeEmployees")
                {
                    var value = property.GetValue(overtime);
                    parameters.Add($"p_{property.Name}", value);
                }
            }


            try
            {
                await UnitOfWork.BeginTransactionAsync();
                var result = await UnitOfWork.Connection.ExecuteAsync(sql, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: UnitOfWork.Transaction);

                if (overtime.OvertimeEmployees != null && overtime.OvertimeEmployees.Count > 0)
                {
                    await InsertOvertimeDetail(overtime);
                }
                await UnitOfWork.CommitTransactionAsync();

                return result;

            }
            catch (Exception ex)
            {
                await UnitOfWork.RollBackTransactionAsync();
                throw new Exception(ResourceVN.Error_Exception);
            }

        }

        /// <summary>
        /// Sửa một
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">thông tin sửa</param>
        /// <returns>1- thành công, 0 - thất bại</returns>
        /// CreatedBy:  KVHAI(20/09/2023)
        public override async Task<int> UpdateAsync(Guid id, Overtime overtime)
        {
            var sql = $"Proc_{TableName}_Update";

            DynamicParameters parameters = new DynamicParameters();
            foreach (var property in overtime.GetType().GetProperties())
            {
                if (property.Name != "OvertimeEmployees")
                {
                    var value = property.GetValue(overtime);
                    parameters.Add($"p_{property.Name}", value);
                }
            }

            try
            {
                await UnitOfWork.BeginTransactionAsync();
                var result = await UnitOfWork.Connection.ExecuteAsync(sql, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: UnitOfWork.Transaction);

                if (overtime.OvertimeEmployees != null && overtime.OvertimeEmployees.Count > 0)
                {
                    string deleteSql = "DELETE FROM overtimedetail WHERE OvertimeId = @OvertimeId";
                    var deleteParameters = new DynamicParameters();
                    deleteParameters.Add("OvertimeId", overtime.OvertimeId);
                    await UnitOfWork.Connection.ExecuteAsync(deleteSql, deleteParameters, transaction: UnitOfWork.Transaction);

                    await InsertOvertimeDetail(overtime);
                }

                await UnitOfWork.CommitTransactionAsync();

                return result;

            }
            catch (Exception ex)
            {
                await UnitOfWork.RollBackTransactionAsync();
                throw new Exception(ResourceVN.Error_Exception);
            }
        }

        /// <summary>
        /// Thêm danh sách nhân viên đăng ký làm thêm
        /// </summary>
        /// <param name="overtime"></param>
        /// <returns>số bản ghi thành công</returns>
        /// CreatedBy:  KVHAI(20/09/2023)
        public async Task<int> InsertOvertimeDetail(Overtime overtime)
        {
            var employeeIds = overtime.OvertimeEmployees.Select(entity => entity.GetId()).ToList();

            var employeeParameters = new List<DynamicParameters>();

            foreach (var employeeId in employeeIds)
            {
                var employeeParameter = new DynamicParameters();
                employeeParameter.Add("p_OvertimeDetailId", Guid.NewGuid());
                employeeParameter.Add("p_OvertimeId", overtime.OvertimeId);
                employeeParameter.Add("p_EmployeeId", employeeId);
                employeeParameter.Add("p_CreatedDate", DateTime.Now);
                employeeParameter.Add("p_CreatedBy", "KVH");
                employeeParameter.Add("p_ModifiedDate", DateTime.Now);
                employeeParameter.Add("p_ModifiedBy", "KVH");
                employeeParameters.Add(employeeParameter);
            }

            string sql1 = "Proc_OvertimeDetail_Insert";
            var result1 = await UnitOfWork.Connection.ExecuteAsync(sql1, employeeParameters, commandType: System.Data.CommandType.StoredProcedure, transaction: UnitOfWork.Transaction);
            return result1;
        }

        /// <summary>
        /// Xuất excel
        /// </summary>
        /// <param name="overtimeExportExcelDto">Điều kiện lọc, tìm kiếm</param>
        /// <returns>File Excel Danh sách đơn đăng ký làm thêm </returns>
        public async Task<OvertimeExportExcel> OvertimeExportToExcelAsync(List<OvertimeExportExcelDto> overtimeExportExcelDto)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(ResourceVN.Overtimes);

                // Độ rộng cột
                var columnWidths = new double[] { 5, 25, 15, 20, 20, 25, 20, 25, 25, 20, 20, 25, 30, 25, 20, 25, 25 };
                for (int i = 0; i < columnWidths.Length; i++)
                {
                    worksheet.Column(i + 1).Width = columnWidths[i];
                }

                // Áp dụng kiểu dáng chung cho các ô dữ liệu
                var dataRange = worksheet.Cells["A4:Q4"].LoadFromCollection(overtimeExportExcelDto, true);
                ApplyCommonCellStyle(dataRange);

                var dataRange1 = worksheet.Cells["A3:Q3"];
                ApplyCommonCellStyle(dataRange1);

                // Áp dụng kiểu dáng cho các ô cột và dòng tiêu đề
                var columnHeaderStyle = worksheet.Cells["B4:Q4"].Style;
                columnHeaderStyle.Font.Bold = true;
                columnHeaderStyle.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                columnHeaderStyle.VerticalAlignment = ExcelVerticalAlignment.Center;
                columnHeaderStyle.Fill.PatternType = ExcelFillStyle.Solid;
                columnHeaderStyle.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

                // Merge các ô
                MergeCellRange(worksheet, "A3:A4", ResourceVN.NumericalOrder);
                MergeCellRange(worksheet, "B3:O3", ResourceVN.Information);
                MergeCellRange(worksheet, "P3:Q3", ResourceVN.OvertimeEmployees);

                // Merge dòng tiêu đề
                var headerRow = worksheet.Cells["A1:Q1"];
                headerRow.Merge = true;
                headerRow.Value = ResourceVN.Overtimes;
                headerRow.Style.Font.Size = 16;
                headerRow.Style.Font.Bold = true;
                headerRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                headerRow.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                // Căn giữa dữ liệu, số thứ tự
                AlignCellsCenter(worksheet, "A4:A" + (overtimeExportExcelDto.Count + 4));
                AlignCellsCenter(worksheet, "F4:F" + (overtimeExportExcelDto.Count + 4));
                AlignCellsCenter(worksheet, "H4:H" + (overtimeExportExcelDto.Count + 4));
                AlignCellsCenter(worksheet, "I4:I" + (overtimeExportExcelDto.Count + 4));

                // Chuyển đổi tệp Excel thành mảng byte và trả về nó dưới dạng FileResult
                var fileBytes = package.GetAsByteArray();
                var result = new OvertimeExportExcel()
                {
                    File = fileBytes,
                    ExcelMimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    FileName = "overtimes.xlsx"
                };
                return result;
            }
        }

        /// <summary>
        /// Áp dụng kiểu dáng chung cho ô trong Excel.
        /// </summary>
        /// <param name="cellRange">Phạm vi ô cần áp dụng kiểu dáng.</param>
        void ApplyCommonCellStyle(ExcelRangeBase cellRange)
        {
            cellRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            cellRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            cellRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            cellRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        }

        /// <summary>
        /// Hợp nhất một phạm vi ô trong Excel và áp dụng kiểu dáng cho ô đã hợp nhất.
        /// </summary>
        /// <param name="worksheet">Bảng tính Excel.</param>
        /// <param name="cellAddress">Địa chỉ của ô cần hợp nhất.</param>
        /// <param name="cellValue">Giá trị của ô.</param>
        void MergeCellRange(ExcelWorksheet worksheet, string cellAddress, string cellValue)
        {
            var cellRange = worksheet.Cells[cellAddress];
            cellRange.Merge = true;
            cellRange.Value = cellValue;
            cellRange.Style.Font.Bold = true;
            cellRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cellRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            cellRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cellRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
        }

        /// <summary>
        /// Căn giữa các ô trong một phạm vi trên bảng tính Excel.
        /// </summary>
        /// <param name="worksheet">Bảng tính Excel.</param>
        /// <param name="cellRangeAddress">Địa chỉ phạm vi ô cần căn giữa.</param>
        void AlignCellsCenter(ExcelWorksheet worksheet, string cellRangeAddress)
        {
            worksheet.Cells[cellRangeAddress].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

    }
}
