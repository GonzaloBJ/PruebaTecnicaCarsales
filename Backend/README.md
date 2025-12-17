# BFF.Web

Proyecto generado con .Net8 para el Backend.

## Requerimientos:

- Asegurar la separación de lógica en capas.
- Utilizar correctamente los patrones de diseño.
- Implementar conceptos SOLID.
- Implementar middleware para el manejo de errores.

## ⚙️ Guía de Configuración y Ejecución
Para levantar y probar la aplicación, siga estos pasos:

### 1. Prerrequisitos
Asegúrese de tener instalado el siguiente software:

* **SDK de .NET:** Versión 8.0 o superior.

### 2. Modo edición de variables de entorno/configuraciones
* El archivo `appsettings.json` y `appsettings.Development.json` contienen las variables utilizadas.
* Se compone de una seccion y una variable, ademas de las proporcionadas por dotnet cli al momento de crear el proyecto:
```json
"core": {
    "rickandmortyBaseURL": "https://rickandmortyapi.com/api/"
  }
```
* **rickandmortyBaseURL**: refiere al endpoint de la api utilizada para esta demo.

### 3. Iniciar el Backend (BFF)
1. Navegue a la carpeta del proyecto:
```bash
cd Backend/BFF.Web
```

2. Restaure las dependencias (si es necesario):
```bash
dotnet restore
```

3. Ejecute la aplicación:
```bash
dotnet run
```

> El BFF se ejecutará por defecto en `https://localhost:5013`.
> El BFF cuanta con swagger UI en `http://localhost:5013/swagger`
