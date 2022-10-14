const passport = require('passport');
// use a local login strategy (as opposed to Google/Facebook - social login)
const { Strategy } = require('passport-local');
const mongoClient = require('../data/mongoClient');

// see https://www.npmjs.com/package/passport

function configLocalStrategy(){
    passport.use(new Strategy({
        // pass the input field names present in the login form
        usernameField: 'username',
        passwordField: 'password'
    }, 
    (username, password, doneCallback) => {
        (async function validateUser(){
            try {
                const db = await mongoClient.connect();
                const user = await db.collection('users').findOne({ username });

                if (user && user.password === password){
                    doneCallback(null, user);
                } else {
                    doneCallback(null, false);
                }
            } catch (error) {
                // bad practice: will propagate error to thr page
                doneCallback(error, false)
            } finally {
                await mongoClient.close();
            }
        })()
    }));
}

function configPassport(app){
    configLocalStrategy();

    app.use(passport.initialize());
    app.use(passport.session());

    passport.serializeUser((user, doneCallback) => {
        // params: error, userId
        doneCallback(null, user);
    });

    passport.deserializeUser((user, doneCallback) => {
        doneCallback(null, user);
    });
}

module.exports = configPassport;