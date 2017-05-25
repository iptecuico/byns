(function () {

    var mainController = function (logger) {
        logger.debug.success('mainController');
    };

    mainController.$inject = ['logger'];

    angular.module('app')
        .controller('mainController', mainController);

}());