namespace InventoryManagement.CommonLayer.Models
{
    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
    public class RegisterResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
