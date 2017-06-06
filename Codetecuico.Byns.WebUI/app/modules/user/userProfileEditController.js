(function () {
    "use strict";

    var userProfileEditController = function (ctUser, userService, logger, $state) {
        var vm = this;
        vm.user = {};

        function initialize() {
            var dbProfile = ctUser.dbProfile();
            if (dbProfile === null) {
                userService.get()
                    .then(function (data) {
                        ctUser.setDbProfile(data);
                        vm.user = data;
                    }, null);
            } else {
                vm.user = dbProfile;
            }
        }

        initialize();

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