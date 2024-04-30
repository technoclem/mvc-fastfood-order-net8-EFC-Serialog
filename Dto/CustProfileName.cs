using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FastFood.Dto
{

   
    public class CustProfileName
    {        
        public required  int CustId { get; set; }

        public required  string CustName { get; set; }
       
    }
}
