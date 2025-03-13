namespace BreezyDrive.UserServices.Application.DTOs.Request
{
    public class CreateUserRequest
    {
        public string Phone { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
