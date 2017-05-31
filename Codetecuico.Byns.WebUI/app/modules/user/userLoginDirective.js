(function () {
    "use strict";

    var app = angular.module("app");

    app.directive("userLogin", function (userService) {
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
                    //if ($location.host() !== "byns.azurewebsites.net") {
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
                    $state.go("home");
                };
            }
        };
    });
}());