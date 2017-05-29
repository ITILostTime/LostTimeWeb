using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LostTimeWeb.DAL;
using LostTimeWeb.WebApp.Authentication;
using LostTimeWeb.WebApp.Models.StudentViewModels;
using LostTimeWeb.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostTimeWeb.WebApp.Controllers
{
    [Route( "api/[controller]" )]
    [Authorize( ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme )]
    public class GitHubController : Controller
    {
        readonly GitHubService _gitHubService;

        public GitHubController( GitHubService gitHubService )
        {
            _gitHubService = gitHubService;
        }

        [HttpGet( "Following" )]
        public async Task<IActionResult> GetFollowedStudents()
        {
            int userId = int.Parse( User.FindFirst( ClaimTypes.NameIdentifier ).Value );
            Result<IEnumerable<Student>> result = await _gitHubService.GetFollowedStudents( userId );
            return this.CreateResult<IEnumerable<Student>, IEnumerable<FollowedStudentViewModel>>( result, o =>
            {
                o.ToViewModel = x => x.Select( s => s.ToFollowedStudentViewModel() );
            } );
        }

        [HttpPut( "follow/{userName}" )]
        public async Task<IActionResult> FollowUser( string userName )
        {
            int userId = int.Parse( User.FindFirst( ClaimTypes.NameIdentifier ).Value );

            var result = await _gitHubService.FollowStudent(userId, userName);

            return this.CreateResult( result );
        }
    }
}
