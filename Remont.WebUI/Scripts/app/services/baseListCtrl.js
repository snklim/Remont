(function() {
    angular.module('remontApp').factory('baseListCtrl', function ($location, pageService, dataFeeder) {

        function baseListCtrl(scope, response, pageUrl, serviceUrl) {

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
                    pageIndex: pageIndex
                }).then(function (r) {
                    scope.items = r.Items;
                    scope.pageInfo = r.PageInfoRequest;

                    scope.pages = pageService.getPages(scope.pageInfo.PageIndex, scope.pageInfo.TotalPages);
                });

                event.preventDefault();

            };
        };

        return {
            create: function (scope, response, pageUrl, serviceUrl) {
                return new baseListCtrl(scope, response, pageUrl, serviceUrl);
            }
        };
    });
})();