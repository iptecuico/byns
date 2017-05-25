(function () {

    var app = angular.module("app");

    app.directive("ctFooter", function () {
        return {
            restrict: "A",
            scope: {},
            templateUrl: "/app/shared/footer.html",
            controllerAs: "vm",
            controller: function (appService) {
                var vm = this;
                vm.appVersion = "0";
                appService.getVersion().then(function (data) {
                    vm.appVersion = data.appVersion;
                });
            }
        }

    });

}());
