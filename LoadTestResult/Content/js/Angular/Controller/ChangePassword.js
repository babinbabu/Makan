app.controller("ChangePassword", function ($scope, $http) {
    $scope.password = {};
    $scope.ChangePassword = function () {
        console.log($scope.password);
        if ($scope.password.newPassword != $scope.password.confirmNewPassword) {
            $.growl.warning({ title: "Warning", message: "New password and Confirm password are must same" });
        }
        else {
            ChangePassword();
        }
    }

    function ChangePassword() {
        $.blockUI({ message: null });
        $http({
            method: 'POST',
            url: '/Account/ChangePassword',
            data: $scope.password
        }).success(function (data) {
            $("#changePassword")[0].reset();
            if (data == "False") {
                $.growl.error({ title: "Error", message: "Invalid old password or some other error occured !" });
            }
            else {
                $.growl.notice({ title: "Success", message: "Password updated successfully !" });
            }
            $.unblockUI();
        }).error(function (data, status) {
            $("#changePassword")[0].reset();
            alert("Error in updating record")
            $.unblockUI();
        });
    }
});