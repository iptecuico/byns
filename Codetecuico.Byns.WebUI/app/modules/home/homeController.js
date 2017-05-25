(function () {

    var homeController = function ($scope, itemService, appInfo) {
        $("title").text("All items" + appInfo.APP_NAME);

        $scope.items = [];
        $scope.itemCount = 0;
        $scope.isLoadingComplete = false;

        var currentSortOrder = itemService.getActiveSort();

        //Populating item list
        $scope.getItems = function () {
            itemService.search(1,50,"")
                .then(function (data) {
                    $scope.items = angular.copy(data.data);
                    itemService.setItems($scope.items);
                    $scope.itemCount = itemService.getItemCount();
                    $scope.isLoadingComplete = true;
                }, null);
        };

        $scope.getItems(currentSortOrder);

    };

    homeController.$inject = ["$scope", "itemService", "appInfo"];
    angular.module("app")
            .controller("homeController", homeController);

}());