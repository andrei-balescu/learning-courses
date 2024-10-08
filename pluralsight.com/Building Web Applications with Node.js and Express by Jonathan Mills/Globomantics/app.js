// use Express library
const express = require('express');
// provides logging utils such as text coloring;
// NOTE: version 5 requires ES6
const chalk = require('chalk');
// advanced debugging
// start app with `DEBUG=* node app.js` to enable logging
// start app with `DEBUG=app node app.js` to show logs for this file only
const debug = require('debug')('app');
// provides debugging info for web traffic
const morgan = require('morgan');
// manage local file paths
// part of Node.js package
const path = require('path');
// handles user management
const passport = require('passport');
const cookieParser = require('cookie-parser');
const expressSession = require('express-session');

// environment variables
const PORT = process.env.PORT || 3000;

const app = express();

const sessionRouter = require('./src/routers/sessionRouter');
const adminRouter = require('./src/routers/adminRouter');
const authRouter = require('./src/routers/authRouter');

// --- middleware ---
// app.use(morgan('combined'));
app.use(morgan('tiny'));
// serve static files from root/public
// NOTE: index.html matches the `/` url by convention
app.use(express.static(path.join(__dirname, '/public/')));
// provides request.body
// used to be bodyparser.json
app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(expressSession({ secret: 'globomantics' }));

// execute passport configuration
// NOTE: all middleware needs to be executed in order before configuring passport
require('./src/config/passport.js')(app);

app.set('views', './src/views');
app.set('view engine', 'ejs');

app.use('/sessions', sessionRouter);
app.use('/admin', adminRouter);
app.use('/auth', authRouter);

app.get('/', (request, response) => {
    response.render('index', { title: 'Globomantics' })
});

app.listen(PORT, () => {
    // using template strings (ES6)
    debug(`listening on port ${chalk.green(PORT)}`);
});