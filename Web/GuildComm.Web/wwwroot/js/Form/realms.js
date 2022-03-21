$(document).ready(function () {
    $('#regionSelect').on('change', function () {
        let region = $('#regionSelect option:selected').text();
        let data = { region: region };

        return $.ajax({
            url: '/RealmsApi/UpdateRealms',
            type: 'GET',
            data: data,
            contentType: 'application/json'
        }).done(function (response) {
            if (response) {
                $('#realmSelect').html('');
                let options = '';

                for (var i = 0; i < response.length; i++) {
                    options += '<option value="' + response[i] + '">' + response[i] + '</option>';
                }

                $('#realmSelect').append(options);
            }
        }).fail(function (error) {
            console.log(error.statusText);
        });
    })
});