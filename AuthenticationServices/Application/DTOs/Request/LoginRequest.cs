using System.ComponentModel.DataAnnotations;

namespace BreezyDrive.AuthenticationServices.Application.DTOs.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        public string Password { get; set; }
    }
}
