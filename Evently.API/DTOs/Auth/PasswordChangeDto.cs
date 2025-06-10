namespace Evently.API.DTOs.Auth;

public class PasswordChangeDto
{
    public required string OldPassword { get; set; }
    public required string NewPassword { get; set; }
}