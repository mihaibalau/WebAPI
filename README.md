# 🌐 WebAPI Project

This project contains the backend API for the application. It defines routes and logic that allow you to interact with the database through HTTP requests.

---

## 📌 Purpose

The WebAPI exposes a set of endpoints that handle operations like:

- Creating and retrieving departments (`/api/department`)
- Managing users (`/api/users`)
- Other entity-based operations (only where needed)

> These endpoints are used to interact with the database. You will use them from the **WinUI project** by sending HTTP requests (GET, POST, DELETE, etc.).

---

## ⚠️ Do Not Modify

You **should not** make any changes to this WebAPI project. It has been fully set up to support the application and should remain untouched unless explicitly instructed.

All your work will be in the **WinUI project**.

---

## 🗄️ Database Setup

To run the API properly:

1. Create a **SQL Server database** named `HospitalDb`.
2. Run the **initialization scripts** found in:

   ```
   Data/
   ```

   These scripts will create the necessary tables and seed any required data.

---

## ✅ Summary

- You **do not code here**.
- Use the provided endpoints from the WinUI client.
- Make sure `HospitalDb` exists and is properly initialized before running the app.



---
## Table creation for testing the api

CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY, -- Auto-incrementing primary key
    Username NVARCHAR(50) NOT NULL UNIQUE, -- Unique username
    Password NVARCHAR(255) NOT NULL, -- Store hashed passwords, so length is higher
    Mail NVARCHAR(100) NOT NULL UNIQUE, -- Unique email
    Role NVARCHAR(50) NOT NULL DEFAULT 'User', -- Default role
    Name NVARCHAR(100) NOT NULL,
    BirthDate DATE NOT NULL, -- 'DateOnly' maps to DATE in SQL
    Cnp NVARCHAR(20) NOT NULL UNIQUE CHECK(LEN(Cnp) = 13), -- Unique identifier
    Address NVARCHAR(255) NULL,
    PhoneNumber NVARCHAR(20) NULL CHECK(LEN(PhoneNumber) = 10 OR LEN(PhoneNumber) = 10),
    RegistrationDate DATETIME NOT NULL DEFAULT GETDATE() -- Automatically set on insert

);

INSERT INTO Users (Username, Password, Mail, Role, Name, BirthDate, Cnp, Address, PhoneNumber)
VALUES 
('john_doe', 'hashed_password_1', 'john@example.com', 'Doctor', 'John Doe', '1990-05-15', '1234567890123', '123 Main St', '1234567890'),
('jane_doe', 'hashed_password_2', 'jane@example.com', 'Doctor', 'Jane Doe', '1995-08-20', '2345678901234', '456 Elm St', '3216540987'),
('alice_smith', 'hashed_password_3', 'alice@example.com', 'Doctor', 'Alice Smith', '1988-12-10', '3456789012345', '789 Oak St', '9871234567'),
('bob_johnson', 'hashed_password_4', 'bob@example.com', 'Doctor', 'Bob Johnson', '1985-07-25', '4567890123456', '147 Pine St', '6549873210');

INSERT INTO Users (Username, Password, Mail, Role, Name, BirthDate, Cnp, Address, PhoneNumber)
VALUES 
('michael_brown', 'hashed_password_5', 'michael@example.com', 'Doctor', 'Michael Brown', '1982-11-05', '5678944434567', '159 Maple St', '7894561230'),
('sarah_wilson', 'hashed_password_6', 'sarahh@example.com', 'Doctor', 'Sarah Wilson', '1991-03-14', '6744412345678', '753 Birch St', '8529637410'),
('david_martinez', 'hashed_password_7', 'david@example.com', 'Doctor', 'David Martinez', '1987-09-21', '7555523456789', '369 Cedar St', '9632581470'),
('emily_davis', 'hashed_password_8', 'emily@example.com', 'Doctor', 'Emily Davis', '1994-06-30', '8901234544490', '951 Redwood St', '7418529630');

CREATE TABLE Doctors (
	UserId INT,
    DepartmentId INT NOT NULL,
	DoctorRating FLOAT NOT NULL DEFAULT 0.0 CHECK(DoctorRating BETWEEN 0.0 AND 5.0),
    LicenseNumber NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_Doctors_Departments
        FOREIGN KEY (DepartmentId) REFERENCES Departments(Id),
	CONSTRAINT FK_Doctors_Users
		FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

INSERT INTO Doctors (UserId, DepartmentId, LicenseNumber)
VALUES
    (1, 1, '696969'),   -- DoctorId = 1, Dept = Cardiology
    (2, 1, '3222'),  -- DoctorId = 2, Dept = Cardiology
    (3, 2, '231231'), -- DoctorId = 3, Dept = Neurology
    (4, 3, '124211');   -- DoctorId = 4, Dept = Pediatrics

CREATE TABLE Patients (
    UserId INT NOT NULL, -- Foreign key reference to Users table
    PatientId INT IDENTITY(1,1) PRIMARY KEY, -- Auto-increment primary key
    BloodType NVARCHAR(3) NOT NULL CHECK (BloodType IN ('A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-')), -- Enum-like constraint
    EmergencyContact NVARCHAR(20) NOT NULL CHECK(LEN(EmergencyContact) = 10), -- Phone number for emergency contact
    Allergies NVARCHAR(255) NULL, -- Can be NULL if no allergies
    Weight FLOAT NOT NULL CHECK (Weight > 0), -- Prevent invalid weight values
    Height INT NOT NULL CHECK (Height > 0), -- Height in cm, must be positive

    CONSTRAINT FK_Patients_Users FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE
);

INSERT INTO Patients (UserId, BloodType, EmergencyContact, Allergies, Weight, Height)
VALUES 
(5, 'A+', '1112223333', 'Peanuts', 60.5, 165),  -- Jane Doe
(6, 'O-', '2223334444', 'None', 80.0, 175),     -- Mike Davis
(7, 'B+', '3334445555', 'Pollen', 70.2, 170);   -- Sarah Miller

CREATE TABLE Logs (
    LogId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NULL, 
    ActionType NVARCHAR(50) NOT NULL CHECK (ActionType IN ('LOGIN', 'LOGOUT', 'UPDATE_PROFILE', 'CHANGE_PASSWORD', 'DELETE_ACCOUNT', 'CREATE_ACCOUNT')),  
    Timestamp DATETIME NOT NULL DEFAULT GETDATE(),  -- Auto-set when action occurs

    CONSTRAINT FK_Logs_Users FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE SET NULL
);

INSERT INTO Logs (UserId, ActionType)
VALUES 
(3, 'LOGIN'),
(5, 'UPDATE_PROFILE'),
(7, 'LOGOUT');


-- All Users
SELECT * FROM Users;

-- All Doctors
SELECT * FROM Doctors;

-- All Patients
SELECT * FROM Patients;

-- All Logs
SELECT * FROM Logs;