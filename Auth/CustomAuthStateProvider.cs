using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace auth.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly JwtService _jwtService;
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    private readonly IJSRuntime _jsRuntime;

    private const string TokenKey = "authToken";

    
    public CustomAuthStateProvider(JwtService jwtService, IJSRuntime jsRuntime)
    {
        _jwtService = jwtService;
        _jsRuntime = jsRuntime;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await GetTokenFromStorage();

        if (string.IsNullOrEmpty(token))
        {
            return new AuthenticationState(_anonymous);
        }
        
        var principal = _jwtService.ValidateToken(token);
        
        return principal == null ? new AuthenticationState(_anonymous) : new AuthenticationState(principal);
    }

    public async Task Login(string username, string role)
    {
        var token = _jwtService.GenerateToken(username, role);
        await SaveTokenToStorage(token);

        var principal = _jwtService.ValidateToken(token);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal!)));
    }

    public async Task Logout()
    {
        await ClearTokenFromStorage();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }

    private async Task<string?> GetTokenFromStorage()
    {
        return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);
    }

    private async Task SaveTokenToStorage(string token)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
    }

    private async Task ClearTokenFromStorage()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
    }
}