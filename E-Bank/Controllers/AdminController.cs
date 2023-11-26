using E_Bank.Dto;
using E_Bank.Models;
using E_Bank.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        //private IAdminService _adminService;

        //public AdminController(IAdminService accountService)
        //{
        //    _adminService = accountService;
        //}






        //private AdminDto ModelToDto(Admin customer)
        //{
        //    return new AdminDto()
        //    {
        //      UserId = customer.UserId,
        //      FirstName = customer.FirstName,
        //      LastName = customer.LastName,
        //      AdminId = customer.AdminId,
              

        //    };
        //}

        //[HttpGet("")]
        //public IActionResult GetAll()
        //{
        //    List<AdminDto> result = new List<AdminDto>();
        //    var DataList = _adminService.GetAll();

        //    if (DataList.Count == 0)
        //    {
        //        return BadRequest("No customer Added");
        //    }
        //    foreach (var Data in DataList)
        //    {
        //        result.Add(ModelToDto(Data));
        //    }
        //    return Ok(result);
        //}

        //[HttpGet("{id:int}")]
        //public IActionResult Get(int id)
        //{
        //    var CustomerData = _adminService.GetById(id);

        //    if (CustomerData != null)
        //    {
        //        return Ok(CustomerData);
        //    }
        //    return BadRequest("Your search Id NotFound");
        //}


        //private Admin ConvertoModel(AdminDto adminDto)
        //{
        //    return new Admin()
        //    {
        //     AdminId = adminDto.AdminId,
        //      FirstName= adminDto.FirstName,
        //      LastName= adminDto.LastName,
        //      UserId= adminDto.UserId

        //    };
        //}

        //[HttpPost("")]
        //public IActionResult Post(AdminDto customerDto)
        //{
        //    var customer = ConvertoModel(customerDto);
        //    var status = _adminService.Add(customer);

        //    if (status != null)
        //    {
        //        return Ok(status);
        //    }
        //    return BadRequest("cannot added");
        //}

        //[HttpPut]

        //public IActionResult Put(AdminDto customerDto)
        //{
        //    var Customer = _adminService.GetById(customerDto.AdminId);

        //    if (Customer != null)
        //    {
        //        var modified = ConvertoModel(customerDto);
        //        _adminService.Update(modified);
        //        return Ok(modified);
        //    }
        //    return BadRequest("Cannot modify data not found");
        //}

        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{
        //    var matched = _adminService.GetById(id);
        //    if (matched != null)
        //    {
        //        _adminService.Delete(matched);
        //        return Ok(matched);
        //    }
        //    return BadRequest("cannot find id to delete");
        //}
    }
}
