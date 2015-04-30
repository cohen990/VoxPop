$(document).ready(function () {


    //document.getElementById("comment-box-2").addEventListener("mouseenter", function () {
    //    $("#comment-box-colour-2").addClass("modal-hover").addClass("modal-highlight-2");

    //    $("#comment-option-2").addClass("story-votebuttons-modal-option-highlight");
    //});

    //for (var i = 0; i < 13; i++) {
    //    (function (i) {

    //        document.getElementById("comment-box-" + i).addEventListener("mouseenter", function () {
    //            $("#comment-box-colour-" + i).addClass("modal-hover").addClass("modal-highlight-" + i);

    //            $("#comment-option-" + i).addClass("story-votebuttons-modal-option-highlight");

    //        });

    //        document.getElementById("comment-box-" + i).addEventListener("mouseleave", function () {
    //            $("#comment-box-colour-" + i).removeClass("modal-hover").removeClass("modal-highlight-" + i);

    //            $("#comment-option-" + i).removeClass("story-votebuttons-modal-option-highlight");

    //        });
    //    }(i));
    //}


    for (var i = 0; i < 13; i++) {
        (function (i) {

            document.getElementById("comment-box-reply-" + i).addEventListener("mouseenter", function () {
                $("#comment-box-reply-colour-" + i).addClass("modal-hover").addClass("modal-highlight-" + i);

                $("#comment-reply-option-" + i).addClass("story-votebuttons-modal-option-highlight");

            });

            document.getElementById("comment-box-reply-" + i).addEventListener("mouseleave", function () {
                $("#comment-box-reply-colour-" + i).removeClass("modal-hover").removeClass("modal-highlight-" + i);

                $("#comment-reply-option-" + i).removeClass("story-votebuttons-modal-option-highlight");

            });
        }(i));
    }


});
