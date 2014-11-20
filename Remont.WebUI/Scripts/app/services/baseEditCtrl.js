(function() {
    angular.module('remontApp').factory('baseEditCtrl', function ($http, $location) {

        function baseEditCtrl(scope, item, pageUrl, serviceUrl) {
            scope.item = item;

            scope.save = function () {

                $http.post('' + serviceUrl + '', item).success(function (newItem) {
                    if (scope.item.Id == 0) {
                        $location.path('' + pageUrl + '/edit/' + newItem.Id);
                    } else if (scope.onSave) {
                        scope.onSave(newItem);
                    }
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