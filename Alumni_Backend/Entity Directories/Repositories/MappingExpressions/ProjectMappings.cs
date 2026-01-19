using System.Linq.Expressions;
using Entity_Directories.Services.DTO;
using Alumni_Portal.Infrastructure.Data_Models;
namespace Entity_Directories.Repositories.MappingExpressions
{
    internal class MappingExtension
    {
        public Expression<Func<CreateProjectDTO, Projects>> CreateMapping()
        {
            return p => new Projects
            {
                Project_Academic_ID = p.Project_Academic_ID,
                Project_Name = p.Project_Name,

                Project_Type_Value = p.Project_Type,
                Project_Year = p.Project_Year,

            };
        }
    }
}
