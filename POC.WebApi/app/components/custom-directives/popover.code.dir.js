(function function_name() {
	'use strict';

	angular.module("stats-app").directive("popoverCode",function( $uibModal, $document){
	    return {
	    
	        link: function (scope, iElement, iAttrs) {
	            var button = angular.element("<div id='source-button' class='btn btn-success btn-xs'>&lt; &gt;</div>").click(function(){
	            	var parent = angular.copy(angular.element(this).parent());

	            	var rickshaw = parent.find('rickshaw');
	            	if(rickshaw){
	            		parent.find('rickshaw').empty();	            		
	            	}

	            	var flot = parent.find('flot');
	            	if(flot){
	            		parent.find('flot').empty();	            		
	            	}

	            	var rating = parent.find('.uib-rating');
	            	if (rating) {
	            	    parent.find('.uib-rating').empty();
	            	}

	            	var accordion = parent.find('uib-accordion');
	            	if (accordion) {

	            	    var panel = accordion.find('.panel-group');

	            	    var panelHtml = angular.element(panel.html());
	            	    panel.remove();
	            	    accordion.append(panelHtml)
	            	   // parent.find('uib-accordion').empty();
	            	}
			    
			    	var html =  parent.html();
			    	html = cleanSource(html);			     
			     	openModal('lg',null,html);
			  	});

	            iElement.hover(function(){
				    iElement.append(button);
				    button.show();
				  }, function(){
				    button.hide();
				  });


	           var openModal = function (size, parentSelector,html) {
				    var parentElem = parentSelector ? 
				      angular.element($document[0].querySelector('.app-content' + parentSelector)) : undefined;
				    var modalInstance = $uibModal.open({
				      animation: false,
				      ariaLabelledBy: 'modal-title',
				      ariaDescribedBy: 'source-modal',
				      templateUrl: 'sourceModal.html',
				      controller: 'sourceModalCtrl',
				      controllerAs: '$ctrl',
				      size: size,
				      appendTo: parentElem,
				      resolve: {
				        code: function () {
				          return html;
				        }
				      }
				    });
		        };

		       var cleanSource = function (html) {
                    html = html.replace(/×/g, "&times;")
                                .replace(/«/g, "&laquo;")
                                .replace(/»/g, "&raquo;")
                                .replace(/←/g, "&larr;")
                                .replace(/→/g, "&rarr;")
                                .replace(' ng-pristine', '')
                                .replace(' ng-untouched', '')
                                .replace(' ng-valid', '')
                                .replace(' ng-not-empty', '')
                                .replace(' ng-binding', '')
                                .replace(' ng-hide', '')
                        .replace(' ng-scope', '')
                        .replace(' panel', '')
                        .replace(' panel-open', '')
                        .replace(' ng-hide', '')
                        .replace(' ng-hide', '')
                        .replace(' ng-hide', '')
                        .replace(' ng-hide', '')
                        .replace(' ng-hide', '')
                                .replace(' ng-isolate-scope','');

				    var lines = html.split(/\n/);

				    lines.shift();
				    lines.splice(-1, 1);

				    var indentSize = lines[0].length - lines[0].trim().length;
				    var re = new RegExp(" {" + indentSize + "}");

				    lines = lines.map(function(line){
				      if (line.match(re)) {
				        line = line.substring(indentSize);
				      }

				      return line;
				    });

				    lines = lines.join("\n");

				    return lines;
			}
	    }
	}
});



//angular.module("stats-app").controller('ModalInstanceCtrl', function ($uibModalInstance, code) {
//  var $ctrl = this;
//  $ctrl.code = code;

//  $ctrl.ok = function () {
//    $uibModalInstance.close($ctrl.selected.item);
//  };

//  $ctrl.cancel = function () {
//    $uibModalInstance.dismiss('cancel');
//  };
//});

angular.module("stats-app").controller('sourceModalCtrl', function ($uibModalInstance, code) {
    var $ctrl = this;
    $ctrl.code = code;

    $ctrl.ok = function () {
        $uibModalInstance.close($ctrl.selected.item);
    };

    $ctrl.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});


})();