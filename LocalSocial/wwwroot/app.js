var app = angular.module('StarterApp', ['ngMaterial']);

angular.module('app', ['ngMaterial']);

app.controller('AppCtrl', ['$scope', '$mdSidenav', function($scope, $mdSidenav) {
        $scope.toggleSidenav = function(menuId) {
            $mdSidenav(menuId).toggle();
        };

        var list = [];
        for (var i = 0; i < 100; i++) {
            list.push({
                name: 'List Item ' + i,
                idx: i
            });
        }
        $scope.list = list;
        $scope.imagePath = 'img/small.png';
    }
]);

app.controller('MenuController', MenuController);
app.controller('ContentController', ContentController);

