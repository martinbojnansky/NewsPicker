$(function () {
    $('#input_apiKey').off();
    $('#input_apiKey').on('change',
        function () {
            window.authorizations.add('key', new ApiKeyAuthorization('Authorization', this.value, 'header'));
        });
})();