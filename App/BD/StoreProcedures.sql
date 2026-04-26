USE INSCRIPCIONES_APP_BD;
GO
USE INSCRIPCIONES_APP_BD;
GO

CREATE PROCEDURE PS_INSERT_INSCRIPCION
    @NOMBRE NVARCHAR(100),
    @APELLIDO_1 NVARCHAR(100),
    @APELLIDO_2 NVARCHAR(100) = NULL,
    @GRADO NVARCHAR(20),
    @SECCION NVARCHAR(10),
    @CORREO NVARCHAR(250),
	@CONFIRMADO BIT,

    @RESULTADO BIT OUTPUT,
    @LISTA_ACIERTOS NVARCHAR(MAX) OUTPUT,
    @LISTA_ERRORES NVARCHAR(MAX) OUTPUT,
    @REGISTRO_ID INT OUTPUT,
    @ERROR_ID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        -- Validaciones básicas
        IF LTRIM(RTRIM(@NOMBRE)) = '' OR 
           LTRIM(RTRIM(@APELLIDO_1)) = '' OR 
           LTRIM(RTRIM(@GRADO)) = '' OR 
           LTRIM(RTRIM(@SECCION)) = '' OR 
           LTRIM(RTRIM(@CORREO)) = '' OR
		   LTRIM(RTRIM(@CONFIRMADO)) = ''
            THROW 50001, 'Hay datos incompletos o erróneos que son obligatorios.', 1;

        -- Validación básica de correo
        IF CHARINDEX('@', @CORREO) = 0 OR CHARINDEX('.', @CORREO) = 0
            THROW 50002, 'El correo no tiene un formato válido.', 1;

        -- Validación de duplicados
        IF EXISTS (SELECT 1 FROM TB_INSCRIPCIONES WHERE CORREO = @CORREO)
            THROW 50003, 'Ya existe una inscripción con este correo.', 1;

        BEGIN TRANSACTION;

        INSERT INTO TB_INSCRIPCIONES (NOMBRE, APELLIDO_1, APELLIDO_2, GRADO, SECCION, CORREO, CONFIRMADO)
        VALUES (@NOMBRE, @APELLIDO_1, @APELLIDO_2, @GRADO, @SECCION, @CORREO, @CONFIRMADO);

        SET @REGISTRO_ID = SCOPE_IDENTITY();

        COMMIT TRANSACTION;

        -- Outputs exitosos
        SET @RESULTADO = 1;
        SET @LISTA_ACIERTOS = 'Inscripción realizada correctamente.';
        SET @LISTA_ERRORES = '';
        SET @ERROR_ID = 0;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;

        DECLARE @ErrNum INT = ERROR_NUMBER();
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();

        -- Guardar todos los errores en la tabla
        INSERT INTO TB_ERRORES (ORIGEN, MENSAJE, PROCEDIMIENTO, STACK_TRACE)
        VALUES ('PS_INSERT_INSCRIPCION', @ErrMsg, ERROR_PROCEDURE(), @ErrMsg);

        SET @ERROR_ID = @ErrNum;

        -- Outputs amigables para el frontend
        SET @RESULTADO = 0;
        SET @LISTA_ACIERTOS = '';
        SET @LISTA_ERRORES = CASE 
                                WHEN @ErrNum >= 50000 THEN @ErrMsg
                                ELSE 'Ocurrió un error inesperado, contacte al administrador.'
                             END;
        SET @REGISTRO_ID = 0;

        THROW; -- Para que el backend también reciba el error
    END CATCH
END
GO

CREATE PROCEDURE PS_GET_INSCRIPCIONES
    @RESULTADO BIT OUTPUT,
    @LISTA_ACIERTOS NVARCHAR(MAX) OUTPUT,
    @LISTA_ERRORES NVARCHAR(MAX) OUTPUT,
    @REGISTRO_ID INT OUTPUT,
    @ERROR_ID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO TB_BITACORA (TABLA_NOMBRE, ACCION, REGISTRO_ID, LUGAR, DETALLE)
        VALUES ('TB_INSCRIPCIONES', 'SELECT', 1, 'PS_GET_INSCRIPCIONES', 'Se consultaron todas las inscripciones');

        SELECT * FROM TB_INSCRIPCIONES ORDER BY FECHA_REGISTRO DESC;

        SET @RESULTADO = 1;
        SET @LISTA_ACIERTOS = 'Consulta realizada correctamente.';
        SET @LISTA_ERRORES = '';
        SET @REGISTRO_ID = 1;
        SET @ERROR_ID = 0;

    END TRY
    BEGIN CATCH
        DECLARE @ErrNum INT = ERROR_NUMBER();
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();

        INSERT INTO TB_ERRORES (ORIGEN, MENSAJE, PROCEDIMIENTO, STACK_TRACE)
        VALUES ('PS_GET_INSCRIPCIONES', @ErrMsg, ERROR_PROCEDURE(), @ErrMsg);

        SET @ERROR_ID = @ErrNum;

        SET @RESULTADO = 0;
        SET @LISTA_ACIERTOS = '';
        SET @LISTA_ERRORES = CASE 
                                WHEN @ErrNum >= 50000 THEN @ErrMsg
                                ELSE 'Ocurrió un error inesperado, contacte al administrador.'
                             END;
        SET @REGISTRO_ID = 0;

        THROW;
    END CATCH
END
GO

CREATE PROCEDURE PS_GET_CORREOS
    @RESULTADO BIT OUTPUT,
    @LISTA_ACIERTOS NVARCHAR(MAX) OUTPUT,
    @LISTA_ERRORES NVARCHAR(MAX) OUTPUT,
    @REGISTRO_ID INT OUTPUT,
    @ERROR_ID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO TB_BITACORA (TABLA_NOMBRE, ACCION, REGISTRO_ID, LUGAR, DETALLE)
        VALUES ('TB_CORREOS_DESTINO', 'SELECT', 1, 'PS_GET_CORREOS', 'Se consultaron los correos destino');

        SELECT * FROM TB_CORREOS_DESTINO;

        SET @RESULTADO = 1;
        SET @LISTA_ACIERTOS = 'Consulta realizada correctamente.';
        SET @LISTA_ERRORES = '';
        SET @REGISTRO_ID = 1;
        SET @ERROR_ID = 0;

    END TRY
    BEGIN CATCH
        DECLARE @ErrNum INT = ERROR_NUMBER();
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();

        INSERT INTO TB_ERRORES (ORIGEN, MENSAJE, PROCEDIMIENTO, STACK_TRACE)
        VALUES ('PS_GET_CORREOS', @ErrMsg, ERROR_PROCEDURE(), @ErrMsg);

        SET @ERROR_ID = @ErrNum;

        SET @RESULTADO = 0;
        SET @LISTA_ACIERTOS = '';
        SET @LISTA_ERRORES = CASE 
                                WHEN @ErrNum >= 50000 THEN @ErrMsg
                                ELSE 'Ocurrió un error inesperado, contacte al administrador.'
                             END;
        SET @REGISTRO_ID = 0;

        THROW;
    END CATCH
END
GO