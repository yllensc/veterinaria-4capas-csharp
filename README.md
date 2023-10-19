# Veterinary

Backend de una veterinaria para gestión administrativa con CSharp a través de estructura de 4 capas y code-first migration

## Comenzando 🚀

El proyecto de desarrollo de software tiene como objetivo principal la creación de un sistema de administración para una veterinaria. Este sistema permitirá a los administradores y al personal de la veterinaria gestionar de manera eficiente y efectiva todas las actividades relacionadas con la atención de mascotas y la gestión de clientes.
## Requerimientos funcionales
1. Autenticación y autorización:
    - El sistema debe implementar protección en los endpoints utilizando JWT (JSON Web Tokens). El token tiene una duracion de 1 minuto.
    - Se debe implementar refresh token.
    - Debe restringir las peticiones a los endpoints según los roles de los usuarios.
2. Se debe permitir realizar procesos de creacion, edicion, eliminacion y listado de informacion de cada una de las tablas
3. El backend debe permitir restringir peticiones consecutivos usando tecnicas de limitacion por IP.
4. El backend debe permitir realizar la paginacion en  las peticiones get de todos los controladores.
5. Los controladores deben implementar 2 versiones diferentes (Query y Header)

## EndPoints requeridos
1.  Crear un consulta que permita visualizar los veterinarios cuya especialidad sea Cirujano vascular.
2.  Listar los medicamentos que pertenezcan a el laboratorio Genfar
3.  Mostrar las mascotas que se encuentren registradas cuya especie sea felina.
4.  Listar los propietarios y sus mascotas.
5.  Listar los medicamentos que tenga un precio de venta mayor a 50000
6.  Listar las mascotas que fueron atendidas por motivo de vacunacion en el primer trimestre del 2023
7.  Listar todas las mascotas agrupadas por especie.
8.  Listar todos los movimientos de medicamentos y el valor total de cada movimiento.
9.  Listar las mascotas que fueron atendidas por un determinado veterinario.
10. Listar los proveedores que me venden un determinado medicamento.
11. Listar las mascotas y sus propietarios cuya raza sea Golden Retriver
12. Listar la cantidad de mascotas que pertenecen a una raza a una raza. Nota: Se debe mostrar una lista de las razas y la cantidad de mascotas que pertenecen a la raza.


### Pre-requisitos 📋

- .NET 7.0
- MySQL

### Instalación 🔧

Migración de la base de datos (code-first migration):
Ejecuta los comandos:
```
1. dotnet ef migrations add ¨[nombreDeLaMigracion] --project ./Persistence --startup-project API --output-dir ./Data/Migrations
2. dotnet ef database update --project ./Infrastructure --startup-project ./API
```

Ejecución de la WebApi (desde la ruta del proyecto):
Ejecuta los comandos:
```
1. cd API
2. dotnet run
```
Al terminar, como es un proyecto local de momento, obtienes la información del localhost:
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/4fcda1fd-d1b6-41f9-9e29-3125dac99651)

## Ejecutando las pruebas ⚙️
### Ojito 👀:
El proyecto tiene una colección de postman con la petición del token que caduca cada 2 minutos, los 12 endpoints del requerimiento y un CRUD de prueba para Appointment.
Aquí ➡️: [CollectionPostman](https://github.com/yllensc/veterinaria-4capas-csharp/blob/main/VeterinarianEndpoints.postman_collection)
### User 👨‍💻💁‍♂️💁‍♀️:
#### 1. Register <br>
Endpoint: ```http://localhost:5223/api/User/register```

Método: ```POST```
<br>
Body:
```{"Email": "v2@gmail.com","UserName": "veterinario2","Password": "1234","IdenNumber": "123423344678"}```

#### 2. Token <br>
Endpoint: ```http://localhost:5223/api/User/token```

Método: ```POST```
<br>
Body: 
```{"UserName": "usuario8","Password": "1234"}```

#### 3. Refresh token <br>
Endpoint: ```http://localhost:5223/api/veterinaria/refresh-token```

Método: ```POST```
<br>
Body:
```{"RefreshToken":"9YIa9WNUKqobsKEr4R9z/dsUFr5Dm0x9fjj0IBXkYMw="}```

#### 4. Add role <br>
Endpoint: ```http://localhost:5223/api/User/addrole```

Método: ```POST```
<br>
Body:
```{ "UserName": "veterinario2","Role": "Veterinarian","Name": "juana banana","PhoneNumber": "3019284930","Specialty": "aves"}```
## Autenticación y autorización
Para este ejercicio, creé 3 roles, Administrator, Veterinarian, WithOutRole. Casi todas las peticiones HTTP autorizan a los usuarios, y hay peticiones en particular con restricciones por role. Por ejemplo, los veterinarios no tienen acceso a sus propios datos ni a los medicamentos y su respectiva gestión de compra y venta, para esas peticiones solo tienen permiso los admi.

### (Forbidden)
#### Ejemplo de un usuario con role de veterinario intentando acceder a sus propios datos:
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/6e84a912-2999-48fb-ade0-c16543e675a1)

