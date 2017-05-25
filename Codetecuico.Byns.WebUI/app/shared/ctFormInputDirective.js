(function () {

    var ctFormInput = function () {
        return {
            restrict: "A",
            link: function (scope, element) {
                var div = element[0];
                div.classList.add("form-group");

                var input = div.querySelector("input");
                if (input !== null)
                    input.classList.add("form-control");

                var label = div.querySelector("label");
                if (label !== null)
                    label.classList.add("control-label");
            }
        }
    }

    angular.module("app")
            .directive("ctFormInput", ctFormInput);

}());
