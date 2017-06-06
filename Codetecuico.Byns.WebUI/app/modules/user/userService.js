(function () {
    "use strict";

    var app = angular.module("app");

    var userService = function (apiService) {
        /* Active functions below
         *
         */
        var me = function () {
            return apiService.user.me().$promise;
        };

        var get = function () {
            return apiService.user.get().$promise;
        };

        var create = function (user) {
            return apiService.user.save(user).$promise;
        };

        var update = function (user) {
            return apiService.user.update({ id: user.id }, user).$promise;
        };

        return {
            me: me,
            get: get,
            create: create,
            update: update
        };
    };

    app.factory("userService", ["apiService", userService]);
}());