(function () {


    var categoryController = function ($scope, $stateParams, itemService, appInfo) {
        $("title").text($stateParams.name + appInfo.APP_NAME);

        $scope.items = [];
        $scope.itemCount = 0;

        var getItemsByCategory = function () {
            $scope.categoryName = $stateParams.name;
            itemService.getItemsByCategory($scope.categoryName)
                .then(function (data) {
                    $scope.items = data;
                    $scope.itemCount = itemService.getItemCount();
                }, null);
        };

        getItemsByCategory();
    };

    var app = angular.module("app");
    app.controller("categoryController", ["$scope", "$stateParams", "itemService", "appInfo", categoryController]);

}());