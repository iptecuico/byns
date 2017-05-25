(function () {

    var logger = function () {

        toastr.options = {
            //"debug": false,
            "positionClass": "toast-bottom-right"
            //"onclick": null,
            //"fadeIn": 300,
            //"fadeOut": 1000,
            //"timeOut": 5000,
            //"extendedTimeOut": 1000
        }

        var success = function (msg) {
            toastr.success(msg);
        };

        var info = function (msg) {
            toastr.info(msg);
        };

        var warning = function (msg) {
            toastr.warning(msg);
        };

        var error = function (msg) {
            toastr.error(msg);
        };

        // Developer code
        var isDebugActive = true;
        var debugPrefix = 'DEBUG: ';
        var logToConsole = true;

        var setMessage = function (msg) {
            return debugPrefix + msg;
        }

        var isValidLog = function (msg) {
            return (isDebugActive && msg !== "" && msg !== null);
        }

        var logMessage = function (logType, msg) {

            if (!isValidLog(msg)) {
                return;
            }

            var message = setMessage(msg);

            if (logToConsole) {

                if (logType === "info")
                    console.info(message);

                else if (logType === "success")
                    console.info(message);

                else if (logType === "warning")
                    console.warn(message);

                else if (logType === "error")
                    console.error(message);

            } else {

                if (logType === "info")
                    toastr.info(message);

                else if (logType === "success")
                    toastr.success(message);

                else if (logType === "warning")
                    toastr.warning(message);

                else if (logType === "error")
                    toastr.error(message);
            }
        }

        var debug = {

            info: function (msg) {
                logMessage("info", msg);
            },
            success: function (msg) {
                logMessage("success", msg);
            },
            warning: function (msg) {
                logMessage("warning", msg);
            },
            error: function (msg) {
                logMessage("error", msg);
            }
        };

        return {
            debug: debug,
            success: success,
            info: info,
            warning: warning,
            error: error
        }

    }

    angular.module('ctLogger')
            .factory('logger', logger);

}());