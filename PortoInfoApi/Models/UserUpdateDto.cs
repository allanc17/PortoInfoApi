public class LoginRequestDto
{
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
}
public class UserDto
{
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public required string Role { get; set; }
}
public class UserUpdateDto
{
    public required int Id { get; set; }
    public string? Username { get; set; }
    public string? PasswordHash { get; set; }
    public string? Role { get; set; }
}