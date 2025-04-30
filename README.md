# Codebase overview

Our codebase consists of 3 projects: the web api, the winui project, class library project. A more in-depth description of each is down below.

# 🌐 WebAPI Project

This is the **backend API** for the application. It defines HTTP routes used to interact with the system's data.

## 📌 Purpose

The WebAPI exposes endpoints for working with application entities such as:

- Departments (`/api/department`)
- Users (`/api/users`)
- [Other relevant entities]

These endpoints support operations like `GET`, `POST`, `DELETE`, etc., depending on what was implemented.

You will **use this API** from the **WinUI project** by making HTTP requests to these endpoints.

---

## 🚫 Do Not Modify This Project

You are **not supposed to make any changes** in this project.

✅ **Your work is only done in the WinUI project**, where you will call these API endpoints.

---

## 🔍 Testing the API (Optional)

You can view and test the available routes using Swagger UI:

```
https://localhost:<port>/swagger/index.html
```

Make sure the API is running before using Swagger.

---

### WinUI project

---

### Class Library project
