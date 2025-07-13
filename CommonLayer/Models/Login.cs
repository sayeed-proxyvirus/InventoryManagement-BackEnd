﻿namespace InventoryManagement.CommonLayer.Models
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
