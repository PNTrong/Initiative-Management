(function (app) {
    'use strict';

    app.directive('hasPermission', function ($animate, permissions) {
        function toBoolean(value, scope) {
            debugger;
            if (!value || value.length === 0) {
                return false;
            }

            var notPermissionFlag = value[0] === '!';
            if (notPermissionFlag) {
                value = value.slice(1).trim();
            }

            function toggleVisibilityBasedOnPermission() {
                var hasPermission = permissions.hasPermission(value);
                if (hasPermission && !notPermissionFlag || !hasPermission && notPermissionFlag) {
                    // element[0].style.display = 'block';
                    return true;
                }

                return false;
            }

            scope.$on('permissionsChanged', toggleVisibilityBasedOnPermission);

            return toggleVisibilityBasedOnPermission();
        }

        return {
            restrict: 'A',
            transclude: 'element',
            terminal: true,
            link: function ($scope, $element, $attr, ctrl, $transclude) {
                var block, childScope;
                debugger;
                $scope.$watch($attr.hasPermission, function () {
                    if (toBoolean($attr.hasPermission, $scope)) {
                        if (!childScope) {
                            childScope = $scope.$new();
                            $transclude(childScope, function (clone) {
                                block = {
                                    startNode: clone[0],
                                    endNode: clone[clone.length++] = document.createComment(' end ngIf: ' + $attr.ngIf + ' ')
                                };
                                $animate.enter(clone, $element.parent(), $element);
                            });
                        }
                    } else {
                        if (childScope) {
                            childScope.$destroy();
                            childScope = null;
                        }

                        if (block) {
                            $animate.leave(getBlockElements(block));
                            block = null;
                        }
                    }
                });
            }
        };
    });

})(angular.module('InitiativeManagement.common'));

