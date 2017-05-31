(function () {
    "use strict";

    var app = angular.module("app");

    app.directive("itemSortBox", function () {
        return {
            scope: {
                items: "=",
                itemCount: "="
            },
            templateUrl: "/app/modules/item/itemSortBox.html",
            controller: function ($scope, itemService) {
                $scope.sortOptions = itemService.getSortOrderOptions();
                $scope.currentSortOrder = itemService.getActiveSort();

                $scope.applySort = function (sortOrder) {
                    if ($scope.currentSortOrder.name !== sortOrder.name) {
                        console.log("Applying sort order by: " + sortOrder.name);
                        $scope.currentSortOrder = sortOrder;

                        itemService.getItems(sortOrder)
                            .then(function (data) {
                                $scope.items = data;
                                $scope.itemCount = itemService.getItemCount();
                                itemService.setActiveSort(sortOrder);
                            }, null);
                    }
                };
            }
        };
    });
}());