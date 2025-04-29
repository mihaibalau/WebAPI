# WebAPI

Our project works in the following way:
We have two repositories: 
  1) WebAPI (this repo) 🌐
      This project runs a local web server that hosts a local database.
      Think of it like a server hosted somewhere on the internet — but in our case, it's running on your own machine.
      You can send HTTP requests to this server to fetch or modify data.
      
      Example:
      When you're using the app and need a list of users to display, you call a method in your client app that makes an HTTP request to this WebAPI.
      Instead of directly querying a database (e.g., SQL Server), you query this API, which returns the needed data.
      
      ✅ For homework purposes, the WebAPI is not deployed to an online server — you run it locally as a separate project.
     
  2) Client repo (where the actual app is): *soon to be documented*


Extra note: most of the files in this project are irrelevant for our purpose, don't waste too much time trying to understand what every folder is.

---

## WebAPI Project Structure

```
.
├── 📜 LICENSE                                      # License file for the project
├── 📂 Models
│   └── Department.cs                               # Department model 
├── 📂 Pages
│   ├── ❗ Error.cshtml                             # Razor page to display error details
│   ├── ❗ Error.cshtml.cs                          # Code-behind for error page handling
│   ├── 🏠 Index.cshtml                             # Razor page for the home/index page
│   ├── 🏠 Index.cshtml.cs                          # Code-behind for index page logic
│   ├── 🔒 Privacy.cshtml                           # Razor page for privacy policy
│   ├── 🔒 Privacy.cshtml.cs                        # Code-behind for privacy page logic
│   ├── 📂 Shared
│   │   ├── 🧩 _Layout.cshtml                       # Common layout (header/footer) for pages
│   │   ├── 🎨 _Layout.cshtml.css                   # CSS for shared layout styling
│   │   └── ✅ _ValidationScriptsPartial.cshtml     # Partial view for client-side validation scripts
│   ├── 🧰 _ViewImports.cshtml                      # Import namespaces and tag helpers globally
│   └── 🚀 _ViewStart.cshtml                        # Set the layout for Razor pages
├── 🚀 Program.cs                                   # Entry point: sets up and runs the web app
├── 📂 Properties
│   └── 🎛️ launchSettings.json                      # Defines profiles for local launch (e.g., ports, environment)
├── 📖 README.md                                    # Project documentation and instructions
├── 📦 WebApi.csproj                                # C# project file with references and build settings
├── 🧩 WebApi.sln                                   # Solution file for the project
├── ⚙️ appsettings.Development.json                 # App settings specific to Development environment
├── ⚙️ appsettings.json                             # Main app configuration (connection strings, logging, etc.)
└── 🌐 wwwroot                                      # Static files like CSS, JS, images (not C#-related)
```
