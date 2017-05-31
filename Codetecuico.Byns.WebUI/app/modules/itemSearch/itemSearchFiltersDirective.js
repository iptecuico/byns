(function () {
    "use strict";

    var app = angular.module("app");

    app.directive("itemSearchFilters", function () {
        return {
            scope: {
                items: "=",
                itemCount: "="
            },
            templateUrl: "/app/modules/itemSearch/itemSearchFilters.html",
            controller: function ($scope, itemService, logger) {
                $scope.filterList = itemService.getFilters();
                logger.debug.info("Loaded filter - " + $scope.filterList.length);

                $scope.removeFilter = function (filter) {
                    logger.debug.info("Filter removed - " + filter.value);

                    itemService.removeFilter(filter);
                    $scope.filterList = itemService.getFilters();

                    var activeSort = itemService.getActiveSort();

                    itemService.getItems(activeSort)
                        .then(function (data) {
                            $scope.items = data;
                            $scope.itemCount = itemService.getItemCount();
                        }, null);
                };
            }
        };
    });
}());