using Microsoft.AspNetCore.Mvc;

namespace OAS.Jala.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class USerController : ControllerBase
{
    public USerController()
    {
        
    }
    
    /// <summary>
    ///  Get a new user
    /// </summary>
    /// <returns>Return an user</returns>
    /// <response code="200"></response>
    /// <response code="404"></response>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesHeader(typeof(Person))]
    [NeedAuthorization]
    public ActionResult<Person> Get()
    {
        return Ok(new User());
    }

    /// <summary>
    ///  Create a new User
    /// </summary>
    /// <param name="user">The user data to be created</param>
    /// <returns>A the newly created user</returns>
    [HttpPost]
    [Consumes("application/json")]
    [ProducesHeader(typeof(Person))]
    public ActionResult<User> CreatePerson(Person user)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        return Ok(user);
    }
}