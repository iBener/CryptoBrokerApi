# CryptoBrokerAPI Microservice Architecture Sample

You can see SwaggerUi of other services by selecting definition:

![image](https://github.com/iBener/CryptoBrokerApi/assets/5037744/6855742b-dc70-43bc-9dfe-b48c016f9aca)

## Usage
Just open with Visual Studio make sure you select "docker-compose" as startup project and than run

Sample create order json:
```json
{
  "userId": "ibrahim"
  "amount": 100,
  "price": 25000,
  "notificationChannels": [
    "sms",
    "email",
    "push"
  ]
}
```

## Features

- [x] Clean Architecture
- [x] API Gateway
- [x] Docker
- [x] RabbitMQ
- [ ] ELK Stack (or Graylog)
- [x] CQRS
- [ ] Saga Pattern
- [x] MediatR
- [x] MassTransit
- [ ] Autofac
- [x] Automapper
- [x] FluentValidation
- [x] Entity Framework Core
- [ ] Serilog


