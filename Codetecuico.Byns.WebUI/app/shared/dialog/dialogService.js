(function () {
    var dialogService = function ($uibModal) {
        var confirm = function (title, message, data, buttons, action) {
            var modalInstance = $uibModal.open({
                templateUrl: "app/shared/dialog/confirm.html",
                controller: "dialogController",
                controllerAs: "vm",
                backdrop: "static",
                resolve: {
                    data: function () {
                        return {
                            title: title,
                            message: message,
                            data: data,
                            buttons: buttons,
                            action: action
                        };
                    }
                },
                size: "sm"
            });

            return modalInstance.result;
        }

        return {
            confirm: confirm
        };
    };

    var module = angular.module("app");

    dialogService.$inject = ["$uibModal"];
    module.factory("dialogService", dialogService);
}());