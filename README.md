[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/ggPzmVTe)
# ASP.NET Core MVC - Product Management Assignment (Monolithic-DependencyInjection App)

## Overview
Welcome to your GitHub Classroom MVC assignment! 🎉  
This project is a **Product Management System** built with **ASP.NET Core MVC** and **SQLite**, following a **layered architecture** with **Dependency Injection** (DI).

### Existing Features
- **ASP.NET MVC** (UI Layer) for presentation and user interaction.
- **ProductsBLL** (Business Logic Layer) for business rules and validations.
- **ProductsDAL** (Data Access Layer) for database interactions using SQLite.

#### Functionalities:
- Add a Product
- View Product Details (by ID)

## Your Task
Enhance the application by adding **Edit** and **Delete** functionality for products, ensuring that **Dependency Injection** is properly utilized throughout the layers.

### Learning Objectives
By completing this assignment, you will:
- Understand how a **Monolithic Application** with **layered architecture** and **Dependency Injection** is structured and implemented.
- Learn how **ASP.NET MVC** follows the **separation of concerns** principle and how Dependency Injection fits into this.
- Implement **Update (Edit)** and **Delete** operations in the **Business Logic Layer** (ProductsBLL) using Dependency Injection for better separation of concerns.
- Modify the **Controller** (ProductsController in ASP.NET MVC) to support these operations using Dependency Injection.
- Update the **Views** (UI Layer) to include **Edit** and **Delete** options.
- Ensure changes are reflected properly in the **SQLite database** (ProductsDAL) using DI to manage data interactions.