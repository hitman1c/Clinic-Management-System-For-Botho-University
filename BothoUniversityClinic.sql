
-- BothoUniversityClinic.sql
CREATE DATABASE IF NOT EXISTS `BothoUniversityClinic` DEFAULT CHARACTER SET = 'utf8mb4';
USE `BothoUniversityClinic`;

-- Roles
CREATE TABLE IF NOT EXISTS Roles (
  RoleId INT AUTO_INCREMENT PRIMARY KEY,
  RoleName VARCHAR(50) NOT NULL UNIQUE
);
INSERT INTO Roles (RoleName) VALUES ('Student'),('Provider'),('Administrator'),('Receptionist') ON DUPLICATE KEY UPDATE RoleName=RoleName;

-- Users
CREATE TABLE IF NOT EXISTS Users (
  UserId INT AUTO_INCREMENT PRIMARY KEY,
  Username VARCHAR(80) NOT NULL UNIQUE,
  Fullname VARCHAR(200),
  RoleId INT NOT NULL,
  PasswordHash VARCHAR(512) NOT NULL,
  MustChangePassword TINYINT(1) DEFAULT 1,
  IsActive TINYINT(1) DEFAULT 1,
  Contact VARCHAR(150),
  CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);

-- Departments
CREATE TABLE IF NOT EXISTS Departments (
  DepartmentId INT AUTO_INCREMENT PRIMARY KEY,
  Name VARCHAR(120) NOT NULL,
  Description TEXT
);

-- Staff
CREATE TABLE IF NOT EXISTS Staff (
  StaffId INT AUTO_INCREMENT PRIMARY KEY,
  UserId INT NOT NULL,
  DepartmentId INT,
  Position VARCHAR(120),
  FOREIGN KEY (UserId) REFERENCES Users(UserId),
  FOREIGN KEY (DepartmentId) REFERENCES Departments(DepartmentId)
);

