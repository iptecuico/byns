(function () {

    var app = angular.module("app");

    var editItemController = function (logger, itemService, item, $uibModalInstance) {

        var vm = this;
        vm.currencies = [];
        vm.item = item;

        initialize();

        function initialize() {
            vm.currencies = itemService.getCurrencies();
        };

        vm.update = function () {
            itemService.update(vm.item.id, vm.item)
                        .then(function () {
                            //success 
                            $uibModalInstance.close();
                        }, function (error) {
                            //error
                            logger.error(error.data.message);
                        });
        };

        vm.cancel = function () {
            $uibModalInstance.dismiss();
        }

    };

    editItemController.$inject = ["logger", "itemService", "item", "$uibModalInstance"];
    app.controller("editItemController", editItemController);

}());