### Exclusive E-Commerce Website:-

This project is a full-stack web application developed using ASP.NET Core MVC. It simulates a basic e-commerce platform where users can browse products, manage a cart, place orders, and track order history. The application also includes an admin panel for managing products and orders.

---

### Features:-

### User Module:-
- User registration and login functionality
- Session-based authentication
- Display success and error messages using SweetAlert
- Browse products from database and external API
- Add products to cart
- Update product quantity in cart
- Remove products from cart
- Checkout system with billing details

### Payment Methods:-
- Card payment
- UPI payment
- Net banking
- Cash on Delivery (COD)

### Order Management:-
- Place orders successfully
- View order history
- Order status tracking:
  - Placed
  - Shipped
  - Delivered
  - Cancelled
- Invoice generation with print option

---

## API Integration:-

The application integrates with the FakeStore API to fetch product data dynamically. These products can also be added to the cart and used within the application.

---

## Admin Module:-

- View all orders
- Update order status
- Manage product listings

---

## Technologies Used:-

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server (SSMS)
- Bootstrap
- JavaScript and jQuery
- SweetAlert2
- FakeStore API

---

## Project Structure:-
Controllers/
AccountController.cs
CartController.cs
OrderController.cs
ApiController.cs

Models/
User.cs
Product.cs
Cart.cs
Order.cs

Views/
Account/
Cart/
Order/
Api/

Data/
AppDbContext.cs

### Security:-

Basic authentication has been implemented. Passwords are stored in plain text for demonstration purposes. This can be improved by implementing password hashing.

---

### Future Enhancements:-

- Password encryption and hashing
- Email verification
- Payment gateway integration (Razorpay or Stripe)
- Improved UI and responsiveness
- Role-based authentication (Admin/User)

---

### Author:-

Arjya Biswal

---

### Note:-

This project is developed for academic purposes and demonstrates the core concepts of ASP.NET Core MVC, database integration, API usage, and web application development.