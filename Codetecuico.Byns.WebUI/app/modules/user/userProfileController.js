(function () {
    "use strict";

    var userProfileController = function (userService) {
        var vm = this;
        vm.profile = {};

        function initialize() {
            //var profile = ctUser.profile();
            userService.get()
                .then(function (data) {
                    vm.profile = data;
                }, null);
        }

        initialize();
    };

    userProfileController.$inject = ["userService"];
    angular.module("app")
        .controller("userProfileController", userProfileController);
}());