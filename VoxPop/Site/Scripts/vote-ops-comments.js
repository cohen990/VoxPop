$(document).ready(function () {

    /// Comments
    for (var i = 0; i < 13; i++) {
        (function (i) {

            document.getElementById("comment-box-" + i).addEventListener("mouseenter", function () {
                $("#comment-box-colour-" + i).addClass("modal-hover").addClass("modal-highlight-" + i);

                $("#comment-option-" + i).addClass("story-votebuttons-modal-option-highlight");

            });

            document.getElementById("comment-box-" + i).addEventListener("mouseleave", function () {
                $("#comment-box-colour-" + i).removeClass("modal-hover").removeClass("modal-highlight-" + i);

                $("#comment-option-" + i).removeClass("story-votebuttons-modal-option-highlight");

            });
        }(i));
    }


})