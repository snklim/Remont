
var remontApp = angular.module('remontApp', ['ngRoute']).config(function ($routeProvider, $httpProvider) {

    $routeProvider.when('/customer/list', {
        templateUrl: 'pages/customer_list.html',
        controller: 'CustomerListCtrl',
        resolve: {
            response: function ($route, $http, $q) {
                var deferred = $q.defer();

                $http.get('/api/customer/').success(function (data) {
                    deferred.resolve(data);
                });

                return deferred.promise;
            }
        }
    }).when('/customer/edit/:id', {
        templateUrl: 'pages/customer_edit.html',
        controller: 'CustomerEditCtrl',
        resolve: {
            customer: function ($route, $http, $q) {
                var deferred = $q.defer();

                $http.get('/api/customer/',
                {
                    params: $route.current.params
                }).success(function(data) {
                    deferred.resolve(data.Item);
                });

                return deferred.promise;
            }
        }
    }).when('/customer/create', {
        templateUrl: 'pages/customer_edit.html',
            controller: 'CustomerEditCtrl',
        resolve: {
            customer: function() {
                return {

                };
            }
        }
    }).when('/orders', {
        templateUrl: 'pages/orders.html'
    }).otherwise({
        redirectTo: '/customer/list'
    });

    $httpProvider.interceptors.push(function ($q, $timeout) {
        return {
            'request': function (config) {

                $('#ajaxIndicator').show();

                return config;
            },

            'response': function (response) {

                $timeout(function() {
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