(function () {

    var app = angular.module("app");

    var itemCardDetailsController = function ($scope, $stateParams, itemService) {
        //$scope.item = {};

        itemService.getItem($stateParams.id)
            .then(function (data) {
                $scope.item = data[0];
                console.log($scope.item);
            }, null);
    };

    app.controller("itemCardDetailsController", ["$scope", "$stateParams", "itemService", itemCardDetailsController]);

}());