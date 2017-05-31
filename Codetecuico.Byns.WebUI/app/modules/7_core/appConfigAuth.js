(function () {
    "use strict";

    var initAuth = function (authProvider, jwtInterceptorProvider, $httpProvider, jwtOptionsProvider, authInfo) {
        authProvider.init({
            domain: authInfo.AUTH0_DOMAIN,
            clientID: authInfo.AUTH0_CLIENT_ID//,
            //loginState: "home"
        });

        var refreshingToken = null;
        jwtInterceptorProvider.tokenGetter = function (store, jwtHelper) {
            var token = store.get("token");
            //var refreshToken = store.get("refreshToken");
            if (token) {
                if (!jwtHelper.isTokenExpired(token)) {
                    return store.get("token");
                } else {
                    //if (refreshingToken === null) {
                    //refreshingToken = auth.refreshIdToken(refreshToken).then(function (idToken) {
                    //    store.set("token", idToken);
                    //    return idToken;
                    //}).finally(function () {
                    //    refreshingToken = null;
                    //});
                    //}
                    return refreshingToken;
                }
            }
        };

        jwtOptionsProvider.config({
            whiteListedDomains: ["localhost", "bynsapi.azurewebsites.net"]
        });

        $httpProvider.interceptors.push("jwtInterceptor");
        delete $httpProvider.defaults.headers.common["X-Requested-With"];
        $httpProvider.defaults.headers.post = { "Content-Type": "application/json" };
        $httpProvider.defaults.headers.common["Access-Control-Allow-Origin"] = "*";
        $httpProvider.defaults.headers.common["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS";
        $httpProvider.defaults.headers.common["Accept-header"] = "application/json";

        console.info("app.config auth executed");
    };

    initAuth.$inject = ["authProvider", "jwtInterceptorProvider", "$httpProvider", "jwtOptionsProvider", "authInfo"];

    angular.module("app")
            .config(initAuth);
}());