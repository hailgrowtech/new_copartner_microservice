//using Microsoft.AspNetCore.Mvc;
//using UserService.Logic;

//namespace UserService.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//public class UserUtilController : ControllerBase
//{
//    private readonly IUserBusinessProcessor _logic;
//    private readonly ILogger<UserUtilController> _logger;

//    public UserUtilController(IUserBusinessProcessor logic, ILogger<UserUtilController> logger)
//    {
//        this._logic = logic;
//        this._logger = logger;
//    }

//    //[HttpGet("{mobileNumber}")]
//    //public ActionResult<bool> CheckIfMobileNumberAlreadyExists(string MobileEmail) => Ok(_logic.CheckIfUserExistsWithMobileEmail(phoneEmail));

//    //[HttpPatch]
//    //public ActionResult<bool> ResetPassword(UserPasswordDTO userPasswordDTO)
//    //{
//    //    return Ok(_logic.ResetPassword(userPasswordDTO));
//    //}
//}
