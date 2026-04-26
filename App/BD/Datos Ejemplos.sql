-- Insertar registros en la tabla TB_INSCRIPCIONES
INSERT INTO TB_INSCRIPCIONES (NOMBRE, APELLIDO_1, APELLIDO_2, GRADO, SECCION, CORREO, CONFIRMADO)
VALUES 
('Sebastián', 'Quirós', 'Ramírez', '10', 'A', 'sebastian.quiros@example.com', 1),
('María', 'Fernández', 'Muñoz', '11', 'B', 'maria.fernandez@example.com', 0),
('Carlos', 'Soto', 'Jiménez', '9', 'C', 'carlos.soto@example.com', 1),
('Andrea', 'García', 'López', '12', 'A', 'andrea.garcia@example.com', 0);
GO


-- Insertar registros en la tabla TB_CORREOS_DESTINO
INSERT INTO TB_CORREOS_DESTINO (CORREO)
VALUES
('coordinacion.colegio@example.com'),
('direccion.academica@example.com'),
('soporte.tecnico@example.com');
GO
