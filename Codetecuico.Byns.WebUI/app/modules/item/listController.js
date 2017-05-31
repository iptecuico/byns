(function () {
    "use strict";

    var app = angular.module("app");

    var listController = function (itemService, userService, itemModalService, dialogService, logger) {
        var vm = this;
        vm.filterText = "";
        vm.isLoadingComplete = false;

        vm.recordCount = 0;
        vm.pageNumber = 1;
        vm.pageSize = 5;

        vm.getList = function () {
            logger.debug.info(vm.filterText);
            itemService.search(vm.pageNumber, vm.pageSize, vm.filterText)
                .then(function (data) {
                    vm.recordCount = data.recordCount;
                    vm.pageNumber = data.pageNumber;
                    vm.pageSize = data.pageSize;
                    vm.items = angular.copy(data.data);
                    vm.isLoadingComplete = true;
                }, null);
        };

        vm.getList();

        vm.refresh = function (item) {
            var index = vm.items.indexOf(item);
            if (index > -1) {
                vm.items.splice(index, 1);
            }
        };

        vm.add = function () {
            itemModalService.add()
                .then(function () {
                    //save callback
                    vm.getList();
                }, function () {
                    //cancel callback
                });
        };

        vm.edit = function (item) {
            var itemCopy = angular.copy(item);

            itemModalService.edit(itemCopy)
                .then(function () {
                    //save callback
                    item.image = itemCopy.image;
                    item.name = itemCopy.name;
                    item.description = itemCopy.description;
                    item.price = itemCopy.price;
                    item.currency = itemCopy.currency;
                    item.category = itemCopy.category;
                    item.condition = itemCopy.condition;
                    item.remarks = itemCopy.remarks;
                    logger.success("Update successful.");
                }, function () {
                    //cancel callback
                });
        };

        vm.delete = function (item) {
            dialogService.confirm("Delete", "Are you sure you want to delete this item?", item.name, ["Yes", "Cancel"], "delete")
                .then(function () {
                    //yes
                    itemService._delete(item)
                        .then(function () {
                            //success
                            vm.refresh(item);
                            logger.success("Delete successful.");
                        }, function (error) {
                            //error
                            logger.error(error.data.message);
                        });
                },
                function () {
                    //cancel
                });
        };

        vm.search = function () {
            vm.getList();
        };

        vm.pageChanged = function () {
            vm.getList();
        };
    };

    listController.$inject = ["itemService", "userService", "itemModalService", "dialogService", "logger"];
    app.controller("listController", listController);
}());