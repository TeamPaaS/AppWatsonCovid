var countrywiseApiSettings = {
    "url": "https://api.covid19api.com/summary",
    "method": "GET",
    "timeout": 0,
};

$.ajax(countrywiseApiSettings).done(function (response) {
    var global = response.Global;
    jQuery("#covidGlobalSummary").html("<div class='text-xs font-weight-bold text-primary text-uppercase mb-1'>Global Affected:</div><div class='h5 mb-0 font-weight-bold text-gray-800'>" + global.TotalConfirmed+"</div>");
    var countries = response.Countries;
    var indiaDetails = {};
    jQuery.each(countries, function (indx, valu) {
        if (valu.Country.toLowerCase() === "india") {
            indiaDetails = valu;
            jQuery("#covidIndiaTotal").html("<div class='text-xs font-weight-bold text-primary text-uppercase mb-1'>Total Affected in INDIA::</div><div class='h5 mb-0 font-weight-bold text-gray-800'>" + valu.TotalConfirmed + "</div>");
            jQuery("#covidIndiaNew").html("<div class='text-xs font-weight-bold text-primary text-uppercase mb-1'>New case in INDIA:</div><div class='h5 mb-0 font-weight-bold text-gray-800'>" + valu.NewConfirmed + "</div>");
            jQuery("#covidIndiaRecovered").html("<div class='text-xs font-weight-bold text-primary text-uppercase mb-1'>Total Recovered in INDIA:</div><div class='h5 mb-0 font-weight-bold text-gray-800'>" + valu.TotalRecovered + "</div>");
        }
    });
});