namespace CoreDemo.Models
{
    public class PasswordChangeModel
    {
        public string? currentPassword {  get; set; }
        public string? newPassword { get; set; }
        public string? confirmNewPassword { get; set; }
    }
}
