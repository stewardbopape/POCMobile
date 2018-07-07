(function() {
	'use strict';

	angular
		.module('stats-app')
		.config(configure)
		.run(appRun);

	configure.$inject = ['$qProvider'];
	appRun.$inject = [];
	function configure ($qProvider) {
		$qProvider.errorOnUnhandledRejections(false);
	}
	function appRun() {}

})();
