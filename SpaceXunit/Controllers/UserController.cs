using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace SpaceXunit.Controllers;

[Authorize]
[ApiController]
public class UserController : BaseController
{
    private readonly IMapper _mapper;

    #region Constructor

    public UserController(IMapper mapper)
    {
        _mapper = mapper;  
    }

    #endregion

    #region Public Method

    // GET: api/user
    //[HttpGet]
    //[IgnoreAntiforgeryToken]
    //public async Task<IActionResult> GetUsersAsync()
    //{
    //    var method = "GetUsersAsync";

    //    var users = await _userService.GetUsersAsync();
    //    if (users is null)
    //    {
    //        return await SystemLog.InternalServerError(
    //            string.Format(Message.InternalServerError, "users"), elapsed.Milliseconds, _serverUrl, _logApiUrl, IconFileInfo.GetFileInfo(new StackFrame(0, true), method));
    //    }

    //    return await SystemLog.Success(
    //        string.Format(Message.Searched, "User"), elapsed.Milliseconds, _serverUrl, _logApiUrl, IconFileInfo.GetFileInfo(new StackFrame(0, true), method), users);
    //}

    #endregion

    #region Private Method

    private string? GetIpAddress()
    {
        if (Request?.Headers == null)
            return string.Empty;

        if (Request.Headers.TryGetValue("X-Forwarded-For", out var forwardedFor))
        {
            return forwardedFor.FirstOrDefault();
        }
        else
        {
            return HttpContext?.Connection?.RemoteIpAddress?.MapToIPv4()?.ToString() ?? string.Empty;
        }
    }

    #endregion
}
