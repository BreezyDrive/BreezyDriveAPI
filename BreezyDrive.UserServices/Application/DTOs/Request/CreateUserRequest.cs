namespace BreezyDrive.UserServices.Application.DTOs.Request
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }

        public IFormFile DrivingLicense { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
