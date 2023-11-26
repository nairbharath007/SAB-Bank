using E_Bank.Dto;
using E_Bank.Models;
using E_Bank.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        //private IRoleService _roleService;

        //public RoleController(IRoleService roleService)
        //{
        //    _roleService = roleService;
        //}

        //private Role DtoToModel(RoleDto role)
        //{
        //    return new Role()
        //    {
        //        RoleId = role.RoleId,
        //        RoleName = role.RoleName,
        //    };
        //}


        //[HttpPost("")]
        //public IActionResult Post(RoleDto role)
        //{
        //    var Converted = DtoToModel(role);
        //    var sucess = _roleService.Add(Converted);
        //    if (sucess != null)
        //    {
        //      return  Ok(sucess);
        //    }
        //    return BadRequest("Adding error");
            
            
        //}


        //private RoleDto ModelToDto(Role customer)
        //{
        //    return new RoleDto()
        //    {
        //        RoleId = customer.RoleId,
        //        RoleName = customer.RoleName,

               

        //    };
        //}


        //[HttpPut]

        //public IActionResult Put(RoleDto customerDto)
        //{
        //    var Customer = _roleService.GetById(customerDto.RoleId);

        //    if (Customer != null)
        //    {
        //        var modified = DtoToModel(customerDto);
        //        _roleService.Update(modified);
        //        return Ok(modified);
        //    }
        //    return BadRequest("Cannot modify data not found");
        //}

        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{
        //    var matched = _roleService.GetById(id);
        //    if (matched != null)
        //    {
        //        _roleService.Delete(matched);
        //        return Ok(matched);
        //    }
        //    return BadRequest("cannot find id to delete");
        //}



        //[HttpGet("")]
        //public IActionResult GetAll()
        //{
        //    List<RoleDto> result = new List<RoleDto>();
        //    var DataList = _roleService.GetAll();

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
        //    var CustomerData = _roleService.GetById(id);

        //    if (CustomerData != null)
        //    {
        //        return Ok(CustomerData);
        //    }
        //    return BadRequest("Your search Id NotFound");
        //}










    }
}
