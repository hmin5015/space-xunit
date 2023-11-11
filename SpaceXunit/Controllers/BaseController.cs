using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace SpaceXunit.Controllers;

public class BaseController : ControllerBase, IDisposable
{
    static BaseController()
    {

    }

    public void Dispose()
    {

    }
}
