# Playstore.Inventory.Service project
Project using:
- .NET 6 WebAPI.
- MongoDB
- RabbitMQ
- Docker for containerization

## Endpoints
### /items

Get items in user's inventory:  
Request: **GET** /items/{userId}
``` json
GET /items/3fa85f64-5717-4562-b3fc-2c963f66afa6
```
Response:
``` json
[
  {
    "catalogItemId": "89cbd415-2dd0-4d83-b154-6a8baee44d1b",
    "name": "Antidote",
    "description": "Cures poisoning",
    "quantity": 1,
    "acquiredDate": "2024-11-02T13:10:58.1762214+00:00"
  }
]
```

---

Grant items to user:  
Request: **POST** /items
``` json
POST /items
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "catalogItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "quantity": 3
}
```
Response: 200 Success  