namespace Bridge.Core.Resources
{
    public class AuthResponse
    {
        public UserResource User { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
