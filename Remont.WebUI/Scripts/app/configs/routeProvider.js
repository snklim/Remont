(function() {
    angular.module('remontApp').config(function ($routeProvider) {
        var pages = [
            {
                name: 'table'
            },
            {
                name: 'customer'
            },
            {
                name: 'order',
                resolveEdit: {
                    orderStatuses: function (dataFeeder) {
                        return dataFeeder
                            .create('/api/orderStatus/')
                            .get(null, function (data) {
                                return data.Items;
                            });
                    }
                }
            }
        ];

        $.each(pages, function(i, page) {

            var name = page.name;
            var ctrlName = name.substring(0, 1).toUpperCase() + name.substring(1);

            $routeProvider.when('/' + name + '/list', {
                templateUrl: 'pages/' + name + '_list.html',
                controller: '' + ctrlName + 'ListCtrl',
                resolve: {
                    response: function (dataFeeder) {
                        return dataFeeder.create('/api/' + name + '/').get();
                    }
                }
            });

            var resolveEdit = {
                item: function ($route, dataFeeder) {
                    return dataFeeder
                        .create('/api/' + name + '/')
                        .get($route.current.params, function (data) {
                            return data.Item;
                        });
                }
            };

            if (page.resolveEdit != null) {
                for (var r in page.resolveEdit) {
                    resolveEdit[r] = page.resolveEdit[r];
                }
            }

            $routeProvider.when('/' + name + '/edit/:id', {
                templateUrl: 'pages/' + name + '_edit.html',
                controller: '' + ctrlName + 'EditCtrl',
                resolve: resolveEdit
            });

            $routeProvider.when('/' + name + '/create', {
                templateUrl: 'pages/' + name + '_edit.html',
                controller: '' + ctrlName + 'EditCtrl',
                resolve: {
                    item: function () {
                        return {};
                    }
                }
            });

        });

        $routeProvider.otherwise({
            redirectTo: '/' + pages[0].name + '/list'
        });

    });
})();