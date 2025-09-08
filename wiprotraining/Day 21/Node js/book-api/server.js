const express = require('express');
const app = express();
const PORT = 3000;

const bookRoutes = require('./services/bookService');

// Middleware
app.use(express.json());

// Root route
app.get('/', (req, res) => {
    res.json({ message: "Welcome to Book Management API" });
});

// Book routes
app.use('/books', bookRoutes);

// Start server
app.listen(PORT, () => {
    console.log(`Server running on http://localhost:${PORT}`);
});
