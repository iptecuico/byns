(function () {

    var app = angular.module("app");

    app.directive("listCard", function () {
        return {
            transclude: true,
            scope: {
                item: "="
            },
            templateUrl: "/app/modules/item/listCard/listCard.html",
            controllerAs: "vm",
            controller: function () {

            }
        }

    });

}());
