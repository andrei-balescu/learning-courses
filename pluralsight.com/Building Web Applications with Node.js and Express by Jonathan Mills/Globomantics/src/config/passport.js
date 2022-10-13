const passport = require('passport');
// use a local login strategy (as opposed to Google/Facebook - social login)
const { Strategy } = require('passport-local');

// see https://www.npmjs.com/package/passport

function configLocalStrategy(){
    passport.use(new Strategy({
        // pass the input field names present in the login form
        usernameField: 'username',
        passwordField: 'password'
    }, 
    (username, password, doneCallback) => {
        const user = { username, password, name: 'Ozzie' };
        doneCallback(null, user);
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