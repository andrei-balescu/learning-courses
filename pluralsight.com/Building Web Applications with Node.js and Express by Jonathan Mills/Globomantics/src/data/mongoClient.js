const debug = require('debug')('app:mongoClient');
const { MongoClient } = require('mongodb');
const { connect } = require('../routers/sessionRouter');

let client;

async function connectToDB(){
    const url = 'mongodb://ozzie:pluralsight@localhost:27017/Globomantics';

    client = await MongoClient.connect(url);
    debug(`connected to ${url}`);

    return client.db();
}

/**
 * Use this in a try/catch/finally block
 */
async function closeConnection(){
    await client.close();
    debug('closed connection');
}

module.exports = {
    connect: connectToDB,
    close: closeConnection
}