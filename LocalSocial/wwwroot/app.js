var app = angular.module('StarterApp', ['ngMaterial']);

app.controller('AppCtrl', ['$scope', '$mdSidenav', function ($scope, $mdSidenav) {
    $scope.toggleSidenav = function (menuId) {
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
}]);

app
  .module('sidenavDemo2', ['ngMaterial'])
  .controller('AppCtrl', function ($scope, $timeout, $mdSidenav) {
      $scope.toggleLeft = buildToggler('left');
      $scope.toggleRight = buildToggler('right');

      function buildToggler(componentId) {
          return function () {
              $mdSidenav(componentId).toggle();
          }
      }
  });