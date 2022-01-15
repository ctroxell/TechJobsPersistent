using System.ComponentModel.DataAnnotations;

namespace TechJobsPersistent.ViewModels
{
    public class AddEmployerViewModel
    {
        [Required(ErrorMessage = "A name is required for every employer.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "A location is required for every employer.")]
        public string Location { get; set; }

        public AddEmployerViewModel() { }
    }
}
