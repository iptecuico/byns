(function () {
    "use strict";

    var app = angular.module("app");

    var appService = function ($q, store, apiService) {

        var version = "";

        var getVersion = function () {
            if (version === "") {
                version = apiService.app.getVersion().$promise;
            }
            return version;
        };

        return {
            getVersion: getVersion
        };
    };

    app.factory("appService", ["$q", "store", "apiService", appService]);

}());