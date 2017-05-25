(function () {

    var app = angular.module("app");

    app.directive("categoryListPanel", function () {

        return {
            scope: {
                items: "=",
                itemCount: "="
            },
            templateUrl: "/app/modules/category/categoryListPanel.html",
            controller: function ($scope, categoryService, itemService) {

                //Category panel
                $scope.categorySortOrder = categoryService.getSortOrder();

                //Populating category list
                $scope.getCategories = function () {
                    $scope.categories = categoryService.getCategories();
                };

                $scope.getCategories();

                $scope.addCatFilter = function (value) {
                    itemService.setCategoryFilter(value);
                    var activeSort = itemService.getActiveSort();

                    itemService.getItems(activeSort)
                        .then(function (data) {
                            $scope.items = data;
                            $scope.itemCount = itemService.getItemCount();
                        }, null);

                };
            }
        }

    });

}());