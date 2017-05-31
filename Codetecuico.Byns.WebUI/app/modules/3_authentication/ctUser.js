(function () {
    "use strict";

    var ctUser = function (store, auth, jwtHelper, $q, logger) {
        var dbProfileKey = "dbProfile";
        var profileKey = "profile";
        var tokenKey = "token";
        var refreshTokenKey = "refreshToken";

        var setToken = function (token) {
            return store.set(tokenKey, token);
        };

        var setDbProfile = function (profile) {
            return store.set(dbProfileKey, profile);
        };

        var getAuth = function () {
            return auth;
        };

        var getDbProfile = function () {
            return store.get(dbProfileKey);
        };

        var getProfile = function () {
            return store.get(profileKey);
        };

        var getToken = function () {
            return store.get(tokenKey);
        };

        var getRefreshToken = function () {
            return store.get(refreshTokenKey);
        };

        var isTokenExpired = function (token) {
            return jwtHelper.isTokenExpired(token);
        };

        var signIn = function () {
            var deferred = $q.defer();

            auth.signin({ authParams: { scope: "openid offline_access" } }
                        , function (profile, token, accessToken, state, refreshToken) {
                            store.set(profileKey, profile);
                            store.set(tokenKey, token);
                            store.set(refreshTokenKey, refreshToken);

                            var data = {
                                profile: profile,
                                auth: auth
                            };

                            deferred.resolve(data);
                        }, function (error) {
                            deferred.reject(error);
                        });

            return deferred.promise;
        };

        var signOut = function () {
            auth.signout();
            store.remove(profileKey);
            store.remove(dbProfileKey);
            store.remove(tokenKey);
            store.remove(refreshTokenKey);

            store.remove("myUserProfile"); //temporary only
        };

        var authenticate = function (token) {
            if (auth.isAuthenticated === false) {
                logger.debug.info("triggered ctUser.authenticate");
                var profile = getProfile();
                auth.authenticate(profile, token);
            }
        };

        var refreshAndAuthenticate = function (refreshingToken, refreshToken) {
            logger.debug.info("triggered ctUser.refreshAndAuthenticate");

            if (refreshingToken === null) {
                refreshingToken = auth.refreshIdToken(refreshToken)
                                        .then(function (idToken) {
                                            setToken(idToken);
                                            var profile = getProfile();
                                            auth.authenticate(profile, idToken);
                                        }).finally(function () {
                                            refreshingToken = null;
                                        });
            }
            return refreshingToken;
        };

        var mockSignIn = function () {
            store.set(profileKey, { name: "Test User", user_id: "google-oauth2|114343767643441344703" });
            store.set(tokenKey, "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczovL2NvZGV0ZWN1aWNvLmF1dGgwLmNvbS8iLCJzdWIiOiJnb29nbGUtb2F1dGgyfDExNDM0Mzc2NzY0MzQ0MTM0NDcwMyIsImF1ZCI6IlpVQ09ZTndtUlJpSmlvcVVDS2RvQnRUSTIxdmdGbVpwIiwiZXhwIjoxNDkxNTcwMTE5LCJpYXQiOjE0OTE1MzQxMTksImF6cCI6IlpVQ09ZTndtUlJpSmlvcVVDS2RvQnRUSTIxdmdGbVpwIn0.ZE5M7_kNkBV1SrmWRPAO1-Fc79eiAXkyy-2olIwdGFQ");
            store.set(refreshTokenKey, "testRefreshToken");
        };

        return {
            setDbProfile: setDbProfile,
            setToken: setToken,

            auth: getAuth,
            dbProfile: getDbProfile,
            profile: getProfile,
            token: getToken,
            refreshToken: getRefreshToken,
            isTokenExpired: isTokenExpired,

            signIn: signIn,
            signOut: signOut,
            authenticate: authenticate,
            refreshAndAuthenticate: refreshAndAuthenticate,

            mockSignIn: mockSignIn
        }
    };

    ctUser.$inject = ["store", "auth", "jwtHelper", "$q", "logger"];

    angular.module("ctAuthentication")
            .factory("ctUser", ctUser);
}());