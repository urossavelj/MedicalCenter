using MedicalCenter.Db;
using MedicalCenter.Db.Models;
using MedicalCenter.Interfaces;
using MedicalCenter.Requests;
using Microsoft.EntityFrameworkCore;

namespace MedicalCenter.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(ILogger<AppointmentService> logger)
        {
            _logger = logger;
        }

        public List<Appointments> GetAppointments()
        {
            using (var db = new Context())
            {
                var appointments = db.Appointments.ToList();
                return appointments;
            }
        }

        public List<Appointments> GetDoctorsAppointments(int userId)
        {
            using (var db = new Context())
            {
                var doctor = db.Doctors.Where(d => d.UserId == userId).FirstOrDefault();
                var appointments = db.Appointments.Where(x => x.DoctorId == doctor.Id).ToList();
                return appointments;
            }
        }

        public List<Appointments> GetBookedAppointmentsForDoctorForTimeFrame(GetDoctorAppointmentsRequest request)
        {
            using (var db = new Context())
            {
                var appointments = db.Appointments.Where(x =>
                x.DoctorId == request.DoctorId &&
                x.AppointmentDateTime >= request.From &&
                x.AppointmentDateTime <= request.To)
                    .ToList();
                return appointments;
            }
        }

        public bool CreateAppointment(CreateAppointmentRequest request)
        {
            using (var db = new Context())
            {
                var existingAppointment = db.Appointments.Where(x =>
                x.AppointmentDateTime <= request.BookingTime.AddMinutes(30) &&
                x.AppointmentDateTime >= request.BookingTime.AddMinutes(-30)
                ).FirstOrDefault();

                if (existingAppointment != null)
                {
                    _logger.LogError("Trying to overbook an appointment");
                    return false;
                }
                else
                {
                    var appointment = new Appointments
                    {
                        AppointmentDateTime = request.BookingTime,
                        DoctorId = request.DoctorId,
                        PatientId = request.PatientId,
                    };

                    db.Appointments.Add(appointment);
                    var response = db.SaveChanges();

                    if (response == 1)
                        return true;
                    else
                    {
                        _logger.LogError("An error occured when creating an appointment");
                        return false;
                    }
                }
            }
        }

        public bool UpdateAppointmentTime(UpdateAppointmentRequest request)
        {
            using (var db = new Context())
            {
                var appointment = db.Appointments.Where(x => x.Id == request.AppointmentId).FirstOrDefault();

                if (appointment != null)
                {
                    appointment.AppointmentDateTime = request.BookingTime;

                    db.Appointments.Update(appointment);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    _logger.LogError("Appointment doesn't exist");
                    return false;
                }
            }
        }

        public bool DeleteAppointment(int appointmentId, int userId)
        {
            using (var db = new Context())
            {
                var user = db.Users.Where(u => u.Id == userId).FirstOrDefault();
                var userType = user?.UserType;

                if (user == null)
                {
                    _logger.LogError("User doesn't exist");
                    return false;
                }

                Appointments appointment = null;

                switch (userType)
                {
                    case Enums.UserType.Doctor:
                        appointment = db.Appointments.Include(app => app.Doctor).Where(x => x.Id == appointmentId && x.Doctor.UserId == userId).FirstOrDefault();
                        break;
                    case Enums.UserType.Patient:
                        appointment = db.Appointments.Include(app => app.Patient).Where(x => x.Id == appointmentId && x.Patient.UserId == userId).FirstOrDefault();
                        break;
                }

                if (appointment != null)
                {
                    db.Appointments.Remove(appointment);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    _logger.LogError("Appointment doesn't exist");
                    return false;
                }
            }
        }
    }
}
