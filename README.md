# Controlbox - Book Review Application

A full-stack web application that allows users to browse, search, and review books. Built with a strict Clean Architecture approach using .NET for the backend and React (Vite) for the frontend.

## 🚀 Live Demo
- **Frontend (Vercel):** https://controlbox-project-amd.vercel.app/
- **Backend API:** https://controlboxapi-latest.onrender.com/

## 🛠️ Tech Stack
**Frontend:**
- React 18 (Vite)
- TypeScript
- Tailwind CSS
- Zustand (State Management)
- React Router DOM
- Axios

**Backend:**
- .NET 10 (Web API)
- CQRS Pattern with MediatR
- Entity Framework Core
- PostgreSQL
- ASP.NET Core Identity & JWT Authentication

## 🏗️ Architecture
The backend strictly follows **Clean Architecture** principles, separated into four layers:
1. **Domain:** Enterprise logic and entities (No external dependencies).
2. **Application:** Business logic, Use Cases (CQRS), and Interface definitions.
3. **Infrastructure:** External concerns (Database, Identity, JWT generation).
4. **Api:** Controllers, Dependency Injection setup, and HTTP routing.

## 💻 Local Setup Instructions

### Prerequisites
- .NET 10 SDK
- Node.js (v18+)
- PostgreSQL (Running locally or via Docker)

### 1. Backend Setup
1. Navigate to the API folder:
   \`\`\`bash
   cd BookReview.Api
   \`\`\`
2. Update the connection string in `appsettings.json` with your local PostgreSQL credentials.
3. Apply database migrations (The seeder will automatically populate initial data on the first run):
   \`\`\`bash
   dotnet ef database update --project ../BookReview.Infrastructure
   \`\`\`
4. Run the API:
   \`\`\`bash
   dotnet run
   \`\`\`
   *(The API will usually run on `http://localhost:5086`. Swagger UI is available at `/swagger`)*

### 2. Frontend Setup
1. Open a new terminal and navigate to the client folder:
   \`\`\`bash
   cd client
   \`\`\`
2. Install dependencies:
   \`\`\`bash
   npm install
   \`\`\`
3. Create a `.env` file in the `client` directory and point it to your local API:
   \`\`\`env
   VITE_API_URL=http://localhost:5086/api
   \`\`\`
4. Start the development server:
   \`\`\`bash
   npm run dev
   \`\`\`

## 🔄 CI/CD Pipeline
This repository utilizes **GitHub Actions** for Continuous Integration. Upon pushing to the `main` branch, the workflow automatically:
- Restores and builds the .NET Backend.
- Installs dependencies and builds the React Frontend.

## For Register in the app, use STRONG password

Deployment is handled via Vercel for the frontend (connected to the GitHub repository for automatic deployments), Render for the backend API and Amazon RDS for PostgreSQL Database.
