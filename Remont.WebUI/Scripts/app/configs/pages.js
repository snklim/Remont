(function () {

    var itemResolvers = [

        function (serviceUrl, tableId) {

            return function ($route, dataFeeder) {

                var id = $route && $route.current && $route.current.params && $route.current.params.id ?
                    $route.current.params.id : 0;

                var params = {
                    tableId: tableId,
                    id: id
                };

                return dataFeeder
                    .create(serviceUrl)
                    .get(params, function(data) {
                        return data.Item;
                    });
            }
        },

        function (serviceUrl, tableId) {

            return function ($route, dataFeeder) {

                var id = $route && $route.current && $route.current.params && $route.current.params.id ?
                    $route.current.params.id : 0;

                var params = {
                    tableId: tableId,
                    id: id
                };

                return dataFeeder
                    .create(serviceUrl)
                    .get(params, function (data) {
                        return data.Items[0].Rows[0];
                    });
            }
        }

    ];

    function resolver(pageUrl, ctrlName, title, serviceUrl, itemResolverId, tableId) {

        var self = {
            name: pageUrl,
            ctrlName: ctrlName,
            viewName: ctrlName,
            serviceUrl: serviceUrl,
            title: title,

            resolver: {

                extData: function() {
                    return {
                        pageUrl: pageUrl,
                        serviceUrl: serviceUrl,
                        tableId: tableId
                    }
                },

                item: itemResolvers[itemResolverId](serviceUrl, tableId),

                response: function (dataFeeder) {
                    return dataFeeder.create(serviceUrl).get({
                        tableId: tableId
                    });
                }

            }
        };

        return self;
    };

    window.pages = [
        new resolver('table', 'Table', 'Table', '/api/table/', 0),
        new resolver('customer', 'Generic', 'Customer', '/api/generic', 0, 1)
    ];
})();