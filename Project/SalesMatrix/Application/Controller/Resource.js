/// <reference path="../../scripts/angular.js" />

var app = angular.module('ResourceApp', []);
app.controller('ResourceCreate', function ($scope, $http, $window, $filter) {
    $scope.ResourceTypeList = [{ Name: 'Header Menu' }, { Name: 'Menu Item' }, { Name: 'Link' }, { Name: 'Text' }, { Name: 'Image' }, { Name: 'Video' }];
    $scope.resource_Type = 'Header Menu';
    $scope.buttonDisable = true;
    $scope.resource_Status = "true";
    var validName = false;
    $scope.Active = true;
    $scope.f = function (e) {
        alert(e);
    }
    $scope.CheckValidation = function () {
        if (validName === true) {
            $scope.buttonDisable = false;
        } else {
            $scope.buttonDisable = true;
        }
    };


    $scope.Create = function (resource) {
        module.Status = $scope.module_Status;
        $http.post("/Resource/Create", module)
            .then(function (result) {
                $scope.module = {};
                alert(result.data);
                $window.location.href = "/Resource/Create";
            }, function (result) {
                alert(result.data);
            });
    };

});