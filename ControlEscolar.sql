CREATE DATABASE JSanchezControlEscolarWCF;
USE JSanchezControlEscolarWCF;

CREATE TABLE Alumno(
	IdAlumno INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(50),
	ApellidoPaterno VARCHAR(50),
	ApellidoMaterno VARCHAR(50)
)
GO

CREATE TABLE Materia(
	IdMateria INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(50),
	Costo DECIMAL
)
GO

CREATE PROCEDURE AlumnoAdd --'Daniel','Esquivel','Jimenez'
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50)
AS
INSERT INTO Alumno(
				   Nombre,
				   ApellidoPaterno,
				   ApellidoMaterno) VALUES(
										   @Nombre,
										   @ApellidoPaterno,
										   @ApellidoMaterno)
GO

CREATE PROCEDURE AlumnoUpdate
@IdAlumno INT,
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50)
AS
UPDATE Alumno 
SET
	Nombre = @Nombre,
	ApellidoPaterno = @ApellidoPaterno,
	ApellidoMaterno = @ApellidoMaterno
	WHERE IdAlumno = @IdAlumno
GO

CREATE PROCEDURE AlumnoDelete
@IdAlumno INT
AS
DELETE FROM Alumno WHERE IdAlumno = @IdAlumno
GO

CREATE PROCEDURE AlumnoGetAll
AS
SELECT IdAlumno,
	   Nombre,
	   ApellidoPaterno,
	   ApellidoMaterno 
	   FROM Alumno
GO

ALTER PROCEDURE AlumnoGetById 1
@IdAlumno INT
AS
SELECT
	   Nombre,
	   ApellidoPaterno,
	   ApellidoMaterno 
	   FROM Alumno
	   WHERE IdAlumno = @IdAlumno
GO

CREATE PROCEDURE MateriaAdd --'Estructurada',500
@Nombre VARCHAR(50),
@Costo DECIMAL
AS
INSERT INTO Materia(
					Nombre,
					Costo) VALUES (
								   @Nombre,
								   @Costo)
GO

CREATE PROCEDURE MateriaUpdate
@IdMateria INT,
@Nombre VARCHAR(50),
@Costo DECIMAL
AS
UPDATE Materia 
SET
	Nombre = @Nombre,
	Costo = @Costo
	WHERE IdMateria = @IdMateria
GO

CREATE PROCEDURE MateriaDelete
@IdMateria INT
AS
DELETE FROM Materia WHERE IdMateria = @IdMateria
GO

CREATE PROCEDURE MateriaGetAll
AS
SELECT IdMateria,
	   Nombre,
	   Costo 
	   FROM Materia
GO

CREATE PROCEDURE MateriaGetById
@IdMateria INT
AS
SELECT IdMateria,
	   Nombre,
	   Costo 
	   FROM Materia
	   WHERE IdMateria = @IdMateria
GO

CREATE TABLE AlumnoMateria(
	IdAlumnoMateria INT IDENTITY(1,1) PRIMARY KEY,
	IdAlumno INT,
	IdMateria INT,
	CONSTRAINT fk_AlumnoMateria_Alumno FOREIGN KEY (IdAlumno) REFERENCES Alumno (IdAlumno),
	CONSTRAINT fk_AlumnoMateria_Materia FOREIGN KEY (IdMateria) REFERENCES Materia (IdMateria)
)
GO

CREATE PROCEDURE AlumnoMateriaAdd --5,11
@IdAlumno INT,
@IdMateria INT
AS
INSERT INTO AlumnoMateria(
						  IdAlumno,
						  IdMateria) VALUES (
											 @IdAlumno,
											 @IdMateria)
GO

--No se utiliza
CREATE PROCEDURE AlumnoMateriaUpdate
@IdAlumnoMateria INT,
@IdAlumno INT,
@IdMateria INT
AS
UPDATE AlumnoMateria 
SET
	IdAlumno = @IdAlumno,
	IdMateria=@IdMateria
	WHERE IdAlumnoMateria=@IdAlumnoMateria
GO
--
CREATE PROCEDURE AlumnoMateriaDelete
@IdAlumnoMateria INT
AS
DELETE FROM AlumnoMateria WHERE IdAlumnoMateria=@IdAlumnoMateria
GO


--Materias Que tiene el alumno
ALTER PROCEDURE AlumnoMateriaGetAll --1
@IdAlumno INT
AS
SELECT IdAlumnoMateria,IdAlumno,
	   Materia.IdMateria, 
	   Materia.Nombre,
	   Materia.Costo 
	   FROM AlumnoMateria
INNER JOIN Materia ON AlumnoMateria.IdMateria = Materia.IdMateria
WHERE IdAlumno=@IdAlumno
GO

CREATE PROCEDURE AlumnoMateriaGetById --1
@IdAlumnoMateria INT
AS
SELECT
	  IdAlumnoMateria,
	  Alumno.Nombre AS NombreAlumno,
	  Alumno.ApellidoPaterno,
	  Alumno.ApellidoMaterno, 
	  Materia.Nombre AS Materia,
	  Materia.Costo
	  FROM AlumnoMateria
INNER JOIN Alumno ON AlumnoMateria.IdAlumno = Alumno.IdAlumno
INNER JOIN Materia ON AlumnoMateria.IdMateria = Materia.IdMateria
WHERE IdAlumnoMateria=@IdAlumnoMateria
GO

AlumnoMateriaGetAll 1
MateriaGetAll
AlumnoGetAll
--Materias Que no tiene el alumno
--CREATE PROCEDURE AlumnoMateriasDisponibles
--@IdAlumno INT
--AS
--SELECT AlumnoMateria.IdAlumno,Materia.IdMateria,Materia.Nombre, Materia.Costo FROM Materia
----INNER JOIN AlumnoMateria ON Materia.IdMateria = AlumnoMateria.IdMateria
----INNER JOIN Alumno ON AlumnoMateria.IdAlumno = Alumno.IdAlumno
----INNER JOIN AlumnoMateria ON Alumno.IdAlumno = AlumnoMateria.IdMateria
--WHERE Materia.IdMateria NOT IN(
--							   SELECT IdMateria FROM AlumnoMateria WHERE IdAlumno=1)

--Materias Que no tiene el alumno
CREATE PROCEDURE AlumnoMateriasDisponibles --1
@IdAlumno INT
AS
SELECT Materia.IdMateria,Materia.Nombre, Materia.Costo FROM AlumnoMateria
INNER JOIN Materia ON Materia.IdMateria = AlumnoMateria.IdMateria
WHERE Materia.IdMateria NOT IN(
							   SELECT AlumnoMateria.IdMateria FROM AlumnoMateria WHERE AlumnoMateria.IdAlumno=@IdAlumno)
GO

--Materias Que tiene el alumno
ALTER PROCEDURE AlumnoMateriaGetAll 1
@IdAlumno INT
AS
SELECT IdAlumnoMateria,
	   Alumno.IdAlumno,
	   Alumno.Nombre AS NombreAlumno,
	   Alumno.ApellidoPaterno,
	   Alumno.ApellidoMaterno,
	   Materia.IdMateria, 
	   Materia.Nombre AS Materia,
	   Materia.Costo 
	   FROM AlumnoMateria
INNER JOIN Materia ON AlumnoMateria.IdMateria = Materia.IdMateria
INNER JOIN Alumno ON AlumnoMateria.IdAlumno = Alumno.IdAlumno
WHERE Alumno.IdAlumno=@IdAlumno
GO

