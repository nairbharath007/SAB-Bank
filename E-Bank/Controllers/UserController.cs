using E_Bank.Dto;
using E_Bank.Exceptions;
using E_Bank.Models;
using E_Bank.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Ctrl+K, Ctrl+U --> uncomment
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        private User DtoToModel(UserDto userdto)
        {
            return new User()
            {
                //UserId=userdto.userId
                UserName = userdto.UserName,
                Password = userdto.Password,
                RoleId = userdto.RoleId,
            };
        }


        private UserDto ModelToDto(User user)
        {
            return new UserDto()
            {
                // UserId=user.UserId
                Password = user.Password,
                RoleId = user.RoleId,
                UserName = user.UserName,
            };
        }


        [HttpGet("")]
        public IActionResult GetAll()
        {
            List<UserDto> result = new List<UserDto>();
            var DataList = _userService.GetAll();

            if (DataList.Count == 0)
            {
                return BadRequest("No customer Added");
            }
            foreach (var Data in DataList)
            {
                result.Add(ModelToDto(Data));
            }
            return Ok(result);
        }

        //[HttpGet("{id:int}")]
        //public IActionResult Get(int id)
        //{
        //    var CustomerData = _userService.GetById(id);

        //    if (CustomerData != null)
        //    {
        //        return Ok(CustomerData);
        //    }
        //    throw new UserNotFoundException("Cannot find the match id");
        //}



        //[HttpPost("")]
        //public IActionResult Post(UserDto userD)
        //{
        //    var user = DtoToModel(userD);
        //  var  responce= _userService.Add(user);
        //    if (responce != null)
        //    {
        //        return Ok(responce);
        //    }
        //    return BadRequest("Not Added");
        //}


        //[HttpPut]

        //public IActionResult Put(UserDto userDto)
        //{
        //    var Customer = _userService.GetById(userDto.UserId);

        //    if (Customer != null)
        //    {
        //        var modified = DtoToModel(userDto);
        //        _userService.Update(modified);
        //        return Ok(modified);
        //    }
        //    return BadRequest("Cannot modify data not found");
        //}

        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{
        //    var matched = _userService.GetById(id);
        //    if (matched != null)
        //    {
        //        _userService.Delete(matched);
        //        return Ok(matched);
        //    }
        //    return BadRequest("cannot find id to delete");
        //}


    }


}
