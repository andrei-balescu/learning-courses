# Playstore.Catalog.Service project
Project using:
- .NET 6 WebAPI.
- MongoDB
- RabbitMQ
- Docker for containerization

## Endpoints
### /items

Retrieve all items in catalog  
Request: **GET** /items  
``` json
GET /items
```
Response: 200 Success  
``` json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "Potion",
    "description": "Restores a small amount of HP",
    "price": 5,
    "createdDate": "2024-11-03T06:22:42.364Z"
  }
]
```

---

Retrieve specific item details  
Request: **GET** /items/{id}  
``` json
GET /items/3fa85f64-5717-4562-b3fc-2c963f66afa6
```
Response: 200 Success  
``` json
{
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "Potion",
    "description": "Restores a small amount of HP",
    "price": 5,
    "createdDate": "2024-11-03T06:22:42.364Z"
}
```

---

Create new item in catalog  
Request: **POST** /items  
``` json
POST /items
{
  "name": "Bronze sword",
  "description": "Deals a small amount of damage",
  "price": 20
}
```
Response: 200 Success  
``` json
{
  "id": "77981ced-f62f-491f-8768-a4ed521b4afb",
  "name": "Bronze sword",
  "description": "Deals a small amount of damage",
  "price": 20,
  "createdDate": "2024-11-03T06:30:24.276Z"
}
```

---

Update existing item  
Request: **PUT** /items/{id}  
``` json
PUT /items/3fa85f64-5717-4562-b3fc-2c963f66afa6
{
  "name": "Antidote",
  "description": "Cures poison",
  "price": 7
}
```
Response: 204 No content  

---

Delete item from catalog  
Request: **DELETE** /items/{id}  
``` json
DELETE /items/77981ced-f62f-491f-8768-a4ed521b4afb
```
Response: 200 Success  