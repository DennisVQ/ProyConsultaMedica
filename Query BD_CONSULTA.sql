USE MASTER
GO

IF DB_ID('BD_CONSULTA') IS NOT NULL
DROP DATABASE BD_CONSULTA  
GO

CREATE DATABASE BD_CONSULTA  
go
  
USE BD_CONSULTA
go


CREATE TABLE TB_PAIS (
   id_pais INT identity(1,1) PRIMARY KEY,
   descripcion VARCHAR(15)NOT NULL
)
go

 CREATE TABLE TB_ESPECIALIDAD (
   id_especialidad INT identity(1,1) PRIMARY KEY,
   descripcion VARCHAR(15)NOT NULL   
)
go

CREATE TABLE TB_MEDICO(
   id_medico INT identity(1,1) PRIMARY KEY ,
   nombres VARCHAR(30) NOT NULL,
   apellidos VARCHAR(30) NOT NULL,  
   sexo VARCHAR(10) NOT NULL, 
   fechaNacimiento DATE NOT NULL,
   email VARCHAR(30) NOT NULL,
   telefono VARCHAR(15) NOT NULL,
   dni CHAR(8) NOT NULL,  
   cmp int NOT NULL,
   presentacion VARCHAR(400), /* ?? */ 
   centro_estudios_univ VARCHAR(30) NOT NULL,
   lugarLaboral VARCHAR(30) NOT NULL,  
   usuario VARCHAR(50)  NOT NULL Unique,
   clave VARCHAR(50)  NOT NULL,
   estado int, 
   fechaRegistro DATETIME,
   id_especialidad int FOREIGN KEY REFERENCES TB_ESPECIALIDAD(id_especialidad),
   id_pais int FOREIGN KEY REFERENCES TB_PAIS(id_pais)

)
GO

/* Faltaria hacer una tabla de Universidades y centro laboral*/
/* Falta otros Estudios como Certificaciones, Doctorados, etc*/


CREATE TABLE TB_CONSULTA (
   id_consulta INT identity(1,1) PRIMARY KEY,   
   codigo_generado VARCHAR(30) NOT NULL,   
   correo varchar(30) NOT NULL,
   sexo VARCHAR(8) NOT NULL,
   edad int NOT NULL,
   tipo_paciente VARCHAR(10) NOT NULL,
   msje_pregunta VARCHAR(500) NOT NULL,
   msje_respuesta VARCHAR(500),
   fechaPregunta DATETIME,
   fechaRespuesta DATETIME,  
   calificacion INT NOT NULL,  
   estado int, /*Generado, Respondido, Calificado*/
   id_especialidad int FOREIGN KEY REFERENCES TB_ESPECIALIDAD(id_especialidad),
   /**/
   id_medico int FOREIGN KEY REFERENCES TB_MEDICO(id_medico),   
)
go

insert into tb_especialidad values ('General');
insert into tb_especialidad values ('Reumatologia');
insert into tb_especialidad values ('Oncologia');


insert into tb_pais values ('Peru');
insert into tb_pais values ('Chile');
insert into tb_pais values ('Cuba');

/*Al Dr. solo se va a valorar una Especialidad*/


CREATE PROCEDURE SP_InsertarConsulta
(
@codigo_generado VARCHAR(30),
@correo varchar(30),
@sexo VARCHAR(8),
@edad int,
@tipo_paciente VARCHAR(10),
@msje_pregunta VARCHAR(500),
@fechaPregunta DATETIME,
@msje_respuesta VARCHAR(500),
@fechaRespuesta DATETIME,  
@calificacion INT,  
@estado int,
@id_especialidad int,
@id_medico int
) 
as
begin
 insert into TB_CONSULTA(codigo_generado, correo, sexo, edad, tipo_paciente, msje_pregunta, fechaPregunta, msje_respuesta,
 fechaRespuesta, calificacion, estado, id_especialidad, id_medico) 
 values (@codigo_generado, @correo, @sexo, @edad, @tipo_paciente, @msje_pregunta, @fechaPregunta, @msje_respuesta,
 @fechaRespuesta, @calificacion, @estado, @id_especialidad, @id_medico)
end
GO


CREATE PROCEDURE SP_ActualizarConsulta
(
@id_consulta INT,
/*@msje_pregunta VARCHAR(500),*/
/*@fechaPregunta DATETIME,*/
@msje_respuesta VARCHAR(500),
@fechaRespuesta DATETIME,  
@calificacion INT,  
@estado int
) 
as
update tb_consulta set @msje_respuesta = msje_respuesta, @fechaRespuesta = fechaRespuesta, 
@calificacion = calificacion, @estado = estado
where @id_consulta = id_consulta
GO