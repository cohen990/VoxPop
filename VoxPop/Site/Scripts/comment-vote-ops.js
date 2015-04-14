$(document).ready(function () {

    for (var i = 0; i < 13; i++) {
        (function (i) {

            document.getElementById("modal-box-" + i).addEventListener("mouseenter", function () {
                $("#story-votebuttons-modal-box-colour-" + i).addClass("modal-hover").addClass("modal-highlight-" + i);

                $("#story-votebuttons-modal-button-" + i).addClass("story-votebuttons-modal-button-highlight");
                $("#modal-value-" + i).addClass("story-votebuttons-modal-value-highlight");
                $("#story-votebuttons-modal-option-" + i).addClass("story-votebuttons-modal-option-highlight");

            });

            document.getElementById("modal-box-" + i).addEventListener("mouseleave", function () {
                $("#story-votebuttons-modal-box-colour-" + i).removeClass("modal-hover").removeClass("modal-highlight-" + i);

                $("#story-votebuttons-modal-button-" + i).removeClass("story-votebuttons-modal-button-highlight");
                $("#modal-value-" + i).removeClass("story-votebuttons-modal-value-highlight");
                $("#story-votebuttons-modal-option-" + i).removeClass("story-votebuttons-modal-option-highlight");

            });
        }(i));
    }
})