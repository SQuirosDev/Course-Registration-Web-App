# Course Registration Web Application

## 📌 General Description

The **Course Registration Web Application** is a **personal project** developed with the goal of facilitating the registration process, notifications, and management of students interested in a robotics course.

Although it was initially conceived as a possible solution for our **TCU**, developing an application was not a mandatory requirement for that project. Since the application could not be deployed at the time, the system was ultimately consolidated as a **personal project**, maintaining a professional approach in its architecture, design, and functionality.

---

## 🧠 Project Context

The application was designed to inform both students and parents about the content, objectives, and details of the robotics course.  
Interested students can register through a web form, with their information being stored in a database and a confirmation email notification being sent upon successful registration.

Once the registration period ends, the system allows administrators to generate an **Excel report** containing the data stored in the database.  
This report is generated through a password-protected mechanism and is automatically sent via email to authorized recipients, such as the school and the system developers.

---

## 🏗️ Project Architecture

- **Architecture type:** Layered architecture  
  (each layer separated into its own project or document)

### 🗄️ Database
- SQL Server  
- Relational tables  
- Stored Procedures:
  - Registration insertion
  - Registered data retrieval

### ⚙️ Backend
- Developed using .NET  
- Data access implemented with LINQ  
- REST API acting as an intermediary for:
  - Data insertion
  - Data retrieval
- Email notification handling  
- Excel file generation based on stored data

### 🎨 Frontend
- Developed using ASP.NET MVC  
- Direct connection with backend APIs  
- Clean and user-friendly interface  
- Data input forms  
- Design focused on a clear and structured user experience  

> **Note:** Each layer of the system implements its own validations to ensure data integrity and consistency.

---

## 🧰 Technologies Used

### Languages
- SQL (Transact-SQL)
- C#
- HTML
- CSS
- JavaScript
- Razor

### Frameworks & Libraries
- .NET
- ASP.NET MVC
- Bootstrap

### Tools
- SQL Server
- Visual Studio
- Visual Studio Code

---

## 🚀 Main Features

- Student registration form  
- Automatic email notification upon registration  
- Excel report generation  
- Automatic sending of the Excel file via email to authorized recipients  

---

## 🎥 Demo Video

📺 **Project video:**  
https://youtu.be/wzhUfG6OCHM?si=BUbWUi8_uMs-Reol

---

## 📂 Repository

🔗 **GitHub Repository:**  
https://github.com/SQuirosDev/Course-Registration-Web-App

---

## 📊 Project Level
- **Intermediate**

---

## 📝 Additional Notes

- This was my **seventh programmed project** and my **fourth personal project**.  
- Initially, the project was intended for potential use within a TCU, although developing an application was **not mandatory** for that work.  
- Since the application could not be deployed, it was formally finalized as a **personal project**.  
- The system features a well-defined layered architecture (Database, Backend, and Frontend).  
- At the time of development, the project did not present high complexity due to the experience and knowledge already acquired.  
- It is a well-designed system with a visually appealing frontend and complete functionality.  
- The project was developed over the course of one academic term.

---

### 👥 Team Contributions

The project was developed as a team, with the following responsibilities:

- **Database:** Sebastian Quiros  
- **Backend and API development:** Sebastian Quiros  
- **Frontend logic, validations, and API integration:** Sebastian Quiros  
- **Frontend views and visual design:** Anyelo Valdivia  

---

---

# Aplicación web de inscripción para curso

## 📌 Descripción General

La **Aplicación web de inscripción para curso** es un proyecto **personal** desarrollado con el objetivo de facilitar el proceso de inscripción, notificación y gestión de alumnos interesados en un curso de robótica.

Aunque inicialmente fue concebido como una posible solución para nuestro **TCU**, el desarrollo de una aplicación no era un requisito obligatorio para dicho proyecto. Debido a que la aplicación no pudo ser desplegada en su momento, el sistema se consolidó finalmente como un **proyecto personal**, manteniendo un enfoque profesional en su arquitectura, diseño y funcionalidades.

