import { obtenerPosts, eliminarPost, crearPost } from "./Services/PostServices.js";
import { createPostComponent } from "./Component/Post.js";
import { obtenerDeLocalStorage, guardarEnLocalStorage } from "./Services/Auth.js";
import { loginOrCreateUser, logoutUser } from "./Services/UserServices.js";
import { EditarPerfil } from "./Utils/Perfil.js";
import { formPost, SubirPost } from "./Utils/Helpers.js";

// Agregar eventos a los botones
const btnCerraModal = document.getElementById("cerrarModal");
// Agregar botón si no hay usuario en el localStorage
const usuario = obtenerDeLocalStorage("userLogin");
const loginContainer = document.getElementById("login");
const formLogin = document.getElementById("form-login");

// const postForm = document.getElementById("form-post");


const res = await obtenerPosts();
// console.log(res);
createPostComponent(res);


// Función para abrir el modal
const abrirModal = () => {
    document.getElementById("modal").style.display = "block";
};


const cerrarModal = () => {
    document.getElementById("modal").style.display = "none";
};

btnCerraModal.addEventListener("click", cerrarModal);

if (!usuario) {
    const botonLogin = document.createElement("button");
    botonLogin.innerText = "Iniciar sesión";
    botonLogin.classList.add("btn");
    botonLogin.onclick = abrirModal;
    loginContainer.appendChild(botonLogin);

} else {
    EditarPerfil(usuario)
}

formLogin.addEventListener("submit", async (event) => {
    event.preventDefault(); // Evita que el formulario se envíe de manera tradicional

    const formData = new FormData(formLogin);
    const userData = {
        nombre: formData.get("nombre"),
        password: formData.get("password")
    };
    var res = await loginOrCreateUser(userData);
    console.log(res)
    guardarEnLocalStorage("userLogin", res);
    cerrarModal();
    document.location.reload();
});


formPost.addEventListener("submit", async (e) => {
    e.preventDefault();
    await SubirPost(usuario.id, usuario.nombre);

})