$(function () {
    $(document).ready(function () {
        $('.text-alert').hide();
        validateLogin();
    });
    function validateLogin() {

        var username = $("#Username").val();
        var password = $("#Password").val();
        if (username === null) {
            $("#usernameID").after('<span class="text-alert on-right"> asdasdh</span>')
            $("#usernameID").after().addClass('error-state');

            $("#usernameID").focus();
            return false;
        }
        if (password === null) {
            $("#passwordID").after('<span class="text-alert on-right"> asdasdh</span>')
            $("#passwordID").after().addClass('form-control');
            $("#passwordID").focus();
            return false;
        }
    }


}
);
