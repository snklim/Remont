(function() {
    angular.module('remontApp').constant('pageResolver', function(page) {

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
            }
        };

        var resolverListBase = {
            response: function(dataFeeder) {
                return dataFeeder.create(serviceUrl).get({
                    tableId: tableId
                });
            }
        };

        var resolverEditBase = {
            response: function ($route, dataFeeder) {

                var id = $route && $route.current && $route.current.params && $route.current.params.id ?
                    $route.current.params.id : 0;

                if (tableId < 0 && id == 0) {
                    return {};
                }

                var params = {
                    tableId: tableId,
                    id: id,
                    action: 'item'
                };

                return dataFeeder
                    .create(serviceUrl)
                    .get(params);
            },
        };

        var resolverEdit = _.extend(page.resolverEdit || {}, resolverEditBase, resolverBase);

        var resolverList = _.extend(page.resolverList || {}, resolverListBase, resolverBase);

        var self = {
            name: pageUrl,
            ctrlName: ctrlName,
            viewName: ctrlName,
            serviceUrl: serviceUrl,
            title: title,

            resolverEdit: resolverEdit,

            resolverList: resolverList,
        };

        return self;
    });
})();