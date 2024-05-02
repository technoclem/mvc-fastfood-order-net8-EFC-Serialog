using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FastFoodEFC.Dto
{
    public class ProflileImage
    {
        [Required(ErrorMessage = "Profile Image required")]
        [DisplayName("Profile Image")]
        public IFormFile? ImageFile { get; set; }
    }
}
