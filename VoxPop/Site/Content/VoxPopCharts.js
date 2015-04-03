var VoxPopCharts = (function () {

    var EmptyChart = [
        {
            value: 1,
            color: "#ddd",
            highlight: "#bbb",
            label: "Be the first to vote on this Story",

        }
    ];

    //Segment colours
    //red/blue/yellow/green/purple/orange/red/blue/yellow/green/purple/orange/red/red/red/red etc... (repeating red after 12 colours)
    var colourList = [
        { colour: "#FFd1d1", highlight: "#FF7C8B" },
        { colour: "#bbdeff", highlight: "#88c5ff" },
        { colour: "#fffd9f", highlight: "#FFFB53" },
        { colour: "#7fff80", highlight: "#49ff4a" },
        { colour: "#d8b2d8", highlight: "#bf7fbf" },
        { colour: "#ffd27f", highlight: "#ffc04c" },
        { colour: "#ff2a2a", highlight: "#ff0000" },
        { colour: "#37c4df", highlight: "#1d9db5" },
        { colour: "#ffff32", highlight: "#ffff00" },
        { colour: "#32ff33", highlight: "#00ff01" },
        { colour: "#993299", highlight: "#732573" },
        { colour: "#ffae19", highlight: "#e59400" },
        { colour: "#FFd1d1", highlight: "#FF7C8B" }
    ];

    var colournumber = 0;

    //Resets colours to start after each chart
    function InitializeChart() {
        colournumber = 0;
    }

    //

    //    Possible way to implement %'s for chart legends
    //var currentOption = 0;
    //var perOption = [];

    //function totalVotes() {
    //    if (currentOption >= numPoll) {
    //        perOption = [];
    //        currentOption = 0;
    //        perOption[currentOption] = votesPerOption;
    //    } else {
    //        perOption[currentOption] = votesPerOption;

    //    }
    //}

    //

    function GetPollData(optionName, votes) {
        var result = {
            value: votes,
            color: colourList[colournumber].colour,
            highlight: colourList[colournumber].highlight,
            label: optionName
        };

        colournumber++;

        if (colournumber >= colourList.length) {
            colournumber = 0;
        }

        return result;
    }

    var GetPreparedData = function (data) {

        var noVotes = true;

        for (var i = 0; i < data.length; i++) {
            data[i].label = DecodeHtml(data[i].label);

            if (data[i].value !== 0) {
                noVotes = false;
            }
        }

        if (noVotes === true) {
            data = EmptyChart;
        }

        return data;
    }


    var GenerateChart = function (identifier, data) {

        data = GetPreparedData(data);

        var context = document.getElementById(identifier).getContext("2d");

        if (data === EmptyChart) {
            var myPieChart = new Chart(context).Pie(data, {
                tooltipCornerRadius: 15,
                tooltipTemplate: "<%if (label){%><%=label%> <%}%><%= '' %>",
                animationEasing: "easeOutQuart",
                animateScale: true,
                segmentShowStroke: true,
                segmentStrokeColor: "#060606",
                segmentStrokeWidth: 5
            });
        }

        else {
            var myPieChart = new Chart(context).Pie(data, {
                tooltipCornerRadius: 15,
                animationEasing: "easeOutQuart",
                //animateScale: true,
                segmentShowStroke: true,
                segmentStrokeColor: "#060606",
                segmentStrokeWidth: 5
            });
        }

        //Links votebutton boxes to segments
        for (var i = 0; i < data.length; i++) {
            (function (i) {

                document.getElementById("story-votebuttons-box-" + i).addEventListener("mouseover", function () {

                    var activeSegment = myPieChart.segments[i];
                    activeSegment.save();
                    activeSegment.fillColor = activeSegment.highlightColor;
                    if (document.getElementById("story-votebuttons-value-" + i).innerHTML !== "0")
                    {
                        myPieChart.showTooltip([activeSegment]);
                    }
                    activeSegment.restore();
                });

                //document.getElementById("story-votebuttons-box-" + i).addEventListener("mouseleave", function () {
                //    myPieChart.draw();
                //});
            }(i));
        }

        //for (var i = 0; i < data.length; i++) {
        //    (function (i) {
        //        document.getElementById("story-votebuttons-box-" + i).addEventListener("mouseleave", function () {
        //            myPieChart.draw();
        //        });
        //    }(i));
        //}


    }

    function DecodeHtml(inputString) {
        return $("<div/>").html(inputString).text();
    }

    return {
        DecodeHtml: DecodeHtml,
        GenerateChart: GenerateChart,
        GetPollData: GetPollData,
        InitializeChart: InitializeChart,
        GetPreparedData: GetPreparedData
    }

})();

