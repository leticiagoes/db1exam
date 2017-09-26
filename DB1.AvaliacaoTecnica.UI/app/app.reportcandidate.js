var app = angular.module('AppReportCandidate', []);
app.controller('ReportCandidateController', function ($scope, $http) {
    var urlAPI = "http://localhost:64762/api/reportcandidate";
    var successClass = 'alert alert-success';
    var errorClass = 'alert alert-danger';
    var iconSuccessClass = 'glyphicon glyphicon-ok';
    var iconErrorClass = 'glyphicon glyphicon-remove';

    $scope.showLoading;
    $scope.classMessage;
    $scope.responseMessage;
    $scope.iconMessage;
    $scope.noResult = false;
    $scope.itemList = null;
    getItemList();

    $scope.showItemList = function () {
        $scope.classMessage = null;
        $scope.responseMessage = null;
        $scope.iconMessage = null;
        getItemList();
    }

    function getItemList() {
        $scope.showLoading = true;
        $http.get(urlAPI).then(function onSuccess(response) {
            gotList(response);
            $scope.showLoading = false;
        }, function onError(response) {
            checkResponse(response);
            $scope.showLoading = false;
        });
    }

    function gotList(response) {
        $scope.itemList = response.data;
        if (response.status == 204) {
            $scope.noResult = true;
        } else {
            $scope.noResult = false;
        }
    }

    function checkResponse(response) {
        if (response.status == 200) {
            $scope.classMessage = successClass;
            $scope.responseMessage = response.data;
            $scope.iconMessage = iconSuccessClass;
        } else {
            if (response.status == 400)
                $scope.responseMessage = response.data.Message;
            else
                $scope.responseMessage = response.data;
            $scope.classMessage = errorClass;
            $scope.iconMessage = iconErrorClass;
        }
    }
});