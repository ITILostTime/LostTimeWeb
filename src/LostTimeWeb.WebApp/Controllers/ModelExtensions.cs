using LostTimeWeb.DAL;
using LostTimeWeb.WebApp.Models.ClassViewModels;
using LostTimeWeb.WebApp.Models.StudentViewModels;
using LostTimeWeb.WebApp.Models.TeacherViewModels;

namespace LostTimeWeb.WebApp.Controllers
{
    public static class ModelExtensions
    {
        public static ClassViewModel ToClassViewModel( this Class @this )
        {
            return new ClassViewModel
            {
                ClassId = @this.ClassId,
                Name = @this.Name,
                Level = @this.Level
            };
        }
        
        public static StudentViewModel ToStudentViewModel( this Student @this )
        {
            return new StudentViewModel
            {
                StudentId = @this.StudentId,
                FirstName = @this.FirstName,
                LastName = @this.LastName,
                BirthDate = @this.BirthDate,
                Photo = @this.Photo,
                GitHubLogin = @this.GitHubLogin
            };
        }

        public static FollowedStudentViewModel ToFollowedStudentViewModel( this Student @this )
        {
            return new FollowedStudentViewModel
            {
                StudentId = @this.StudentId,
                FirstName = @this.FirstName,
                LastName = @this.LastName,
                GitHubLogin = @this.GitHubLogin
            };
        }

        public static TeacherViewModel ToTeacherViewModel( this Teacher @this )
        {
            return new TeacherViewModel
            {
                TeacherId = @this.TeacherId,
                FirstName = @this.FirstName,
                LastName = @this.LastName
            };
        }

        public static AssignedClassViewModel ToAssignedClassViewModel( this Class @this )
        {
            return new AssignedClassViewModel
            {
                ClassId = @this.ClassId,
                Name = @this.Name
            };
        }
    }
}
