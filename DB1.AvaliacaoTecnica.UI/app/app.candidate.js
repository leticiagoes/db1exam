﻿var app = angular.module('AppCandidate', []);
app.controller('CandidateController', function ($scope, $http) {
    var urlAPI = "http://localhost:64762/api/candidate";
    var urlAPI_Opportunity = "http://localhost:64762/api/opportunity";
    var urlAPI_Technology = "http://localhost:64762/api/technology";
    var successClass = 'alert alert-success';
    var errorClass = 'alert alert-danger';
    var iconSuccessClass = 'glyphicon glyphicon-ok';
    var iconErrorClass = 'glyphicon glyphicon-remove';

    $scope.showLoading;
    $scope.classMessage;
    $scope.responseMessage;
    $scope.iconMessage;
    $scope.noResult = false;
    $scope.item = null;
    $scope.itemList = null;
    $scope.options = null;
    $scope.optionSelected;
    $scope.checkboxList = null;
    $scope.checksSelected = null;
    getItemList();

    $scope.showItemList = function () {
        $scope.item = null;
        $scope.classMessage = null;
        $scope.responseMessage = null;
        $scope.iconMessage = null;
        getItemList();
    }

    $scope.addItem = function () {
        $scope.itemList = null;
        newItem();
        getOpportunityList();
        getTechnologyList();
    }

    $scope.saveItem = function (item) {
        item.IdOpportunity = $scope.optionSelected.Id;

        if (item.Id > 0)
            updateItem(item);
        else
            saveItem(item);

        newItem();
    }

    $scope.editItem = function (item) {
        $scope.itemList = null;
        getItemDetails(item);
    }

    $scope.deleteItem = function (id) {
        deleteItem(id);
        $scope.item = null;
        getItemList();
    }

    function newItem() {
        $scope.item = { 'Id': 0, 'Name': null };
        $scope.optionSelected;
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

    function getOpportunityList() {
        $scope.showLoading = true;
        $http.get(urlAPI_Opportunity).then(function onSuccess(response) {
            $scope.options = response.data;
            $scope.showLoading = false;
        }, function onError(response) {
            checkResponse(response);
            $scope.showLoading = false;
        });
    }

    function getTechnologyList() {
        $scope.showLoading = true;
        $http.get(urlAPI_Technology).then(function onSuccess(response) {
            $scope.checkboxList = response.data;
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
        }
    }

    function getItemDetails(item) {
        $http.get(urlAPI + "/" + item.Id).then(function onSuccess(response) {
            $scope.item = response.data;
            $scope.optionSelected.Id = response.data.IdOpportunity;
        }, function onError(response) {
            checkResponse(response);
        });
    }

    function saveItem(item) {
        $http.post(urlAPI, item).then(function onSuccess(response) {
            checkResponse(response);
        }, function onError(response) {
            checkResponse(response);
        });
    }

    function updateItem(item) {
        $http.put(urlAPI + "/" + item.Id, item).then(function onSuccess(response) {
            checkResponse(response);
        }, function onError(response) {
            checkResponse(response);
        });
    }

    function deleteItem(id) {
        $http.delete(urlAPI + "/" + id).then(function onSuccess(response) {
            checkResponse(response);
        }, function onError(response) {
            checkResponse(response);
        });
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