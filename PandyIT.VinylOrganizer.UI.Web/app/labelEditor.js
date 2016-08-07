/// <reference path="../scripts/typings/angularjs/angular.d.ts" />
var LabelApplication;
(function (LabelApplication) {
    var LabelEditor = (function () {
        function LabelEditor() {
        }
        LabelEditor.editorModule = angular.module("editorModule", []);
        return LabelEditor;
    }());
    LabelApplication.LabelEditor = LabelEditor;
})(LabelApplication || (LabelApplication = {}));
