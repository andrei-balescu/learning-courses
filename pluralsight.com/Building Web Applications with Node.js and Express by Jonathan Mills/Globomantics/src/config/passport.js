const passport = require('passport');

// see https://www.npmjs.com/package/passport

module.exports = function configPassport(app){
    app.use(passport.initialize());
    app.use(passport.session());

    passport.serializeUser((user, doneCallback) => {
        // params: error, userId
        doneCallback(null, user);
    });

    passport.deserializeUser((user, done) => {
        done(null, user);
    });
}