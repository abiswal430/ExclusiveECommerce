# Exclusive E-Commerce Website

## Overview

Exclusive E-Commerce Website is a full-stack web application developed using ASP.NET Core MVC. The project demonstrates the implementation of an online shopping platform where users can browse products, manage a shopping cart, place orders, and track their purchase history.

The system also includes API integration, database connectivity, authentication, invoice generation, and an admin module for managing products and orders.

This project was developed for academic and learning purposes to understand real-world web application development using ASP.NET technologies.

---

# Key Features

## User Authentication

- User Registration
- User Login and Logout
- Session-Based Authentication
- Success and Error Notifications using SweetAlert

---

## Product Management

- Display products from SQL Server database
- Fetch products dynamically using FakeStore API
- Product image display
- Product price display
- Responsive product cards

---

## Shopping Cart System

- Add products to cart
- Remove products from cart
- Update product quantity
- Save products for later
- Dynamic total price calculation

---

## Checkout and Payment

- Checkout page with billing details
- Multiple payment options:
  - Card Payment
  - UPI Payment
  - Net Banking
  - Cash on Delivery (COD)

---

## Order Management

- Place orders successfully
- View order history
- Track order status:
  - Placed
  - Shipped
  - Delivered
  - Cancelled
- Download and print invoice

---

## Contact Module

- Contact form with validation
- Save contact messages into SQL Server database
- Success and error popup notifications

---

## Admin Module

- View all customer orders
- Update order status
- Manage products
- Monitor application data

---

# API Integration

The project integrates with the FakeStore API to fetch external product data dynamically.

API Features:
- Fetch product list using HttpClient
- Deserialize JSON response
- Display API products inside the application
- Add API products directly to cart

---

# Technologies Used

## Frontend
- HTML5
- CSS3
- Bootstrap 5
- JavaScript
- jQuery

## Backend
- ASP.NET Core MVC
- C#

## Database
- SQL Server
- Entity Framework Core

## External Libraries
- SweetAlert2
- Newtonsoft.Json

## API
- FakeStore API

---

# Project Architecture

The application follows the MVC (Model-View-Controller) architecture.

## Controllers
Handles application logic and user requests.

- HomeController
- AccountController
- CartController
- OrderController
- ApiController

## Models
Represents database entities and application data.

- User.cs
- Product.cs
- Cart.cs
- Order.cs
- ContactMessage.cs

## Views
Contains Razor UI pages.

- Account
- Cart
- Order
- Api
- Home

## Data
Handles database connectivity.

- AppDbContext.cs

---

# Database Tables

The project uses the following tables:

- Users
- Products
- Cart
- Orders
- OrderItems
- ContactMessages

---

# Security

Basic authentication has been implemented using sessions.

Current implementation stores passwords in plain text for demonstration purposes. In a production environment, password hashing and encryption should be implemented for better security.

---

# Future Improvements

The project can be enhanced further with:

- Password hashing and encryption
- Email verification
- Razorpay or Stripe payment gateway integration
- JWT authentication
- Role-based authorization
- Product search and filtering
- Wishlist functionality
- Advanced admin analytics
- Mobile responsive optimization

---

# How to Run the Project

## Step 1
Clone the repository.

```bash
git clone <repository-url>
```

## Step 2
Open the project in Visual Studio.

## Step 3
Update the SQL Server connection string in:

```bash
appsettings.json
```

## Step 4
Run database migrations.

```bash
dotnet ef database update
```

## Step 5
Run the application.

```bash
dotnet run
```

---

# Learning Outcomes

This project helped in understanding:

- ASP.NET Core MVC architecture
- Entity Framework Core
- SQL Server integration
- REST API integration
- Session management
- CRUD operations
- Shopping cart functionality
- Order processing system
- GitHub version control

---

# Author

Arjya Biswal

---

# Academic Note

This project was developed as part of academic coursework for Web Application Development using ASP.NET and demonstrates practical implementation of modern web application development concepts.