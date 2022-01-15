using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TechJobsPersistent.Data;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        public string JobName { get; set; }
        public int EmployerId { get; set; }
        public List<SelectListItem> Employers { get; set; }
        public List<Skill> Skills { get; set; }
        public AddJobViewModel() { }
        public AddJobViewModel(List<Employer> employers, List<Skill> skills) {

            CreateSelectListItems(employers);
            Skills = skills;
        }

        public void CreateSelectListItems(List<Employer> employers)
        {
            Employers = new List<SelectListItem>();

            foreach (Employer employer in employers)
            {
                Employers.Add(
                    new SelectListItem
                    {
                        Value = employer.Id.ToString(),
                        Text = employer.Name
                    });
            }
        }

    }
}
