

# 📚 Smart Student Query Management API

A **.NET 8 Web API** for managing student queries with AI-powered instant answers using **OpenAI API (ChatGPT models)**.
This project allows students to submit queries, stores them in a database, and generates automated responses.

---

## Architecture 
<img width="1536" height="1024" alt="image" src="https://github.com/user-attachments/assets/41755169-4f2f-4ad6-8035-e6eddb8eebd7" />


## 🚀 Features

* ✅ Student query submission & storage
* ✅ AI-powered answers using **OpenAI Chat Completions API**
* ✅ 7 RESTful API endpoints
* ✅ DTO-based request handling
* ✅ Secure & scalable with **async/await, DI, error handling**
* ✅ Auto-documented with **Swagger**

---

## 🛠️ Tech Stack

* **Backend:** C#, .NET 8, ASP.NET Core Web API
* **Database:** EF Core, SQLite (code-first migrations)
* **AI Integration:** OpenAI API (`gpt-3.5-turbo` / `gpt-4`)
* **Tools:** Swagger (API docs), curl/Postman (testing)

---

## 📂 Project Structure

```
SmartStudentQueryAPI/
 ┣ Controllers/      # API endpoints
 ┣ Data/             # DbContext & migrations
 ┣ Models/           # Entity models
 ┣ Services/         # AIHelper (OpenAI integration)
 ┣ DTOs/             # Request/response DTOs
 ┣ Program.cs        # App startup & DI
 ┗ SmartStudentQueryAPI.csproj
```

---

## ⚡ Setup & Run

### 1️⃣ Clone Repo


git clone https://github.com/yourusername/SmartStudentQueryAPI.git
cd SmartStudentQueryAPI


### 2️⃣ Configure OpenAI API Key

In **appsettings.json**:

```json
{
  "OpenAI": {
    "ApiKey": "YOUR_OPENAI_API_KEY",
    "Model": "gpt-3.5-turbo"
  }
}
```

### 3️⃣ Apply Migrations & Run

dotnet ef database update
dotnet run


API will be available at 👉 `https://localhost:5001/swagger`

---

## 📌 Example API Usage

### Add a Query


POST /api/query
Content-Type: application/json

{
  "studentId": 1,
  "queryText": "When is the next exam?"
}


### Get AI Answer

```http
GET /api/query/{id}/answer
```

Response:

```json
{
  "query": "When is the next exam?",
  "aiAnswer": "The exam schedule will be announced soon. Please check your academic portal for updates."
}
```

---

