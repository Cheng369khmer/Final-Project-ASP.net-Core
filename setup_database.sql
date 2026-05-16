-- =============================================
-- MyPortfolio Database Setup Script
-- Run this in phpMyAdmin or MySQL terminal
-- =============================================

-- 1. Create Database name provided by XAMPP : Finaldb
CREATE DATABASE IF NOT EXISTS Finaldb
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE Finaldb;

-- 2. Drop the existing table if you are recreating it (Optional, but recommended for a clean start)
-- DROP TABLE IF EXISTS Users;

-- 3. Create Users Table with all fields including ProfilePhoto and IsActive
CREATE TABLE IF NOT EXISTS Users (
    ID           INT AUTO_INCREMENT PRIMARY KEY,
    Name         VARCHAR(100) NOT NULL,
    Email        VARCHAR(150) NOT NULL UNIQUE,
    Gender       VARCHAR(20) NOT NULL DEFAULT '',
    Password     VARCHAR(255) NOT NULL,
    Role         VARCHAR(50)  NOT NULL DEFAULT 'User',
    Remark       TEXT         NOT NULL DEFAULT '',
    ProfilePhoto VARCHAR(255) DEFAULT '',       -- ថែមថ្មី: សម្រាប់ទុករូបថត
    IsActive     BOOLEAN      DEFAULT 1         -- ថែមថ្មី: សម្រាប់សម្គាល់ Active/Inactive (1=Active, 0=Inactive)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 4. Insert Dummy Data (1 Admin + 5 Users)
-- We set IsActive to 1 (True) for everyone initially, and leave ProfilePhoto empty so the default avatars will show.
INSERT INTO Users (Name, Email, Gender, Password, Role, Remark, ProfilePhoto, IsActive) VALUES
('Admin',        'admincheng@portfolio.com', 'Male',   'admin123', 'Admin', 'System Administrator', '', 1),
('Sophea Meas',  'sopheaclu@gmail.com',    'Female', 'user123',  'User',  'Web Developer',        '', 1),
('Dara Keo',     'daraclu@gmail.com',      'Male',   'user123',  'User',  'UI/UX Designer',       '', 1),
('Malis Chan',   'malisclu@gmail.com',     'Female', 'user123',  'User',  'Graphic Designer',     '', 1),
('Ratha Sok',    'rathaclu@gmail.com',     'Male',   'user123',  'User',  'Backend Developer',    '', 1),
('Sreyla Pich',  'sreylaclu@gmail.com',    'Female', 'user123',  'User',  'Mobile Developer',     '', 1);

-- 5. Verify Data Insertion
SELECT * FROM Users;