# 📋 FormularioBack

API REST desarrollada en **.NET 8**, **Entity Framework Core** y **SQL Server** para la gestión de formularios de opción múltiple.  
Permite crear cuestionarios, registrar respuestas de usuarios y consultar resultados con estadísticas.

---

## 🚀 Características principales

- ✅ Crear formularios con preguntas y opciones (marcando cuáles son correctas).  
- 📝 Registrar respuestas de usuarios a un formulario.  
- 📊 Obtener el detalle de una respuesta, verificando si las opciones seleccionadas fueron correctas.  
- 📈 Listar formularios con estadísticas (cantidad de respuestas y puntaje promedio).  
- 🌐 CORS habilitado globalmente para aceptar solicitudes desde cualquier origen (solo en desarrollo).  

---

## 📂 Estructura del proyecto
```bash
FormularioBack/
│── BDScripts/ # Scripts SQL para la base de datos
│── Context/ # DbContext y configuración de EF Core
│── Controllers/ # Controladores de la API
│── Dtos/ # Data Transfer Objects (DTOs)
│── Models/ # Modelos generados por Entity Framework
│── Services/ # Lógica de negocio
│── appsettings.json # Configuración de la aplicación
│── Program.cs # Punto de entrada principal
```

---

## 🗄️ Base de datos

Este proyecto utiliza **SQL Server** como motor de base de datos.  

Los scripts necesarios para crear las tablas y secuencias se encuentran en:  
`BDScripts/TablasYSecuencias.sql`

Ejecuta este archivo en tu instancia de SQL Server para crear la base de datos inicial.

---

## ⚙️ Configuración

### 1. Cadena de conexión

En el archivo `appsettings.json`, actualiza la conexión según tu entorno:

**Ejemplo con autenticación de Windows:**
```json
"ConnectionStrings": {
  "FormularioDB": "Server=TU_SERVIDOR;Database=FormularioDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
**Ejemplo con autenticación SQL:**
```json
"ConnectionStrings": {
  "FormularioDB": "Server=TU_SERVIDOR;Database=FormularioDB;User Id=MI_USUARIO;Password=MI_PASSWORD;TrustServerCertificate=True;"
}
```
## ▶️ Ejecución

1. Clona el repositorio **FormularioBack**.  
2. Configura la cadena de conexión en `appsettings.json`.  
3. Ejecuta el script `BDScripts/TablasYSecuencias.sql` en tu SQL Server.  
4. Corre el proyecto:  
   ```bash
   dotnet run
   ```
## 📖 Notas

- En desarrollo, **CORS está habilitado globalmente (AllowAnyOrigin)**.  
- Para producción, se recomienda **restringir los orígenes** únicamente a los dominios que consumirán la API.  


