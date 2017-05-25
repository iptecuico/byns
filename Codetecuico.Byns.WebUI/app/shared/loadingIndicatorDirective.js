(function () {
    var app = angular.module("app");

    app.directive("loadingIndicator", function () {
        return {
            scope: {},
            templateUrl: "/app/shared/loadingIndicator.html"
        }
    });
}());