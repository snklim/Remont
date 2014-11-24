(function() {
    angular.module('remontApp').directive('controlType', function ($templateCache, $compile) {
        return {
            link: function (scope, element, attributes) {

                var controlId = 'control_' + (attributes.type).toLowerCase() + '.html';

                $templateCache.get(controlId).then(function(html) {
                    element.html(html);
                    $compile(element.contents())(scope);
                });
            }
        };
    });
})();