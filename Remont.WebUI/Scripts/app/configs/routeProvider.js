(function() {
    angular.module('remontApp').constant('pages', window.pages).config(function ($routeProvider, pages) {

        $.each(pages, function(i, page) {

            var name = page.name;
            var ctrlName = page.ctrlName;
            var viewName = page.viewName;

            $routeProvider.when('/' + name + '/list',
            {
                templateUrl: 'pages/' + viewName + '_list.html',
                controller: '' + ctrlName + 'ListCtrl',
                resolve: page.resolver
            });

            $routeProvider.when('/' + name + '/edit/:id', {
                templateUrl: 'pages/' + viewName + '_edit.html',
                controller: '' + ctrlName + 'EditCtrl',
                resolve: page.resolver
            });

            $routeProvider.when('/' + name + '/create', {
                templateUrl: 'pages/' + viewName + '_edit.html',
                controller: '' + ctrlName + 'EditCtrl',
                resolve: page.resolver
            });

        });

    });
})();