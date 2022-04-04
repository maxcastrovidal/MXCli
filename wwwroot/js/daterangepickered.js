$(function () {
    $('.daterangepickered').daterangepicker({
        "autoApply": true,
        "autoUpdateInput": false,
        "ranges": {
            "Hoy": [moment(), moment()],
            "Ayer": [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            "Últimos 7 dias": [moment().subtract(6, 'days'), moment()],
            "Últimos 30 dias": [moment().subtract(29, 'days'), moment()],
            "Este mes": [moment().startOf('month'), moment().endOf('month')],
            "Mes pasado": [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        },
        "locale": {
            "format": "DD-MM-YYYY",
            "separator": " - ",
            "applyLabel": "Apply",
            "cancelLabel": "Cancel",
            "fromLabel": "From",
            "toLabel": "To",
            "customRangeLabel": "Custom",
            "weekLabel": "W",
            "daysOfWeek": ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"],
            "monthNames": ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
            "firstDay": 1
        },
        "showCustomRangeLabel": false,
        "alwaysShowCalendars": true,
        "parentEl": "div1",
        "opens": "left"
    }, function (start, end, label) {
        console.log('New date range selected: ' + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD') + ' (predefined range: ' + label + ')');
    });


    $('.daterangepickered').on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format('DD-MM-YYYY') + ' - ' + picker.endDate.format('DD-MM-YYYY'));
    });

    $('.daterangepickered').on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
    });

});
