const express = require('express');
const debug = require('debug')('app:authRouter');
const mongoClient = require('../data/mongoClient');

const authRouter = express.Router();

authRouter.route('/signup')
    .get((request, response) => {
        response.render('signup');
    })
    .post((request, response) => {

        (async function(){
            try {
                await addUser(request.body);

                request.logIn(request.body, () => {
                    response.redirect('/auth/profile');
                });
            } catch (error) {
                debug(error);
                response.status(500).send('Could not create user');
            } finally {
                await mongoClient.close();
            }
        })();
    });

authRouter.route('/profile')
    .get((request, response) => {
        response.json(request.user);
    });

async function addUser(formData){
    const user = { username, password } = formData;

    const db = await mongoClient.connect();
    const dbResult = await db.collection('users').insertOne(user);
    debug(dbResult);
}

module.exports = authRouter;