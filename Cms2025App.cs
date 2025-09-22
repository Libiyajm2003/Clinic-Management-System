using ConsoleAppCms2025.Model;
using ConsoleAppCms2025.Repository;
using ConsoleAppCms2025.Service;
using ConsoleAppCms2025.Utility;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleAppCms2025
{
    public class Cms2025App
    {
        private static readonly string _connectionString =
            System.Configuration.ConfigurationManager.ConnectionStrings["CsWinSql"].ConnectionString;

        static async Task Main(string[] args)
        {
            while (true)
            {
            lblUserName:
                Console.Clear();
                Console.WriteLine("-------------------");
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("  L O G I N   ");
                Console.ResetColor();
                Console.WriteLine("-------------------");

                Console.Write("Enter Username: ");
                string userName = Console.ReadLine();

                if (!CustomValidation.IsValidUserName(userName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid user name, try again");
                    Console.ResetColor();
                    goto lblUserName;
                }

            lblPassword:
                Console.Write("Enter Password: ");
                string password = CustomValidation.ReadPassword();

                if (!CustomValidation.IsValidPassword(password))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Password, try again");
                    Console.ResetColor();
                    goto lblPassword;
                }

                IUserService userService = new UserServiceImpl();
                User user = await userService.LoginAsync(userName, password);

                if (user == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid credentials. Try again.");
                    Console.ResetColor();
                    continue;
                }

                int roleId = user.RoleId;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Welcome {user.FullName}!");
                Console.ResetColor();

                if (roleId == 1)
                    await ShowReceptionistDashboardAsync(user);
                else if (roleId == 2)
                    await ShowDoctorDashboardAsync(user);
                else
                    Console.WriteLine("Role not recognized.");
            }
        }

        #region Receptionist Dashboard
        private static async Task ShowReceptionistDashboardAsync(User user)
        {
            IPatientRepository patientRepo = new PatientRepositoryImpl();
            IPatientService patientService = new PatientServiceImpl(patientRepo);

            IAppointmentRepository appointmentRepo = new AppointmentRepositoryImpl();
            IAppointmentService appointmentService = new AppointmentServiceImpl(appointmentRepo);

            IBillingRepository billingRepo = new BillingRepositoryImpl();
            IBillingService billingService = new BillingServiceImpl(billingRepo);

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n----- RECEPTIONIST DASHBOARD -----");
                Console.WriteLine("1. Search Patient by MMR");
                Console.WriteLine("2. Search Patient by Phone");
                Console.WriteLine("3. Register Patient");
                Console.WriteLine("4. View Patient Bills by MMR");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter MMR Number: ");
                        string mmrSearch = Console.ReadLine();
                        var patientByMMR = await patientService.GetPatientByMMRAsync(mmrSearch);
                        if (patientByMMR != null)
                        {
                            DisplayPatientInfo(patientByMMR);
                            await ShowPatientMenuAsync(patientByMMR, appointmentService, user);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Patient not found.");
                            Console.ResetColor();
                        }
                        break;

                    case "2":
                        Console.Write("Enter Phone Number: ");
                        string phoneSearch = Console.ReadLine();
                        var patientsByPhone = await patientService.GetPatientsByPhoneAsync(phoneSearch);

                        if (patientsByPhone != null && patientsByPhone.Count > 0)
                        {
                            if (patientsByPhone.Count == 1)
                            {
                                var patient = patientsByPhone[0];
                                DisplayPatientInfo(patient);
                                await ShowPatientMenuAsync(patient, appointmentService, user);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Multiple patients found with this phone number:");
                                Console.ResetColor();

                                for (int i = 0; i < patientsByPhone.Count; i++)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"{i + 1}. MMR: {patientsByPhone[i].MMRNumber}, Name: {patientsByPhone[i].FullName}, Age: {patientsByPhone[i].Age}, Gender: {patientsByPhone[i].Gender}");
                                    Console.ResetColor();

                                }

                                Console.Write("Choose a patient (enter number): ");
                                if (int.TryParse(Console.ReadLine(), out int selectedIndex) &&
                                    selectedIndex > 0 && selectedIndex <= patientsByPhone.Count)
                                {
                                    var selectedPatient = patientsByPhone[selectedIndex - 1];
                                    DisplayPatientInfo(selectedPatient);
                                    await ShowPatientMenuAsync(selectedPatient, appointmentService, user);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Invalid selection.");
                                    Console.ResetColor();
                                }
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Patient not found.");
                            Console.ResetColor();
                        }
                        break;

                    case "3":
                        await RegisterPatientAsync(patientService, appointmentService, user);
                        break;

                    case "4": // View Patient Bills
                        Console.Write("Enter Patient MMR: ");
                        string mmr = Console.ReadLine();
                        var bills = await billingService.GetBillingsByPatientMMRAsync(mmr);

                        if (bills.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No bills found for this patient.");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{"BillId",-8} {"AppointmentId",-12} {"DoctorId",-8} {"ConsultationFee",-15} {"BillDate",-12}");
                            foreach (var bill in bills)
                            {
                                Console.WriteLine($"{bill.BillId,-8} {bill.AppointmentId,-12} {bill.DoctorId,-8} {bill.ConsultationFee,-15:C} {bill.BillDate:dd-MM-yyyy}");
                            }
                            Console.ResetColor();
                        }
                        break;

                    case "5":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        private static async Task RegisterPatientAsync(IPatientService patientService, IAppointmentService appointmentService, User user)
        {
            Patient newPatient = new Patient();
            newPatient.MMRNumber = await patientService.GenerateNextMMRNumberAsync();
            Console.WriteLine($"Generated MMR Number: {newPatient.MMRNumber}");

            Console.Write("Enter Full Name: ");
            newPatient.FullName = Console.ReadLine();

            Console.Write("Enter Gender: ");
            newPatient.Gender = Console.ReadLine();

            DateTime dob;
            while (true)
            {
                Console.Write("Enter Date of Birth (dd-MM-yyyy): ");
                string dobInput = Console.ReadLine();
                if (DateTime.TryParseExact(dobInput, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out dob))
                {
                    if (dob > DateTime.Now)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid DOB. Cannot be in the future.");
                        Console.ResetColor();
                        continue;
                    }

                    int calculatedAge = DateTime.Now.Year - dob.Year;
                    if (dob.AddYears(calculatedAge) > DateTime.Now) calculatedAge--;

                    if (calculatedAge < 0 || calculatedAge > 120)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Age. Must be between 0 and 120.");
                        Console.ResetColor();
                        continue;
                    }

                    newPatient.Age = calculatedAge;
                    newPatient.DOB = dob;

                    // Show calculated age
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Calculated Age: {newPatient.Age}");
                    Console.ResetColor();

                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid DOB format.");
                    Console.ResetColor();
                }
            }

            Console.Write("Enter Phone: ");
            newPatient.Phone = Console.ReadLine();

            Console.Write("Enter Address: ");
            newPatient.Address = Console.ReadLine();

            Console.Write("Enter Membership ID (or leave blank): ");
            string memInput = Console.ReadLine();
            newPatient.MembershipId = string.IsNullOrEmpty(memInput) ? null : int.Parse(memInput);

            newPatient.IsActive = true;

            int added = await patientService.AddPatientAsync(newPatient);
            Console.ForegroundColor = added > 0 ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(added > 0 ? "Patient registered successfully!" : "Failed to register patient.");
            Console.ResetColor();

            if (added > 0)
                await ShowPatientMenuAsync(newPatient, appointmentService, user);
        }

        private static void DisplayPatientInfo(Patient patient)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(
                $"MMR: {patient.MMRNumber}, " +
                $"Name: {patient.FullName}, " +
                $"Gender: {patient.Gender}, " +
                $"DOB: {patient.DOB:dd-MM-yyyy}, " +
                $"Age: {patient.Age}, " +
                $"Phone: {patient.Phone}, " +
                $"Address: {patient.Address}, " +
                $"Membership: {patient.MembershipId}"
            );
            Console.ResetColor();
        }

        private static async Task ShowPatientMenuAsync(Patient patient, IAppointmentService appointmentService, User user)
        {
            bool back = false;

            while (!back)
            {
                Console.WriteLine("\n----- PATIENT MENU -----");
                Console.WriteLine("1. View Patient Details");
                Console.WriteLine("2. Show Doctors (Today & Tomorrow)");
                Console.WriteLine("3. Book Appointment");
                Console.WriteLine("4. Back to Reception Dashboard");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayPatientInfo(patient);
                        break;

                    case "2":
                        IDoctorRepository doctorRepo = new DoctorRepositoryImpl();
                        IDoctorService doctorService = new DoctorServiceImpl(doctorRepo);

                        var doctors = await doctorService.GetAllDoctorsAsync();
                        Console.WriteLine("\nAvailable Doctors:");
                        foreach (var doc in doctors)
                        {
                            Console.WriteLine($"{doc.DoctorId}. {doc.DoctorName} ({doc.SpecializationName}) - Fee: {doc.ConsultationFee:C}");
                        }
                        break;

                    case "3":
                        Appointment newAppointment = new Appointment();
                        newAppointment.PatientMMR = patient.MMRNumber;
                        newAppointment.UserId = user.UserId;

                        Console.Write("Enter Doctor ID: ");
                        newAppointment.DoctorId = int.Parse(Console.ReadLine());

                        while (true)
                        {
                            Console.Write("Enter Appointment Schedule (Today/tomorrow): ");
                            string dateInput = Console.ReadLine().ToLower();
                            if (dateInput == "today")
                            {
                                newAppointment.AppointmentDate = DateTime.Today;
                                break;
                            }
                            else if (dateInput == "tomorrow")
                            {
                                newAppointment.AppointmentDate = DateTime.Today.AddDays(1);
                                break;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid input. Enter 'Today' or 'Tomorrow'.");
                                Console.ResetColor();
                            }
                        }

                        Console.Write("Enter Time Slot (e.g., 10:00 AM - 10:30 AM): ");
                        newAppointment.TimeSlot = Console.ReadLine();

                        Console.Write("Enter Consultation Type (General/Follow-up): ");
                        newAppointment.ConsultationType = Console.ReadLine();

                        newAppointment.IsVisited = false;

                        var booked = await appointmentService.BookAppointmentAsync(newAppointment);

                        if (booked != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Appointment Booked Successfully!");
                            Console.WriteLine($"AppointmentId: {booked.AppointmentId}, Token: {booked.TokenNo}, Date: {booked.AppointmentDate:dd-MM-yyyy}, Time: {booked.TimeSlot}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Failed to book appointment.");
                            Console.ResetColor();
                        }
                        break;

                    case "4":
                        back = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }
        #endregion

        #region Doctor Dashboard
        private static async Task ShowDoctorDashboardAsync(User user)
        {
            IAppointmentRepository appointmentRepo = new AppointmentRepositoryImpl();
            IAppointmentService appointmentService = new AppointmentServiceImpl(appointmentRepo);

            IConsultationService consultationService = new ConsultationServiceImpl();

            IPrescriptionRepository prescriptionRepo = new PrescriptionRepositoryImpl();
            IPrescriptionService prescriptionService = new PrescriptionServiceImpl(prescriptionRepo);

            ILabResultRepository labRepo = new LabResultRepositoryImpl();
            ILabResultService labResultService = new LabResultServiceImpl(labRepo);

            IDoctorService doctorServiceFull = new DoctorServiceImpl(new DoctorRepositoryImpl());

            int doctorId = 0;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                string query = "SELECT DoctorId FROM TblDoctor WHERE UserId=@UserId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", user.UserId);
                    object result = await cmd.ExecuteScalarAsync();
                    if (result != null)
                        doctorId = Convert.ToInt32(result);
                }
            }

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n----- DOCTOR DASHBOARD -----");
                Console.WriteLine("1. View My Appointments");
                Console.WriteLine("2. Add Consultation");
                Console.WriteLine("3. Prescribe Medicine");
                Console.WriteLine("4. Add Lab Result");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": // View Appointments
                        await DisplayAppointmentsAsync(appointmentService, doctorId);
                        break;

                    case "2": // Add Consultation
                        await AddConsultationAsync(appointmentService, consultationService);
                        break;

                    case "3": // Prescribe Medicine
                        await AddPrescriptionAsync(prescriptionService);
                        break;

                    case "4": // Add Lab Result
                        await AddLabResultAsync(labResultService);
                        break;

                    case "5":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        private static async Task DisplayAppointmentsAsync(IAppointmentService appointmentService, int doctorId)
        {
            var appointments = await appointmentService.GetAppointmentsByDoctorAsync(doctorId);

            if (appointments.Count == 0)
            {
                Console.WriteLine("No appointments found.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n--- Not Visited Appointments ---");
            Console.WriteLine($"{"AppId",-8} {"Token",-8} {"Date",-12} {"Time",-10} {"PatientMMR",-12}");
            Console.ResetColor();

            foreach (var appt in appointments.FindAll(x => !x.IsVisited))
            {
                string dateStr = appt.AppointmentDate.ToString("dd-MM-yyyy");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{appt.AppointmentId,-8} {appt.TokenNo,-8} {dateStr,-12} {appt.TimeSlot,-10} {appt.PatientMMR,-12}");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n--- Visited Appointments ---");
            Console.WriteLine($"{"AppId",-8} {"Token",-8} {"Date",-12} {"Time",-10} {"PatientMMR",-12}");
            Console.ResetColor();

            foreach (var appt in appointments.FindAll(x => x.IsVisited))
            {
                string dateStr = appt.AppointmentDate.ToString("dd-MM-yyyy");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{appt.AppointmentId,-8} {appt.TokenNo,-8} {dateStr,-12} {appt.TimeSlot,-10} {appt.PatientMMR,-12}");
                Console.ResetColor();
            }
        }

        private static async Task AddConsultationAsync(IAppointmentService appointmentService, IConsultationService consultationService)
        {
            int consultAppointmentId = ReadIntInput("Enter Appointment ID: ");
            string symptoms = ReadStringInput("Symptoms: ");
            string diagnosis = ReadStringInput("Diagnosis: ");
            string notes = ReadStringInput("Notes: ");

            await consultationService.AddConsultationAsync(consultAppointmentId, symptoms, diagnosis, notes);
            await appointmentService.MarkAsVisitedAsync(consultAppointmentId);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Consultation added and appointment marked as visited.");
            Console.ResetColor();
        }

        private static async Task AddPrescriptionAsync(IPrescriptionService prescriptionService)
        {
            int prescAppointmentId = ReadIntInput("Enter Appointment ID: ");
            string medicineName = ReadStringInput("Medicine Name: ");
            string dosage = ReadStringInput("Dosage: ");
            string duration = ReadStringInput("Duration (e.g., 5 days): ");
            int qty = ReadIntInput("Quantity: ");

            await prescriptionService.AddPrescriptionAsync(prescAppointmentId, medicineName, dosage, duration, qty);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Prescription added: AppointmentId={prescAppointmentId}, MedicineName={medicineName}, Dosage={dosage}, Duration={duration}, Qty={qty}");
            Console.ResetColor();
        }

        private static async Task AddLabResultAsync(ILabResultService labResultService)
        {
            int labAppointmentId = ReadIntInput("Enter Appointment ID: ");
            string labTestName = ReadStringInput("Lab Test Name: ");
            string resultValue = ReadStringInput("Lab Test Value: ");
            string remarks = ReadStringInput("Remarks: ");

            await labResultService.AddLabResultAsync(labAppointmentId, labTestName, resultValue, remarks);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Lab result added.");
            Console.ResetColor();
        }
        #endregion

        private static int ReadIntInput(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out value))
                    return value;
                Console.WriteLine("Invalid input. Enter a number.");
            }
        }

        private static string ReadStringInput(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This field cannot be empty. Please enter a value.");
                    Console.ResetColor();
                }
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }

    }
}
