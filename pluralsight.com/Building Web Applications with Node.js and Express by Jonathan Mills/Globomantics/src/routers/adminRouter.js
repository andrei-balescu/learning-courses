const express = require('express');
const debug = require('debug')('app:adminRouter');
const { MongoClient } = require('mongodb');
const sessionData = require('../data/sessions.json');
const { post } = require('./sessionRouter');

const adminRouter = express.Router();

adminRouter.route('/')
    .post((request, response) => {
        const url = 'mongodb://ozzie:pluralsight@localhost:27017/Globomantics';

        (async function mongo(){
            let client;
            
            try {
                client = await MongoClient.connect(url);
                debug('connected to the MongoDB');

                const db = client.db();

                const dbResponse = db.collection('sessions').insertMany(sessionData);
                response.json(dbResponse);
            } catch (error) {
                debug(error.stack);
                response.status(500).json({ error: "Data could not be saved" });
            }
        }());

        // response.send('cannot connect');
    })

module.exports = adminRouter;