(function() {
    angular.module('remontApp').factory('baseListCtrl', function ($location, pageService, dataFeeder) {

        function baseListCtrl(scope, response, pageUrl, serviceUrl, tableId) {

            var feeder = dataFeeder.create(serviceUrl);

            scope.pageInfo = response.PageInfoRequest;

            scope.items = response.Items;

            scope.pages = pageService.getPages(scope.pageInfo.PageIndex, scope.pageInfo.TotalPages);

            scope.edit = function (id) {

                if (id > 0) {
                    $location.path(pageUrl + '/edit/' + id);
                } else {
                    $location.path(pageUrl + '/create/');
                }

            };

            scope.pageChange = function () {

                var pageIndex = $(event.target).attr('data-pageindex');

                if (pageIndex == 'prev') {
                    pageIndex = scope.pageInfo.PageIndex - 1;
                } else if (pageIndex == 'next') {
                    pageIndex = scope.pageInfo.PageIndex + 1;
                }

                feeder.get({
                    pageIndex: pageIndex,
                    tableId: tableId
                }).then(function (r) {
                    scope.items = r.Items;
                    scope.pageInfo = r.PageInfoRequest;

                    scope.pages = pageService.getPages(scope.pageInfo.PageIndex, scope.pageInfo.TotalPages);
                });

                event.preventDefault();

            };

            scope.paginationVisible = function () {
                return response.PageInfoRequest.TotalPages > 1;
            };
        };

        return {
            create: function (scope, response, pageUrl, serviceUrl, tableId) {
                return new baseListCtrl(scope, response, pageUrl, serviceUrl, tableId);
            }
        };
    });
})();