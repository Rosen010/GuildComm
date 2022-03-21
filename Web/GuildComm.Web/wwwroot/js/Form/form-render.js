$(document).ready(function () {
    $("#characterForm").click(function () {
        $("#formContainer").load("/CharactersApi/CharacterForm");
    });
});