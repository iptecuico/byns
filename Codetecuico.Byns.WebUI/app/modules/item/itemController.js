(function () {
    "use strict";

    var itemController = function (appInfo) {
        $("title").text("Items" + appInfo.APP_NAME);
    };

    itemController.$inject = ["appInfo"];
    angular.module("app")
            .controller("itemController", itemController);
}());