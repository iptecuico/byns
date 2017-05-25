(function () {
    var app = angular.module("app");

    app.directive("ctHeader", function () {
        return {
            restrict: "A",
            scope: {},
            transclude: true,
            templateUrl: "/app/shared/header.html",
        }
    });
}());