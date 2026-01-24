using System.Linq.Expressions;

using Alumni_Portal.Infrastructure.Data_Models;
using Entity_Directories.Services.DTO;

namespace Entity_Directories.Repositories.MappingExpressions
{
    public  class UserMappings
    {
    
        public static Expression<Func<Individuals, userDirectoryDTO>> AlumniMapping()
        {

            return i => new userDirectoryDTO
            {
                Individual_Name = i.Individual_Name,
                Individual_Email = i.Individual_Email,
                Individual_Current_Industry = i.Individual_Current_Industry,
                Individual_Current_Role = i.Individual_Current_Role,
                noMentorships = i.Individual_Mentorship_Count,
                noSponsorships = i.Individual_Sponsorship_Count,
                Program = i.Academic_Details
               .OrderByDescending(a => a.Individual_Academic_Graduation_Year)
               .Select(a => new ProgramInfoDTO
               {
                   Program = a.Individual_Academic_Program_Value,
                   Graduation_Year = a.Individual_Academic_Graduation_Year,
                   Department = a.Individual_Academic_Department_Value,
               }).FirstOrDefault()

            };
        }

        public static Expression<Func<Individuals, userDirectoryDTO>> StudentMapping()
        {
            return i => new userDirectoryDTO

            {
                Individual_Name = i.Individual_Name,
                Individual_Email = i.Individual_Email,
                Program = i.Academic_Details
                            .Select(a => new ProgramInfoDTO
                            {
                                Program = a.Individual_Academic_Program_Value,
                                Graduation_Year = a.Individual_Academic_Graduation_Year,
                                Department = a.Individual_Academic_Department_Value,
                            }).FirstOrDefault(),
            };

        } 

        public static Expression<Func<Individuals, userDirectoryDTO>> SupervisorMapping()
        {
            return i => new userDirectoryDTO

            {
                Individual_Name = i.Individual_Name,
                Individual_Email = i.Individual_Email,
                Program = i.Academic_Details
                            .Select(a => new ProgramInfoDTO
                            {
                                Department = a.Individual_Academic_Department_Value,
                                Designation = a.Individual_Academic_Designation,
                            }).FirstOrDefault()
            };

        }

        public static Expression<Func<NewUserDTO, Individuals>> NewUserMapping()
        {
            return n => new Individuals
            {
                Individual_Institution_ID = n.Individual_Institution_ID,
                Individual_Name = n.Individual_Name,
                Individual_Email = n.Individual_Email,
                Individual_Type_Value = n.Individual_Type_Value,
                Campus_ID = n.Campus_ID,
                Client_ID = n.Client_ID,
                Campus_Reference_Key = n.Campus_Reference_Key,
                Client_Reference_Key = n.Client_Reference_Key,
                Individual_Is_Alumni = n.Individual_Is_Alumni,
                Individual_Contact_Number_Primary = n.Individual_Contact_Number_Primary,

                Academic_Details= n.Academic_Details.Select(a => new Individual_Academics
                {
 
                    Individual_Academic_Student_ID = a.Student_ID,
                    Individual_Academic_Program_ID=a.Program_ID,
                    Individual_Academic_Program_Value = a.Program,
                    Individual_Academic_Batch=a.Batch,
                    Individual_Academic_Enrollment_Year = a.Enrollment_Year,
                    Individual_Academic_Graduation_Year = a.Graduation_Year,
                    Individual_Academic_Department_ID=a.Department_ID,
                    Individual_Academic_Department_Value = a.Department,
                    Individual_Academic_Designation=a.Designation
                    
                }).ToList()

            };
        }


    }
}
