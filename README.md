# Atharv Construction

A lightweight expense/commission tracker for a construction contractor's government tender works, commission-based works, and private works. Built for daily use on a phone browser.

## Tech Stack

- **Frontend**: Angular 21 (standalone components, signals), Angular Material, mobile-first responsive UI.
- **Backend**: .NET 8 Web API, EF Core, Pomelo.EntityFrameworkCore.MySql, layered as Controllers → Services → Repositories.
- **Database**: MySQL 8.0.
- **Auth**: Custom mobile-number-only login (no OTP), JWT issued on match. No self-signup — only the Super Admin manages Manager accounts.

## Project Structure

```
construction-expense-manager/
  backend/
    ConstructionExpenseManager.sln
    ConstructionExpenseManager.Api/
      Controllers/   Services/   Repositories/   Models/   DTOs/   Data/   Common/
      appsettings.json
      Dockerfile
  frontend/
    src/app/
      core/        (models, services, guards, interceptors)
      layout/      (app shell: toolbar + sidenav)
      features/    (login, dashboard, tender-works, commission-works, private-works, users)
      shared/      (gst-vendor-form dialog, reused by tender & commission modules)
```

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Node.js 22+ and npm (Angular CLI is used via `npx`, no global install needed)
- MySQL 8.0 running locally on `localhost:3306` (already installed per your setup)

## Local Setup

### 1. Database

No manual schema creation needed — EF Core migrations create the database and tables. Just make sure MySQL is running and you know the `root` password.

### 2. Backend

```
cd backend/ConstructionExpenseManager.Api
```

Edit `appsettings.json` and set your real MySQL password:

```json
"ConnectionStrings": {
  "Default": "Server=localhost;Port=3306;Database=construction_expense_db;User=root;Password=YOUR_PASSWORD;"
}
```

Also change `Jwt:Secret` to your own long random string before any real/deployed use.

Install the EF Core CLI tool once (if you don't have it):

```
dotnet tool install --global dotnet-ef
```

Create and apply the initial migration:

```
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Run the API:

```
dotnet run
```

On first run, the app auto-applies pending migrations and seeds 5 users (1 Super Admin + 4 Managers) with **placeholder mobile numbers**:

| Name | Mobile Number | Role |
|---|---|---|
| Super Admin | 9000000001 | SuperAdmin |
| Manager 1 | 9000000002 | Manager |
| Manager 2 | 9000000003 | Manager |
| Manager 3 | 9000000004 | Manager |
| Manager 4 | 9000000005 | Manager |

Log in once as Super Admin (mobile `9000000001`) and use **Manage Managers** to update everyone's real mobile numbers (or edit `Data/DbSeeder.cs` before the first run and re-migrate).

The API runs on `https://localhost:5001` / `http://localhost:5000` by default (check the console output) with Swagger UI at `/swagger` in development.

### 3. Frontend

```
cd frontend
npm install
```

Confirm `src/environments/environment.ts` points at your backend:

```ts
export const environment = {
  production: false,
  apiBaseUrl: 'http://localhost:5000/api'
};
```

Run it:

```
npm start
```

Open `http://localhost:4200` on your phone or desktop browser (same Wi-Fi network — use your machine's LAN IP instead of `localhost` to test from a phone).

## Design Notes / Assumptions

- **Manager scoping (v1)**: all 4 Managers can view/add/update every work (Tender, Commission, Private). No per-manager work assignment table in v1 — this was the simpler option you approved. It can be added later without breaking the schema.
- **Deletes**: only the Super Admin can delete records (tenders, RA bills, GST vendor entries, milestones, categories, payments, materials). Managers can view/add/update only, per spec.
- **GST Vendor Sub-ledger**: implemented once as a shared table/service (`GstVendorEntry` + `GstVendorLedgerService`) reused by both the Tender module and the Commission module, since the fields and math are identical.
- **Dashboard "filter by work"**: interpreted as filtering by **work type** (Tender / Commission / Private), since the dashboard is a cross-work summary — filtering to one specific work would just duplicate that work's own detail page. Let me know if you actually wanted a per-work-instance filter instead.
- **RA Bills**: each RA bill stores its own corporator/officer commission %, defaulting to 10%/8%, editable per bill.

## API Overview

All endpoints are under `/api`. See Swagger (`/swagger`) once running for the full schema. Summary:

- `POST /auth/login`, `GET /auth/me`
- `GET/POST/PUT/DELETE /users` (Super Admin only)
- `GET/POST/PUT/DELETE /tender-works`, plus `/ra-bills` and `/gst-vendors` sub-routes
- `GET/POST/PUT/DELETE /commission-works`, plus `/gst-vendors` sub-routes
- `GET/POST/PUT/DELETE /private-works`, plus `/milestones`, `/categories`, `/categories/{id}/payments`, `/materials` sub-routes
- `GET /dashboard/summary?fromDate=&toDate=&workType=`

Delete endpoints require the `SuperAdmin` role; everything else just requires a valid login.

## Free Deployment

The code is provider-agnostic (standard ASP.NET Core + MySQL + static Angular build), so any host that runs a .NET 8 container/app and MySQL works. As of today, reasonable free-tier options:

**Backend (.NET API)**
- [Render](https://render.com) — free Web Service tier, deploys directly from the included `Dockerfile`. Free tier spins down when idle (cold start on next request).
- [Fly.io](https://fly.io) — small free allowance, deploys the same `Dockerfile` via `fly launch`.

**Database (MySQL)**
- [Aiven](https://aiven.io) — free trial plan for a small MySQL instance.
- [Railway](https://railway.app) — MySQL plugin, free usage credit for small workloads.
- [Clever Cloud](https://www.clever-cloud.com) — small free "Dev" MySQL plan.

Free-tier terms change often — check current limits before committing.

**Frontend (Angular static build)**
- [Cloudflare Pages](https://pages.cloudflare.com) or [Netlify](https://netlify.com) — both have generous permanent free tiers, git-based CI, custom domains, HTTPS.

### Deployment steps (generic)

1. **Database**: provision a MySQL instance, note its host/port/user/password/database name.
2. **Backend**:
   - Set the connection string via environment variable (no code change needed — ASP.NET Core config supports env var overrides):
     ```
     ConnectionStrings__Default=Server=<host>;Port=<port>;Database=<db>;User=<user>;Password=<password>;
     Jwt__Secret=<your-long-random-secret>
     Cors__AllowedOrigins__0=https://your-frontend-domain.example
     ```
   - Deploy the `Dockerfile` in `backend/ConstructionExpenseManager.Api/` to your chosen host.
   - The app auto-applies EF Core migrations and seeds the 5 users on first boot.
3. **Frontend**:
   - Edit `frontend/src/environments/environment.prod.ts` and set `apiBaseUrl` to your deployed backend URL.
   - `npm run build` (uses the production configuration) and deploy the `dist/frontend/browser` folder to your static host.
4. Log in as Super Admin and update the 4 managers' real mobile numbers from **Manage Managers**.
