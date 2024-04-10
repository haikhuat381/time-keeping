namespace MISA.WEB07.MF1755.KVHAI.Domain
{
    public enum Filter
    {
        /// <summary>
        /// Loại điều kiện: Chứa
        /// </summary>
        Contains = 1,

        /// <summary>
        /// Loại điều kiện: Không chứa
        /// </summary>
        DoesNotContain = 2,

        /// <summary>
        /// Loại điều kiện: Bắt đầu với
        /// </summary>
        StartsWith = 3,

        /// <summary>
        /// Loại điều kiện: Kết thúc với
        /// </summary>
        EndsWith = 4,

        /// <summary>
        /// Loại điều kiện: Bằng
        /// </summary>
        Equals = 5,

        /// <summary>
        /// Loại điều kiện: Khác
        /// </summary>
        NotEqual = 6,

        /// <summary>
        /// Loại điều kiện: Trống
        /// </summary>
        IsEmpty = 7,

        /// <summary>
        /// Loại điều kiện: Không trống
        /// </summary>
        IsNotEmpty = 8,

        /// <summary>
        /// Loại điều kiện: Nhỏ hơn
        /// </summary>
        LessThan = 9,

        /// <summary>
        /// Loại điều kiện: Nhỏ hơn hoặc bằng
        /// </summary>
        LessThanOrEqual = 10,

        /// <summary>
        /// Loại điều kiện: Lớn hơn
        /// </summary>
        GreaterThan = 11,

        /// <summary>
        /// Loại điều kiện: Lớn hơn hoặc bằng
        /// </summary>
        GreaterThanOrEqual = 12,

        /// <summary>
        /// Loại điều kiện: Hôm nay
        /// </summary>
        Today = 13,

        /// <summary>
        /// Loại điều kiện: Tuần này
        /// </summary>
        ThisWeek = 14,

        /// <summary>
        /// Loại điều kiện: Tháng này
        /// </summary>
        ThisMonth = 15,

        /// <summary>
        /// Loại điều kiện: Năm nay
        /// </summary>
        ThisYear = 16,

        /// <summary>
        /// Loại điều kiện: Hôm qua
        /// </summary>
        Yesterday = 17,

        /// <summary>
        /// Loại điều kiện: Tuần trước
        /// </summary>
        LastWeek = 18,

        /// <summary>
        /// Loại điều kiện: Tháng trước
        /// </summary>
        LastMonth = 19,

        /// <summary>
        /// Loại điều kiện: Năm trước
        /// </summary>
        LastYear = 20,

        /// <summary>
        /// Loại điều kiện: Ngày mai
        /// </summary>
        Tomorrow = 21,

        /// <summary>
        /// Loại điều kiện: Tuần sau
        /// </summary>
        NextWeek = 22,

        /// <summary>
        /// Loại điều kiện: Tháng sau
        /// </summary>
        NextMonth = 23,

        /// <summary>
        /// Loại điều kiện: Năm sau
        /// </summary>
        NextYear = 24,

        /// <summary>
        /// Loại điều kiện: Trong khoảng
        /// </summary>
        InRange = 25,

        /// <summary>
        /// Loại điều kiện: Bằng (date)
        /// </summary>
        EqualsDate = 26,

        /// <summary>
        /// Loại điều kiện: Khác (date)
        /// </summary>
        NotEqualsDate = 27,

    }

}
