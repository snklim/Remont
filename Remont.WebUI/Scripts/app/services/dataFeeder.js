(function() {
    angular.module('remontApp').factory('dataFeeder', function($http, $q) {

        function feeder(serviceUrl) {
            this.serviceUrl = serviceUrl;
        };

        feeder.prototype.get = function(params, selector) {
            var deferrer = $q.defer();

            $http.get(this.serviceUrl, { params: params }).success(function(data) {
                if (selector)
                    deferrer.resolve(selector(data));
                else
                    deferrer.resolve(data);
            });

            return deferrer.promise;
        };

        return {
            create: function(serviceUrl) {
                return new feeder(serviceUrl);
            }
        };

    });
})();