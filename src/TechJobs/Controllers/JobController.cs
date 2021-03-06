﻿using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;
using System.Collections.Generic;
using System.Linq;


namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;
       
        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view
            Job job = jobData.Find(id);

            return View(job);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.

            
            if (ModelState.IsValid)
            {
                Employer anEmployer = jobData.Employers.Find(newJobViewModel.EmployerID);
                Location aLocation = jobData.Locations.Find(newJobViewModel.LocationID);
                PositionType aPosition = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID);
                CoreCompetency aCoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetenciesID);

                Job job = new Job
                {
                    Name = newJobViewModel.Name,
                    Employer = anEmployer,
                    Location = aLocation,
                    PositionType = aPosition,
                    CoreCompetency = aCoreCompetency
                };
                jobData.Jobs.Add(job);

                return Redirect(string.Format("/Job?id={0}",job.ID));

            }
            return View(newJobViewModel);
        }
    }
}
