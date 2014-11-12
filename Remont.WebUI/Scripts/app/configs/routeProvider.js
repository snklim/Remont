(function() {
    angular.module('remontApp').config(function($routeProvider) {
        $routeProvider.when('/order/list', {
            templateUrl: 'pages/order_list.html',
            controller: 'OrderListCtrl',
            resolve: {
                response: function(dataFeeder) {
                    return dataFeeder.create('/api/order/').get();
                }
            }
        }).when('/order/edit/:id', {
            templateUrl: 'pages/order_edit.html',
            controller: 'OrderEditCtrl',
            resolve: {
                orderStatuses: function(dataFeeder) {
                    return dataFeeder
                        .create('/api/orderStatus/')
                        .get(null, function(data) {
                            return data.Items;
                        });
                },
                order: function($route, dataFeeder) {
                    return dataFeeder
                        .create('/api/order/')
                        .get($route.current.params, function(data) {
                            return data.Item;
                        });
                }
            }
        }).when('/order/create', {
            templateUrl: 'pages/order_edit.html',
            controller: 'OrderEditCtrl',
            resolve: {
                orderStatuses: function(dataFeeder) {
                    return dataFeeder
                        .create('/api/orderStatus/')
                        .get(null, function(data) {
                            return data.Items;
                        });
                },
                order: function() {
                    return {
                        OrderDate: new Date()
                    };
                }
            }
        }).when('/customer/list', {
            templateUrl: 'pages/customer_list.html',
            controller: 'CustomerListCtrl',
            resolve: {
                response: function(dataFeeder) {
                    return dataFeeder.create('/api/customer/').get();
                }
            }
        }).when('/customer/edit/:id', {
            templateUrl: 'pages/customer_edit.html',
            controller: 'CustomerEditCtrl',
            resolve: {
                customer: function($route, dataFeeder) {
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
    });
})();