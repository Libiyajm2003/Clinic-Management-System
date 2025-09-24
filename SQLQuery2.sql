-- Create the main database
CREATE DATABASE cmsv2025db;
USE cmsv2025db;

-- Create table for roles like doctor or receptionist
CREATE TABLE TblRole (
    RoleId INT IDENTITY(1,1) PRIMARY KEY,
    RoleName VARCHAR(50) NOT NULL,
    IsActive BIT DEFAULT 1
);

-- Create table for users with details like name gender and role
CREATE TABLE TblUser (
    UserId INT IDENTITY(1001,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Gender CHAR(1),
    JoiningDate DATE,
    MobileNumber VARCHAR(15),
    UserName VARCHAR(50) UNIQUE,
    Password VARCHAR(50),
    RoleId INT FOREIGN KEY REFERENCES TblRole(RoleId),
    IsActive BIT DEFAULT 1
);

-- Create table for specializations of doctors
CREATE TABLE TblSpecialization (
    SpecializationId INT IDENTITY(1,1) PRIMARY KEY,
    SpecializationName VARCHAR(100) NOT NULL,
    IsActive BIT DEFAULT 1
);

-- Create table for doctors with their user and specialization
CREATE TABLE TblDoctor (
    DoctorId INT IDENTITY(1,1) PRIMARY KEY,
    DoctorName NVARCHAR(50) NOT NULL,
    UserId INT FOREIGN KEY REFERENCES TblUser(UserId),
    SpecializationId INT FOREIGN KEY REFERENCES TblSpecialization(SpecializationId),
    ConsultationFee DECIMAL(10,2),
    IsActive BIT DEFAULT 1
);

-- Create table for membership types for patients
CREATE TABLE TblMembership (
    MembershipId INT IDENTITY(1,1) PRIMARY KEY,
    MembershipType VARCHAR(50),
    IsActive BIT DEFAULT 1
);

-- Create a sequence for automatic patient numbers
CREATE SEQUENCE SeqPatient START WITH 1 INCREMENT BY 1;

-- Create table for patients with automatic MMR number
CREATE TABLE TblPatient (
    MMRNumber NVARCHAR(10) PRIMARY KEY 
        DEFAULT 'MMR' + RIGHT('0000' + CAST(NEXT VALUE FOR SeqPatient AS VARCHAR(4)), 4),
    FullName NVARCHAR(100) NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    Age INT NOT NULL,
    Phone NVARCHAR(15) NOT NULL,
    Address NVARCHAR(250) NULL,
    MembershipId INT NULL FOREIGN KEY REFERENCES TblMembership(MembershipId),
    PatientId INT IDENTITY(1,1) UNIQUE,
    DOB DATE NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

-- Create table for appointments with patient doctor and time details
CREATE TABLE TblAppointment (
    AppointmentId INT IDENTITY(1,1) PRIMARY KEY,
    TokenNo AS ('T' + RIGHT('000' + CAST(AppointmentId AS VARCHAR(3)),3)) PERSISTED UNIQUE,
    AppointmentDate DATE DEFAULT GETDATE(),
    TimeSlot VARCHAR(50),
    ConsultationType VARCHAR(50),
    PatientMMR NVARCHAR(10) FOREIGN KEY REFERENCES TblPatient(MMRNumber),
    DoctorId INT FOREIGN KEY REFERENCES TblDoctor(DoctorId),
    UserId INT FOREIGN KEY REFERENCES TblUser(UserId),
    IsVisited BIT DEFAULT 0,
    IsActive BIT DEFAULT 1
);

-- Create table for consultation notes linked to appointments
CREATE TABLE TblConsultation (
    ConsultationId INT IDENTITY(1,1) PRIMARY KEY,
    AppointmentId INT FOREIGN KEY REFERENCES TblAppointment(AppointmentId),
    Symptoms VARCHAR(200),
    Diagnosis VARCHAR(200),
    Notes VARCHAR(300),
    CreatedDate DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);

-- Create table for medicine categories
CREATE TABLE TblMedicineCategory (
    MedicineCategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName VARCHAR(50)
);

-- Create table for medicines with price and category
CREATE TABLE TblMedicine (
    MedicineId INT IDENTITY(1,1) PRIMARY KEY,
    MedicineName NVARCHAR(100),
    CategoryId INT FOREIGN KEY REFERENCES TblMedicineCategory(MedicineCategoryId),
    Unit VARCHAR(20),
    UnitPrice DECIMAL(10,2) DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);

-- Create table for medicine prescriptions for appointments
CREATE TABLE TblMedicinePrescription (
    MedicinePrescriptionId INT IDENTITY(1,1) PRIMARY KEY,
    MedicineId INT FOREIGN KEY REFERENCES TblMedicine(MedicineId),
    Dosage VARCHAR(50),
    Duration VARCHAR(50),
    Qty INT DEFAULT 1,
    AppointmentId INT FOREIGN KEY REFERENCES TblAppointment(AppointmentId),
    MedicineName NVARCHAR(100) NULL,
    IsActive BIT DEFAULT 1
);

-- Create table for lab test categories
CREATE TABLE TblLabTestCategory (
    LabTestCategoryId INT IDENTITY(1,1) PRIMARY KEY,
    LabTestCategoryName NVARCHAR(50)
);

-- Create table for lab tests with details and category
CREATE TABLE TblLabTest (
    LabTestId INT IDENTITY(1,1) PRIMARY KEY,
    TestName NVARCHAR(100),
    Amount DECIMAL(10,2),
    MinRange NVARCHAR(50),
    MaxRange NVARCHAR(50),
    SampleRequired NVARCHAR(50),
    LabTestCategoryId INT FOREIGN KEY REFERENCES TblLabTestCategory(LabTestCategoryId)
);

-- Create table for lab test prescriptions linked to appointments
CREATE TABLE TblLabPrescription (
    LabTestPrescriptionId INT IDENTITY(1,1) PRIMARY KEY,
    LabTestId INT FOREIGN KEY REFERENCES TblLabTest(LabTestId),
    AppointmentId INT FOREIGN KEY REFERENCES TblAppointment(AppointmentId),
    LabTestValue NVARCHAR(50),
    Remarks NVARCHAR(200),
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- Create table for billing linked to appointments
CREATE TABLE TblBilling (
    BillId INT IDENTITY(1,1) PRIMARY KEY,
    AppointmentId INT FOREIGN KEY REFERENCES TblAppointment(AppointmentId) UNIQUE,
    PatientMMR NVARCHAR(10) FOREIGN KEY REFERENCES TblPatient(MMRNumber),
    DoctorId INT FOREIGN KEY REFERENCES TblDoctor(DoctorId),
    ConsultationFee DECIMAL(10,2),
    MedicineCharges DECIMAL(10,2) DEFAULT 0,
    LabCharges DECIMAL(10,2) DEFAULT 0,
    TotalAmount AS (ConsultationFee + MedicineCharges + LabCharges) PERSISTED,
    BillDate DATETIME DEFAULT GETDATE(),
    IsPaid BIT DEFAULT 0
);

-- Update billing after consultation
CREATE TRIGGER trg_AfterConsultationInsert
ON TblConsultation
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO TblBilling (AppointmentId, PatientMMR, DoctorId, ConsultationFee)
    SELECT i.AppointmentId, a.PatientMMR, a.DoctorId, d.ConsultationFee
    FROM inserted i
    INNER JOIN TblAppointment a ON i.AppointmentId = a.AppointmentId
    INNER JOIN TblDoctor d ON a.DoctorId = d.DoctorId
    WHERE NOT EXISTS (
        SELECT 1 
        FROM TblBilling b
        WHERE b.AppointmentId = i.AppointmentId
    );
END;

-- Update billing after medicines are added
CREATE TRIGGER trg_AfterMedicineInsert
ON TblMedicinePrescription
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE b
    SET b.MedicineCharges = b.MedicineCharges + (m.UnitPrice * i.Qty)
    FROM TblBilling b
    INNER JOIN TblAppointment a ON b.AppointmentId = a.AppointmentId
    INNER JOIN inserted i ON a.AppointmentId = i.AppointmentId
    INNER JOIN TblMedicine m ON i.MedicineId = m.MedicineId;
END;

-- Update billing after lab tests are added
CREATE TRIGGER trg_AfterLabPrescriptionInsert
ON TblLabPrescription
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE b
    SET b.LabCharges = b.LabCharges + t.Amount
    FROM TblBilling b
    INNER JOIN TblAppointment a ON b.AppointmentId = a.AppointmentId
    INNER JOIN inserted i ON a.AppointmentId = i.AppointmentId
    INNER JOIN TblLabTest t ON i.LabTestId = t.LabTestId;
END;

-- Insert default roles
INSERT INTO TblRole (RoleName, IsActive) VALUES
('Receptionist',1),
('Doctor',1);

-- Insert default users
INSERT INTO TblUser (FullName, Gender, JoiningDate, MobileNumber, UserName, Password, RoleId, IsActive) VALUES
('Akhil Receptionist', 'M', GETDATE(), '9876543210', 'Receptionist', 'Akhil@123', 1, 1),
('Dr. Meena', 'F', GETDATE(), '9876500001', 'MeenaD', 'Meena@123', 2, 1),
('Dr. Raj', 'M', GETDATE(), '9876500002', 'RajD', 'Raj@123', 2, 1),
('Dr. Kiran', 'M', GETDATE(), '9876500003', 'KiranD', 'Kiran@123', 2, 1);

-- Insert default specializations
INSERT INTO TblSpecialization (SpecializationName, IsActive) VALUES
('Cardiologist', 1),
('Dermatologist', 1),
('Neurologist', 1);

-- Insert doctors
INSERT INTO TblDoctor (DoctorName, UserId, SpecializationId, ConsultationFee, IsActive) VALUES
('Dr. Meena', 1001, 1, 500, 1),
('Dr. Raj', 1002, 2, 400, 1),
('Dr. Kiran', 1003, 3, 600, 1);

-- Insert membership types
INSERT INTO TblMembership (MembershipType, IsActive) VALUES
('General', 1),
('Premium', 1);

-- Insert patients
INSERT INTO TblPatient (FullName, Gender, Age, Phone, Address, MembershipId, IsActive) VALUES
('John Doe', 'Male', 35, '9876100001', 'Chennai', 1, 1),
('Jane Smith', 'Female', 28, '9876100002', 'Bangalore', 2, 1),
('Alex Kumar', 'Male', 40, '9876100003', 'Hyderabad', 1, 1);

-- Insert appointments
INSERT INTO TblAppointment (TimeSlot, ConsultationType, PatientMMR, DoctorId, UserId, IsVisited, IsActive) VALUES
('10:00 AM', 'InPerson', 'MMR0001', 1, 1001, 0, 1),
('10:15 AM', 'InPerson', 'MMR0002', 2, 1002, 0, 1),
('10:30 AM', 'Online', 'MMR0003', 3, 1003, 0, 1);

-- Insert medicine categories
INSERT INTO TblMedicineCategory (CategoryName) VALUES
('Antibiotics'), ('Painkillers'), ('Vitamins');

-- Insert medicines
INSERT INTO TblMedicine (MedicineName, CategoryId, Unit, UnitPrice, IsActive) VALUES
('Amoxicillin', 1, 'Tablet', 20, 1),
('Paracetamol', 2, 'Tablet', 10, 1),
('Vitamin C', 3, 'Capsule', 15, 1);

-- Insert medicine prescriptions for appointments
INSERT INTO TblMedicinePrescription (MedicineId, Dosage, Duration, Qty, AppointmentId, IsActive) VALUES
(1, '500mg', '5 days', 10, 2, 1),
(2, '650mg', '3 days', 6, 3, 1),
(3, '1 Capsule', '7 days', 7, 1, 1);

-- Insert lab test categories
INSERT INTO TblLabTestCategory (LabTestCategoryName) VALUES
('Blood Test'), ('Urine Test');

-- Insert lab tests
INSERT INTO TblLabTest (TestName, Amount, MinRange, MaxRange, SampleRequired, LabTestCategoryId) VALUES
('CBC', 300, '4.0', '11.0', 'Blood', 1),
('Urine Routine', 200, 'Normal', 'Abnormal', 'Urine', 2);

-- Insert lab prescriptions
INSERT INTO TblLabPrescription (LabTestId, AppointmentId, LabTestValue, Remarks) VALUES
(1, 2, '5.5', 'Normal'),
(2, 3, '3.5', 'All good');

-- Insert billing records
INSERT INTO TblBilling (AppointmentId, PatientMMR, DoctorId, ConsultationFee, MedicineCharges, LabCharges, IsPaid) VALUES
(2, 'MMR1001', 1, 500, 200, 300, 0),
(3, 'MMR1002', 2, 400, 60, 200, 0),
(1, 'MMR1003', 3, 600, 105, 0, 1);

-- Check all patients
SELECT * FROM TblPatient;
