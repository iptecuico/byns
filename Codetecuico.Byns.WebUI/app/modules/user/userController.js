(function () {
    "use strict";

    var userController = function (appInfo) {
        $("title").text("User" + appInfo.APP_NAME);

        //$scope.user = userService.getUserInfo($stateParams.userId);
        //$scope.closeModal = function (id) {
        //    console.log(id);
        //    $("#" + id).modal("hide");
        //};
    };

    userController.$inject = ["appInfo"];
    angular.module("app")
        .controller("userController", userController);
}());