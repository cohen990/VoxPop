var EmptyChart = [
    {
        value: 1,
        color: "#d6d6c2",
        highlight: "#ddd",
        label: "Be the first to vote on this Story"

    }];

//Segment colours
//r/b/y/g/p/o/r/b/y/g/p/o/r(repeated)
var colors = ["#FFd1d1", "#bbdeff", "#fffd9f", "#7fff80", "#d8b2d8", "#ffc04c", "#ff1919", "#3abed7", "#ffff32", "#32ff33", "#993299", "#ffae19", "#FFd1d1"]
var highlighters = ["#FF7C8B", "#9fd1ff", "#FFFB53", "#5AFF5B", "#bf7fbf", "#ffb732", "#ff0000", "#22b6d2", "#ffff00", "#00ff01", "#993299", "#FFA500", "#FF7C8B"]
var colornumber = 0;

//Resets colours to start after each chart
function initializeChart() {
    if (colornumber >= numPoll) {
        colornumber = 0;
    }
    else  {}
}
//

//Possible way to implement %'s for shart legends
var currentOption = 0;
var perOption = [];

function totalVotes() {
    if (currentOption >= numPoll) {
        perOption = [];
        currentOption = 0;
        perOption[currentOption] = votesPerOption;
    }
    else {
        perOption[currentOption] = votesPerOption;

    }
}
//

function GetPollData(optionName, votes) {
    var result = {
        value: votes,
        color: colors[colornumber],
        highlight: highlighters[colornumber],
        label: optionName
    };
    //totalVotes();
    //currentOption++;
    colornumber++;
    initializeChart();


    return result;
}


function GenerateChart(identifier, data) {

    noVotes = true;

    for (var i = 0; i < data.length; i++) {
        data[i].label = DecodeHtml(data[i].label);

        if (data[i].value !== 0) {
            noVotes = false;
        }
    }

    var context = document.getElementById(identifier).getContext("2d");


    if (noVotes === true) {
        data = EmptyChart;
    }

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





