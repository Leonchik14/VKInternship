using InternshipProject.RepositoryPattern;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;


namespace InternshipProject.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UnitOfWork _userTables;

        public UserController(ILogger<UserController> logger, DataBaseContext dataBaseContext)
        {
            _logger = logger;
            _userTables = new UnitOfWork(dataBaseContext);
        }

        [HttpPost(Name ="AddUser")]
        public async Task<IActionResult> AddUserAsync([FromBody] AddUserJSON data)
        {
            if (data.role > 1 || data.role < 0) return BadRequest();
            
            User user = new Models.User(data.login, data.password, DateTime.Now, Models.UserGroup.Count, UserState.Count);

            if (await _userTables.Users.CreateAsync(user, (Role)data.role))
            {
                await _userTables.State.CreateAsync(new UserState(data.stateDescription));
                await _userTables.Group.CreateAsync(new UserGroup((Role)data.role, data.groupDescription));
                await _userTables.SaveAsync();
                return Ok(user);
            }
            else
            {
                return BadRequest(user);
            } 
        }

        [HttpPost( Name = "DeleteUser")]
        public async Task<IActionResult> DeleteUserAsync([FromBody] Login data)
        {
            if (!await _userTables.Users.IsExistAsync(data.login))
            {
                return BadRequest();
            }

            await _userTables.State.UpdateAsync((await _userTables.Users.GetUserByLogin(data.login)).UserStateId);
            await _userTables.SaveAsync();
            return Ok(await GetUserAsync(data));
        }

        [HttpGet(Name = "GetUser")]
        public async Task<IActionResult> GetUserAsync([FromBody] Login data)
        {
            if (!await _userTables.Users.IsExistAsync(data.login))
            {
                return BadRequest();
            }

            return Ok(await _userTables.Users.GetElementAsync(data.login));
        }
        [HttpGet(Name = "GetAll")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            return Ok(await _userTables.Users.GetAllAsync());
        }
    }
}
