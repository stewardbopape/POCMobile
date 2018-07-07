'use strict';

function sessionCtrl($scope, $state) {
  $scope.signin = function () {
    $state.go('user.signin');
  };

  $scope.submit = function () {
    $state.go('app.dashboard');
  };
}

angular
  .module('stats-app')
  .controller('sessionCtrl', ['$scope', '$state', sessionCtrl]);
