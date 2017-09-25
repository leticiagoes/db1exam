angular.module('db1.avaliacao.tecnica')
    .config([
        '$routeProvider', function($routeProvider) {
            $routeProvider.otherwise({
                redirectTo: '/'
            });
        }
    ]);