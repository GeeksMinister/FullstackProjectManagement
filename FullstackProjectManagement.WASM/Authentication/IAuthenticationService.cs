namespace FullstackProjectManagement.WASM.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel?> Login(AuthenticationUserModel user);
        Task LogOut();
    }
}