CREATE DATABASE DB_Lab7Persona;
GO
USE DB_Lab7Persona;
GO
CREATE TABLE Persona
(
    IdPersona INT IDENTITY(1,1) PRIMARY KEY,
    NombreCompleto VARCHAR(100) NOT NULL,
    DNI CHAR(8) NOT NULL,
    Telefono VARCHAR(15) NOT NULL,
    Activo BIT NOT NULL CONSTRAINT DF_Persona_Activo DEFAULT (1),
    CONSTRAINT UQ_Persona_DNI UNIQUE (DNI)
);
GO
INSERT INTO Persona (NombreCompleto, DNI, Telefono, Activo) VALUES
('Andrea Torres', '73451289', '987654321', 1),
('Carlos Mendoza', '71234568', '912345678', 1),
('Lucia Ramirez', '75678912', '998112233', 1);
GO
