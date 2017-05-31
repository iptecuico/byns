(function () {
    "use strict";

    var appConfig = function (routeErrorHandler, routeAuthHandler, logger, $rootScope, $timeout, $location, $window) {
        //Redirect to HTTPS
        var forceSsl = function () {
            if ($location.protocol() !== "https" && $location.host() === "byns.azurewebsites.net") {
                $window.location.href = $location.absUrl().replace("http", "https");
            }
        };
        forceSsl();

        routeErrorHandler.start();
        routeAuthHandler.start();

        $rootScope.layout = {};
        $rootScope.layout.loading = false;

        $rootScope.$on("$stateChangeStart", function () {
            //show loading gif
            $timeout(function () {
                $rootScope.layout.loading = true;
            });
        });
        $rootScope.$on("$stateChangeSuccess", function () {
            //hide loading gif
            $timeout(function () {
                $rootScope.layout.loading = false;
            }, 200);
        });
        $rootScope.$on("$stateChangeError", function () {
            //hide loading gif
            $rootScope.layout.loading = false;
        });

        logger.debug.info("app.run executed");
    };

    appConfig.$inject = ["routeErrorHandler", "routeAuthHandler", "logger", "$rootScope", "$timeout", "$location", "$window"];

    angular.module("app")
            .run(appConfig);
}());