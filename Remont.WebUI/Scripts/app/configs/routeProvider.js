(function() {
    angular.module('remontApp')
        .constant('pages', [
            {
                name: 'table',
                title: 'Table'
            }])
        .config(function ($routeProvider, pages) {

        $.each(pages, function(i, page) {

            var name = page.name;
            var ctrlName = name.substring(0, 1).toUpperCase() + name.substring(1);
            var params = (page.params ? page.params : '');

            var resolveList = {
                response: function(dataFeeder) {
                    return dataFeeder.create('/api/' + name + params).get();
                }
            };

            if (page.resolveList != null) {
                for (var r in page.resolveList) {
                    resolveList[r] = page.resolveList[r];
                }
            }

            $routeProvider.when('/' + name + '/list' + params,
                {
                templateUrl: 'pages/' + name + '_list.html',
                controller: '' + ctrlName + 'ListCtrl',
                resolve: resolveList
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

            $routeProvider.when('/' + name + '/edit' + params + '/:id', {
                templateUrl: 'pages/' + name + '_edit.html',
                controller: '' + ctrlName + 'EditCtrl',
                resolve: resolveEdit
            });

            $routeProvider.when('/' + name + '/create' + params, {
                templateUrl: 'pages/' + name + '_edit.html',
                controller: '' + ctrlName + 'EditCtrl',
                resolve: resolveCreate
            });

        });

        $routeProvider.otherwise({
            redirectTo: '/' + pages[0].name + '/list'
        });

    });
})();