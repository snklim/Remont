(function () {
    angular.module('remontApp').constant('pages', window.pages).config(function ($routeProvider, pages, pageResolver) {

        $.each(pages, function(i, page) {

            page = pageResolver(page);

            var name = page.name;
            var ctrlName = page.ctrlName;
            var viewName = page.viewName;

            $routeProvider.when('/' + name + '/list',
            {
                templateUrl: 'pages/' + viewName + '_list.html',
                controller: '' + ctrlName + 'ListCtrl',
                resolve: page.resolverList
            });

            $routeProvider.when('/' + name + '/edit/:id', {
                templateUrl: 'pages/' + viewName + '_edit.html',
                controller: '' + ctrlName + 'EditCtrl',
                resolve: page.resolverEdit
            });

            $routeProvider.when('/' + name + '/create', {
                templateUrl: 'pages/' + viewName + '_edit.html',
                controller: '' + ctrlName + 'EditCtrl',
                resolve: page.resolverEdit
            });

        });

        if (pages && pages.length > 0) {
            $routeProvider.otherwise('/' + pages[0].pageUrl + '/list');
        }

    });
})();