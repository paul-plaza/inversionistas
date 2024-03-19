using Investors.Administrator.Users.Application.Commands.CreateUser;
using Investors.Administrator.Users.Application.Commands.DeleteUserProfile;
using Investors.Administrator.Users.Application.Commands.UpdateUser;
using Investors.Administrator.Users.Application.Querys.GetUsersAdmin.UserAdminProfile;
using Investors.Administrator.Users.Application.Querys.GetUsersAdmin.UsersAdministrators;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Shared.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investors.Administrator.Presentation.Controllers
{
    /// <summary>
    /// Controlador de usuarios
    /// </summary>
    [Route("api/administrators")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class UserAdministratorController : BaseController
    {
        private readonly ISender _sender;
        private readonly IUserSession _userSession;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="userSession"></param>
        public UserAdministratorController(ISender sender, IUserSession userSession)
        {
            _sender = sender;
            _userSession = userSession;
        }

        /// <summary>
        /// Perfil del usuario administrador
        /// </summary>
        /// <returns></returns>
        [HttpGet("profile", Name = nameof(Profile))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Profile()
        {
            var result = await _sender.Send(new OptionsByUserQuery(_userSession.Email));
            return result.IsFailure ? FromResult(result) : Ok(result.Value);
        }

        /// <summary>
        /// Perfil del usuario administrador
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetAdmins))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAdmins()
        {
            var result = await _sender.Send(new AllUsersWithProfileQuery());
            return result.IsFailure ? FromResult(result) : Ok(result.Value);
        }



        /// <summary>
        ///   Crear un usuario administrador
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost(Name = nameof(CreateUser))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] UserToCreateRequest user)
        {
            var result = await _sender.Send(new CreateUserCommand(user, _userSession.Id));
            return result.IsFailure ? FromResult(result) : Ok(result.Value);

        }

        /// <summary>
        /// actualizar usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut(Name = nameof(UpdateUser))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest user)
        {
            var result = await _sender.Send(new UpdateUserCommand(user, _userSession.Id));
            return result.IsFailure ? FromResult(result) : Ok(result.Value);
        }

        /// <summary>
        /// eliminar usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("{userId:int}", Name = nameof(DeleteUser))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var result = await _sender.Send(new DeleteUserProfileCommand(userId, _userSession.Id));
            return result.IsFailure ? BadRequest() : NoContent();
        }
    }
}