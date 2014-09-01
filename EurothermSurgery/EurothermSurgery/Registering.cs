using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EurothermSurgery.Models;
using System.Globalization;
using System.Threading;

namespace EurothermSurgery
{
    public class Registering
    {
        // A private instance of the class
        private static Registering instance;
        private List<Patient> Patients;

        // The constructor for this message is private, so that cannot be instantiated
        private Registering()
        {
            try
            {
                Patients =  new List<Patient>();
            }
            catch { }
        }
       
        // This private static property, will check if the static instance of the registration
        // is instantiated if it is then it will return the static instance, if not it will
        // create a new instance.
        public static Registering Instance
        {
            get
                {
                if (instance == null)
                instance = new Registering();
                return instance;
                }
        }

        /// <summary>
        /// Returns a list of all the Patients currently registered
        /// </summary>
        /// <returns><c>List<Patient></c></returns>
        public List<Patient> GetPatients()
        {
            return Patients;
        }

        /// <summary>
        /// Adds a new Patient to the Patients collection
        /// </summary>
        /// <param name="patient">The new patient to be registered.
        public void CreatePatient(Patient patient)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            patient.Forename = textInfo.ToTitleCase(patient.Forename.ToLower());
            patient.Surname = textInfo.ToTitleCase(patient.Surname.ToLower());

            patient.PatientId = GetNextPatientId();
            Patients.Add(patient);
        }

        /// <summary>
        /// Updates an existing Patient
        /// </summary>
        /// <param name="patient">The new details to be stored for the patient
        public void UpdatePatient(Patient patient)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            Patient currentPatient = Patients.Where(p => p.PatientId == patient.PatientId).FirstOrDefault();
            currentPatient.Forename = textInfo.ToTitleCase(patient.Forename.ToLower());
            currentPatient.Surname = textInfo.ToTitleCase(patient.Surname.ToLower());
            currentPatient.DateOfBirth = patient.DateOfBirth;
            currentPatient.Note = patient.Note;
        }

        /// <summary>
        /// Get the patient data for the supplied patientId
        /// </summary>
        /// <param name="patientId">The patientId (unique identifier) for the patient to retrieve
        /// <returns><c>Patient</c></returns>
        public Patient ReadPatient(int patientId)
        {
            return  Patients.Where(p => p.PatientId == patientId).FirstOrDefault();
        }

        /// <summary>
        /// Checks to see if the patient details already exist on the system
        /// </summary>
        /// <param name="forename">The patients Forename
        /// <param name="surname">The patients Surname
        /// <param name="dateOfBirth">The patients Date of Birth
        /// <returns><c>true</c> if the patient already exists in the collection; otherwise <c>false</c>.</returns>
        public bool PatientExists(string forename, string surname, DateTime? dateOfBirth)
        {
            Patient patient = Patients.Where(p => p.Forename.ToLower() == forename.ToLower() && p.Surname.ToLower() == surname.ToLower() && p.DateOfBirth == dateOfBirth).FirstOrDefault();
            return (patient != null);
        }

        /// <summary>
        /// Gets the next available unique identifier for the patient.  This is mainly here to allow the applocation
        /// to be easily enhanced to store more information against the patient.
        /// </summary>
        /// <returns><c>patientId</c> the next available unique patient identifier.</returns>
        //This method wouldnt be needed if using a DB where PatientId would be an identity column
        public int GetNextPatientId()
        {
            Patient latestPatient = Patients.OrderByDescending(p => p.PatientId).FirstOrDefault();
            return (latestPatient == null ? 1 : latestPatient.PatientId++);
        }
    }   


}