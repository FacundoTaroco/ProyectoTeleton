//Maneja la logica del client-side del alta de preguntas frecuentes

document.addEventListener("DOMContentLoaded", function () {
    document.querySelector("#txtRespuesta").disabled = true;
    document.querySelector("#divCategoriaExistente").style.display = "block";
    document.querySelector("#divCategoriaNueva").style.display = "none";

    document.querySelector("#txtNuevaCat").value = "";

document.querySelector("#checkboxNuevaCategoria").addEventListener("change", cambioCheckbox);

    document.querySelector("#selectCategoria").addEventListener("change", actualizarRespuesta);
    document.querySelector("#selectCategoria").addEventListener("change", actualizarDescripcionCategoria);

});






function cambioCheckbox() {
    document.querySelector("#txtRespuesta").value = "";
    let quiereCrearCategoria = document.querySelector("#checkboxNuevaCategoria").checked;


    if (quiereCrearCategoria) {

        document.querySelector("#divCategoriaExistente").style.display = "none";
        document.querySelector("#divCategoriaNueva").style.display = "block";

        document.querySelector("#selectCategoria").value = "noSeleccionado";


        document.querySelector("#txtRespuesta").disabled = false;




    } else {

        document.querySelector("#divCategoriaExistente").style.display = "block";
        document.querySelector("#divCategoriaNueva").style.display = "none";

        document.querySelector("#txtNuevaCat").value = "";


        document.querySelector("#txtRespuesta").disabled = true;

    }




}

function actualizarRespuesta() {
    let select = document.querySelector("#selectCategoria");
    let opcionSeleccionada = select.options[select.selectedIndex]
    let texto = opcionSeleccionada.getAttribute('categoria-respuesta')

    document.querySelector("#txtRespuesta").value = texto;
}

function actualizarDescripcionCategoria() {
    let select = document.querySelector("#selectCategoria");
    let opcionSeleccionada = select.options[select.selectedIndex]
    let texto = opcionSeleccionada.getAttribute('categoria-descripcion')

    document.querySelector("#descripcionCategoria").innerHTML = texto;
}