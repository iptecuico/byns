(function () {
    "use strict";

    var app = angular.module("app");

    app.directive("itemSearchBox", function () {
        return {
            scope: {
                items: "=",
                itemCount: "="
            },
            templateUrl: "/app/modules/itemSearch/itemSearchBox.html",
            controller: function ($scope, itemService) {
                $scope.searchItems = function (filter) {
                    itemService.search(1, 50, filter)
                            .then(function (data) {
                                $scope.items = data.data;
                                itemService.setItems($scope.items);
                                $scope.itemCount = itemService.getItemCount();
                            }, null);
                };
            }
        };
    });
}());