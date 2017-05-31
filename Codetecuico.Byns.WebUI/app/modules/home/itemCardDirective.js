(function () {
    "use strict";

    var app = angular.module("app");

    app.directive("itemCard", function () {
        return {
            templateUrl: "/app/modules/home/itemCard.html",
            scope: {
                item: "=",
                showUserInfo: "="
            }
        };
    });
}());