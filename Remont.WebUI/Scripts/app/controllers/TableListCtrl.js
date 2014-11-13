(function() {
    angular.module('remontApp').controller('TableListCtrl', function($scope, baseListCtrl, response) {

        baseListCtrl.create($scope, response, 'table', 'api/table');

    });
})();