(function () {
    "use strict";

    var dialogController = function ($uibModalInstance, data) {
        var vm = this;

        vm.prop = data;

        vm.cancel = function () {
            $uibModalInstance.dismiss();
        }

        vm.ok = function () {
            $uibModalInstance.close();
        }
    };

    dialogController.$inject = ["$uibModalInstance", "data"];

    angular.module("app")
        .controller("dialogController", dialogController);
}());