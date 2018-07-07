'use strict';

function tableEditableCtrl($scope, $filter, $http, editableOptions, editableThemes) {
	editableThemes.bs3.inputClass = 'input-sm';
	editableThemes.bs3.buttonsClass = 'btn-sm';
	editableOptions.theme = 'bs3';

	$scope.users = [
		{
			id: 1,
			name: 'awesome user1',
			status: 2,
			group: 4,
			groupName: 'admin'
				},
		{
			id: 2,
			name: 'awesome user2',
			status: undefined,
			group: 3,
			groupName: 'vip'
				},
		{
			id: 3,
			name: 'awesome user3',
			status: 2,
			group: null
				}
	];

	$scope.statuses = [
		{
			value: 1,
			text: 'status1'
				},
		{
			value: 2,
			text: 'status2'
				},
		{
			value: 3,
			text: 'status3'
				},
		{
			value: 4,
			text: 'status4'
				}
	];

	$scope.groups = [];
	$scope.loadGroups = function () {
		return $scope.groups.length ? null : $http.get('data/groups.json').success(function (data) {
			$scope.groups = data;
		});
	};

	$scope.showGroup = function (user) {
		if (user.group && $scope.groups.length) {
			var selected = $filter('filter')($scope.groups, {
				id: user.group
			});
			return selected.length ? selected[0].text : 'Not set';
		} else {
			return user.groupName || 'Not set';
		}
	};

	$scope.showStatus = function (user) {
		var selected = [];
		if (user.status) {
			selected = $filter('filter')($scope.statuses, {
				value: user.status
			});
		}
		return selected.length ? selected[0].text : 'Not set';
	};

	$scope.checkName = function (data, id) {
		if (id === 2 && data !== 'awesome') {
			return 'Username 2 should be `awesome`';
		}
	};

	$scope.saveUser = function (data, id) {
		//$scope.user not updated yet
		angular.extend(data, {
			id: id
		});
		//return $http.post('/saveUser', data);
	};

	// remove user
	$scope.removeUser = function (index) {
		$scope.users.splice(index, 1);
	};

	// add user
	$scope.addUser = function () {
		$scope.inserted = {
			id: $scope.users.length + 1,
			name: '',
			status: null,
			group: null
		};
		$scope.users.push($scope.inserted);
	};

	$scope.checkName2 = function (data) {
		if (data !== 'awesome') {
			return 'Username should be `awesome`';
		}
	};

	$scope.saveColumn = function (column) {
		var results = [];
		/*angular.forEach($scope.users, function(user) {
			results.push($http.post('/saveColumn', {column: column, value: user[column], id: user.id}));
		})
		return $q.all(results);*/
	};

	$scope.checkName3 = function (data, id) {
		if (id === 2 && data !== 'awesome') {
			return 'Username 2 should be `awesome`';
		}
	};

	// filter users to show
	$scope.filterUser = function (user) {
		return user.isDeleted !== true;
	};

	// mark user as deleted
	$scope.deleteUser = function (id) {
		var filtered = $filter('filter')($scope.users, {
			id: id
		});
		if (filtered.length) {
			filtered[0].isDeleted = true;
		}
	};

	// add user
	$scope.addUser2 = function () {
		$scope.users.push({
			id: $scope.users.length + 1,
			name: '',
			status: null,
			group: null,
			isNew: true
		});
	};

	// cancel all changes
	$scope.cancel = function () {
		for (var i = $scope.users.length; i--;) {
			var user = $scope.users[i];
			// undelete
			if (user.isDeleted) {
				delete user.isDeleted;
			}
			// remove new
			if (user.isNew) {
				$scope.users.splice(i, 1);
			}
		}
	};

	// save edits
	$scope.saveTable = function () {
		var results = [];
		for (var i = $scope.users.length; i--;) {
			var user = $scope.users[i];
			// actually delete user
			if (user.isDeleted) {
				$scope.users.splice(i, 1);
			}
			// mark as not new
			if (user.isNew) {
				user.isNew = false;
			}

			// send on server
			//results.push($http.post('/saveUser', user));
		}

		return; // $q.all(results);
	};
}

angular
	.module('stats-app')
	.controller('tableEditableCtrl', ['$scope', '$filter', '$http', 'editableOptions', 'editableThemes', tableEditableCtrl]);
