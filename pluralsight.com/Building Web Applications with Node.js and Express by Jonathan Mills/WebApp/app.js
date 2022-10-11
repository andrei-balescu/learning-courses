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

const app = express();

// app.use(morgan('combined'));
app.use(morgan('tiny'));

app.get('/', (request, response) => {
    response.send('Hello world!')
});

app.listen(3000, () => {
    // using template strings (ES6)
    debug(`listening on port ${chalk.green('3000')}`);
});