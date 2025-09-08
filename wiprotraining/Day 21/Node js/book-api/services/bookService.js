const express = require('express');
const fs = require('fs').promises;
const path = require('path');
const EventEmitter = require('events');

const router = express.Router();
const eventEmitter = new EventEmitter();

const filePath = path.join(__dirname, '../data/books.json');

// Event Listeners
eventEmitter.on('bookAdded', () => console.log('📚 Book Added'));
eventEmitter.on('bookUpdated', () => console.log('✏️ Book Updated'));
eventEmitter.on('bookDeleted', () => console.log('🗑️ Book Deleted'));

// Helper function to read books
const readBooks = async () => {
    try {
        const data = await fs.readFile(filePath, 'utf-8');
        return JSON.parse(data);
    } catch (err) {
        console.error('Error reading books:', err);
        return [];
    }
};

// Helper function to write books
const writeBooks = async (books) => {
    try {
        await fs.writeFile(filePath, JSON.stringify(books, null, 2));
    } catch (err) {
        console.error('Error writing books:', err);
    }
};

// GET all books
router.get('/', async (req, res) => {
    const books = await readBooks();
    res.json(books);
});

// GET book by ID
router.get('/:id', async (req, res) => {
    const books = await readBooks();
    const book = books.find(b => b.id === parseInt(req.params.id));
    if (!book) return res.status(404).json({ error: 'Book not found' });
    res.json(book);
});

// POST create book
router.post('/', async (req, res) => {
    const { title, author } = req.body;
    if (!title || !author) return res.status(400).json({ error: 'Title and author required' });

    const books = await readBooks();
    const newBook = {
        id: books.length ? books[books.length - 1].id + 1 : 1,
        title,
        author
    };
    books.push(newBook);
    await writeBooks(books);

    eventEmitter.emit('bookAdded');
    res.status(201).json(newBook);
});

// PUT update book
router.put('/:id', async (req, res) => {
    const { title, author } = req.body;
    const books = await readBooks();
    const index = books.findIndex(b => b.id === parseInt(req.params.id));
    if (index === -1) return res.status(404).json({ error: 'Book not found' });

    books[index] = { ...books[index], title: title || books[index].title, author: author || books[index].author };
    await writeBooks(books);

    eventEmitter.emit('bookUpdated');
    res.json(books[index]);
});

// DELETE book
router.delete('/:id', async (req, res) => {
    const books = await readBooks();
    const filteredBooks = books.filter(b => b.id !== parseInt(req.params.id));

    if (books.length === filteredBooks.length) return res.status(404).json({ error: 'Book not found' });

    await writeBooks(filteredBooks);

    eventEmitter.emit('bookDeleted');
    res.json({ message: 'Book deleted successfully' });
});

module.exports = router;


