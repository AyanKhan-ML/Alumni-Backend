using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Directories.Services.DTO
{
    public class NewUserDTO
    {
        public required string Individual_Institution_ID { get; set; }
        public required string Individual_Name { get; set; }
        public string? Individual_Email { get; init; }

        public required int Individual_Type_ID { get; set; }
        public required string Individual_Type_Value { get; set; }
        public string? Individual_Contact_Number_Primary { get; init; }
        public bool Individual_Is_Alumni { get; init; }

        public required int Client_ID { get; set; }

        public required int Campus_ID { get; set; }

        public string Client_Reference_Key { get; set; } = string.Empty;

        public string Campus_Reference_Key { get; set; } = string.Empty;


        public IEnumerable<ProgramInfoDTO>? Academic_Details { get; init; }

    }


    
    
}
