using Dapper;
//using MISA.WEB07.MF1755.KVHAI.Application;
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

namespace MISA.WEB07.MF1755.KVHAI.Infrastructure
{
    public class EmployeeRepository : BaseRepository<Employee, EmployeeSearchPaging, EmployeeFilterResult, EmployeeFilter, EmployeeSort>, IEmployeeRepository
    {
        private readonly IMapper _mapper;
        public EmployeeRepository(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Lấy câu truy vấn
        /// </summary>
        /// <param name="filterSql"></param>
        /// <param name="isNoPaging"></param>
        /// <returns></returns>
        public override string CreateSqlFilters(EmployeeSearchPaging employeeSearchPaging, string filterSql, string sortSql, bool? isNoPaging)
        {
            string sql = @"
                        SELECT SQL_CALC_FOUND_ROWS e.*, d.DepartmentName, d.DepartmentCode
                        FROM employee e
                        LEFT JOIN department d ON e.DepartmentId = d.DepartmentId
                        WHERE (@Keyword IS NULL 
                               OR e.EmployeeCode LIKE @Keyword 
                               OR e.FullName LIKE @Keyword)
                               " + filterSql;

            //       " + filterSql + @"
            //ORDER BY e.CreatedDate DESC";

            if (!string.IsNullOrWhiteSpace(sortSql))
            {
                sql += sortSql;
            } 
            else
            {
                sql += @" ORDER BY e.CreatedDate DESC";
            }

            // mặc định nếu không có điều kiện sắp xếp nào được cung cấp
            //if (string.IsNullOrWhiteSpace(sortSql))
            //{
            //    sql += @" ORDER BY e.CreatedDate DESC";
            //}

            if (isNoPaging == true)
            {
                sql = sql + " ; SELECT FOUND_ROWS() AS TotalRecords;";
            }
            else
            {
                sql = sql + " LIMIT @PageSize OFFSET @Offset ; " +
                    "SELECT FOUND_ROWS() AS TotalRecords;";
            }
            return sql;
        }

        /// <summary>
        /// Lấy dữ liệu
        /// </summary>
        /// <param name="totalRecords"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override EmployeeFilterResult GetDataFilters(int totalRecords, List<Employee> data)
        {
            var result = new EmployeeFilterResult()
            {
                TotalRecords = totalRecords,
                Data = data
            };
            return result;
        }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        public async Task<string> GetNewEmployeeCodeAsync()
        {
            var sql = "Proc_Employee_GetMaxCode";
            var maxEmployeeCode = await UnitOfWork.Connection.ExecuteScalarAsync<int?>(sql, commandType: System.Data.CommandType.StoredProcedure, transaction: UnitOfWork.Transaction);

            int newEmployeeCode = maxEmployeeCode.GetValueOrDefault(0) + 1;
            var result = "NV-" + newEmployeeCode;
            return result;
        }


        /// <summary>
        /// Check đã tồn tại Mã nhân viên hay chưa
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>true - có, false - chưa có</returns>
        public async Task<bool> IsExistEmployeeAsync(Guid? employeeId, string employeeCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("p_EmployeeCode", employeeCode);

            string sql;
            if (employeeId.HasValue)
            {
                // Kiểm tra trừ nhân viên có "employeeId" tương ứng
                sql = "Proc_Employee_CheckCodeExceptMe";
                parameters.Add("p_EmployeeId", employeeId);
            }
            else
            {
                // Kiểm tra tất cả nhân viên
                sql = "Proc_Employee_CheckCodeAll";
            }

            var res = UnitOfWork.Connection.QueryFirstOrDefault<string>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: UnitOfWork.Transaction);

            var result = res != null;

            return result;
        }

        /// <summary>
        /// Xuất excel
        /// </summary>
        /// <param name="employeeSearchPaging">Điều kiện lọc, tìm kiếm</param>
        /// <returns>File Excel Danh sách nhân viên</returns>

        public async Task<EmployeeExportExcel> EmployeeExportToExcelAsync(List<EmployeeExportExcelDto> employeeExportExcelDto)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(ResourceVN.Employees);
                // Thiết lập độ rộng cột
                var columnWidths = new double[] { 5, 20, 35, 20, 20, 25, 20, 20, 20, 25, 20, 20, 20, 35, 20, 20, 20, 70 };
                for (int i = 0; i < columnWidths.Length; i++)
                {
                    worksheet.Column(i + 1).Width = columnWidths[i];
                }
                // Thiết lập định dạng và viền cho dữ liệu
                var dataRange = worksheet.Cells["A3:R3"].LoadFromCollection(employeeExportExcelDto, true);
                dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;


                // Định dạng cho tên cột và dòng tiêu đề
                var columnHeaderStyle = worksheet.Cells["A3:R3"].Style;
                columnHeaderStyle.VerticalAlignment = ExcelVerticalAlignment.Center;
                columnHeaderStyle.Fill.PatternType = ExcelFillStyle.Solid;
                columnHeaderStyle.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                // Merge dòng tiêu đề
                var headerRow = worksheet.Cells["A1:R1"];
                headerRow.Merge = true;
                headerRow.Value = ResourceVN.Employees;
                headerRow.Style.Font.Size = 16;
                headerRow.Style.Font.Bold = true;
                headerRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                headerRow.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                // căn giữa date, stt
                worksheet.Cells["A3:A" + (employeeExportExcelDto.Count + 3)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["D3:D" + (employeeExportExcelDto.Count + 3)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["G3:G" + (employeeExportExcelDto.Count + 3)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Chuyển đổi tệp Excel thành mảng byte và trả về nó dưới dạng FileResult
                var fileBytes = package.GetAsByteArray();
                var result = new EmployeeExportExcel()
                {
                    File = fileBytes,
                    ExcelMimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    FileName = "employees.xlsx"
                };
                return result;
            }
        }
    }
}
