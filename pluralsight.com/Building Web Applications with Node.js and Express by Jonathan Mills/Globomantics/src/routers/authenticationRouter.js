const express = require('express');
const adminRouter = require('./adminRouter');

adminRouter.route('/signup')
    .get((request, response) => {
        response.render('signup');
    });

module.exports = adminRouter;