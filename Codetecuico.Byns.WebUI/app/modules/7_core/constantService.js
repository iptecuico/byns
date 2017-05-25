(function () {

    angular.module("app")
        .constant("appInfo", {
            APP_NAME: " | Byns by Codetecuico",
            APP_VERSION: "v1.0"
        });

    //angular.module("app")
    //    .constant("apiService", {
    //        BASE_URL: "http://localhost:14303/api"
    //        //BASE_URL: "http://bynsapi.azurewebsites.net/api"
    //    });

    angular.module("app")
        .constant("authInfo", {
            AUTH0_DOMAIN: 'codetecuico.auth0.com',
            AUTH0_CLIENT_ID: 'ZUCOYNwmRRiJioqUCKdoBtTI21vgFmZp'
        });
}());