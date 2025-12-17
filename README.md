# 🚀 Prueba Técnica Carsales: 
Cliente Rick and Morty (BFF + Angular)Este proyecto es la solución a la prueba técnica de desarrollo, la cual consiste en crear una aplicación que consume la API de **Rick and Morty** (principalmente la información de episodios) utilizando una arquitectura **Backend for Frontend (BFF)** implementada con **.NET 8** y un cliente web desarrollado con **Angular**.

El objetivo es demostrar buenas prácticas de desarrollo, manejo de arquitectura, paginación, manejo de errores y documentación.

## 🛠️ Tecnologías Utilizadas
| Componente | Tecnología | Versión Clave | Características Implementadas |
| --- | --- | --- | --- |
| **Backend (BFF)** | **.NET** | .NET 8 | Patrones de Diseño, Separación de Lógica (Servicios/Controladores), Manejo de Errores. |
| **Frontend** | **Angular** | Angular 18 | Componentes Standalone, Tipado Fuerte, Paginación, Directivas modernas, Signals, Estilos CSS puros (sin frameworks). |
| **API** | **Rick and Morty API** | - | Consumo de la información de episodios. |

## 📐 Arquitectura del Proyecto
El proyecto sigue una arquitectura **Backend for Frontend (BFF)**, donde el servicio de .NET actúa como una capa de abstracción y seguridad entre el cliente Angular y la API pública de Rick and Morty.

* **Frontend (Angular):** Se comunica **únicamente** con el BFF para solicitar datos. Esta estructurado bajo **Feature-First**.
* **Backend (BFF - .NET):** Es el responsable de llamar a la API externa, aplicar cualquier lógica de negocio intermedia (si fuera necesaria), y estructurar la respuesta de manera óptima y exclusivamente para el frontend web.

## 📋 Criterios de Evaluación Cubiertos
Se han abordado los siguientes puntos solicitados en los criterios de la prueba:

### Criterios Generales
* ✅ **Paginación:** Implementada en el Frontend para la lista de episodios, comunicándose con el Backend que maneja los parámetros de página.
* ✅ **SOLID y Patrones de Diseño:**
* **Inyección de Dependencias (DI):** Uso extensivo en .NET para desacoplar servicios.
* **Principio de Responsabilidad Única (SRP):** El BFF separa la lógica de llamada API del controlador (usando servicios).


* ✅ **Manejo de Errores:** Control de errores HTTP en ambos extremos (BFF y Angular), con mensajes informativos al usuario.
* ✅ **Documentación:** Este `README` detalla la configuración y ejecución.
* ✅ **Archivos de Configuración:** Uso de `appsettings.json` en .NET y `environment.ts` en Angular.
* ✅ **No Utilización de Frameworks CSS:** Estilos implementados con CSS puro.
* ✅ **GitHub:** Código alojado en el repositorio (enlace provisto).
* ✅ **Filtros/Interacciones:** Se ha implementado un filtro simple por nombre de episodio.

### Criterios Backend (.NET 8)
* ✅ **Utilizar .NET 8:** El proyecto BFF ha sido configurado con la última versión LTS.
* ✅ **Separación de Lógica:** La lógica de consumo de la API está encapsulada en la capa de servicios, desacoplando los controladores.

### Criterios Frontend (Angular)
* ✅ **Tipado de Variables:** Uso de `interfaces` y `types` de TypeScript en Angular para definir los modelos de datos, asegurando la integridad de los datos recibidos del BFF.
* ✅ **Funcionalidades Modernas de Angular (v17+):**
* **Componentes Standalone:** Todos los componentes, directivas y módulos están definidos como Standalone.

## ⚙️ Guía de Configuración y Ejecución
Para levantar y probar la aplicación, siga estos pasos:

### 1. Prerrequisitos
Asegúrese de tener instalado el siguiente software:

* **SDK de .NET:** Versión 8.0 o superior.
* **Node.js:** Versión 18.x o superior.
* **Angular CLI:** Instalar globalmente (`npm install -g @angular/cli`).

### 2. Estructura del Repositorio
El repositorio contiene dos carpetas principales:

```
/
├── Backend/
│   └── BFF.Web/  <- Proyecto .NET 8
│   
└── Frontend/
    └── Web/ <- Proyecto Angular 18

```

### 3. Modo edición de variables de entorno/configuraciones
1. Para el backend:
* El archivo `appsettings.json` y `appsettings.Development.json` contienen las variables utilizadas.
* Se compone de una seccion y una variable, ademas de las proporcionadas por dotnet cli al momento de crear el proyecto:
```json
"core": {
    "rickandmortyBaseURL": "https://rickandmortyapi.com/api/"
  }
```
* **rickandmortyBaseURL**: refiere al endpoint de la api utilizada para esta demo.

2. Para el frontend:
* El archivo `environment.ts` y  `environment.development.ts` comtienen las variables utilizadas.
* Se compone de una constante cuyo valor es un objeto que contiene una variable:
```ts
{
  BFFUrl: "http://localhost:5013/api/"
}
```
* **BFFUrl**: refiere al endpoint del api de nuestro backend.


### 4. Iniciar el Backend (BFF)
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


### 5. Iniciar el Frontend (Cliente Angular)
1. Abra una nueva terminal y navegue a la carpeta del cliente:
```bash
cd Frontend/Web
```

2. Instale las dependencias:
```bash
npm install
```

3. Ejecute la aplicación en modo de desarrollo:
```bash
ng serve
```

> El cliente Angular se ejecutará por defecto en `http://localhost:4200`.


Una vez que ambos servicios estén levantados, abra **`http://localhost:4200`** en su navegador para interactuar con la aplicación.