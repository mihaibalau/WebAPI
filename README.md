# 🌐 WebAPI Project

This project contains the backend API for the application. It defines routes and logic that allow you to interact with the database through HTTP requests.

---

## 📌 Purpose

The WebAPI exposes a set of endpoints that handle operations like:

- Creating and retrieving departments (`/api/department`)
- Managing users (`/api/users`)
- Other entity-based operations (only where needed)

> These endpoints are used to interact with the database. You will use them from the **WinUI project** by sending HTTP requests (GET, POST, DELETE, etc.).

---

## ⚠️ Do Not Modify

You **should not** make any changes to this WebAPI project. It has been fully set up to support the application and should remain untouched unless explicitly instructed.

All your work will be in the **WinUI project**.

---

## 🗄️ Database Setup

To run the API properly:

1. Create a **SQL Server database** named `HospitalDb`.
2. Run the **initialization scripts** found in:

   ```
   Data/
   ```

   These scripts will create the necessary tables and seed any required data.

---

## ✅ Summary

- You **do not code here**.
- Use the provided endpoints from the WinUI client.
- Make sure `HospitalDb` exists and is properly initialized before running the app.
