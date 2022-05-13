google.charts.load('current', {packages: ['corechart']});
google.charts.setOnLoadCallback(refreshStatisticsGraph);

function refreshStatisticsGraph() {
    $.ajax({
        type: 'GET',
        url: 'https://localhost:7197/ReportCard/getCandlestickChartData',
        dataType: 'json'
    }).success(function(candlestickChartDataDTOList) {
        console.log(candlestickChartDataDTOList);
        renderGraph('chart', candlestickChartDataDTOList);
    });
}

function renderGraph(elementID, candlestickChartDataDTOList) {
    var chartData = [];

    function assembleDataHeader() {
        var headerArray = [];
        headerArray.push('Class', 'Min', '1st', '3rd', 'Max');
        return headerArray;
    }

    chartData.push(assembleDataHeader());

    candlestickChartDataDTOList.map(function (dataNode) {
        var dataArray = [];
        dataArray.push(dataNode['title']);
        dataArray.push(dataNode['min']);
        dataArray.push(dataNode['quartile1']);
        dataArray.push(dataNode['quartile3']);
        dataArray.push(dataNode['max']);
        chartData.push(dataArray);
    });

    var data = google.visualization.arrayToDataTable(chartData);

    var options = {
        title : 'Test Scores by Class',
        legend: 'none'
    };

    var chart = new google.visualization.CandlestickChart(document.getElementById(elementID));
    chart.draw(data, options);
}