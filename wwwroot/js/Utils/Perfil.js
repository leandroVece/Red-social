const NombreH2 = document.getElementById("nombre-user");
const SubNombre = document.getElementById("Subname");

export const EditarPerfil = (data) => {
    if (data != null) {
        NombreH2.innerText = data.nombre;
        SubNombre.innerHTML = data.nombre;
    }
}