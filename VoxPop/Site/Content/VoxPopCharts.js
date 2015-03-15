var VoxPopCharts = (function (){
    var EmptyChart = [
        {
            value: 1,
            color: "#d6d6c2",
            highlight: "#ddd",
            label: "Be the first to vote on this Story"

        }
    ];

    //Segment colours
    //red/blue/yellow/green/purple/orange/red/blue/yellow/green/purple/orange/red/red/red/red etc... (repeating red after 12 colours)
    var colourList = [
        { colour: "#FFd1d1", highlight: "#FF7C8B" },
        { colour: "#bbdeff", highlight: "#9fd1ff" },
        { colour: "#fffd9f", highlight: "#FFFB53" },
        { colour: "#7fff80", highlight: "#5AFF5B" },
        { colour: "#d8b2d8", highlight: "#bf7fbf" },
        { colour: "#ffc04c", highlight: "#ffb732" },
        { colour: "#ff1919", highlight: "#ff0000" },
        { colour: "#3abed7", highlight: "#22b6d2" },
        { colour: "#ffff32", highlight: "#ffff00" },
        { colour: "#32ff33", highlight: "#00ff01" },
        { colour: "#993299", highlight: "#993299" },
        { colour: "#ffae19", highlight: "#FFA500" },
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

    var GetPreparedData = function(data) {

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

        var myPieChart = new Chart(context).Pie(data, {
            animationEasing: "easeOutQuart",
            animateScale: true,
            //segmentShowStroke : true,
            //segmentStrokeColor: "#040404",
            //segmentStrokeWidth : 4
        });

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