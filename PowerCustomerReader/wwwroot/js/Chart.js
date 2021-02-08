var ctx = document.getElementById('myChart').getContext('2d');
var chart = new Chart(ctx, {
    // The type of chart we want to create
    type: 'doughnut',

    // The data for our dataset
    data: {
        labels: ['ENK', 'ANDRE', 'AS 0-4 ansatte', 'AS 5-10 ansatte', 'AS >10 ansatte'],
        datasets: [{
            label: 'Fordelingen av organisasjonsform/antall ansatte',
            backgroundColor: ["#FFCA3E", "#FF6F50", "#D03454", "#9C2162", "#772F67"],
            data: [@Model.organisationStats.ENK, @Model.organisationStats.ANDRE, @Model.organisationStats.AS_ZeroToFour, @Model.organisationStats.AS_FiveToTen, @Model.organisationStats.AS_OverTen]
            }]
        },

// Configuration options go here
options: { }
    });

document.getElementById('changeToBar').onclick = function () {
    chart.destroy();
    chart = new Chart(ctx, {
        type: 'bar',

        // The data for our dataset
        data: {
            labels: ['ENK', 'ANDRE', 'AS 0-4 ansatte', 'AS 5-10 ansatte', 'AS >10 ansatte'],
            datasets: [{
                label: 'Fordelingen av organisasjonsform/antall ansatte',
                backgroundColor: ["#FFCA3E", "#FF6F50", "#D03454", "#9C2162", "#772F67"],
                data: [@Model.organisationStats.ENK, @Model.organisationStats.ANDRE, @Model.organisationStats.AS_ZeroToFour, @Model.organisationStats.AS_FiveToTen, @Model.organisationStats.AS_OverTen]
}]
        },

// Configuration options go here
options: { }
        });
    };

document.getElementById('changeToSmult').onclick = function () {
    chart.destroy();
    chart = new Chart(ctx, {
        type: 'doughnut',

        // The data for our dataset
        data: {
            labels: ['ENK', 'ANDRE', 'AS 0-4 ansatte', 'AS 5-10 ansatte', 'AS >10 ansatte'],
            datasets: [{
                label: 'Fordelingen av organisasjonsform/antall ansatte',
                backgroundColor: ["#FFCA3E", "#FF6F50", "#D03454", "#9C2162", "#772F67"],
                data: [@Model.organisationStats.ENK, @Model.organisationStats.ANDRE, @Model.organisationStats.AS_ZeroToFour, @Model.organisationStats.AS_FiveToTen, @Model.organisationStats.AS_OverTen]
}]
        },

// Configuration options go here
options: { }
        });
    };