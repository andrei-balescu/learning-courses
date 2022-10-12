const express = require('express');
const sessionData = require('../data/sessions.json');

const sessionRouter = express.Router();

sessionRouter.route('/')
    .get((request, response) => {
        response.render('sessions', {
            sessions: sessionData
        });
    });

sessionRouter.route('/:id')
    .get((request, response) => {
        const id = request.params.id;

        response.render('session', {
            session: sessionData[id]
        });
    });


// Each file in a Node.js project is treated as a module that can export values to be used by other modules.
// module.exports is an object in a Node.js file that holds the exported values and functions from that module.
module.exports = sessionRouter;