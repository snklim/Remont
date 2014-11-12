(function() {
    angular.module('remontApp').config(function ($httpProvider) {

        $httpProvider.interceptors.push(function ($q, $timeout) {
            return {
                'request': function (config) {

                    $('#ajaxIndicator').show();

                    return config;
                },

                'response': function (response) {

                    $timeout(function () {
                        $('#ajaxIndicator').fadeOut(100);
                    }, 100);

                    return response;
                },

                'responseError': function (rejection) {
                    console.log(rejection);

                    return $q.reject(rejection);
                }
            };
        });

    });
})();