### (Unuthorized)
#### Ejemplo de un usuario con permiso pero con el token vencido:
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/fc39621d-526d-4257-bd34-0b0a1db2c93c)


## Cruds
Las entidades que lo requieren tienen la implementación del CRUD, accediento a cada controller a través del nombre de la entidad y claramente de acuerdo a la solicitud cambian los parámetros de los POST.
### Ejemplo para Appointment del CRUD
#### GET 
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/2e62ccdd-1f55-4d73-b005-770b6444c88a)
#### POST
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/005ba64f-8afb-4ad2-b75c-2ea275639497)
#### PUT
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/27bec93c-fbd4-4977-be27-f50922d83eca)
#### DELETE
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/021f5324-687f-4767-9090-f50b92bf6bef)
## Versionado y paginado
La implementación de versiones se implementaron en las peticiones tipo GET, tanto de los CRUD como de los endpoints, y cobra relevancia en los gets que retornan listas.

Es decir, si accedes a los GET tal cual a través de la ruta del endpoint, estás accediendo a la versión por defecto (1.0). Ahora bien, para probar la versión 1.1, tienes que indicar en los headers la key: X-Version con su value en 1.1 y si quieres jugar con los parámetros de paginado y filtro, opcional puedes cambiar el pageIndex, pageSize o search (que puede ser tipo int o string, de acuerdo a la utilidad en cada endpoint) en el query de la solicitud, algo así:

![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/5916bf3d-0cdf-41c4-ac93-29b8ad338f45)

## RateLimit
El ratelimit define la cantidad de peticiones permitidas en el tiempo y sobrepasar el límite se refleja de esta manera:
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/f9f831e4-1564-4db6-9172-1f22f0673b0c)

## Endpoints ✌️🤘🆗😺🦝🐶🦄
Los endpoints son de tipo GET, por lo que todos cuentan con su versión 1.0 y 1.1, para los ejemplos, las consultas se van a presentar con diferentes versiones:
1. ``` http://localhost:5223/api/Veterinarian/cardiovascularSurgeonVeterinarian ``` <br>
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/19ae4e83-72f8-4e87-88f4-641c86c4c2fe)
2. ``` http://localhost:5223/api/Laboratory/medicineByGenfar ``` <br>
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/ad86673a-2369-4949-aeb8-ca1c504da58a)
3. ``` http://localhost:5223/api/Specie/petsBySpecieGato ``` <br>
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/e0b5aebf-102f-4960-a268-d636dceb92b0)
4. ``` http://localhost:5223/api/Owner/ownersWithPets ``` <br>
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/b19bf23b-2920-4b77-9c49-3a21a282a055)
5. ``` http://localhost:5223/api/Medicine/medicineWithLessThan600?search=Fenbendazol 10%  ``` <br>
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/bce49249-9a45-4a58-be9d-9fa5706139e4)
6. ``` http://localhost:5223/api/Appointment/petsOn2023On4forHerida en la pata ``` <br>
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/85e57941-6124-4a5b-8677-70159aa0f623)
7. ``` http://localhost:5223/api/Specie/speciesOnGroups?search=Perro ``` <br>
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/a795c917-ef30-44a2-a3f2-8d44df1025d9)
8. ``` http://localhost:5223/api/MedicineMovement/listMovementsWithTotal ``` <br>
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/18a99874-88f1-4ca6-8ecf-f6f0a8abee89)
9. ``` http://localhost:5223/api/Appointment/petsCaredByVeterinarian2?search=Daisy ``` <br>
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/1cb26e09-3e43-4905-be9b-064ee306295c)
10. ``` http://localhost:5223/api/Medicine/providerWithThisAmpicilina 500mg ``` <br>
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/b1f2a240-150e-47c0-96de-8501d263f7f2)
11. ``` http://localhost:5223/api/Pet/petsWithThisBudgerigar ``` <br>
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/b540321a-a498-4a41-9732-2b04d32ca4b2)
12. ``` http://localhost:5223/api/Pet/countPetsByRace ``` <br>
![image](https://github.com/yllensc/veterinaria-4capas-csharp/assets/117176562/f4bbfa9c-887c-402a-800e-340c276bdcc8)




















## Construido con 🛠️

* [ASP.NET Core]([http://www.dropwizard.io/1.0.2/docs/](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-7.0&tabs=visual-studio)) - El framework web usado
* [MySql]([https://maven.apache.org/](https://dev.mysql.com/doc/workbench/en/wb-mysql-utilities.html)) - Base de datos


## Autor✒️

* **Yllen Santamaría** - [Yllensc](https://github.com/yllensc)
