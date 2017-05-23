using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITI.PrimarySchool.DAL;

namespace ITI.PrimarySchool.WebApp.Services
{
    public class GitHubService
    {
        readonly GitHubClient _gitHubClient;
        readonly StudentGateway _studentGateway;
        readonly UserGateway _userGateway;

        public GitHubService( GitHubClient gitHubClient, StudentGateway studentGateway, UserGateway userGateway )
        {
            _gitHubClient = gitHubClient;
            _studentGateway = studentGateway;
            _userGateway = userGateway;
        }

        // TODO: missing return in line 26 added
        public async Task<Result<IEnumerable<Student>>> GetFollowedStudents( int userId )
        {
            User user = _userGateway.FindById( userId );
            if( user == null ) return Result.Failure<IEnumerable<Student>>( Status.BadRequest, "Unknown user." );
            if( user.GithubAccessToken == string.Empty ) return Result.Failure<IEnumerable<Student>>( Status.BadRequest, "This user is not a known github user." );

            IEnumerable<string> logins = await _gitHubClient.GetFollowedUsers( user.GithubAccessToken );
            IEnumerable<Student> students = _studentGateway.GetByGitHubLogin( logins );

            return Result.Success( Status.Ok, students );
        }

        public async Task<Result> FollowStudent( int userId, string userName )
        {
            var user = _userGateway.FindById(userId);
            if( user == null ) return Result.Failure( Status.BadRequest, "Unknown user." );
            if( user.GithubAccessToken == string.Empty ) return Result.Failure( Status.BadRequest, "This user is not a known github user." );

            var success = await _gitHubClient.FollowUser(user.GithubAccessToken, userName);

            return success ? Result.Success( Status.Ok ) : Result.Failure( Status.BadRequest, $"Can't follow user {userName}" );
        }
    }
}
