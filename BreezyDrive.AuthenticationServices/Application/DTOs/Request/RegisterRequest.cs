namespace BreezyDrive.AuthenticationServices.Application.DTOs.Request
{
    public class RegisterRequest
    {
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Email { get; set; }
    }
}
