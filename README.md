# Codebase overview

Our codebase consists of 3 projects: the web api, the winui project, class library project. A more in-depth description of each is down below.

# 🌐 WebAPI Project – Backend REST API

This project serves as the **backend** for the application, exposing a structured set of RESTful endpoints that allow interaction with the system's data via HTTP.

It is built using **ASP.NET Core Web API**, and follows modern architecture practices including:

- ✅ Dependency Injection
- ✅ Clean separation of concerns (Controllers, Repositories, Entities, DTOs)
- ✅ Swagger for API documentation and testing

---

## 📁 Project Purpose

The WebAPI acts as the central hub for managing business entities such as:

- Departments (`/api/department`)
- Users (`/api/users`)
- [Other entities depending on the system]

This API is **consumed by the WinUI frontend**. All create, read, update, and delete (CRUD) operations are performed via HTTP requests.

---

## 📡 API Usage

Each route follows standard REST conventions:

| Method | Route               | Description                       |
|--------|--------------------|-----------------------------------|
| GET    | `/api/department`   | Get all departments               |
| POST   | `/api/department`   | Create a new department           |
| DELETE | `/api/department/{id}` | Delete department by ID         |
| PUT    | (if implemented)    | Update department details         |

> ⚠️ **Note:** Not all HTTP methods are implemented for every entity — only use routes that are explicitly defined.

---

## 🚫 Do Not Modify

You are **not supposed to make changes** to this project.

Your task lies within the **WinUI project**, where you will consume these endpoints using HTTP requests via your repository classes.

---

## 🧪 API Testing

You can view and test the API endpoints using the built-in **Swagger UI**:

```
https://localhost:<port>/swagger/index.html
```

Make sure your API is running before accessing the Swagger page.

---

Happy coding! 🎯

---

### WinUI project

---

### Class Library project
