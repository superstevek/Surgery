using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EurothermSurgery;
using EurothermSurgery.Models;
using EurothermSurgery.Models.ViewModels;


namespace EurothermSurgery.Controllers
{
    public class HomeController : Controller
    {

        #region Public GETS
        public ActionResult Index()
        {

            ViewBag.Title = "Registered Patients";

            HomeViewModel model = new HomeViewModel();

            //Ordered here by Surname, Forename, DofB (descending, assume older people visit doctor more often, check assumption with Analysts)
            model.RegisteredPatients = Registering.Instance.GetPatients().OrderBy(p => p.Surname).ThenBy(p => p.Forename).ThenByDescending(p => p.DateOfBirth);

            return View("Index", model);
        }


        public ActionResult AddPatient()
        {
            Patient model = new Patient();
            return PartialView("Partials/_PatientAddEdit", model);
        }

        public ActionResult UpdatePatient(int patientId)
        {
            Patient model = Registering.Instance.ReadPatient(patientId);
            return PartialView("Partials/_PatientAddEdit", model);
        }
        #endregion Public GETS


        #region Public POSTS
        [HttpPost]
        public ActionResult AddEditPatient(Patient model)
        {
            if (ModelState.IsValid)
            {
                if (model.PatientId == 0)
                {
                    //This is a new Patient
                    Registering.Instance.CreatePatient(model);
                    return Json(new { url = Url.Action("Index") });
                }
                else
                {
                    //This is update to an existing system
                    Registering.Instance.UpdatePatient(model);
                    return Json(new { url = Url.Action("Index") });
                }
            }

            return PartialView("Partials/_PatientAddEdit", model);
        }

        #endregion Public GETS

        #region Dummy Data Setup
        //This region would be removed prior to release
        public ActionResult AddTestData()
        {

            SetupTestData();

            return RedirectToAction("Index");
        }


        private void SetupTestData()
        {
            Patient test1 = new Patient();
            test1.Forename = "Bobby";
            test1.Surname = "Pilsbury";
            test1.Note = "Nam iaculis ante ac bibendum consectetur. Ut sed ornare felis, et bibendum dui. Suspendisse tempus sodales tellus, vitae scelerisque mi pharetra placerat. In commodo et dui vel blandit. Donec metus purus, fermentum vel dictum quis, faucibus quis mi. Mauris rutrum hendrerit est luctus tempor. Donec magna ex, pellentesque a quam ac, iaculis rhoncus tellus. ";
            test1.DateOfBirth = Convert.ToDateTime("14/06/1977");
            Registering.Instance.CreatePatient(test1);

            Patient test2 = new Patient();
            test2.Forename = "Harry";
            test2.Surname = "Randomovski";
            test2.Note = "Mauris bibendum turpis ut aliquam eleifend. Maecenas urna ex, pellentesque a mi et, tempus dignissim libero. Maecenas sed turpis orci. Sed pellentesque et nisl eu mollis. Nulla a vulputate elit, eget dapibus lacus. Pellentesque rutrum turpis eget eros suscipit pellentesque. Sed in nunc neque. Phasellus in viverra diam. Praesent congue turpis id ullamcorper sagittis.";
            test2.DateOfBirth = Convert.ToDateTime("15/06/1977");
            Registering.Instance.CreatePatient(test2);

            Patient test3 = new Patient(); 
            test3.Forename = "Lisa";
            test3.Surname = "Carliggy";
            test3.Note = "Nam non erat consequat, laoreet odio in, tincidunt ex. Donec ultrices justo vel placerat mattis. In hac habitasse platea dictumst. Nulla facilisi. Etiam ligula felis, pretium at tempor eu, luctus nec mi. Vivamus lobortis a diam eget gravida. Morbi interdum, neque sed vehicula semper, ex augue luctus erat, non ultricies nunc lectus sit amet justo. Integer tempus dolor lectus, at tempor enim lacinia ut. Mauris eget lectus sed elit pretium consequat. ";
            test3.DateOfBirth = Convert.ToDateTime("16/06/1977");
            Registering.Instance.CreatePatient(test3);

            Patient test4 = new Patient();
            test4.Forename = "Rose";
            test4.Surname = "Newmanns";
            test4.Note = "Cras nec velit vitae dui condimentum luctus ut sit amet nunc. In bibendum ultrices elit vitae viverra. Suspendisse augue enim, ultrices in nibh sit amet, tincidunt blandit ipsum. Sed luctus fringilla erat, vel venenatis ante venenatis in. Mauris pretium elementum hendrerit. Mauris elementum libero vitae felis fermentum accumsan. Mauris consequat diam arcu, ac dictum tortor pulvinar a. ";
            test4.DateOfBirth = Convert.ToDateTime("17/06/1977");
            Registering.Instance.CreatePatient(test4);

            Patient test5 = new Patient();
            test5.Forename = "Winston";
            test5.Surname = "Jones";
            test5.Note = "Ut dapibus faucibus erat sed tincidunt. Morbi vel tempus turpis. Duis euismod tellus nec arcu blandit, sed maximus lorem finibus. Nam suscipit egestas cursus. Aliquam varius metus id turpis laoreet dictum. Aliquam vel condimentum metus. In fringilla vulputate ipsum in commodo. ";
            test5.DateOfBirth = Convert.ToDateTime("18/06/1977");
            Registering.Instance.CreatePatient(test5);

            Patient test6 = new Patient();
            test6.Forename = "Lloyd";
            test6.Surname = "Farhar-Etams";
            test6.Note = "Quisque enim tortor, aliquam eget efficitur quis, commodo ac magna. Donec lobortis, ex et vehicula facilisis, mauris libero porta sem, a scelerisque libero mauris nec justo. Morbi non lacinia quam, vitae sollicitudin diam. Aliquam erat volutpat. Fusce feugiat, risus ac convallis ultrices, sapien massa tempor lacus, eget maximus justo massa vel elit. Vivamus vestibulum erat vitae eros feugiat eleifend.";
            test6.DateOfBirth = Convert.ToDateTime("19/06/1977");
            Registering.Instance.CreatePatient(test6);
        }

        #endregion Dummy Data Setup

    }
}
