$(document).ready(function () {
    /// Stories
    for (var i = 0; i < 4; i++) {
        (function (i) {

            document.getElementById("story-votebuttons-box-" + i).addEventListener("mouseenter", function () {
                $("#story-votebuttons-box-colour-" + i).addClass("hover").addClass("highlight-" + i);
            });

            document.getElementById("story-votebuttons-box-" + i).addEventListener("mouseleave", function () {
                $("#story-votebuttons-box-colour-" + i).removeClass("hover").removeClass("highlight-" + i);
            });

            //document.getElementById("story-chart").addEventListener("mouseleave", function () {
            //    $("#story-votebuttons-box-colour-" + i).removeClass("hover").removeClass("highlight-" + i);
            //});

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



    /// Modal
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

document.getElementById("story-chart").addEventListener("mouseenter", function () {
    $("#story-votebuttons").addClass("story-votebuttons-on-chart-hover")
});

document.getElementById("story-chart").addEventListener("mouseleave", function () {
    $("#story-votebuttons").removeClass("story-votebuttons-on-chart-hover")
});
