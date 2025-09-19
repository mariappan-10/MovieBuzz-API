# MovieBuzz API

Movie database API built with .NET 9.0 for searching movies and managing personal watchlists using the OMDb API.

## Features

- **Movie Search**: Search by title with year/page filters
- **Movie Details**: Get full movie info using IMDb ID
- **User Authentication**: JWT-based auth with role-based access
- **Watchlist Management**: Personal movie watchlists
- **Admin Panel**: User management and system monitoring

## Tech Stack

- .NET 9.0 Web API
- Entity Framework Core + MySQL
- JWT Authentication
- Swagger/OpenAPI
- Clean Architecture

## API Endpoints

**Authentication**
- `POST /api/Account/register` - Register user
- `POST /api/Account/login` - Login

**Movies**
- `GET /api/Movies/{imdbId}` - Get movie details
- `GET /api/Movies/search/{title}` - Search movies
- `POST /api/Movies/add-to-watchlist` - Add to watchlist
- `GET /api/Movies/display-watchlist` - View watchlist

**Admin**
- `GET /api/Account/users` - List all users
- `GET /api/Movies/admin/all-users-watchlists` - All watchlists