(function () {
    var app = angular.module("app");

    app.directive("ctMenuItem", function () {
        return {
            restrict: "A",
            scope: {
                id: "@",
                description: "@",
                route: "@"
            },
            templateUrl: "/app/shared/menu/menuItem.html",
            link: function (scope, element) {
                // exclude the directive"s own element
                element.replaceWith(element.contents());
            }
        }
    });
}());