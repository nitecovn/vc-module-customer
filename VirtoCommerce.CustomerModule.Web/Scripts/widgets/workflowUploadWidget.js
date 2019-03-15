angular.module('virtoCommerce.customerModule')
    .controller('virtoCommerce.customerModule.workflowWidgetController',
        ['$scope', 'platformWebApp.bladeNavigationService', 'virtoCommerce.customerModule.workflows',
            function ($scope, bladeNavigationService, workflows) {
                blade = $scope.blade;
                $scope.workflow = "0";

                // workflows.get({ id: blade.currentEntity.id })
                //     .$promise
                //     .then(function(data) {
                //         console.log(data);
                //         // data = data.data;
                //         // $scope.workflow = typeof data !== 'undefined' && typeof data.workflowStates !== 'undefined' && data.workflowStates.length ? "1" : "0";
                //         // blade.workflow = data;
                //     }, function(response) {
                //         bladeNavigationService.setError(response, blade);
                //     });

                $scope.openWorkflowBlade = function () {
                    var newBlade = {
                        id: 'workflowBlade',
                        title: 'customer.blades.organization-workflow.title',
                        controller: 'virtoCommerce.customerModule.uploadFlowController',
                        template: 'Modules/$(VirtoCommerce.Customer)/Scripts/blades/upload-workflow.tpl.html',
                        isClosingDisabled: true
                    };
                    bladeNavigationService.showBlade(newBlade, $scope.blade);
                };
}]);