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




    [HttpPost]
    [Route("ItemTransCreateInfo")]
    public async Task<IActionResult> CreateInformation(ItemTransCreateInformationRequest request)
    {
        CreateInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.CreateInformation(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpPost]
    [Route("GDCreateInfo")]
    public async Task<IActionResult> CreateInformation(GDCreateInformationRequest request)
    {
        CreateInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.CreateInformation(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpPost]
    [Route("ItemCreateInfo")]
    public async Task<IActionResult> CreateInformation(IItemCreateInformationRequest request)
    {
        CreateInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.CreateInformation(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpPost]
    [Route("SuppilerCreateInfo")]
    public async Task<IActionResult> CreateInformation(SupplierInfoCreateInformationRequest request)
    {
        CreateInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.CreateInformation(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpPost]
    [Route("ItemCatCreateInfo")]
    public async Task<IActionResult> CreateInformation(ItemCatCreateInfoCreateInformationRequest request)
    {
        CreateInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.CreateInformation(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpPost]
    [Route("SubCatCreateInfo")]
    public async Task<IActionResult> CreateInformation(SubCatCreateInfoCreateInformationRequest request)
    {
        CreateInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.CreateInformation(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }






    [HttpGet]
    [Route("ItemInfoView")]
    public async Task<IActionResult> ItemReadInformation()
    {
        ReadInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.ItemReadInformation();

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpGet]
    [Route("ItemCInfoView")]
    public async Task<IActionResult> ICReadInformation()
    {
        ReadInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.ICReadInformation();

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpGet]
    [Route("ItemTransInfoView")]
    public async Task<IActionResult> ItemTransReadInformation()
    {
        ReadInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.ItemTransReadInformation();

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpGet]
    [Route("ItemTransCInfoView")]
    public async Task<IActionResult> ITCReadInformation()
    {
        ReadInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.ITCReadInformation();

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpGet]
    [Route("GDInfoView")]
    public async Task<IActionResult> GDReadInformation()
    {
        ReadInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.GDReadInformation();

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpGet]
    [Route("GDCInfoView")]
    public async Task<IActionResult> GDCReadInformation()
    {
        ReadInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.GDCReadInformation();

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpGet]
    [Route("SupplierInfoView")]
    public async Task<IActionResult> SupplierReadInformation()
    {
        ReadInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.SupplierReadInformation();

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpGet]
    [Route("SupplierCInfoView")]
    public async Task<IActionResult> SupCReadInformation()
    {
        ReadInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.SupCReadInformation();

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpGet]
    [Route("ItemCatInfoView")]
    public async Task<IActionResult> ItemCatInformation()
    {
        ReadInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.ItemCatInformation();

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);

    }
    [HttpGet]
    [Route("ItemCatCInfoView")]
    public async Task<IActionResult> ItemCatCReadInformation()
    {
        ReadInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.ItemCatCReadInformation();

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);

    }
    [HttpGet]
    [Route("SubCatInfoView")]
    public async Task<IActionResult> SubCatInformation()
    {
        ReadInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.SubCatInformation();

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);

    }
    [HttpGet]
    [Route("SubCatCInfoView")]
    public async Task<IActionResult> SubCatCReadInformation()
    {
        ReadInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.SubCatCReadInformation();

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);

    }






    [HttpPut]
    [Route("ItemUpInfo")]
    public async Task<IActionResult> UpdateInformation(ItemUpdateInformationRequest request)
    {
        UpdateInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.UpdateInformation(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpPut]
    [Route("ItemTransUpInfo")]
    public async Task<IActionResult> UpdateInformation(ItemTransUpdateInformationRequest request)
    {
        UpdateInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.UpdateInformation(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpPut]
    [Route("GDUpInfo")]
    public async Task<IActionResult> UpdateInformation(GDUpdateInformationRequest request)
    {
        UpdateInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.UpdateInformation(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpPut]
    [Route("SupplierUpInfo")]
    public async Task<IActionResult> UpdateInformation(SupplierUpdateInformationRequest request)
    {
        UpdateInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.UpdateInformation(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpPut]
    [Route("ItemCatUpInfo")]
    public async Task<IActionResult> UpdateInformation(ItemCatUpdateInformationRequest request)
    {
        UpdateInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.UpdateInformation(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpPut]
    [Route("SubCatUpInfo")]
    public async Task<IActionResult> UpdateInformation(SubCatUpdateInformationRequest request)
    {
        UpdateInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.UpdateInformation(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }







    [HttpPost]
    [Route("ItemDeleteInfo")]
    public async Task<IActionResult> IItemDeleteInfo(DeleteInformationRequest request)
    {
        DeleteInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.IItemDeleteInfo(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpPost]
    [Route("GDDeleteInfo")]
    public async Task<IActionResult> GDDeleteInfo(DeleteInformationRequest request)
    {
        DeleteInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.GDDeleteInfo(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpPost]
    [Route("SupplierDeleteInfo")]
    public async Task<IActionResult> SupplierDeleteInfo(DeleteInformationRequest request)
    {
        DeleteInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.SupplierDeleteInfo(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpPost]
    [Route("ItemCatDeleteInfo")]
    public async Task<IActionResult> ItemCatDeleteInfo(DeleteInformationRequest request)
    {
        DeleteInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.ItemCatDeleteInfo(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpPost]
    [Route("SubCatDeleteInfo")]
    public async Task<IActionResult> SubCatDeleteInfo(DeleteInformationRequest request)
    {
        DeleteInformationResponse response = null;
        try
        {

            response = await _invApplicationSL.SubCatDeleteInfo(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }



    [HttpPost]
    [Route("SubCatSearchbyCatInfo")]
    public async Task<IActionResult> SubCatSearchInformationByCat(SearchInformationByNameRequest request)
    {
        SearchInformationByNameResponse response = null;
        try
        {

            response = await _invApplicationSL.SubCatSearchInformationByCat(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }
    [HttpPost]
    [Route("ItemSearchbyCatInfo")]
    public async Task<IActionResult> ItemSearchInformationByCat(SearchInformationByNameRequest request)
    {
        SearchInformationByNameResponse response = null;
        try
        {

            response = await _invApplicationSL.ItemSearchInformationByCat(request);

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Exception Message : " + ex.Message;
        }

        return Ok(response);
    }




}
