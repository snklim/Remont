(function () {

    function Resolver(page) {

        var pageUrl = page.pageUrl,
            ctrlName = page.ctrlName,
            title = page.title,
            serviceUrl = page.serviceUrl,
            tableId = page.tableId;

        var resolverBase = {
            extData: function() {
                return {
                    pageUrl: pageUrl,
                    serviceUrl: serviceUrl,
                    tableId: tableId
                }
            },

            item: function($route, dataFeeder) {

                var id = $route && $route.current && $route.current.params && $route.current.params.id ?
                    $route.current.params.id : 0;

                if (tableId < 0 && id == 0) {
                    return {};
                }

                var params = {
                    tableId: tableId,
                    id: id
                };

                return dataFeeder
                    .create(serviceUrl)
                    .get(params, function(data) {
                        return data.Item;
                    });
            },

            response: function(dataFeeder) {
                return dataFeeder.create(serviceUrl).get({
                    tableId: tableId
                });
            }

        };

        var resolverEdit = page.resolverEdit || {};

        var self = {
            name: pageUrl,
            ctrlName: ctrlName,
            viewName: ctrlName,
            serviceUrl: serviceUrl,
            title: title,

            resolverBase: resolverBase,

            resolverEdit: resolverEdit
        };

        return self;
    };

    angular.module('remontApp').constant('pages', window.pages).config(function ($routeProvider, pages) {

        $.each(pages, function(i, page) {

            page = new Resolver(page);

            var name = page.name;
            var ctrlName = page.ctrlName;
            var viewName = page.viewName;

            $routeProvider.when('/' + name + '/list',
            {
                templateUrl: 'pages/' + viewName + '_list.html',
                controller: '' + ctrlName + 'ListCtrl',
                resolve: page.resolverBase
            });

            $routeProvider.when('/' + name + '/edit/:id', {
                templateUrl: 'pages/' + viewName + '_edit.html',
                controller: '' + ctrlName + 'EditCtrl',
                resolve: _.extend(page.resolverBase, page.resolverEdit)
            });

            $routeProvider.when('/' + name + '/create', {
                templateUrl: 'pages/' + viewName + '_edit.html',
                controller: '' + ctrlName + 'EditCtrl',
                resolve: _.extend(page.resolverBase, page.resolverEdit)
            });

        });

        if (pages && pages.length > 0) {
            $routeProvider.otherwise('/' + pages[0].pageUrl + '/list');
        }

    });
})();