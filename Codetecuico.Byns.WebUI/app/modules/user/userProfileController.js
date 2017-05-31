(function () {
    "use strict";

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
        }
    };

    userProfileController.$inject = ["ctUser", "userService"];
    angular.module("app")
        .controller("userProfileController", userProfileController);
}());