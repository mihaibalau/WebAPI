# 🌐 WebAPI Project

Welcome to the **WebAPI** backend! This project powers the application's connection to the database by exposing a series of HTTP routes (also called endpoints). It allows you to perform operations like retrieving, creating, or deleting data without ever touching the database logic yourself.

---

## 📬 What is a Route?

A **route** is simply a URL endpoint exposed by the API. For example:

```
GET /api/departments
```

This will return a list of all departments. You don't need to worry about how the data is fetched — that's handled inside this API. Your job is to **make requests** to these routes from the **WinUI** project.

---

## ⚙️ Setup Instructions

Before you start using the API, you **must** complete the following setup:

1. 🔧 Open **SQL Server Management Studio**.
2. 🗄️ Create a database named: `HospitalDb`.
3. 📥 Run the **initialization scripts** located in the `Data/` folder of this project.

This will ensure all required tables and initial data are available for the API to function properly.

---

## 🚫 Do Not Modify This Project

> You **must not** make any changes to this WebAPI project.  

All your development will happen inside the **WinUI project**. This API is already set up and ready to use — treat it as a backend service you consume.

---

## ✅ Available Endpoints

Most entities exposed by the API support:

- `GET` – Retrieve data
- `POST` – Create new records
- `DELETE` – Remove records

For example:

- `/api/departments`
- `/api/users`
- ...and more, where relevant

Note: Not all routes support every HTTP method if it wasn't necessary.

---

# 📚 Class Library Project

The **Class Library** is a shared project that contains common logic used across the entire solution. It includes:

- 🧩 **Interfaces** for all repositories
- 📦 **Domain models** (e.g., `Department`, `User`)

This project helps keep the code modular, clean, and easy to maintain.
