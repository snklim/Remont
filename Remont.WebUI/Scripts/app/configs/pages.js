(function () {
    
    function resolver(pageUrl, ctrlName, title, serviceUrl, tableId) {

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

                item: function ($route, dataFeeder) {

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
                        .get(params, function (data) {
                            return data.Item;
                        });
                },

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
        new resolver('table', 'Table', 'Table', '/api/table/', -1),
        new resolver('customer', 'Generic', 'Customer', '/api/generic', 1)
    ];
})();