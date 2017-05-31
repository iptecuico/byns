(function () {
    "use strict";

    var routeErrorHandler = function ($rootScope, $location, logger) {
        var start = function () {
            var handlingRouteChangeError = false;

            $rootScope.$on("$stateChangeError", function (event, current) {
                if (handlingRouteChangeError) {
                    return;
                };

                handlingRouteChangeError = true;

                logger.debug.error("Routing error: name = " + current.name);
                $location.path("/");
            });
        };

        return {
            start: start
        }
    };

    routeErrorHandler.$inject = ["$rootScope", "$location", "logger"];

    angular.module("ctException")
            .factory("routeErrorHandler", routeErrorHandler);
}());