'use strict';

function sortableCtrl($scope) {
  $scope.sortableOpt = {
    placeholder: 'ui-state-highlight',
    connectWith: '.connectedSortable'
  };
}

angular
  .module('stats-app')
  .controller('sortableCtrl', ['$scope', sortableCtrl]);
