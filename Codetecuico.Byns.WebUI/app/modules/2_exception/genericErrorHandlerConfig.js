(function () {

    var appErrorHandler = function ($provide) {

        $provide.decorator('$exceptionHandler', ['$delegate', 'logger', function ($delegate, logger) {
            return function (exception, cause) {
                $delegate(exception, cause);
                logger.debug.error(exception.message);
            };
        }]);

    };

    appErrorHandler.$inject = ['$provide'];

    angular.module('ctException')
            .config(appErrorHandler);

}());