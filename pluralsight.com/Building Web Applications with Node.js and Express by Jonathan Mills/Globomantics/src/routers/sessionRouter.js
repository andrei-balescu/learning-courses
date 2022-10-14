const express = require('express');
const debug = require('debug')('app:sessionRouter');
const { ObjectId } = require('mongodb');
const mongoClient = require('../data/mongoClient');

const sessionRouter = express.Router();

// middleware for sessions router
sessionRouter.use((request, response, next) => {
    if (request.user){
        next();
    } else {
        response.redirect('/auth/signin');
    }
})

sessionRouter.route('/')
    .get((request, response) => {
        (async function(){
            try {
                const db = await mongoClient.connect();

                const sessions = await db
                    .collection('sessions')
                    .find()
                    .toArray();
                response.render('sessions', { sessions });
            } catch (error) {
                debug(error.stack);
                response.status(500).send("Could not retrieve sessions");
            } finally {
                await mongoClient.close();
            }
        }());
    });

sessionRouter.route('/:id')
    .get((request, response) => {
        const id = request.params.id;
        (async function(){
            try {
                const db = await mongoClient.connect();

                const session = await db
                    .collection('sessions')
                    .findOne({ _id: new ObjectId(id) });
                response.render('session', { session });
            } catch (error) {
                debug(error.stack);
                response.status(500).send(`Could not retrieve session ${id}`);
            } finally {
                await mongoClient.close();
            }
        }());
    });

// Each file in a Node.js project is treated as a module that can export values to be used by other modules.
// module.exports is an object in a Node.js file that holds the exported values and functions from that module.
module.exports = sessionRouter;