# ContactFormApi

## About

REST API for integration of ***Contact Forms*** and ***Feedback Forms*** to web applications and static websites.

Messages from ***Contact Forms*** and ***Feedback Forms*** are stored in ***PostgreSQL*** and email notifications are delivered using ***SendGrid***.

## Features

- ASP.NET Core 8
- Layered Architecture
- REST API
- Contact Form
- Feedback Form
- Multi-application support (AppKey)
- PostgreSQL
- FluentValidation
- SendGrid Email Notifications
- Deployed 

## Project Structure

```text
ContactFormApi
│
├── ContactFormApi.Api
│   ├── Controllers
│   ├── Program.cs
│   └── DependencyInjection.cs
│
├── ContactFormApi.Application
│   ├── DTOs
│   ├── Interfaces
│   ├── Settings
│   ├── Validators
│   └── DependencyInjection.cs
│
├── ContactFormApi.Domain
│   ├── Entities
│   └── Enums
│
├── ContactFormApi.Infrastructure
│   ├── Configuration
│   ├── Data
│   ├── Email
│   ├── Migrations
│   ├── Repositories
│   └── DependencyInjection.cs
│
└── README.md
```

### ContactFormApi.Api
Presentation layer.
Contains controllers, application startup and API configuration.

### ContactFormApi.Application
Application layer.
Contains DTOs, business services, validators and interfaces.

### ContactFormApi.Domain
Domain layer.
Contains entities and enums independent of infrastructure.

### ContactFormApi.Infrastructure
Infrastructure layer.
Contains Entity Framework Core, PostgreSQL repositories, SendGrid integration and configuration.

## Deployment

The API is deployed to an Ubuntu VPS using:

- Hetzner VPS
- Ubuntu 22.04
- ASP.NET Core 8
- PostgreSQL
- Nginx
- systemd
- HTTPS (Let's Encrypt)

## API Endpoints

### Contact Form

```
POST /api/contact
```

### Feedback Form

```
POST /api/feedback
```

## Used in Projects

This API is currently integrated into:

- Survey Company Website : 
https://unis-inspections.zalizko.site/

- Surveyor's Website : 
https://surveyor.p.zalizko.site/

## Technologies

- C#
- ASP.NET Core 8
- Entity Framework Core
- PostgreSQL
- FluentValidation
- SendGrid
- Nginx
- systemd
- Git
- Linux

## Multi-Application Support

The API supports multiple independent applications using AppKey configuration.

Each application can have:

- its own email recipients
- its own application name
- independent Contact Forms
- independent Feedback Forms

## Screenshot

<img width="1920" height="1080" alt="ContactFormApi_Screenshot 2026-07-13_103855" src="https://github.com/user-attachments/assets/a391e7cf-8ebc-4734-ba95-0ecea0ccdc62" />


## Status

- Deployed
