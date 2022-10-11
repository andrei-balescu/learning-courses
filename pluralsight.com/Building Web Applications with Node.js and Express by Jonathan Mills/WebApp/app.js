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

const app = express();

// app.use(morgan('combined'));
app.use(morgan('tiny'));
// serve static files from root/public
// NOTE: index.html matches the `/` url by convention
app.use(express.static(path.join(__dirname, '/public/')));

app.get('/', (request, response) => {
    response.send('Hello from my application!')
});

app.listen(3000, () => {
    // using template strings (ES6)
    debug(`listening on port ${chalk.green('3000')}`);
});