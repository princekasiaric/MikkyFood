using System.ComponentModel.DataAnnotations;

namespace MFR.Core.DTO.Request
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string Token { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
