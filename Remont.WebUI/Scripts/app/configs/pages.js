window.pages = [
    {
        name: 'table',
        ctrlName: 'Table',
        viewName: 'Table',
        serviceUrl: '/api/table',
        title: 'Table'
    },
    {
        name: 'customer',
        ctrlName: 'Generic',
        viewName: 'Generic',
        serviceUrl: '/api/generic/1',
        title: 'Customer',
        resolveCreate: {
            extData: function() {
                return {
                    pageUrl: 'customer',
                    serviceUrl: '/api/generic/1'
                }
            },
            item: function($route, dataFeeder) {
                return dataFeeder
                    .create('/api/generic/1/0')
                    .get($route.current.params, function(data) {
                        return data.Items[0].Rows[0];
                    });
            }
        },
        resolveEdit: {
            extData: function() {
                return {
                    pageUrl: 'customer',
                    serviceUrl: '/api/generic/1'
                }
            },
            item: function($route, dataFeeder) {
                return dataFeeder
                    .create('/api/generic/1/' + $route.current.params.id)
                    .get(null, function(data) {
                        return data.Items[0].Rows[0];
                    });
            }
        },
        resolveList: {
            extData: function() {
                return {
                    pageUrl: 'customer',
                    serviceUrl: '/api/generic/1'
                }
            }
        }
    }
];