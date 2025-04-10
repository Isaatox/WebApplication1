namespace WebApplication1.DTOs
{
    public class RegisterRequestDto : AuthRequestDto
    {
        public string DisplayName { get; set; } = default!;
    }
}