---

## 🧠 Contexto del Proyecto

La aplicación fue diseñada para informar tanto a estudiantes como a padres de familia sobre el contenido, objetivos y detalles del curso de robótica.  
Los alumnos interesados pueden registrarse mediante un formulario web, quedando sus datos almacenados en una base de datos y recibiendo una notificación por correo electrónico confirmando su inscripción.

Una vez finalizado el periodo de inscripción, el sistema permite a los administradores generar un **reporte en formato Excel**, el cual contiene la información almacenada de la base de datos.  
Este reporte se genera mediante un mecanismo de seguridad con contraseña y se envía automáticamente por correo electrónico a los destinatarios autorizados, como el colegio y los desarrolladores del sistema.

---

## 🏗️ Arquitectura del Proyecto

- **Tipo de arquitectura:** Arquitectura por capas  
  (cada capa separada en su propio proyecto o documento)

### 🗄️ Base de Datos
- SQL Server  
- Tablas relacionales  
- Stored Procedures:
  - Inserción de inscripciones
  - Consulta de datos registrados

### ⚙️ Backend
- Desarrollo en .NET  
- Acceso a datos mediante LINQ  
- API REST como intermediaria para:
  - Inserción de datos
  - Consulta de información
- Envío de notificaciones por correo electrónico  
- Generación de archivos Excel a partir de los datos almacenados

### 🎨 Frontend
- Desarrollo en ASP.NET MVC  
- Conexión directa con las APIs del backend  
- Interfaz visual cuidada y funcional  
- Formularios para el ingreso de datos  
- Diseño enfocado en una experiencia de usuario clara y ordenada  

> **Nota:** Cada capa del sistema implementa validaciones propias para garantizar la integridad y consistencia de los datos.

---

## 🧰 Tecnologías Utilizadas

### Lenguajes
- SQL (Transact-SQL)
- C#
- HTML
- CSS
- JavaScript
- Razor

### Frameworks y Librerías
- .NET
- ASP.NET MVC
- Bootstrap

### Herramientas
- SQL Server
- Visual Studio
- Visual Studio Code

---

## 🚀 Funcionalidades Principales

- Formulario de inscripción de alumnos  
- Notificación automática por correo electrónico al completar la inscripción  
- Generación de reportes en formato Excel  
- Envío automático del archivo Excel por correo electrónico a destinatarios autorizados  

---

## 🎥 Video Demostrativo

📺 **Video del proyecto:**  
https://youtu.be/wzhUfG6OCHM?si=BUbWUi8_uMs-Reol

---

## 📂 Repositorio

🔗 **Repositorio GitHub:**  
https://github.com/SQuirosDev/Course-Registration-Web-App

---

## 📊 Nivel del Proyecto
- **Intermedio**

---

## 📝 Notas Adicionales

- Este fue mi **séptimo proyecto programado**, y el **cuarto proyecto personal**.  
- Inicialmente el proyecto estaba orientado a un posible uso dentro del TCU, aunque el desarrollo de una aplicación **no era obligatorio** para dicho trabajo.  
- Al no poder ser desplegado, el sistema quedó formalmente como un **proyecto personal**.  
- El proyecto cuenta con una arquitectura bien definida por capas (Base de Datos, Backend y Frontend).  
- En el momento de su desarrollo, no presentó una complejidad elevada debido a la experiencia y conocimientos adquiridos previamente.  
- Se trata de un sistema bien diseñado, con un frontend visualmente atractivo y funcionalidades completas.  
- El desarrollo se realizó a lo largo de un cuatrimestre.

---

### 👥 Aportes del Equipo

El proyecto fue desarrollado en equipo, con las siguientes responsabilidades:

- **Base de datos:** Sebastian Quiros  
- **Backend y desarrollo de la API:** Sebastian Quiros  
- **Lógica del frontend, validaciones y conexión con la API:** Sebastian Quiros  
- **Vistas y diseño visual del frontend:** Anyelo Valdivia
