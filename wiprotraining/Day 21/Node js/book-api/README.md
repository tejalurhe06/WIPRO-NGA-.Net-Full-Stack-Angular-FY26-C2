# Book Management REST API

## Setup
1. Clone/download repository.
2. Run `npm install`.
3. Run `npm start` to start the server (uses nodemon).

## Endpoints
- `GET /` → welcome message
- `GET /books` → returns all books
- `GET /books/:id` → returns book by id
- `POST /books` → add book { title, author }
- `PUT /books/:id` → update book fields
- `DELETE /books/:id` → delete book

## Data
Books are stored in `data/books.json` (array).

## Notes
- Uses async/await and fs.promises
- Emits events: bookAdded, bookUpdated, bookDeleted (logged to console)
