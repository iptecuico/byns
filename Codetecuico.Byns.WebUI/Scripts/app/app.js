(function () {

    angular.module('ctLogger', []);

}());
(function () {

    var logger = function () {

        toastr.options = {
            //"debug": false,
            "positionClass": "toast-bottom-right"
            //"onclick": null,
            //"fadeIn": 300,
            //"fadeOut": 1000,
            //"timeOut": 5000,
            //"extendedTimeOut": 1000
        }

        var success = function (msg) {
            toastr.success(msg);
        };

        var info = function (msg) {
            toastr.info(msg);
        };

        var warning = function (msg) {
            toastr.warning(msg);
        };

        var error = function (msg) {
            toastr.error(msg);
        };

        // Developer code
        var isDebugActive = true;
        var debugPrefix = 'DEBUG: ';
        var logToConsole = true;

        var setMessage = function (msg) {
            return debugPrefix + msg;
        }

        var isValidLog = function (msg) {
            return (isDebugActive && msg !== "" && msg !== null);
        }

        var logMessage = function (logType, msg) {

            if (!isValidLog(msg)) {
                return;
            }

            var message = setMessage(msg);

            if (logToConsole) {

                if (logType === "info")
                    console.info(message);

                else if (logType === "success")
                    console.info(message);

                else if (logType === "warning")
                    console.warn(message);

                else if (logType === "error")
                    console.error(message);

            } else {

                if (logType === "info")
                    toastr.info(message);

                else if (logType === "success")
                    toastr.success(message);

                else if (logType === "warning")
                    toastr.warning(message);

                else if (logType === "error")
                    toastr.error(message);
            }
        }

        var debug = {

            info: function (msg) {
                logMessage("info", msg);
            },
            success: function (msg) {
                logMessage("success", msg);
            },
            warning: function (msg) {
                logMessage("warning", msg);
            },
            error: function (msg) {
                logMessage("error", msg);
            }
        };

        return {
            debug: debug,
            success: success,
            info: info,
            warning: warning,
            error: error
        }

    }

    angular.module('ctLogger')
            .factory('logger', logger);

}());
(function () {

    angular.module('ctException', ['ctLogger']);

}());
(function () {

    var appErrorHandler = function ($provide) {

        $provide.decorator('$exceptionHandler', ['$delegate', 'logger', function ($delegate, logger) {
            return function (exception, cause) {
                $delegate(exception, cause);
                logger.debug.error(exception.message);
            };
        }]);

    };

    appErrorHandler.$inject = ['$provide'];

    angular.module('ctException')
            .config(appErrorHandler);

}());
(function () {
    
    var routeErrorHandler = function ($rootScope, $location, logger) {

        var start = function () {
            var handlingRouteChangeError = false;

            $rootScope.$on('$stateChangeError', function (event, current) {
                if (handlingRouteChangeError) {
                    return;
                };

                handlingRouteChangeError = true;

                logger.debug.error('Routing error: name = ' + current.name);
                $location.path('/');
            });
        };

        return {
            start: start
        }

    };

    routeErrorHandler.$inject = ['$rootScope', '$location', 'logger'];

    angular.module('ctException')
            .factory('routeErrorHandler', routeErrorHandler);

}());
(function () {
     
    angular.module('ctAuthentication', [
                                        "auth0",
                                        "angular-storage",
                                        "angular-jwt"
                                      ]);

}());
(function () {

    var ctUser = function (store, auth, jwtHelper, $q, logger) {
        var dbProfileKey = 'dbProfile';
        var profileKey = 'profile';
        var tokenKey = 'token';
        var refreshTokenKey = 'refreshToken';

        var setToken = function (token) {
            return store.set(tokenKey, token);
        }

        var setDbProfile = function (profile) {
            return store.set(dbProfileKey, profile);
        }

        var getAuth = function () {
            return auth;
        }

        var getDbProfile = function () {
            return store.get(dbProfileKey);
        }

        var getProfile = function () {
            return store.get(profileKey);
        }

        var getToken = function () {
            return store.get(tokenKey);
        }

        var getRefreshToken = function () {
            return store.get(refreshTokenKey);
        }

        var isTokenExpired = function (token) {
            return jwtHelper.isTokenExpired(token);
        }

        var signIn = function () {
            var deferred = $q.defer();

            auth.signin({ authParams: { scope: 'openid offline_access' } }
                        , function (profile, token, accessToken, state, refreshToken) {
                            store.set(profileKey, profile);
                            store.set(tokenKey, token);
                            store.set(refreshTokenKey, refreshToken);

                            var data = {
                                profile: profile,
                                auth: auth
                            }

                            deferred.resolve(data);
                        }, function (error) {
                            deferred.reject(error);
                        });

            return deferred.promise;
        }

        var signOut = function () {
            auth.signout();
            store.remove(profileKey);
            store.remove(dbProfileKey);
            store.remove(tokenKey);
            store.remove(refreshTokenKey);

            store.remove('myUserProfile'); //temporary only
        }

        var authenticate = function (token) {
            if (auth.isAuthenticated === false) {
                logger.debug.info("triggered ctUser.authenticate");
                var profile = getProfile();
                auth.authenticate(profile, token);
            }
        }

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
        }

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

    angular.module('ctAuthentication')
            .factory('ctUser', ctUser);

}());
    (function () {

    var routeAuthHandler = function ($rootScope, $state, ctUser, $timeout) {

        var start = function () {
            var refreshingToken = null;
            var auth = ctUser.auth();

            $rootScope.$on('$stateChangeStart', function (event, current) {

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

    angular.module('ctAuthentication')
            .factory('routeAuthHandler', routeAuthHandler);

}());
(function () {

    angular.module("app", ["ui.router",
                            "ui.bootstrap",
                            "ui.utils",

                            "ngAnimate",
                            "ngResource",

                            "ctLogger",
                            "ctException",
                            "ctAuthentication"]);

}());
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
(function () {
     
    var routeConfig = function ($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise('/');

        $stateProvider
            .state("home", {
                url: "/",
                controller: "homeController",
                templateUrl: "/app/modules/home/home.html"
            })
            .state("cat", {
                url: "/cat/:name",
                controller: "categoryController",
                templateUrl: "/app/modules/category/category.html"
            })
            .state("user", {
                abstract: true,
                url: "/user",
                controller: "userController",
                templateUrl: "/app/modules/user/user.html",
                data: {
                    requiresLogin: true
                }
            })
            .state("user.profile", {
                url: "/profile",
                controller: "userProfileController",
                controllerAs: "vm",
                templateUrl: "/app/modules/user/userProfile.html",
                data: {
                    requiresLogin: true
                }
            })
            .state("user.edit", {
                url: "/edit",
                controller: "userProfileEditController",
                controllerAs: "vm",
                templateUrl: "/app/modules/user/userProfileEdit.html",
                data: {
                    requiresLogin: true
                }
            })
            .state("user.settings", {
                url: "/settings",
                controller: "userProfileEditController",
                controllerAs: "vm",
                template: "settings",
                data: {
                    requiresLogin: true
                }
            })
            .state("user.help", {
                url: "/help",
                controller: "userProfileEditController",
                controllerAs: "vm",
                template: "help",
                data: {
                    requiresLogin: true
                }
            })
            .state("item", {
                abstract: true,
                url: "/item",
                controller: "itemController",
                templateUrl: "/app/modules/item/item.html",
                data: {
                    requiresLogin: true
                }
            })
            .state("item.list", {
                url: "/list",
                controller: "listController",
                controllerAs: "vm",
                templateUrl: "/app/modules/item/list.html",
                data: {
                    requiresLogin: true
                }
            })
            .state("item.post", {
                url: "/post",
                controller: "postItemController",
                controllerAs: "vm",
                templateUrl: "/app/modules/item/postItem.html",
                data: {
                    requiresLogin: true
                }
            })
            .state("item.edit", {
                url: "/edit",
                controller: "editItemController",
                controllerAs: "vm",
                templateUrl: "/app/modules/item/editItem.html",
                data: {
                    requiresLogin: true
                },
                params: {
                    itemId: 0
                }
            })
            .state("item.settings", {
                url: "/settings",
                controller: "itemController",
                controllerAs: "vm",
                template: "item settings",
                data: {
                    requiresLogin: true
                }
            })
            .state("item.help", {
                url: "/help",
                controller: "itemController",
                controllerAs: "vm",
                templateUrl: "/app/modules/item/itemHelp.html",
                data: {
                    requiresLogin: true
                }
            });

        console.info("app.config route executed");
    }

    routeConfig.$inject = ["$stateProvider", "$urlRouterProvider"];

    angular.module("app")
            .config(routeConfig);

}());
(function () {

    var initAuth = function (authProvider, jwtInterceptorProvider, $httpProvider, jwtOptionsProvider, authInfo) {

        authProvider.init({
            domain: authInfo.AUTH0_DOMAIN,
            clientID: authInfo.AUTH0_CLIENT_ID//,
            //loginState: "home"
        });

        var refreshingToken = null;
        jwtInterceptorProvider.tokenGetter = function (store, jwtHelper) {
            var token = store.get('token');
            //var refreshToken = store.get('refreshToken');
            if (token) {
                if (!jwtHelper.isTokenExpired(token)) {
                    return store.get('token');
                } else {
                    if (refreshingToken === null) {
                        //refreshingToken = auth.refreshIdToken(refreshToken).then(function (idToken) {
                        //    store.set('token', idToken);
                        //    return idToken;
                        //}).finally(function () {
                        //    refreshingToken = null;
                        //});
                    }
                    return refreshingToken;
                }
            }
        }

        jwtOptionsProvider.config({
            whiteListedDomains: ["localhost", "bynsapi.azurewebsites.net"]
        });

        $httpProvider.interceptors.push('jwtInterceptor');
        delete $httpProvider.defaults.headers.common["X-Requested-With"];
        $httpProvider.defaults.headers.post = { 'Content-Type': 'application/json' };
        $httpProvider.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
        $httpProvider.defaults.headers.common['Access-Control-Allow-Methods'] = 'GET, POST, PUT, DELETE, OPTIONS';
        $httpProvider.defaults.headers.common['Accept-header'] = 'application/json';

        console.info('app.config auth executed');
    };

    initAuth.$inject = ['authProvider', 'jwtInterceptorProvider', '$httpProvider', 'jwtOptionsProvider', 'authInfo'];

    angular.module("app")
            .config(initAuth);

}());
(function () {

    var appConfig = function (routeErrorHandler, routeAuthHandler, logger, $rootScope, $timeout, $location, $window) {

        //Redirect to HTTPS 
        var forceSsl = function () {
            if ($location.protocol() !== 'https' && $location.host() === 'byns.azurewebsites.net') {
                $window.location.href = $location.absUrl().replace('http', 'https');
            }
        };
        forceSsl();

        routeErrorHandler.start();
        routeAuthHandler.start();

        $rootScope.layout = {};
        $rootScope.layout.loading = false;

        $rootScope.$on('$stateChangeStart', function () {
            //show loading gif
            $timeout(function () {
                $rootScope.layout.loading = true;
            });
        });
        $rootScope.$on('$stateChangeSuccess', function () {
            //hide loading gif
            $timeout(function () {
                $rootScope.layout.loading = false;
            }, 200);
        });
        $rootScope.$on('$stateChangeError', function () {
            //hide loading gif
            $rootScope.layout.loading = false;
        });

        logger.debug.info('app.run executed');
    };

    appConfig.$inject = ['routeErrorHandler', 'routeAuthHandler', 'logger', '$rootScope', '$timeout', '$location', '$window'];

    angular.module("app")
            .run(appConfig);

}());
(function () {
    "use strict";

    var app = angular.module("app");

    var appService = function ($q, store, apiService) {

        var version = "";

        var getVersion = function () {
            if (version === "") {
                version = apiService.app.getVersion().$promise;
            }
            return version;
        };

        return {
            getVersion: getVersion
        };
    };

    app.factory("appService", ["$q", "store", "apiService", appService]);

}());
(function () {

    var apiService = function ($resource, $location) {

        var baseUrlDev = "http://localhost:14303/api";
        var baseUrlProd = "https://bynsapi.azurewebsites.net/api";
        var baseUrl = baseUrlProd;

        if ($location.host() === 'byns.azurewebsites.net') {
            baseUrl = baseUrlProd;
        } else {
            baseUrl = baseUrlDev;
        }
        
        return {
            app: $resource(baseUrl + "/app/version", null, {
                'getVersion': { method: 'GET' }
            }),
            item: $resource(baseUrl + "/item", null, {
                'update': { method: 'PUT' },
                'delete': {
                    method: 'DELETE',
                    params: {
                        id: "@id"
                    }
                },
                'search': {
                    method: 'GET',
                    params: {
                        pageNumber: "@pageNumber",
                        pageSize: "@pageSize",
                        searchText: "@searchText"
                    }
                }
            }),
            user: $resource(baseUrl + "/user", null, {
                'me': {
                    url: baseUrl + "/user/me",
                    method: 'GET'
                },
                'get': { method: 'GET' },
                'update': { method: 'PUT' }
            })
        }
    }

    apiService.$inject = ["$resource", "$location"];

    angular.module("app")
            .factory("apiService", apiService);

}());
(function () {

    var mainController = function (logger) {
        logger.debug.success('mainController');
    };

    mainController.$inject = ['logger'];

    angular.module('app')
        .controller('mainController', mainController);

}());
(function () {
    var app = angular.module("app");

    app.directive("ctHeader", function () {
        return {
            restrict: "A",
            scope: {},
            transclude: true,
            templateUrl: "/app/shared/header.html"
        }
    });
}());
(function () {
    var app = angular.module("app");

    app.directive("ctFooter", function () {
        return {
            restrict: "A",
            scope: {},
            templateUrl: "/app/shared/footer.html",
            controllerAs: "vm",
            controller: function (appService) {
                var vm = this;
                vm.appVersion = "0";
                appService.getVersion().then(function (data) {
                    vm.appVersion = data.appVersion;
                });
            }
        }
    });
}());
(function () {

    var app = angular.module("app");

    app.directive('ctMenu', function () {
        return {
            restrict: "A",
            transclude: true,
            scope: {},
            templateUrl: "/app/shared/menu/menu.html",
            controller: function () {

            }
        }
    });

}());
(function () {

    var app = angular.module("app");

    app.directive('ctMenuItem', function () {
        return {
            restrict: "A",
            scope: {
                id: "@",
                description: "@",
                route: "@"
            },
            templateUrl: "/app/shared/menu/menuItem.html",
            controller: function () {
                //$scope.setSelectedMenu = function (menu) {
                //    $("#" + menu).parents().children().removeClass("active");
                //    $("#" + menu).addClass("active");
                //}
            },
            link: function (scope, element) {
                // exclude the directive's own element
                element.replaceWith(element.contents());
            }
        }
    });

}());
// Requires jquery-ui.*.js, datepicker.css, theme.css

(function () {

    var app = angular.module("app");

    app.directive("datePicker", function () {
        return {
            restrict: "A",
            link: function (scope, element) {
                element.datepicker({ dateFormat: 'dd MM yy' });
            }
        }

    });

}());
(function () {

    var app = angular.module("app");

    app.directive("commentDisqus", function () {
        return {
            scope:{
                url: "=",
                id: "="
            },
            templateUrl: "/app/shared/comment.html",
            controller: function () {

            }
        }

    });

}());

(function () {
    var app = angular.module("app");

    app.directive("loadingIndicator", function () {
        return {
            scope: {},
            templateUrl: "/app/shared/loadingIndicator.html"
        }
    });
}());
(function () {

    var dialogService = function ($uibModal) {

        var confirm = function (title, message, data, buttons, action) {
            var modalInstance = $uibModal.open({
                templateUrl: "app/shared/dialog/confirm.html",
                controller: "dialogController",
                controllerAs: "vm",
                backdrop: "static",
                resolve: {
                    data: function () {
                        return {
                            title: title,
                            message: message,
                            data: data,
                            buttons: buttons,
                            action: action
                        }
                    }
                },
                size: "sm"
            });

            return modalInstance.result;
        }

        return {
            confirm: confirm
        };
    };

    var module = angular.module("app");

    dialogService.$inject = ['$uibModal'];
    module.factory("dialogService", dialogService);

}());
(function () {

    var dialogController = function ($uibModalInstance, data) {
        var vm = this;

        vm.prop = data;

        vm.cancel = function () {
            $uibModalInstance.dismiss();
        }

        vm.ok = function () {
            $uibModalInstance.close();
        }
    };

    dialogController.$inject = ['$uibModalInstance', 'data'];

    angular.module('app')
        .controller('dialogController', dialogController);

}());
(function () {

    angular.module("app")
            .directive('errSrc', function () {
                return {
                    link: function (scope, element, attrs) {
                        element.bind('error', function () {
                            if (attrs.src !== attrs.errSrc) {
                                attrs.$set('src', attrs.errSrc);
                            }
                        });
                    }
                }
            });

}());
(function () {

    var ctFormInput = function () {
        return {
            restrict: "A",
            link: function (scope, element) {
                var div = element[0];
                div.classList.add("form-group");

                var input = div.querySelector("input");
                if (input !== null)
                    input.classList.add("form-control");

                var label = div.querySelector("label");
                if (label !== null)
                    label.classList.add("control-label");
            }
        }
    }

    angular.module("app")
            .directive("ctFormInput", ctFormInput);

}());

(function () {
    angular.module("app")
            .directive("mainLoadingIndicator", function () {
                return {
                    scope: {},
                    templateUrl: "/app/shared/mainLoadingIndicator.html",
                }
            });
}());
(function () {

    var homeController = function ($scope, itemService, appInfo) {
        $("title").text("All items" + appInfo.APP_NAME);

        $scope.items = [];
        $scope.itemCount = 0;
        $scope.isLoadingComplete = false;

        var currentSortOrder = itemService.getActiveSort();

        //Populating item list
        $scope.getItems = function () {
            itemService.search(1,50,"")
                .then(function (data) {
                    $scope.items = angular.copy(data.data);
                    itemService.setItems($scope.items);
                    $scope.itemCount = itemService.getItemCount();
                    $scope.isLoadingComplete = true;
                }, null);
        };

        $scope.getItems(currentSortOrder);

    };

    homeController.$inject = ["$scope", "itemService", "appInfo"];
    angular.module("app")
            .controller("homeController", homeController);

}());
(function () {

    var app = angular.module("app");

    app.directive('itemCard', function () {
        return {
            templateUrl: "/app/modules/home/itemCard.html",
            scope: {
                item: "=",
                showUserInfo: "="
            },
            controller: function () {

            }
        }
    });

}());
(function () {

    var app = angular.module("app");

    var itemCardDetailsController = function ($scope, $stateParams, itemService) {
        //$scope.item = {};

        itemService.getItem($stateParams.id)
            .then(function (data) {
                $scope.item = data[0];
                console.log($scope.item);
            }, null);
    };

    app.controller("itemCardDetailsController", ["$scope", "$stateParams", "itemService", itemCardDetailsController]);

}());
(function () {
    "use strict";

    var app = angular.module("app");

    var userService = function ($http, $q, store, apiService) {

        /* Active functions below
         * 
         */
        var me = function () {
            return apiService.user.me().$promise;
        };

        var get = function () {
            return apiService.user.get().$promise;
        };

        var create = function (user) {
            return apiService.user.save(user).$promise;
        }

        var update = function (user) {
            return apiService.user.update({ id: user.id }, user).$promise;
        };

        return {
            me: me,
            get: get,
            create: create,
            update: update
        };
    };

    app.factory("userService", ["$http", "$q", "store", "apiService", userService]);

}());
(function () {
    var userController = function ($scope, $stateParams, userService, appInfo) {
        $("title").text("User" + appInfo.APP_NAME);

        //$scope.user = userService.getUserInfo($stateParams.userId);
        //$scope.closeModal = function (id) {
        //    console.log(id);
        //    $("#" + id).modal("hide");
        //};
    };

    userController.$inject = ["$scope", "$stateParams", "userService", "appInfo"];
    angular.module("app")
        .controller("userController", userController);
}());
(function () {
    var app = angular.module("app");

    app.directive('userLogin', function (userService) {
        return {
            templateUrl: "/app/modules/user/userLogin.html",
            controllerAs: "vm",
            controller: function (ctUser, $state, logger) {
                var vm = this;

                vm.userProfile = ctUser.profile(); // Also retrieve profile from userService
                vm.auth = ctUser.auth();

                vm.goToProfile = function () {
                    $state.go("user.profile");
                }

                vm.login = function () {
                    //if ($location.host() !== 'byns.azurewebsites.net') {
                    //    ctUser.mockSignIn();
                    //    vm.userProfile = ctUser.profile();
                    //    logger.success("You logged in successfully!");
                    //    $state.reload();
                    //    return;
                    //}

                    ctUser.signIn()
                        .then(function (data) {
                            //success
                            vm.auth = data.auth;

                            //check db user
                            userService.me()
                                .then(function (data) {
                                    ctUser.setDbProfile(data);
                                    logger.success("You logged in successfully!");
                                }, function (error) {
                                    //error
                                    logger.error(error);
                                });

                            vm.userProfile = ctUser.profile();
                            $state.reload();
                        }, function (error) {
                            //error
                            logger.error(error);
                        });
                };

                vm.logout = function () {
                    ctUser.signOut();
                    $state.go('home');
                }
            }
        }
    });
}());
(function () {
    var userProfileController = function (ctUser, userService) {
        var vm = this;
        vm.profile = {};

        initialize();

        function initialize() {
            //var profile = ctUser.profile();
            userService.get()
                .then(function (data) {
                    vm.profile = data;
                }, null);
        };
    };

    userProfileController.$inject = ["ctUser", "userService"];
    angular.module("app")
        .controller("userProfileController", userProfileController);
}());
(function () {
    var userProfileEditController = function (ctUser, userService, logger, $state) {
        var vm = this;
        vm.user = {};

        initialize();

        function initialize() {
            var dbProfile = ctUser.dbProfile();
            if (dbProfile === null) {
                userService.get()
                    .then(function (data) {
                        ctUser.setDbProfile(data);
                        vm.user = data;
                    }, null);
            }
            else {
                vm.user = dbProfile;
            }
        };

        vm.update = function (user) {
            userService.update(user)
                .then(function () {
                    ctUser.setDbProfile(user);
                    logger.success("Your profile updated successfully!");
                    $state.go("user.profile");
                }, null);
        };
    };

    userProfileEditController.$inject = ["ctUser", "userService", "logger", "$state"];
    angular.module("app")
        .controller("userProfileEditController", userProfileEditController);
}());
(function () {

    var itemService = function (apiService) {

        var sortOrder = {};
        var items = [];
        var itemFilters = [];

        var getSortOrderOptions = function () {
            var data = [
                {
                    name: 'Most Recent',
                    value: '-datePosted'
                },
                {
                    name: 'Oldest',
                    value: 'datePosted'
                },
                {
                    name: 'Highest Starred',
                    value: '-starCount'
                },
                {
                    name: 'Lowest to Highest Price',
                    value: 'price'
                },
                {
                    name: 'Highest to Lowest Price',
                    value: '-price'
                }
            ];

            return data;
        };

        var getActiveSort = function () {

            if ($.isEmptyObject(sortOrder) === true) {
                sortOrder = getSortOrderOptions()[0];
            }

            return sortOrder;
        };

        var getItemCount = function () {
            return items.length;
        };

        var getFilters = function () {
            return itemFilters;
        };

        var setActiveSort = function (sort) {

            sortOrder = sort;
        };

        var removeArrayObjectByAttr = function (arr, attr, value) {
            var i = arr.length;
            while (i--) {
                if (arr[i]
                    && arr[i].hasOwnProperty(attr)
                    && (arguments.length > 2 && arr[i][attr] === value)) {

                    arr.splice(i, 1);

                }
            }
            return arr;
        }

        var setCategoryFilter = function (value) {

            removeArrayObjectByAttr(itemFilters, "name", "Category");

            itemFilters.push({
                name: "Category",
                value: value
            });

        };

        var removeFilter = function (filter) {

            removeArrayObjectByAttr(itemFilters, "name", filter.name);

        };

        /* Active functions below
         * 
         */

        // Items specific

        var getCurrencies = function () {
            return [{
                id: "Php",
                name: "Php"
            }, {
                id: "$",
                name: "$"
            }];
        }

        var getItem = function (id) {
            return apiService.item.query({
                $filter: "Id eq " + id
            }).$promise;
        };

        var getItems = function () {
            return apiService.item.query().$promise;
        };

        var save = function (item) {
            return apiService.item.save(item).$promise;
        }

        var update = function (id, item) {
            return apiService.item.update({ id: id }, item).$promise;
        }

        var deleteItem = function (item) {
            return apiService.item.delete({ id: item.id }).$promise;
        }

        var setItems = function (data) {
            items = angular.copy(data);
        }

        var search = function (pageNumber, pageSize, searchText) {
            return apiService.item.search({
                pageNumber: pageNumber,
                pageSize: pageSize,
                searchText: searchText
            }).$promise;
        }

        // User specific
        var getUserItems = function (userId) {
            return apiService.item.query({
                $filter: "UserId eq " + userId
            }).$promise;
        };

        return {
            getSortOrderOptions: getSortOrderOptions,
            getActiveSort: getActiveSort,
            getItemCount: getItemCount,
            getFilters: getFilters,
            setActiveSort: setActiveSort,
            setCategoryFilter: setCategoryFilter,
            removeFilter: removeFilter,

            getCurrencies: getCurrencies,
            getItem: getItem,
            getItems: getItems,
            save: save,
            update: update,
            _delete: deleteItem,
            setItems: setItems,
            search: search,

            getUserItems: getUserItems
        };
    };

    var module = angular.module("app");

    itemService.$inject = ["apiService"];
    module.factory("itemService", itemService);

}());
(function () {

    var itemController = function (appInfo) {
        $("title").text("Items" + appInfo.APP_NAME);
    };

    itemController.$inject = ["appInfo"];
    angular.module("app")
            .controller("itemController", itemController);

}());
(function () {

    var app = angular.module("app");

    app.directive("itemSortBox", function () {
        return {
            scope: {
                items: "=",
                itemCount: "="
            },
            templateUrl: "/app/modules/item/itemSortBox.html",
            controller: function ($scope, itemService) {
                $scope.sortOptions = itemService.getSortOrderOptions();
                $scope.currentSortOrder = itemService.getActiveSort();

                $scope.applySort = function (sortOrder) {
                    if ($scope.currentSortOrder.name !== sortOrder.name) {
                        console.log("Applying sort order by: " + sortOrder.name);
                        $scope.currentSortOrder = sortOrder;

                        itemService.getItems(sortOrder)
                            .then(function (data) {
                                $scope.items = data;
                                $scope.itemCount = itemService.getItemCount();
                                itemService.setActiveSort(sortOrder);
                            }, null);
                    }
                };
            }
        };
    });

}());
(function () {

    var app = angular.module("app");

    var listController = function (itemService, userService, itemModalService, dialogService, logger) {
        var vm = this;
        vm.filterText = "";
        vm.isLoadingComplete = false;

        vm.recordCount = 0;
        vm.pageNumber = 1;
        vm.pageSize = 5; 

        vm.getList = function () {
            logger.debug.info(vm.filterText);
            itemService.search(vm.pageNumber, vm.pageSize, vm.filterText)
                .then(function (data) {
                    vm.recordCount = data.recordCount;
                    vm.pageNumber = data.pageNumber;
                    vm.pageSize = data.pageSize;
                    vm.items = angular.copy(data.data);
                    vm.isLoadingComplete = true;
                }, null);
        };

        vm.getList();

        vm.refresh = function (item) {
            var index = vm.items.indexOf(item);
            if (index > -1) {
                vm.items.splice(index, 1);
            }
        };

        vm.add = function () {
            itemModalService.add()
                .then(function () {
                    //save callback
                    vm.getList();
                }, function () {
                    //cancel callback
                });
        };

        vm.edit = function (item) {

            var itemCopy = angular.copy(item);

            itemModalService.edit(itemCopy)
                .then(function () {
                    //save callback
                    item.image = itemCopy.image;
                    item.name = itemCopy.name;
                    item.description = itemCopy.description;
                    item.price = itemCopy.price;
                    item.currency = itemCopy.currency;
                    item.category = itemCopy.category;
                    item.condition = itemCopy.condition;
                    item.remarks = itemCopy.remarks;
                    logger.success("Update successful.");
                }, function () {
                    //cancel callback
                });
        };

        vm.delete = function (item) {
            dialogService.confirm("Delete", "Are you sure you want to delete this item?", item.name, ["Yes", "Cancel"], "delete")
                .then(function () {
                    //yes
                    itemService._delete(item)
                        .then(function () {
                            //success
                            vm.refresh(item);
                            logger.success("Delete successful.");
                        }, function (error) {
                            //error
                            logger.error(error.data.message);
                        });
                },
                function () {
                    //cancel
                });
        };

        vm.search = function () {
            vm.getList();
        };

        vm.pageChanged = function () {
            vm.getList();
        };

    };

    listController.$inject = ["itemService", "userService", "itemModalService", "dialogService", "logger"];
    app.controller("listController", listController);

}());
(function () {

    var itemModalService = function ($uibModal) {

        var add = function () {
            var modalInstance = $uibModal.open({
                templateUrl: "app/modules/item/postItem.html",
                controller: "postItemController",
                controllerAs: "vm",
                backdrop: "static",
                size: "md"
            });

            return modalInstance.result;
        }

        var edit = function (item) {
            var modalInstance = $uibModal.open({
                templateUrl: "app/modules/item/editItem.html",
                controller: "editItemController",
                controllerAs: "vm",
                backdrop: "static",
                size: "md",
                resolve: {
                    item: function () {
                        return item;
                    }

                }
            });

            return modalInstance.result;
        }

        return {
            add: add,
            edit: edit
        };
    };

    var module = angular.module("app");

    itemModalService.$inject = ['$uibModal'];
    module.factory("itemModalService", itemModalService);

}());
(function () {

    var app = angular.module("app");

    var postItemController = function ($scope, $uibModalInstance, logger, itemService) {

        var vm = this;

        vm.currencies = [];

        var initizialize = function () {
            vm.currencies = itemService.getCurrencies();
        };

        initizialize();
        vm.item = { currency: vm.currencies[0].name };

        vm.save = function (item) {

            item.userId = 1;

            itemService.save(item).then(function () {
                //success
                logger.success("Record successfully saved!");
                $uibModalInstance.close();
            }, function (error) {
                //error
                logger.error(error.data.message);
            });
        };

        vm.cancel = function () {
            $uibModalInstance.dismiss();
        }

        vm.ok = function () {
            $uibModalInstance.close();
        }

    };

    postItemController.$inject = ["$scope", "$uibModalInstance", "logger", "itemService"];
    app.controller("postItemController", postItemController);

}());
(function () {

    var app = angular.module("app");

    var editItemController = function (logger, itemService, item, $uibModalInstance) {

        var vm = this;
        vm.currencies = [];
        vm.item = item;

        initialize();

        function initialize() {
            vm.currencies = itemService.getCurrencies();
        };

        vm.update = function () {
            itemService.update(vm.item.id, vm.item)
                        .then(function () {
                            //success 
                            $uibModalInstance.close();
                        }, function (error) {
                            //error
                            logger.error(error.data.message);
                        });
        };

        vm.cancel = function () {
            $uibModalInstance.dismiss();
        }

    };

    editItemController.$inject = ["logger", "itemService", "item", "$uibModalInstance"];
    app.controller("editItemController", editItemController);

}());
(function () {
    var app = angular.module("app");

    app.directive("listCard", function () {
        return {
            transclude: true,
            scope: {
                item: "="
            },
            templateUrl: "/app/modules/item/listCard/listCard.html",
            controllerAs: "vm",
        }
    });
}());
(function () {

    var app = angular.module("app");

    app.directive("itemSearchFilters", function () {
        return {
            scope: {
                items: "=",
                itemCount: "="
            },
            templateUrl: "/app/modules/itemSearch/itemSearchFilters.html",
            controller: function ($scope, itemService, logger) {

                $scope.filterList = itemService.getFilters();
                logger.debug.info("Loaded filter - " + $scope.filterList.length);

                $scope.removeFilter = function (filter) {
                    logger.debug.info("Filter removed - " + filter.value);
                                        
                    itemService.removeFilter(filter);
                    $scope.filterList = itemService.getFilters();

                    var activeSort = itemService.getActiveSort();

                    itemService.getItems(activeSort)
                        .then(function (data) {
                            $scope.items = data;
                            $scope.itemCount = itemService.getItemCount();
                        }, null);
                };

            }
        };
    });

}());
(function () {

    var app = angular.module("app");

    app.directive("itemSearchBox", function () {
        return {
            scope: {
                items: "=",
                itemCount: "="
            },
            templateUrl: "/app/modules/itemSearch/itemSearchBox.html",
            controller: function ($scope, itemService) {

                $scope.searchItems = function (filter) {
                    itemService.search(1, 50, filter)
                            .then(function (data) {
                                $scope.items = data.data;
                                itemService.setItems($scope.items);
                                $scope.itemCount = itemService.getItemCount();
                            }, null);
                };
            }
        };
    });

}());
(function () {

    var categoryService = function () {
        var initialSortOrder = "name";

        var getCategories = function () {
            var data = [];

            data[0] = {
                name: "Book",
                itemCount: 544
            }
            data[1] = {
                name: "Bag",
                itemCount: 33
            }
            data[2] = {
                name: "Shoes",
                itemCount: 9987
            }
            data[3] = {
                name: "Shirt",
                itemCount: 5
            }
            data[4] = {
                name: "Car",
                itemCount: 23
            }
            data[5] = {
                name: "Bike",
                itemCount: 9
            }
            data[6] = {
                name: "Appliances",
                itemCount: 489571
            }

            // Replace above with $http.get call from API
            return data;
        };

        var getSortOrder = function () {
            return initialSortOrder;
        }

        return {
            getCategories: getCategories,
            getSortOrder: getSortOrder
        };
    };

    var module = angular.module("app");
    module.factory("categoryService", categoryService);

}());
(function () {


    var categoryController = function ($scope, $stateParams, itemService, appInfo) {
        $("title").text($stateParams.name + appInfo.APP_NAME);

        $scope.items = [];
        $scope.itemCount = 0;

        var getItemsByCategory = function () {
            $scope.categoryName = $stateParams.name;
            itemService.getItemsByCategory($scope.categoryName)
                .then(function (data) {
                    $scope.items = data;
                    $scope.itemCount = itemService.getItemCount();
                }, null);
        };

        getItemsByCategory();
    };

    var app = angular.module("app");
    app.controller("categoryController", ["$scope", "$stateParams", "itemService", "appInfo", categoryController]);

}());
(function () {

    var app = angular.module("app");

    app.directive("categoryListPanel", function () {

        return {
            scope: {
                items: "=",
                itemCount: "="
            },
            templateUrl: "/app/modules/category/categoryListPanel.html",
            controller: function ($scope, categoryService, itemService) {

                //Category panel
                $scope.categorySortOrder = categoryService.getSortOrder();

                //Populating category list
                $scope.getCategories = function () {
                    $scope.categories = categoryService.getCategories();
                };

                $scope.getCategories();

                $scope.addCatFilter = function (value) {
                    itemService.setCategoryFilter(value);
                    var activeSort = itemService.getActiveSort();

                    itemService.getItems(activeSort)
                        .then(function (data) {
                            $scope.items = data;
                            $scope.itemCount = itemService.getItemCount();
                        }, null);

                };
            }
        }

    });

}());