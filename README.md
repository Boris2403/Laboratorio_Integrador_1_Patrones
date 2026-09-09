# Sistema de Gestión de Catálogo - LibroFácil

## 1. Descripción del Proyecto
Este proyecto es una Web API desarrollada en C# y .NET 8 que permite administrar el catálogo de libros de la empresa ficticia LibroFácil. Se ha implementado aplicando los principios de Clean Architecture, Diseño Guiado por el Dominio (DDD) básico y principios SOLID para garantizar la mantenibilidad, escalabilidad y protección de las reglas de negocio, reemplazando el enfoque tradicional de desarrollo monolítico.

## 2. Integrantes
* Vinces Cueva Boris Yussef

## 3. Requisitos Previos
* **.NET 8 SDK** instalado en el sistema.
* **SQL Server LocalDB** (instalado por defecto con las herramientas de desarrollo de .NET en Windows) o una instancia de SQL Server.
* **Git** para el control de versiones.
* **Postman** (o herramienta similar) para comprobar los endpoints.

## 4. Instalación y Configuración
1. Clonar o descargar el repositorio local.
2. Abrir una terminal en el directorio raíz del proyecto (`LibroFacil/`).
3. Restaurar los paquetes de NuGet ejecutando:
   ```bash
   dotnet restore
4. Aplicar las migraciones para crear la base de datos LibroFacilDb en SQL Server:
    dotnet ef database update -p src/LibroFacil.Infrastructure -s src/LibroFacil.Api

## 5. Ejecución del Proyecto
Para iniciar la Web API, ejecuta el siguiente comando desde la raíz del proyecto
dotnet run --project src/LibroFacil.Api

La terminal indicará la URL local donde la API está escuchando (generalmente http://localhost:5200 o similar).

## 6. Estructura del Proyecto y Separación de Responsabilidades
La solución adopta una arquitectura modular en capas:

* LibroFacil.Domain: Núcleo del sistema. Contiene la entidad Libro.cs y excepciones de negocio (ReglaDominioException.cs). Su responsabilidad es proteger los datos mediante invariantes (DDD). No depende de ninguna otra capa.

* LibroFacil.Application: Coordina las intenciones del sistema. Define los contratos (ILibroRepository.cs) y orquesta los flujos de negocio mediante LibroService.cs.

* LibroFacil.Infrastructure: Detalle técnico. Implementa el acceso a datos conectando Entity Framework Core y SQL Server (LibroRepositoryEf.cs y LibroFacilDbContext.cs).

* LibroFacil.Api: Capa de presentación HTTP. Contiene los controladores (LibrosController.cs), los DTOs de frontera y configura la Inyección de Dependencias (Composition Root) en Program.cs.

## 7. Principios y Patrones Aplicados
* SRP (Principio de Responsabilidad Única): Cada capa y archivo tiene una única razón de cambiar. Por ejemplo, Libro.cs solo cambia si varían las características del libro, y LibrosController.cs solo cambia si varían las rutas o códigos HTTP.

* DIP (Principio de Inversión de Dependencias): La capa Application depende de la abstracción ILibroRepository (contrato) y no de LibroRepositoryEf (detalle concreto de infraestructura). Program.cs se encarga de inyectar la dependencia.

* DDD Básico: La entidad Libro no es un contenedor de datos anémico; utiliza un patrón de fábrica (Libro.Crear) y encapsula su estado (setters privados) protegiendo invariantes (stock positivo, año válido).

* Clean Architecture: Se respeta la regla de dependencia, donde el dominio está en el centro, aislado completamente de los detalles técnicos como la base de datos o el framework web.