$(document).ready(function () {

    for (var i = 0; i < 4; i++) {
        (function (i) {

            document.getElementById("story-votebuttons-box-" + i).addEventListener("mouseenter", function () {
                $("#story-votebuttons-box-colour-" + i).addClass("hover").addClass("highlight-" + i);
            });

            document.getElementById("story-votebuttons-box-" + i).addEventListener("mouseleave", function () {
                $("#story-votebuttons-box-colour-" + i).removeClass("hover").removeClass("highlight-" + i);
            });
        }(i));
    }

    document.getElementById("story-votebuttons").addEventListener("mouseenter", function () {
        $(".story-votebuttons-button").addClass("story-votebuttons-button-highlight");
        $(".story-votebuttons-value").addClass("story-votebuttons-value-highlight");
        $(".story-votebuttons-option").addClass("story-votebuttons-option-highlight");
    });

    document.getElementById("story-votebuttons").addEventListener("mouseleave", function () {
        $(".story-votebuttons-button").removeClass("story-votebuttons-button-highlight");
        $(".story-votebuttons-value").removeClass("story-votebuttons-value-highlight");
        $(".story-votebuttons-option").removeClass("story-votebuttons-option-highlight");
    });

})