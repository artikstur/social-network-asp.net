using ITISHub.Core.Models;

namespace ITISHub.Application.Interfaces.Auth;

public interface IJwtProvider
{
    string Generate(User user);
}
