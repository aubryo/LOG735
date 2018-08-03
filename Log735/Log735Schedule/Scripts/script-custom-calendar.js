$(document).ready(function () {
    $('#calendar').fullCalendar({
        defaultView: 'agendaWeek',
        header: false,
        //views: {
        //    agendaWeek: {
              
        //        minTime: "08:00:00",
        //        maxTime: "22:00:00",
        //    }
        //},
          allDaySlot: false,
        minTime: "08:00:00",
        maxTime: "22:00:00",
      contentHeight: 'auto',
        events: function (start, end, timezone, callback) {
            $.ajax({
                url: '/Home/GetCalendarData',
                type: "GET",
                dataType: "JSON",

                success: function (result) {
                    var events = [];

                    $.each(result, function (i, data) {
                        events.push(
                            {
                                title: data.Title,
                                description: data.Desc,
                                start: moment(data.Start_Date).format('YYYY-MM-DD'),
                                end: moment(data.End_Date).format('YYYY-MM-DD'),
                                backgroundColor: "#9501fc",
                                borderColor: "#fc0101"
                            });
                    });

                    callback(events);
                }
            });
        },

        //eventRender: function (event, element) {
        //    element.qtip(
        //        {
        //            content: event.description
        //        });
        //},

        editable: false
    });


}); 

