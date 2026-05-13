-- =============================================
-- MyPortfolio Database Setup Script
-- Run this in phpMyAdmin or MySQL terminal
-- =============================================

-- 1. Create Database
CREATE DATABASE IF NOT EXISTS Finaldb
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE Finaldb;

-- 2. Create Users Table
CREATE TABLE IF NOT EXISTS Users (
    ID       INT AUTO_INCREMENT PRIMARY KEY,
    Name     VARCHAR(100) NOT NULL,
    Email    VARCHAR(150) NOT NULL UNIQUE,
    Gender   VARCHAR(20) NOT NULL DEFAULT '',
    Password VARCHAR(255) NOT NULL,
    Role     VARCHAR(50)  NOT NULL DEFAULT 'User',
    Remark   TEXT         NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 3. Insert Dummy Data (1 Admin + 5 Users)
INSERT INTO Users (Name, Email, Gender, Password, Role, Remark) VALUES
('Admin',        'admincheng@portfolio.com', 'Male',   'admin123', 'Admin', 'System Administrator'),
('Sophea Meas',  'sopheaclu@gmail.com',    'Female', 'user123',  'User',  'Web Developer'),
('Dara Keo',     'daraclu@gmail.com',      'Male',   'user123',  'User',  'UI/UX Designer'),
('Malis Chan',   'malisclu@gmail.com',     'Female', 'user123',  'User',  'Graphic Designer'),
('Ratha Sok',    'rathaclu@gmail.com',     'Male',   'user123',  'User',  'Backend Developer'),
('Sreyla Pich',  'sreylaclu@gmail.com',    'Female', 'user123',  'User',  'Mobile Developer');

-- 4. Verify
SELECT * FROM Users;
