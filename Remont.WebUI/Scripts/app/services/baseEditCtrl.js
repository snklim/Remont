(function() {
    angular.module('remontApp').factory('baseEditCtrl', function ($http, $location) {

        function baseEditCtrl(scope, item, pageUrl, serviceUrl) {
            scope.item = item;

            scope.save = function () {

                $http.post('/' + serviceUrl + '', item).success(function (id) {
                    $location.path('' + pageUrl + '/edit/' + id);
                });

            };

            scope.cancel = function () {
                $location.path('' + pageUrl + '/list/');
            };
        };

        return {
            create: function (scope, item, pageUrl, serviceUrl) {
                return new baseEditCtrl(scope, item, pageUrl, serviceUrl);
            }
        };
    });
})();