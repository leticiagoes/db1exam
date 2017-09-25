var app = angular.module('App', []);
app.controller('Controller', function ($scope, $http) {
    $scope.userDetail = null;
    $scope.reposList = null;
    getUsers();

    function getUsers() {
        $http.get("https://api.github.com/users")
            .then(gotUsers, console.error);
    }

    function gotUsers(users) {
        $scope.userList = users.data;
    }

    $scope.showUserList = function () {
        $scope.userDetail = null;
        getUsers();
    }

    $scope.showUserDetails = function (username) {
        getUserDetails(username);
    }

    function getUserDetails(username) {
        $http.get("https://api.github.com/users/" + username).then(function onSuccess(response) {
            $scope.userDetail = response.data;
        });

        getUserRepos(username);
    }

    function getUserRepos(username) {
        $http.get("https://api.github.com/users/" + username + "/repos").then(function onSuccess(response) {
            $scope.reposList = response.data;
        });
    }
});