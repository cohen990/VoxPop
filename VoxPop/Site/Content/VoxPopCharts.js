var VoxPopCharts = (function () {
    var EmptyChart = [
        {
            value: 1,
            color: "#ddd",
            highlight: "#bbb",
            label: "Be the first to vote on this Story"

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

        var myPieChart = new Chart(context).Pie(data, {
            //responsive: true,
            animationEasing: "easeOutQuart",
            animateScale: true,
            segmentShowStroke: true,
            segmentStrokeColor: "#060606",
            segmentStrokeWidth: 5,
            legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><div class=\"comm-how\"><%=segments[i].value%>%</div><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>"
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

//                       //

    var helpers = Chart.helpers;
    var legendHolder = document.getElementById('my-doughnut-legend')
    legendHolder.innerHTML = myPieChart.generateLegend();
    // Include a html legend template after the module doughnut itself
    helpers.each(legendHolder.firstChild.childNodes, function(legendNode, index){
        helpers.addEvent(legendNode, 'mouseover', function(){
            var activeSegment = myPieChart.segments[index];
            activeSegment.save();
            activeSegment.fillColor = activeSegment.highlightColor;
            myPieChart.showTooltip([activeSegment]);
            activeSegment.restore();
        });
    });
    helpers.addEvent(legendHolder.firstChild, 'mouseout', function(){
        myPieChart.draw();
    });
    canvas.parentNode.parentNode.appendChild(legendHolder.firstChild);

    myPieChart.generateLegend();
    document.getElementById('my-doughnut-legend').innerHTML = myPieChart.generateLegend();

//                           //

})();