﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            List<Employer> employers = context.Employers.ToList();
            List<Skill> skills = context.Skills.ToList();
            AddJobViewModel addJobViewModel = new AddJobViewModel(employers, skills);
            return View(addJobViewModel);
        }

        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string[] selectedSkills)
        {
            Employer theEmployer = context.Employers.Find(addJobViewModel.EmployerId);

            if (ModelState.IsValid)
            {
                Job newJob = new Job
                {
                    Name = addJobViewModel.JobName,
                    EmployerId = addJobViewModel.EmployerId,
                    Employer = theEmployer
                };
                
                foreach (string skill in selectedSkills)
                {
                    JobSkill newJobSkill = new JobSkill
                    {
                        Job = newJob,
                        JobId = newJob.Id,
                        SkillId = Int32.Parse(skill)
                    };

                    context.JobSkills.Add(newJobSkill);
                }

                context.Jobs.Add(newJob);
                context.SaveChanges();
            }
            

            
        

            List<Skill> skills = context.Skills.ToList();

            addJobViewModel.Skills = skills;

            List<Employer> employers = context.Employers.ToList();

            addJobViewModel.CreateSelectListItems(employers);

           

            return View("~/Views/Home/AddJob.cshtml", addJobViewModel);
        }
        

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
