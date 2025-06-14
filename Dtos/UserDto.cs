namespace Social.Dto;

public class UserLoginDto
{
    public string Nombre { get; set; }
    public string Password { get; set; }
}

public class UserLogoutDto
{
    public int Id { get; set; }
}