-- Patients (students)
CREATE TABLE IF NOT EXISTS Patients (
  PatientId INT AUTO_INCREMENT PRIMARY KEY,
  UserId INT NOT NULL,
  StudentNumber VARCHAR(50),
  DateOfBirth DATE,
  Gender VARCHAR(10),
  Address TEXT,
  EmergencyContact VARCHAR(200),
  FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- Medical services & pricing
CREATE TABLE IF NOT EXISTS Medical_Services (
  ServiceId INT AUTO_INCREMENT PRIMARY KEY,
  Name VARCHAR(150) NOT NULL,
  Description TEXT,
  Code VARCHAR(50)
);
CREATE TABLE IF NOT EXISTS Service_Pricing (
  PricingId INT AUTO_INCREMENT PRIMARY KEY,
  ServiceId INT NOT NULL,
  Price DECIMAL(10,2) NOT NULL,
  EffectiveFrom DATE,
  FOREIGN KEY (ServiceId) REFERENCES Medical_Services(ServiceId)
);

-- Appointments and history
CREATE TABLE IF NOT EXISTS Appointments (
  AppointmentId INT AUTO_INCREMENT PRIMARY KEY,
  PatientId INT NOT NULL,
  ProviderId INT,
  AppointmentDate DATETIME NOT NULL,
  TimeSlot VARCHAR(50),
  Reason TEXT,
  Status VARCHAR(30) DEFAULT 'Scheduled',
  CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (PatientId) REFERENCES Patients(PatientId),
  FOREIGN KEY (ProviderId) REFERENCES Staff(StaffId)
);

-- Consultations
CREATE TABLE IF NOT EXISTS Consultations (
  ConsultationId INT AUTO_INCREMENT PRIMARY KEY,
  AppointmentId INT,
  PatientId INT NOT NULL,
  ProviderId INT NOT NULL,
  ConsultationDate DATETIME DEFAULT CURRENT_TIMESTAMP,
  Vitals TEXT,
  Notes TEXT,
  Diagnosis TEXT,
  FOREIGN KEY (AppointmentId) REFERENCES Appointments(AppointmentId),
  FOREIGN KEY (PatientId) REFERENCES Patients(PatientId),
  FOREIGN KEY (ProviderId) REFERENCES Staff(StaffId)
);

-- Prescriptions and inventory
CREATE TABLE IF NOT EXISTS Prescriptions (
  PrescriptionId INT AUTO_INCREMENT PRIMARY KEY,
  ConsultationId INT NOT NULL,
  Medication VARCHAR(200),
  Dosage VARCHAR(200),
  Duration VARCHAR(100),
  Instructions TEXT,
  FOREIGN KEY (ConsultationId) REFERENCES Consultations(ConsultationId)
);
CREATE TABLE IF NOT EXISTS Inventory (
  ItemId INT AUTO_INCREMENT PRIMARY KEY,
  Name VARCHAR(200),
  BatchNumber VARCHAR(100),
  Quantity INT DEFAULT 0,
  ReorderLevel INT DEFAULT 0,
  SupplierId INT
);
CREATE TABLE IF NOT EXISTS Suppliers (
  SupplierId INT AUTO_INCREMENT PRIMARY KEY,
  Name VARCHAR(200),
  Contact VARCHAR(200)
);

-- Billing & Payments
CREATE TABLE IF NOT EXISTS Billing (
  BillingId INT AUTO_INCREMENT PRIMARY KEY,
  PatientId INT NOT NULL,
  ServiceId INT,
  Amount DECIMAL(10,2),
  BillingDate DATETIME DEFAULT CURRENT_TIMESTAMP,
  Paid TINYINT(1) DEFAULT 0,
  FOREIGN KEY (PatientId) REFERENCES Patients(PatientId),
  FOREIGN KEY (ServiceId) REFERENCES Medical_Services(ServiceId)
);
CREATE TABLE IF NOT EXISTS Payments (
  PaymentId INT AUTO_INCREMENT PRIMARY KEY,
  BillingId INT NOT NULL,
  Amount DECIMAL(10,2) NOT NULL,
  Method VARCHAR(80),
  PaidAt DATETIME DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (BillingId) REFERENCES Billing(BillingId)
);

-- Laboratory Tests & Results
CREATE TABLE IF NOT EXISTS Laboratory_Tests (
  LabTestId INT AUTO_INCREMENT PRIMARY KEY,
  Name VARCHAR(200),
  Code VARCHAR(80)
);
CREATE TABLE IF NOT EXISTS Lab_Results (
  LabResultId INT AUTO_INCREMENT PRIMARY KEY,
  LabTestId INT NOT NULL,
  PatientId INT NOT NULL,
  Result TEXT,
  ResultDate DATETIME DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (LabTestId) REFERENCES Laboratory_Tests(LabTestId),
  FOREIGN KEY (PatientId) REFERENCES Patients(PatientId)
);

-- Notifications & Messages
CREATE TABLE IF NOT EXISTS Notifications (
  NotificationId INT AUTO_INCREMENT PRIMARY KEY,
  SenderUserId INT NOT NULL,
  ReceiverUserId INT, 
  TargetType VARCHAR(50) DEFAULT 'individual', 
  TargetRoleId INT, 
  Title VARCHAR(200),
  Message TEXT,
  CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
  IsRead TINYINT(1) DEFAULT 0,
  FOREIGN KEY (SenderUserId) REFERENCES Users(UserId),
  FOREIGN KEY (ReceiverUserId) REFERENCES Users(UserId),
  FOREIGN KEY (TargetRoleId) REFERENCES Roles(RoleId)
);
CREATE TABLE IF NOT EXISTS NotificationLog (
  LogId INT AUTO_INCREMENT PRIMARY KEY,
  NotificationId INT,
  SentToUserId INT,
  SentAt DATETIME DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (NotificationId) REFERENCES Notifications(NotificationId),
  FOREIGN KEY (SentToUserId) REFERENCES Users(UserId)
);

-- Feedbacks & responses
CREATE TABLE IF NOT EXISTS Feedbacks (
  FeedbackId INT AUTO_INCREMENT PRIMARY KEY,
  FromUserId INT NOT NULL,
  ToUserId INT, 
  Message TEXT,
  CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
  IsHandled TINYINT(1) DEFAULT 0,
  Response TEXT,
  RespondedBy INT,
  RespondedAt DATETIME,
  FOREIGN KEY (FromUserId) REFERENCES Users(UserId),
  FOREIGN KEY (ToUserId) REFERENCES Users(UserId),
  FOREIGN KEY (RespondedBy) REFERENCES Users(UserId)
);

-- Sessions & Audit logs
CREATE TABLE IF NOT EXISTS User_Sessions (
  SessionId INT AUTO_INCREMENT PRIMARY KEY,
  UserId INT,
  LoggedInAt DATETIME,
  LoggedOutAt DATETIME,
  IpAddress VARCHAR(100),
  FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
CREATE TABLE IF NOT EXISTS Audit_Logs (
  AuditId INT AUTO_INCREMENT PRIMARY KEY,
  UserId INT,
  ActionTaken VARCHAR(200),
  Details TEXT,
  ActionTime DATETIME DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- Sample data: admin, nurse, student
INSERT INTO Users (Username, Fullname, RoleId, PasswordHash, MustChangePassword, IsActive, Contact)
VALUES ('admin','System Administrator', (SELECT RoleId FROM Roles WHERE RoleName='Administrator'), SHA2('admin123',256), 0, 1, 'admin@botho.edu')
ON DUPLICATE KEY UPDATE Username=Username;

INSERT INTO Users (Username, Fullname, RoleId, PasswordHash, MustChangePassword, IsActive, Contact)
VALUES ('student1','Jane Student', (SELECT RoleId FROM Roles WHERE RoleName='Student'), SHA2('student123',256), 1, 1, 'jane.student@botho.edu')
ON DUPLICATE KEY UPDATE Username=Username;

INSERT INTO Users (Username, Fullname, RoleId, PasswordHash, MustChangePassword, IsActive, Contact)
VALUES ('nurse1','Nurse One', (SELECT RoleId FROM Roles WHERE RoleName='Provider'), SHA2('nurse123',256), 0, 1, 'nurse.one@botho.edu')
ON DUPLICATE KEY UPDATE Username=Username;

-- Create Patients record for student1
INSERT INTO Patients (UserId, StudentNumber, DateOfBirth, Gender, Address, EmergencyContact)
VALUES (
 (SELECT UserId FROM Users WHERE Username='student1'),
 'S2025001','2002-05-12','Female','123 Campus Rd','071-000-111'
) ON DUPLICATE KEY UPDATE StudentNumber=StudentNumber;

-- End of script
