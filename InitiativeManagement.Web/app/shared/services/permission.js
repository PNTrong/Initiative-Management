(function (app) {
    'use strict';
    app.factory('permissions', function ($rootScope) {
        var permissionList = [];
        return {
          setPermissions: function(permissions) {
            permissionList = permissions;
            $rootScope.$broadcast('permissionsChanged');
          },
          hasPermission: function (permission) {
            permission = permission.trim();
            return permissionList.some(item => {
              if (typeof item !== 'string') { // item.Name is only used because when I called setPermission, I had a Name property
                return false;
              }

              return item.trim() === permission;
            });
          }
        };
    });
})(angular.module('InitiativeManagement.common'));