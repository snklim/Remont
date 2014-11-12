
var remontApp = angular.module('remontApp', ['ngRoute']).factory('pageService', function() {
    return {
        getPages: function(pageIndex, totalPages) {
            var pages = [];

            pages.push({
                pageIndex: 'prev',
                pageText: '«'
            });

            var left = pageIndex - 2 - (pageIndex + 3 > totalPages ? pageIndex + 3 - totalPages : 0);
            var right = pageIndex + 3 + (left < 0 ? -1 * left : 0);

            left = Math.max(0, left);
            right = Math.min(totalPages, right);

            for (var i = left; i < right; i++) {
                pages.push({
                    pageIndex: i,
                    pageText: i + 1
                });
            }

            pages.push({
                pageIndex: 'next',
                pageText: '»'
            });

            return pages;
        }
    };
}).factory('dataFeeder', function ($http, $q) {

    function feeder(serviceUrl) {
        this.serviceUrl = serviceUrl;
    };

    feeder.prototype.get = function (params, selector) {
        var deferrer = $q.defer();

        $http.get(this.serviceUrl, { params: params }).success(function (data) {
            if (selector)
                deferrer.resolve(selector(data));
            else
                deferrer.resolve(data);
        });

        return deferrer.promise;
    };

    return {
        create: function (serviceUrl) {
            return new feeder(serviceUrl);
        }
    };
}).config(function ($routeProvider, $httpProvider) {

    $routeProvider.when('/order/list', {
        templateUrl: 'pages/order_list.html',
        controller: 'OrderListCtrl',
        resolve: {
            feeder: function (dataFeeder) {
                return dataFeeder.create('/api/order/');
            },
            response: function (dataFeeder) {
                return dataFeeder.create('/api/order/').get();
            }
        }
    }).when('/order/edit/:id', {
        templateUrl: 'pages/order_edit.html',
        controller: 'OrderEditCtrl',
        resolve: {
            orderStatuses: function (dataFeeder) {
                return dataFeeder
                    .create('/api/orderStatus/')
                    .get(null, function (data) {
                        return data.Items;
                    });
            },
            order: function ($route, dataFeeder) {
                return dataFeeder
                    .create('/api/order/')
                    .get($route.current.params, function (data) {
                        return data.Item;
                    });
            }
        }
    }).when('/order/create', {
        templateUrl: 'pages/order_edit.html',
        controller: 'OrderEditCtrl',
        resolve: {
            orderStatuses: function (dataFeeder) {
                return dataFeeder
                    .create('/api/orderStatus/')
                    .get(null, function (data) {
                        return data.Items;
                    });
            },
            order: function () {
                return {
                    OrderDate: new Date()
                };
            }
        }
    }).when('/customer/list', {
        templateUrl: 'pages/customer_list.html',
        controller: 'CustomerListCtrl',
        resolve: {
            feeder: function (dataFeeder) {
                return dataFeeder.create('/api/customer/');
            },
            response: function (dataFeeder) {
                return dataFeeder.create('/api/customer/').get();
            }
        }
    }).when('/customer/edit/:id', {
        templateUrl: 'pages/customer_edit.html',
        controller: 'CustomerEditCtrl',
        resolve: {
            customer: function ($route, dataFeeder) {
                return dataFeeder
                    .create('/api/customer/')
                    .get($route.current.params, function(data) {
                        return data.Item;
                    });
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