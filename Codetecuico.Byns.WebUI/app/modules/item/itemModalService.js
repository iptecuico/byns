(function () {

    var itemModalService = function ($uibModal) {

        var add = function () {
            var modalInstance = $uibModal.open({
                templateUrl: "app/modules/item/postItem.html",
                controller: "postItemController",
                controllerAs: "vm",
                backdrop: "static",
                size: "md"
            });

            return modalInstance.result;
        }

        var edit = function (item) {
            var modalInstance = $uibModal.open({
                templateUrl: "app/modules/item/editItem.html",
                controller: "editItemController",
                controllerAs: "vm",
                backdrop: "static",
                size: "md",
                resolve: {
                    item: function () {
                        return item;
                    }

                }
            });

            return modalInstance.result;
        }

        return {
            add: add,
            edit: edit
        };
    };

    var module = angular.module("app");

    itemModalService.$inject = ['$uibModal'];
    module.factory("itemModalService", itemModalService);

}());