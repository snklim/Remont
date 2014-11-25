(function() {
    angular.module('remontApp').directive('entityPicker', function () {
        return {
            templateUrl: function(elem, attr) {
                return 'Pages/' + attr.page + '.html';
            }
        }
    });
})();