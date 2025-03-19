using System.ComponentModel.DataAnnotations;

namespace BreezyDrive.AuthenticationServices.Application.DTOs.Request
{
    public class RegisterGoogleRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập tên.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập password.")]
        public string Password { get; set; }
        public string Avatar { get; set; }
    }
}
