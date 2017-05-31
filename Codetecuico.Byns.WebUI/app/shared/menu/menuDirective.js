(function () {
    var app = angular.module("app");

    app.directive("ctMenu", function () {
        return {
            restrict: "A",
            transclude: true,
            scope: {},
            templateUrl: "/app/shared/menu/menu.html",
        };
    });
}());