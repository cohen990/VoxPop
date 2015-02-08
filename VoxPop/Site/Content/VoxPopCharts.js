var EmptyChart = [
    {
        value: 1,
        color: "#d6d6c2",
        highlight: "#ddd",
        label: "There are no votes on this poll."
    }];

function GetPollData(optionName, votes) {
    var result = {
        value: votes,
        color: "#FFd1d1",
        highlight: "#ff6e7f",
        label: optionName
    }

    return result;
}

function GenerateChart(identifier, data) {

    noVotes = true;

    for (var i = 0; i < data.length; i++) {
        if (data[i].value !== 0) {
            noVotes = false;
        }
    }

    var context = document.getElementById(identifier).getContext("2d");

    if (noVotes === true) {
        data = EmptyChart;
    }

    var myPieChart = new Chart(context).Pie(data);
}