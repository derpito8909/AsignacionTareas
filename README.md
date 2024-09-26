<h1 align="center">
    asignacion de tareas
</h1>

## Introducción
Asignacion de tareas es un API REST en .net core version 8 para un sistema de asignación de tareas con autenticación de
usuarios y sistema de roles, esta API se desarrollo en la arquitectura MVC con el patron de diseño repositorio y injeccion de dependencias

## Desarrollo

<details open>
<summary>
Pre-requisitos
</summary> <br />
Para ejecutar la aplicacion necesita tener instalado este aplicacion
###

- skd donet 8
</details>

<details open>
<summary>
ejecutar la aplicacion
</summary> <br />

para ejecutar la aplicacion:

1. clone el repositorio de esta direccion

```shell
 git clone git@github.com:derpito8909/AsignacionTareas.git
```
2. ingrese a las carperta cd AsignacionTareas/tareasAPI/
```shell
 cd AsignacionTareas/tareasAPI/
```
3. instale las dependencias con dotnet restore
```shell
 dotnet restore
```
4. publicar la aplicacion
```shell
 dotnet publish -c Release -o out
```
5. ejecutar aplicacion
```shell
 dotnet out/tareasAPI.dll
```
6. la aplicacion esta lista en http://localhost:5000


   
