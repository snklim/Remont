(function() {
    angular.module('remontApp')
        .constant('pages', window.pages).config(function ($routeProvider, pages) {

        $.each(pages, function(i, page) {

            var name = page.name;
            var ctrlName = page.ctrlName;
            var viewName = page.viewName;
            var serviceUrl = page.serviceUrl;

            var resolveList = {
                response: function(dataFeeder) {
                    return dataFeeder.create(serviceUrl).get();
                }
            };

            if (page.resolveList != null) {
                for (var r in page.resolveList) {
                    resolveList[r] = page.resolveList[r];
                }
            }

            $routeProvider.when('/' + name + '/list',
            {
                templateUrl: 'pages/' + viewName + '_list.html',
                controller: '' + ctrlName + 'ListCtrl',
                resolve: resolveList
            });

            var resolveEdit = {
                item: function ($route, dataFeeder) {
                    return dataFeeder
                        .create('/api/' + viewName + '/')
                        .get($route.current.params, function (data) {
                            return data.Item;
                        });
                }
            };

            var resolveCreate = {
                item: function() {
                    return {};
                }
            };

            if (page.resolveEdit != null) {
                for (var r in page.resolveEdit) {
                    resolveEdit[r] = page.resolveEdit[r];
                }
            }

            if (page.resolveCreate != null) {
                for (var r in page.resolveCreate) {
                    resolveCreate[r] = page.resolveCreate[r];
                }
            }

            $routeProvider.when('/' + name + '/edit/:id', {
                templateUrl: 'pages/' + viewName + '_edit.html',
                controller: '' + ctrlName + 'EditCtrl',
                resolve: resolveEdit
            });

            $routeProvider.when('/' + name + '/create', {
                templateUrl: 'pages/' + viewName + '_edit.html',
                controller: '' + ctrlName + 'EditCtrl',
                resolve: resolveCreate
            });

        });

    });
})();