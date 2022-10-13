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
        response.json(request.body);
    });

module.exports = authRouter;