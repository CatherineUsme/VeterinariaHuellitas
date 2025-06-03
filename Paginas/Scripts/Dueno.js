var UrlBase = "http://localhost:44302/";

jQuery(function () {
    //Registrar los botones para responder al evento click
    $("#dvMenu").load("../Paginas/Menu.html")
    LlenarTablaDuenos();

});

function LlenarTablaDuenos() {
    let URL = UrlBase + "api/duenos/ConsultarTodos";
    LlenarTablaXServicios(URL, "#tblDuenos");

}