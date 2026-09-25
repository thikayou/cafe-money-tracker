# Cafe Tracker | Project Overview

A budget tracking web app for a coffee shop, built for the co-owners to track
monthly savings contributions, set goals, jot notes, get reminders, plan
upcoming work, and collect Pinterest-style inspiration — all in one cute,
minimalist, brown-and-green dashboard.


## Tech stack at a glance

| Layer | Choice |
|-------|--------|
| Backend | ASP.NET Core 8 Web API (C#) |
| Database | PostgreSQL (free via Supabase) — SQLite as a zero-install local alternative |
| ORM | Entity Framework Core |
| Auth | ASP.NET Core Identity + JWT |
| Frontend | React 18 + Vite (JavaScript) | 
| Styling | Tailwind CSS with a custom brown/green theme |
| Design | Figma |
| Inspiration board | Custom DB-backed gallery + optional Pinterest widget embed |
| Hosting | Render/Railway (API) + Vercel/Netlify (frontend) + Supabase (DB) |

## Quick start

```bash
# backend
cd backend/CafeTracker.Api
dotnet restore
dotnet ef database update
dotnet run

# frontend (in a second terminal)
cd frontend
npm install
npm run dev
```

Then open `http://localhost:5173`.
