using System.Collections.Generic;
using System.Linq;
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
    public class StudentController : Controller
    {
        readonly StudentService _studentService;

        public StudentController( StudentService studentService )
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult GetStudentList()
        {
            Result<IEnumerable<Student>> result = _studentService.GetAll();
            return this.CreateResult<IEnumerable<Student>, IEnumerable<StudentViewModel>>( result, o =>
            {
                o.ToViewModel = x => x.Select( s => s.ToStudentViewModel() );
            } );
        }

        [HttpGet( "{id}", Name = "GetStudent" )]
        public IActionResult GetStudentById( int id )
        {
            Result<Student> result = _studentService.GetById( id );
            return this.CreateResult<Student, StudentViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToStudentViewModel();
            } );
        }

        [HttpPost]
        public IActionResult CreateStudent( [FromBody] StudentViewModel model )
        {
            Result<Student> result = _studentService.CreateStudent( model.FirstName, model.LastName, model.BirthDate, model.Photo, model.GitHubLogin );
            return this.CreateResult<Student, StudentViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToStudentViewModel();
                o.RouteName = "GetStudent";
                o.RouteValues = s => new { id = s.StudentId };
            } );
        }

        [HttpPut( "{id}" )]
        public IActionResult UpdateStudent( int id, [FromBody] StudentViewModel model )
        {
            Result<Student> result = _studentService.UpdateStudent( id, model.FirstName, model.LastName, model.BirthDate, model.Photo, model.GitHubLogin );
            return this.CreateResult<Student, StudentViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToStudentViewModel();
            } );
        }

        [HttpDelete( "{id}" )]
        public IActionResult DeleteStudent( int id )
        {
            Result<int> result = _studentService.Delete( id );
            return this.CreateResult( result );
        }
    }
}