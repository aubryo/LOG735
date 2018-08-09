$(document).ready(function () {
    $('#calendar').fullCalendar({
        defaultView: 'agendaWeek',
        header: false,
        allDaySlot: false,
        minTime: "08:00:00",
        maxTime: "22:00:00",
      contentHeight: 'auto',   
        editable: false
    });


}); 

