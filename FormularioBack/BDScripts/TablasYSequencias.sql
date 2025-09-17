--CREATE DATABASE FormularioDB;
-- Eliminar en orden inverso a las dependencias
/*
DROP TABLE IF EXISTS RespuestasPreguntas;
DROP TABLE IF EXISTS Respuestas;
DROP TABLE IF EXISTS FormularioHasPreguntas;
DROP TABLE IF EXISTS Opciones;
DROP TABLE IF EXISTS Preguntas;
DROP TABLE IF EXISTS Formularios;
*/

-- ============================
--  SECUENCIAS
-- ============================
CREATE SEQUENCE SeqFormularioHasPreguntas
    START WITH 1
    INCREMENT BY 1;

CREATE SEQUENCE SeqRespuestaPregunta
    START WITH 1
    INCREMENT BY 1;

-- ============================
--  TABLAS PRINCIPALES
-- ============================

CREATE TABLE Formularios (
    IdFormulario INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(200) NOT NULL
);

CREATE TABLE Preguntas (
    IdPregunta INT IDENTITY(1,1) PRIMARY KEY,
    Pregunta NVARCHAR(500) NOT NULL
);

CREATE TABLE Opciones (
    IdOpcion INT IDENTITY(1,1) PRIMARY KEY,
    IdPregunta INT NOT NULL,
    Texto NVARCHAR(300) NOT NULL,
    Correcta BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Opcion_Pregunta FOREIGN KEY (IdPregunta) REFERENCES Preguntas(IdPregunta)
);

-- ============================
--  RELACIÓN MUCHOS A MUCHOS
-- ============================

CREATE TABLE FormularioHasPreguntas (
    IdFHP INT PRIMARY KEY DEFAULT NEXT VALUE FOR SeqFormularioHasPreguntas,
    IdFormulario INT NOT NULL,
    IdPregunta INT NOT NULL,
    CONSTRAINT FK_FHP_Formulario FOREIGN KEY (IdFormulario) REFERENCES Formularios(IdFormulario),
    CONSTRAINT FK_FHP_Pregunta FOREIGN KEY (IdPregunta) REFERENCES Preguntas(IdPregunta)
);

-- ============================
--  RESPUESTAS
-- ============================

CREATE TABLE Respuestas (
    IdRespuesta INT IDENTITY(1,1) PRIMARY KEY,
    IdFormulario INT NOT NULL,
    FechaRespuesta DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Respuesta_Formulario FOREIGN KEY (IdFormulario) REFERENCES Formularios(IdFormulario)
);

CREATE TABLE RespuestasPreguntas (
    IdRP INT PRIMARY KEY DEFAULT NEXT VALUE FOR SeqRespuestaPregunta,
    IdRespuesta INT NOT NULL,
    IdPregunta INT NOT NULL,
    IdOpcionSeleccionada INT NULL,
    CONSTRAINT FK_RP_Respuesta FOREIGN KEY (IdRespuesta) REFERENCES Respuestas(IdRespuesta),
    CONSTRAINT FK_RP_Pregunta FOREIGN KEY (IdPregunta) REFERENCES Preguntas(IdPregunta),
    CONSTRAINT FK_RP_Opcion FOREIGN KEY (IdOpcionSeleccionada) REFERENCES Opciones(IdOpcion)
);
