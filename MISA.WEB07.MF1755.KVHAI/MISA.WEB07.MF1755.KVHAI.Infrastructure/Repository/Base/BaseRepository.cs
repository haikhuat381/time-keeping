using Dapper;
using MISA.WEB07.MF1755.KVHAI.Application;
using MISA.WEB07.MF1755.KVHAI.Domain;
using MISA.WEB07.MF1755.KVHAI.Domain.Resource;
using MySqlConnector;
using OfficeOpenXml.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WEB07.MF1755.KVHAI.Infrastructure
{
    public abstract class BaseRepository<TEntity, TEntitySearchPaging, TEntityFilterResult, TEntityFilter, TEntitySort> : IBaseRepository<TEntity, TEntitySearchPaging, TEntityFilterResult> where TEntity : IEntity where TEntityFilter : IFilter where TEntitySort : ISort where TEntitySearchPaging : ISearchPaging<TEntityFilter, TEntitySort>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual string TableName { get; set; } = typeof(TEntity).Name;

        /// <summary>
        /// Hàm lấy một theo id
        /// </summary>
        /// <param name="employeeId">ID</param>
        /// <returns>Có - trả về, Không có - trả về null</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        public async Task<TEntity?> FindAsync(Guid id)
        {
            var sql = $"Proc_{TableName}_GetById";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"p_{TableName}Id", id);

            var entity = await UnitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(sql, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: UnitOfWork.Transaction);

            return entity;
        }

        /// <summary>
        /// Lấy tất cả
        /// </summary>
        /// <returns>List<TEntity></returns>
        public async Task<List<TEntity>> GetAllAsync()
        {
            var sql = $"Proc_{TableName}_GetAll";

            var entities = await UnitOfWork.Connection.QueryAsync<TEntity>(sql, commandType: System.Data.CommandType.StoredProcedure, transaction: UnitOfWork.Transaction);

            return entities.ToList();
        }

        /// <summary>
        /// Hàm lấy một nhân viên theo id
        /// </summary>
        /// <param name="id">ID nhân viên</param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>Có - trả về nhân viên, Không có - trả về Exception</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            var entity = await FindAsync(id);
            if (entity == null)
            {
                var nameMessage = $"Error_{TableName}NotFound";

                var resourceManager = new ResourceManager(typeof(ResourceVN));
                var message = resourceManager.GetString(nameMessage);
                throw new NotFoundException(message, ErrorCode.NotFound);
            }
            return entity;
        }

        /// <summary>
        /// Lấy danh sách theo danh sách id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>List<TEntity></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<List<TEntity>> GetByListIdAsync(List<Guid> ids)
        {
            var sql = $"SELECT * from {TableName} Where {TableName}Id In @{TableName}Ids";

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add($"{TableName}Ids", ids);

            var result = await UnitOfWork.Connection.QueryAsync<TEntity>(sql, parameters, transaction: UnitOfWork.Transaction);

            var entities = result.ToList();
            if (entities.Count < ids.Count)
            {
                var nameMessage = $"Error_{TableName}NotFound";

                var resourceManager = new ResourceManager(typeof(ResourceVN));
                var message = resourceManager.GetString(nameMessage);
                throw new NotFoundException(message, ErrorCode.NotFound);
            }
            return entities;
        }

        /// <summary>
        /// Lấy danh sách theo phân trang, lọc và tìm kiếm
        /// </summary>
        /// <param name="entitySearchPaging">phân trang, lọc và tìm kiếm</param>
        /// <param name="isNoPaging">Có phân trang hay ko</param>
        /// <returns>Mảng danh sách</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        public async Task<TEntityFilterResult> GetFiltersAsync(TEntitySearchPaging entitySearchPaging, bool? isNoPaging)
        {
            int offset = (entitySearchPaging.GetPageIndex() - 1) * entitySearchPaging.GetPageSize();
            var key = string.IsNullOrEmpty(entitySearchPaging.GetKeyword()) ? null : $"%{entitySearchPaging.GetKeyword()}%";

            var parameters = new DynamicParameters();

            string filterSql = "";
            var filters = entitySearchPaging.GetFilters();

            if (filters != null && filters.Count > 0)
            {
                var filterConditions = new List<string>();

                var sqlGetColumns = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName";
                parameters.Add("TableName", TableName);

                var resultColumns = await UnitOfWork.Connection.QueryAsync<string>(sqlGetColumns, parameters, transaction: UnitOfWork.Transaction);
                var columns = resultColumns.ToArray();


                foreach (var filter in filters)
                {
                    if (filter != null)
                    {
                        if (!columns.Contains(filter.GetKeyCodition()))
                        {
                            var message = new ResourceManager(typeof(ResourceVN)).GetString("Error_FieldFilterInvalid");
                            throw new BadRequestException(message, ErrorCode.Invalid);
                        }

                        string fieldName = $"{char.ToLower(TableName[0])}.{filter.GetKeyCodition()}";
                        string filterText = filter.GetKeywordCodition();
                        string parameterName = $"@{filter.GetKeyCodition()}";

                        switch (filter.GetTypeCodition())
                        {
                            case Filter.Contains: // Chứa
                                filterConditions.Add($"{fieldName} LIKE {parameterName}");
                                filterText = $"%{filterText}%";
                                break;
                            case Filter.DoesNotContain: // Không chứa
                                filterConditions.Add($"{fieldName} NOT LIKE {parameterName}");
                                filterText = $"%{filterText}%";
                                break;
                            case Filter.StartsWith: // Bắt đầu bằng
                                filterConditions.Add($"{fieldName} LIKE {parameterName}");
                                filterText = $"{filterText}%";
                                break;
                            case Filter.EndsWith: // Kết thúc bằng
                                filterConditions.Add($"{fieldName} LIKE {parameterName}");
                                filterText = $"%{filterText}";
                                break;
                            case Filter.Equals: // Bằng
                                filterConditions.Add($"{fieldName} = {parameterName}");
                                break;
                            case Filter.NotEqual: // Khác
                                filterConditions.Add($"{fieldName} <> {parameterName}");
                                break;
                            case Filter.IsEmpty: // Trống
                                filterConditions.Add($"({fieldName} IS NULL OR {fieldName} = '')");
                                break;
                            case Filter.IsNotEmpty: // Không trống
                                filterConditions.Add($"({fieldName} IS NOT NULL AND {fieldName} <> '')");
                                break;
                            case Filter.LessThan: // Nhỏ hơn
                                filterConditions.Add($"CAST({fieldName} AS DATE) < CAST({parameterName} AS DATE)");
                                break;
                            case Filter.LessThanOrEqual: // Nhỏ hơn hoặc bằng
                                filterConditions.Add($"CAST({fieldName} AS DATE) <= CAST({parameterName} AS DATE)");
                                break;
                            case Filter.GreaterThan: // Lớn hơn
                                filterConditions.Add($"CAST({fieldName} AS DATE) > CAST({parameterName} AS DATE)");
                                break;
                            case Filter.GreaterThanOrEqual: // Lớn hơn hoặc bằng
                                filterConditions.Add($"CAST({fieldName} AS DATE) >= CAST({parameterName} AS DATE)");
                                break;
                            case Filter.Today: // Hôm nay
                                filterConditions.Add($"(CASE WHEN DATE({fieldName}) = CURDATE() THEN 1 ELSE 0 END) = 1");
                                break;
                            case Filter.ThisWeek: // Tuần này
                                filterConditions.Add($"(CASE WHEN YEAR({fieldName}) = YEAR(CURDATE()) AND WEEK({fieldName}) = WEEK(CURDATE()) THEN 1 ELSE 0 END) = 1");
                                break;
                            case Filter.ThisMonth: // Tháng này
                                filterConditions.Add($"(CASE WHEN YEAR({fieldName}) = YEAR(CURDATE()) AND MONTH({fieldName}) = MONTH(CURDATE()) THEN 1 ELSE 0 END) = 1");
                                break;
                            case Filter.ThisYear: // Năm nay
                                filterConditions.Add($"(CASE WHEN YEAR({fieldName}) = YEAR(CURDATE()) THEN 1 ELSE 0 END) = 1");
                                break;
                            case Filter.Yesterday: // Hôm qua
                                filterConditions.Add($"(CASE WHEN DATE({fieldName}) = DATE(CURDATE() - INTERVAL 1 DAY) THEN 1 ELSE 0 END) = 1");
                                break;
                            case Filter.LastWeek: // Tuần trước
                                filterConditions.Add($"(CASE WHEN YEAR({fieldName}) = YEAR(CURDATE() - INTERVAL 1 WEEK) AND WEEK({fieldName}) = WEEK(CURDATE() - INTERVAL 1 WEEK) THEN 1 ELSE 0 END) = 1");
                                break;
                            case Filter.LastMonth: // Tháng trước
                                filterConditions.Add($"(CASE WHEN YEAR({fieldName}) = YEAR(CURDATE() - INTERVAL 1 MONTH) AND MONTH({fieldName}) = MONTH(CURDATE() - INTERVAL 1 MONTH) THEN 1 ELSE 0 END) = 1");
                                break;
                            case Filter.LastYear: // Năm trước
                                filterConditions.Add($"(CASE WHEN YEAR({fieldName}) = YEAR(CURDATE() - INTERVAL 1 YEAR) THEN 1 ELSE 0 END) = 1");
                                break;
                            case Filter.Tomorrow: // Ngày mai
                                filterConditions.Add($"(CASE WHEN DATE({fieldName}) = DATE(CURDATE() + INTERVAL 1 DAY) THEN 1 ELSE 0 END) = 1");
                                break;
                            case Filter.NextWeek: // Tuần sau
                                filterConditions.Add($"(CASE WHEN YEAR({fieldName}) = YEAR(CURDATE() + INTERVAL 1 WEEK) AND WEEK({fieldName}) = WEEK(CURDATE() + INTERVAL 1 WEEK) THEN 1 ELSE 0 END) = 1");
                                break;
                            case Filter.NextMonth: // Tháng sau
                                filterConditions.Add($"(CASE WHEN YEAR({fieldName}) = YEAR(CURDATE() + INTERVAL 1 MONTH) AND MONTH({fieldName}) = MONTH(CURDATE() + INTERVAL 1 MONTH) THEN 1 ELSE 0 END) = 1");
                                break;
                            case Filter.NextYear: // Năm sau
                                filterConditions.Add($"(CASE WHEN YEAR({fieldName}) = YEAR(CURDATE() + INTERVAL 1 YEAR) THEN 1 ELSE 0 END) = 1");
                                break;
                            case Filter.InRange: // Trong khoảng
                                string[] dateRange = filterText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                                if (dateRange.Length == 2)
                                {
                                    string startDateText = dateRange[0].Trim();
                                    string endDateText = dateRange[1].Trim();

                                    // Chuyển đổi giá trị ngày bắt đầu và ngày kết thúc thành định dạng DateTime
                                    if (DateTime.TryParse(startDateText, out DateTime startDate) && DateTime.TryParse(endDateText, out DateTime endDate))
                                    {
                                        // Sử dụng startDate và endDate trong truy vấn SQL
                                        filterConditions.Add($"{fieldName} BETWEEN {parameterName}_start AND {parameterName}_end");

                                        // Thêm tham số cho ngày bắt đầu và ngày kết thúc
                                        parameters.Add($"{parameterName}_start", startDate);
                                        parameters.Add($"{parameterName}_end", endDate);
                                    }
                                }
                                break;
                            case Filter.EqualsDate: // Bằng (date)
                                filterConditions.Add($"CAST({fieldName} AS DATE) = CAST({parameterName} AS DATE)");
                                break;
                            case Filter.NotEqualsDate: // Khác (date)
                                filterConditions.Add($"CAST({fieldName} AS DATE) <> CAST({parameterName} AS DATE)");
                                break;
                            default:
                                break;
                        }

                        // Thêm tham số cho các bộ lọc vào danh sách tham số
                        parameters.Add(parameterName, filterText);
                    }
                }

                if (filterConditions.Count > 0)
                {
                    filterSql = $" AND ({string.Join(" AND ", filterConditions)})";
                }
            }

            if (entitySearchPaging.GetDepartmentIds() != null && entitySearchPaging.GetDepartmentIds().Count > 0)
            {
                var departmentIdParameters = new List<string>();
                for (int i = 0; i < entitySearchPaging.GetDepartmentIds().Count; i++)
                {
                    departmentIdParameters.Add($"@DepartmentId{i}");
                    parameters.Add($"@DepartmentId{i}", entitySearchPaging.GetDepartmentIds()[i]);
                }

                filterSql += $" AND ({char.ToLower(TableName[0])}.DepartmentId IN ({string.Join(", ", departmentIdParameters)}))";
            }


            if (entitySearchPaging.GetIds() != null && entitySearchPaging.GetIds().Count > 0)
            {
                var idParameters = new List<string>();
                for (int i = 0; i < entitySearchPaging.GetIds().Count; i++)
                {
                    idParameters.Add($"@Id{i}");
                    parameters.Add($"@Id{i}", entitySearchPaging.GetIds()[i]);
                }
                var typeSql = isNoPaging == true ? "IN" : "NOT IN";

                filterSql += $" AND ({char.ToLower(TableName[0])}.{TableName}Id {typeSql} ({string.Join(", ", idParameters)}))";
            }


            string sortSql = "";
            var sortConditions = entitySearchPaging.GetSorts();

            if (sortConditions != null && sortConditions.Count > 0)
            {
                sortSql += @"ORDER BY ";

                for (var i = 0; i < sortConditions.Count; i++)
                {
                    var condition = sortConditions[i];
                    sortSql += $"{condition.GetKeySort()} {condition.GetTypeSort()}";

                    // Nếu không phải là điều kiện sắp xếp cuối cùng, thêm dấu phẩy
                    if (i < sortConditions.Count - 1)
                    {
                        sortSql += ",";
                    }
                }
            }

            string sql = CreateSqlFilters(entitySearchPaging, filterSql, sortSql, isNoPaging);

            parameters.Add("Keyword", key);
            parameters.Add("PageSize", entitySearchPaging.GetPageSize());
            parameters.Add("Offset", offset);
            parameters.Add("Status", entitySearchPaging.GetStatus());

            var multi = await UnitOfWork.Connection.QueryMultipleAsync(sql, parameters, transaction: UnitOfWork.Transaction);
            var entities = await multi.ReadAsync<TEntity>();
            var totalRecords = await multi.ReadFirstAsync<int>();


            var data = entities.ToList();
            var result = GetDataFilters(totalRecords, data);
            return result;
        }


        /// <summary>
        /// Lấy data đã lọc
        /// </summary>
        /// <param name="totalRecords">Tổng</param>
        /// <param name="data"> mảng giá trị</param>
        /// CreatedBy:  KVHAI(20/09/2023)
        public abstract TEntityFilterResult GetDataFilters(int totalRecords, List<TEntity> data);


        /// <summary>
        /// Lấy sql
        /// </summary>
        /// <param name="entitySearchPaging">entitySearchPaging</param>
        /// <param name="filterSql">lọc</param>
        /// <param name="sortSql">sắp xếp</param>
        /// <param name="isNoPaging"> có phân trang hay ko</param>
        /// CreatedBy:  KVHAI(20/09/2023)
        public abstract string CreateSqlFilters(TEntitySearchPaging entitySearchPaging, string filterSql, string sortSql, bool? isNoPaging);


        /// <summary>
        /// tạo một
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>1- thành công, 0 - thất bại</returns>
        /// CreatedBy:  KVHAI(20/09/2023)
        public virtual async Task<int> InSertAsync(TEntity entity)
        {
            var sql = $"Proc_{TableName}_Insert";

            DynamicParameters parameters = new DynamicParameters();
            foreach (var property in entity.GetType().GetProperties())
            {
                parameters.Add($"p_{property.Name}", property.GetValue(entity));
            }

            var result = await UnitOfWork.Connection.ExecuteAsync(sql, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: UnitOfWork.Transaction);

            return result;
        }

        /// <summary>
        /// Tạo nhiều 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>Số bản ghi thành công</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// CreatedBy:  KVHAI(20/09/2023)
        public Task<int> InSertMultipleAsync(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sửa một
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">thông tin sửa</param>
        /// <returns>1- thành công, 0 - thất bại</returns>
        /// CreatedBy:  KVHAI(20/09/2023)
        public virtual async Task<int> UpdateAsync(Guid id, TEntity entity)
        {
            var sql = $"Proc_{TableName}_Update";

            DynamicParameters parameters = new DynamicParameters();
            foreach (var property in entity.GetType().GetProperties())
            {
                parameters.Add($"p_{property.Name}", property.GetValue(entity));
            }

            var result = await UnitOfWork.Connection.ExecuteAsync(sql, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: UnitOfWork.Transaction);

            return result;
        }

        /// <summary>
        /// Sửa nhiều
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>Số bản ghi thành công</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// CreatedBy:  KVHAI(20/09/2023)
        public Task<int> UpdateMultipleAsync(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Xóa một
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>1- thành công, 0 - thất bại</returns>
        /// CreatedBy:  KVHAI(20/09/2023)
        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            var sql = $"Proc_{TableName}_Delete";


            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"p_{TableName}Id", entity.GetId());

            var result = await UnitOfWork.Connection.ExecuteAsync(sql, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: UnitOfWork.Transaction);

            return result;
        }

        /// <summary>
        /// Xóa nhiều
        /// </summary>
        /// <param name="employee">Danh sách thông tin</param>
        /// <returns>thành công - số bản ghi xóa thành công, thất bại - 0</returns>
        /// CreatedBy:  KVHAI(16/09/2023)
        public virtual async Task<int> DeleteMultipleAsync(List<TEntity> entities)
        {
            var sql = $"Delete from {TableName} Where {TableName}Id In @{TableName}Ids";

            var ids = entities.Select(entity => entity.GetId()).ToList();

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add($"{TableName}Ids", ids);

            var result = await UnitOfWork.Connection.ExecuteAsync(sql, parameters, transaction: UnitOfWork.Transaction);

            return result;
        }
    }
}
