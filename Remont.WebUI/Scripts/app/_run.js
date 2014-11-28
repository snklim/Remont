(function () {

    var remontApp = angular.module('remontApp', ['ngRoute', 'ngSanitize']);

    remontApp.factory('editEntityContext', function($cacheFactory) {
        return $cacheFactory('edit-entity-context');
    });

    remontApp.run(function ($templateCache, $http, $q) {

        var controls = ['control_text.html', 'control_select.html', 'control_entity_picker.html'];
        
        controls.forEach(function (controlId) {

            var deferred = $q.defer();

            $templateCache.put(controlId, deferred.promise);

            $http.get('Pages/Controls/' + controlId).success(function (data) {
                deferred.resolve(data);
            });

        });
    });

})();