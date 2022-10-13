const express = require('express');
const debug = require('debug')('app:adminRouter');
const sessionData = require('../data/sessions.json');
const mongoClient = require('../data/mongoClient');

const adminRouter = express.Router();

adminRouter.route('/')
    .post((request, response) => {
        (async function mongo(){
            try {
                const db = await mongoClient.connect();

                const dbResponse = await db.collection('sessions').insertMany(sessionData);
                response.json(dbResponse);
            } catch (error) {
                debug(error.stack);
                response.status(500).json({ error: "Data could not be saved" });
            } finally {
                await mongoClient.close();
            }
        }());
    })

module.exports = adminRouter;