using InventoryManagement.CommonLayer.Models;
using InventoryManagement.ServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class INVApplicationController : ControllerBase
{
    public readonly IINVApplicationSL _invApplicationSL;

    public INVApplicationController(IINVApplicationSL invApplicationSL)
    {
        _invApplicationSL = invApplicationSL;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        LoginResponse response = null;
        try
        {

            response = await _invApplicationSL.Login(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        RegisterResponse response = null;
        try
        {

            response = await _invApplicationSL.Register(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
}
