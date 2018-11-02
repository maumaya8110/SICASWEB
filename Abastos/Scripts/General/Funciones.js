function SetDateTimePicker(ctrl, startDate) {
    ctrl.datetimepicker({
        language: 'en',
        setStartDate: new Date(),
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0,
        useCurrent: false,
        format: 'dd/mm/yyyy'
    });
}