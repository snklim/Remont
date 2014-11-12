(function() {
    angular.module('remontApp').factory('pageService', function() {
        return {
            getPages: function(pageIndex, totalPages) {
                var pages = [];

                pages.push({
                    pageIndex: 'prev',
                    pageText: '«'
                });

                var left = pageIndex - 2 - (pageIndex + 3 > totalPages ? pageIndex + 3 - totalPages : 0);
                var right = pageIndex + 3 + (left < 0 ? -1 * left : 0);

                left = Math.max(0, left);
                right = Math.min(totalPages, right);

                for (var i = left; i < right; i++) {
                    pages.push({
                        pageIndex: i,
                        pageText: i + 1
                    });
                }

                pages.push({
                    pageIndex: 'next',
                    pageText: '»'
                });

                return pages;
            }
        };
    });
})();