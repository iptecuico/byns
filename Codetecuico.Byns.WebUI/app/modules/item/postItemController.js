(function () {
    "use strict";

    var app = angular.module("app");

    var postItemController = function ($scope, $uibModalInstance, logger, itemService) {
        var vm = this;

        vm.currencies = [];

        var initizialize = function () {
            vm.currencies = itemService.getCurrencies();
        };

        initizialize();
        vm.item = { currency: vm.currencies[0].name };

        vm.save = function (item) {
            item.userId = 1;

            itemService.save(item).then(function () {
                //success
                logger.success("Record successfully saved!");
                $uibModalInstance.close();
            }, function (error) {
                //error
                logger.error(error.data.message);
            });
        };

        vm.cancel = function () {
            $uibModalInstance.dismiss();
        };

        vm.ok = function () {
            $uibModalInstance.close();
        };
    };

    postItemController.$inject = ["$scope", "$uibModalInstance", "logger", "itemService"];
    app.controller("postItemController", postItemController);
}());