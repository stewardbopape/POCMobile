'use strict';

function maskCtrl($scope) {
  $scope.maskOpt = {
    autoclear: false
  };
}

angular
  .module('stats-app')
  .controller('maskCtrl', ['$scope', maskCtrl]);
