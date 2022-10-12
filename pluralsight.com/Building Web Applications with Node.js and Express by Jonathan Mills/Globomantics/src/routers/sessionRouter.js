const express = require('express');
const debug = require('debug')('app:sessionRouter');
const { MongoClient, ObjectId } = require('mongodb');

const sessionRouter = express.Router();

sessionRouter.route('/')
    .get((request, response) => {
        (async function(){
            try {
                const db = await connectToDB();

                const sessions = await db
                    .collection('sessions')
                    .find()
                    .toArray();
                response.render('sessions', { sessions });
            } catch (error) {
                debug(error.stack);
                response.status(500).send("Could not retrieve session data");
            }
        }());
    });

sessionRouter.route('/:id')
    .get((request, response) => {
        const id = request.params.id;
        (async function(){
            try {
                const db = await connectToDB();

                const session = await db
                    .collection('sessions')
                    .findOne({ _id: new ObjectId(id) });
                response.render('session', { session });
            } catch (error) {
                debug(error.stack);
                response.status(500).send("Could not retrieve session data");
            }
        }());
    });

async function connectToDB(){
    const url = 'mongodb://ozzie:pluralsight@localhost:27017/Globomantics';

    const client = await MongoClient.connect(url);
    debug('connected to MongoDB');

    const db = client.db();
    return db;
}

// Each file in a Node.js project is treated as a module that can export values to be used by other modules.
// module.exports is an object in a Node.js file that holds the exported values and functions from that module.
module.exports = sessionRouter;