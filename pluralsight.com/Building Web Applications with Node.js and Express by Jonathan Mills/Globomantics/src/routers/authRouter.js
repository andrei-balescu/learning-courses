const { request, response } = require('express');
const express = require('express');
const debug = require('debug')('app:sessionRouter');
const { MongoClient, ObjectId } = require('mongodb');

const authRouter = express.Router();

authRouter.route('/signup')
    .get((request, response) => {
        response.render('signup');
    })
    .post((request, response) => {
        // TODO: create user
        request.logIn(request.body, () => {
            response.redirect('/auth/profile');
        });
    });

authRouter.route('/profile')
    .get((request, response) => {
        response.json(request.user);
    });

module.exports = authRouter;