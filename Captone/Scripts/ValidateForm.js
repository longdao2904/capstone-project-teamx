$(document).ready(function () {

    $('#123456').validate({

        rules: {
            Username: {
                required: true
            },
            Password: {
                required: true
            },          
            address: {
                minlength: 10,
                required: true
            },

            agree: "required"

        },
        highlight: function (element) {
            $(element).closest('.control-group').removeClass('success').addClass('error');
        },
        success: function (element) {
            element
            .text('OK!').addClass('valid')
            .closest('.control-group').removeClass('error').addClass('success');
        }
    });

    $('#PostRequestForm').validate({

        rules: {
            Width: {
                required: true,
                digits:true
            },
            Height: {
                required: true,
                digits: true
            },
            Length: {
                required: true,
                digits: true
            },
            EstimateWeight: {
                required: true
            },

            TypeOfPayment: {
                required: true
            },
            ReceiverPhone: {
                required: true,
                number: true,
                minlength: 10,
                maxlength:11
            },
            ReceiverAddress: {
                required:true
            },
            ReceiverName: {
                required: true,
                minlength:6
            },
            SenderPhone: {
                required: true,
                number: true,
                minlength: 10,
                maxlength: 11
            },

            SenderAddress: {
                required: true
            },
            name: {
                required: true
            },

            username: {
                minlength: 6,
                required: true
            },

            password: {
                required: true,
                minlength: 6
            },
            confirm_password: {
                required: true,
                minlength: 6,
                equalTo: "#password"
            },

            email: {
                required: true,
                email: true
            },


            address: {
                minlength: 10,
                required: true
            },

            agree: "required"

        },
        highlight: function (element) {
            $(element).closest('.control-group').removeClass('success').addClass('error');
        },
        success: function (element) {
            element
            .text('OK!').addClass('valid')
            .closest('.control-group').removeClass('error').addClass('success');
        }
    });

}); // end document.ready