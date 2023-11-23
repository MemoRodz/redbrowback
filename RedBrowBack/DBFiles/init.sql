-- ________________________________ USAR BASE DE DATOS ________________________________
USE redbrowuser;

-- ________________________________ CREAR TABLA ROL ________________________________
CREATE TABLE Rol (
    idRol INT AUTO_INCREMENT PRIMARY KEY,
    descripcion VARCHAR(30),
    esActivo TINYINT(1),
    fechaRegistro TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ________________________________ CREAR TABLA USUARIO ________________________________
CREATE TABLE Usuario (
    idUsuario INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50),
    correo VARCHAR(50),
    edad INT,
    idRol INT,
    clave VARCHAR(100),
    esActivo TINYINT(1),
    fechaRegistro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (idRol) REFERENCES Rol(idRol)
);

-- ________________________________ INSERTAR ROLES ________________________________
INSERT INTO Rol (descripcion, esActivo)
VALUES
    ('Administrador', 1),
    ('Empleado', 1),
    ('Supervisor', 1);

-- ________________________________ INSERTAR USUARIO ________________________________    
INSERT INTO Usuario (nombre, correo, edad, idRol, clave, esActivo)
VALUES
    ('CapaciTech Kids', 'capacitechkids@gmail.com', 27, 1, 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 1);
    
