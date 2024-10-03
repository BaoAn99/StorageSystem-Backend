namespace StorageSystem.Application.Enums
{
    public enum ErrorCode
    {
        // Mã lỗi chung
        None = 0, // Không có lỗi
        UnknownError = 1, // Lỗi không xác định
        InvalidRequest = 2, // Yêu cầu không hợp lệ
        Unauthorized = 3, // Không được phép
        Forbidden = 4, // Truy cập bị cấm
        NotFound = 5, // Không tìm thấy
        Conflict = 6, // Xung đột
        InternalServerError = 7, // Lỗi máy chủ nội bộ
        ServiceUnavailable = 8, // Dịch vụ không khả dụng

        // Mã lỗi cụ thể cho người dùng
        UserNotFound = 1001, // Người dùng không tồn tại
        InvalidCredentials = 1002, // Tài khoản hoặc mật khẩu không hợp lệ
        AccountLocked = 1003, // Tài khoản bị khóa
        AccountDisabled = 1004, // Tài khoản bị vô hiệu hóa
        UserAlreadyExists = 1005, // Người dùng đã tồn tại
        PasswordTooWeak = 1006, // Mật khẩu quá yếu
        EmailNotVerified = 1007, // Email chưa được xác minh

        // Mã lỗi cho yêu cầu
        ItemNotFound = 2001, // Mục không tồn tại
        InsufficientStock = 2002, // Không đủ hàng tồn kho
        InvalidPaymentMethod = 2003, // Phương thức thanh toán không hợp lệ
        PaymentFailed = 2004, // Thanh toán thất bại
        OrderNotFound = 2005, // Đơn hàng không tồn tại

        // Mã lỗi cho dữ liệu
        ValidationError = 3001, // Lỗi xác thực dữ liệu
        DataIntegrityError = 3002, // Lỗi tính toàn vẹn dữ liệu
        DataNotFound = 3003, // Dữ liệu không tồn tại
        DataAlreadyExists = 3004, // Dữ liệu đã tồn tại
        DataProcessingError = 3005, // Lỗi xử lý dữ liệu

        // Mã lỗi cho hệ thống
        TimeoutError = 4001, // Lỗi hết thời gian
        NetworkError = 4002, // Lỗi mạng
        DatabaseConnectionError = 4003, // Lỗi kết nối cơ sở dữ liệu
        ServiceTimeout = 4004, // Dịch vụ hết thời gian
        ApiRateLimitExceeded = 4005 // Vượt quá giới hạn tốc độ API
    }

}
