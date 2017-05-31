(function () {
    var routeAuthHandler = function ($rootScope, $state, ctUser, $timeout) {
        var start = function () {
            var refreshingToken = null;
            var auth = ctUser.auth();

            $rootScope.$on("$stateChangeStart", function (event, current) {
                var token = ctUser.token();
                var refreshToken = ctUser.refreshToken();

                if (token) {
                    if (!ctUser.isTokenExpired(token)) {
                        ctUser.authenticate(token);
                    }
                    else {
                        if (refreshToken) {
                            //if (refreshingToken === null) {
                            //    refreshingToken = auth.refreshIdToken(refreshToken)
                            //                            .then(function (idToken) {
                            //                                ctUser.setToken(idToken);
                            //                                var profile = ctUser.profile();
                            //                                auth.authenticate(profile, idToken);
                            //                            }).finally(function () {
                            //                                refreshingToken = null;
                            //                            });
                            //}
                            return ctUser.refreshAndAuthenticate(refreshingToken, refreshToken);
                        } else {
                            $state.go("home");
                        }
                    }
                } else {
                    var isSecuredPage = current.hasOwnProperty("data");
                    if (isSecuredPage) {
                        if (current.data.requiresLogin) {
                            event.preventDefault();
                            $state.go("home");

                            //hide loading image on unauthenticated redirection to home
                            $timeout(function () {
                                $rootScope.layout.loading = false;
                            }, 200);
                        }
                    }
                };
            });
            auth.hookEvents();
        };

        return {
            start: start
        }
    };

    routeAuthHandler.$inject = ["$rootScope", "$state", "ctUser", "$timeout"];

    angular.module("ctAuthentication")
            .factory("routeAuthHandler", routeAuthHandler);
}());