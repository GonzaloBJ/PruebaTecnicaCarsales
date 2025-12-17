# BFF.Web

Proyecto generado con angular18 para el frontend.

## Requerimientos:

- Separar la lógica en componentes según sea necesario.
- Asegurar la separación de lógica en capas.
- NO crear componentes repetitivos.
- Asegurarse de implementar todo lo solicitado en la evaluación.
- Agregar nuevas funcionalidades no solicitadas. Implementar filtros, búsquedas y más interacciones en la web.
- Implementar un componente para presentar errores correctamente al usuario.
- Realizar un diseño atractivo.
- Utilizar tipado en las variables, NO utilizar "any".
- Utilizar más funcionalidades propias de angular, Hidratación incremental, Signals, Directivas Mejoradas, Standalone components.
- Evitar llamadas innecesarias en servicios (next(), complete())


## ⚙️ Guía de Configuración y Ejecución
Para levantar y probar la aplicación, siga estos pasos:

### 1. Prerrequisitos
Asegúrese de tener instalado el siguiente software:

* **Node.js:** Versión 18.x o superior.
* **Angular CLI:** Instalar globalmente (`npm install -g @angular/cli`).


### 2. Modo edición de variables de entorno/configuraciones
* El archivo `environment.ts` y  `environment.development.ts` comtienen las variables utilizadas.
* Se compone de una constante cuyo valor es un objeto que contiene una variable:
```ts
{
  BFFUrl: "http://localhost:5013/api/"
}
```
* **BFFUrl**: refiere al endpoint del api de nuestro backend.



### 3. Iniciar el Frontend (Cliente Angular)
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

