/// <reference path="../../scripts/angular.js" />

var app = angular.module('UserApp', ['ngFileUpload']);
app.controller('UserCreate', function ($scope, $http, $window,Upload) {
    $scope.Init = function () {
        $scope.GetActiveRole();
    };
    $scope.GetActiveRole = function () {
        $http.get("/Role/GetAllActiveRole")
            .then(function (response) {
                $scope.Role = response.data;
                console.log($scope.Role);
            }), function (response) {
                console.log(response.status);
            };
    };
    $scope.user_Status = "true";
    $scope.buttonDisable = true;
    $scope.UserNameExist = false;
    var validName = false;
    var validComparePassword = false;
    var roleSelected = false;
    $scope.CheckValidation = function () {
        if (validName === true && validComparePassword === true && roleSelected === true) {
            $scope.buttonDisable = false;
        } else {
            $scope.buttonDisable = true;
        }
    };    
    $scope.CheckUserName = function (UserName) {
        $http.get("/User/UserNameExists", { params: { userName: UserName } })
            .then(function (response) {
                if (response.data === true) {
                    $scope.UserNameExist = true;
                    validName = false;
                }
                else {
                    $scope.UserNameExist = false;
                    validName = true;
                }
            }), function (response) {
                alert(response.status);
            };
        $scope.CheckValidation();
    };
    $scope.ComparePassword = function (password, confirmPassword) {
        if (password === confirmPassword) {
            $scope.UnMatchedPassword = false;
            validComparePassword = true;
        } else {
            $scope.UnMatchedPassword = true;
            validComparePassword = false;
        }
        $scope.CheckValidation();
    };
    $scope.RoleDropdown = function (value) {
        if (value === undefined || value === null || value === "") {
            roleSelected = false;
            $scope.InvalidRole = true;
        } else {
            roleSelected = true;
            $scope.InvalidRole = false;
        }
        $scope.CheckValidation();
    };
    $scope.UploadFiles = function (files) {
        $scope.SelectedFiles = files;
    };

    $scope.Create = function (user) {
        user.Status = $scope.user_Status;
        user.RoleId = $scope.roleId;
        if (validName === true && validComparePassword === true && roleSelected === true) {
            if ($scope.SelectedFiles && $scope.SelectedFiles.length) {
                Upload.upload({
                    url: '/User/Upload/',
                    data: {
                        files: $scope.SelectedFiles
                    }
                });
            }
            $http.post("/User/Create", user)
                .then(function (result) {
                    alert(result.data);
                    $scope.user = {};
                    $window.location.href = "/User/Index";
                }, function (result) {
                    alert(result.status);
                });
        }
    };
});

app.controller('UserList', function ($scope, $http, $filter) {

    $scope.Init = function () {
        $scope.GetAllActive();
    };
    $scope.Active = true;
    $scope.GetAllActive = function () {
        $http.get("/User/GetAllActiveUser")
            .then(function (result) {
                $scope.UserList = result.data;
                $scope.pagination($scope.UserList);
            }), function (response) {
                console.log(response.status);
            };
    };
    $scope.GetAllInactiveUser = function () {
        $http.get("/User/GetAllInactiveUser")
            .then(function (result) {
                $scope.UserList = result.data;
                $scope.pagination($scope.UserList);
            }), function (response) {
                console.log(response.status);
            };
    };
    $scope.ClickActiveInactive = function (e) {
        if ($scope.Active === true) {
            $scope.GetAllInactiveUser();
        } else {
            $scope.GetAllActive();
        }
    };

    $scope.pagination = function (list) {
        $scope.sort = {
            sortingOrder: 'id',
            reverse: false
        };
        $scope.filteredItems = [];
        $scope.groupedItems = [];
        $scope.itemsPerPage = 10;
        $scope.pagedItems = [];
        $scope.currentPage = 0;
        $scope.items = list;
        if (($scope.items.length % $scope.itemsPerPage) === 0) {
            $scope.gap = parseInt(($scope.items.length / $scope.itemsPerPage));
        } else {
            $scope.gap = parseInt(($scope.items.length / $scope.itemsPerPage) + 1);
        }

        var searchMatch = function (haystack, needle) {
            if (!needle) {
                return true;
            }
            return haystack.toLowerCase().indexOf(needle.toLowerCase()) !== -1;
        };

        // init the filtered items
        $scope.search = function () {
            $scope.filteredItems = $filter('filter')($scope.items, function (item) {
                for (var attr in item) {
                    if (searchMatch(item[attr], $scope.query))
                        return true;
                }
                return false;
            });
            // take care of the sorting order
            if ($scope.sort.sortingOrder !== '') {
                $scope.filteredItems = $filter('orderBy')($scope.filteredItems, $scope.sort.sortingOrder, $scope.sort.reverse);
            }
            $scope.currentPage = 0;
            // now group by pages
            $scope.groupToPages();
        };


        // calculate page in place
        $scope.groupToPages = function () {
            $scope.pagedItems = [];

            for (var i = 0; i < $scope.filteredItems.length; i++) {
                if (i % $scope.itemsPerPage === 0) {
                    $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)] = [$scope.filteredItems[i]];
                } else {
                    $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)].push($scope.filteredItems[i]);
                }
            }
        };

        $scope.range = function (size, start, end) {
            var ret = [];
            if (size < end) {
                end = size;
                start = size - $scope.gap;
            }
            for (var i = start; i < end; i++) {
                ret.push(i);
            }
            return ret;
        };

        $scope.prevPage = function () {
            if ($scope.currentPage > 0) {
                $scope.currentPage--;
            }
        };

        $scope.nextPage = function () {
            if ($scope.currentPage < $scope.pagedItems.length - 1) {
                $scope.currentPage++;
            }
        };

        $scope.setPage = function () {
            $scope.currentPage = this.n;
        };

        // functions have been describe process the data for display
        $scope.search();
    };
});