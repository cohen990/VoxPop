$(document).ready(function () {
    $('.trigger').click(function () {
        $('.modal-wrapper').toggleClass('open');
        $('.page-wrapper').toggleClass('blur');
        return false;
    });

    function closeModal() {
        $('.modal-wrapper').removeClass('open');
        $('.page-wrapper').removeClass('blur');

    }

    $('.modal-wrapper').on('click', function () {
        closeModal();
    });
});