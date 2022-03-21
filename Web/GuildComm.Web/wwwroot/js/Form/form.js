$(document).ready(function () {
    function updateRealms(region) {
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
    }

    function bindChangeEvent(formId) {
            $(formId).on('change', function () {
                let region = $(formId + " option:selected").text()
                updateRealms(region);
            });
    }

    function setup() {
        bindChangeEvent("#guildRegionSelect");

        $("#characterForm").click(function () {
            $("#formContainer").load("/CharactersApi/CharacterForm", function () {
                bindChangeEvent("#characterRegionSelect");
                $("#characterForm").prop("disabled", true);
                $("#guildForm").prop("disabled", false);
            });
        });

        $("#guildForm").click(function () {
            $("#formContainer").load("/GuildsApi/GuildForm", function () {
                bindChangeEvent("#guildRegionSelect");
                $("#characterForm").prop("disabled", false);
                $("#guildForm").prop("disabled", true);
            });
        });
    }

    setup();
});