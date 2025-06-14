import { crearPost, eliminarPost } from "../Services/PostServices.js";
import { comentar } from "../Services/ComentarioServices.js";

export const formPost = document.getElementById("form-post");

export const SubirPost = async (id, nombre) => {
    const contenido = formPost.querySelector("textarea").value;
    const fechaCreacion = new Date().toISOString();

    const postData = {
        Id: "",
        IdUser: id,
        UserName: nombre,
        fechaCreacion: fechaCreacion,
        Contenido: contenido
    };

    var res = await crearPost(postData);

    if (res) {
        window.location.reload();
    }
};


export const crearComentario = async (idpost, id, nombre) => {
    const ComentarioData = {
        IdUser: id,
        UserName: nombre,
        fechaCreacion: fechaCreacion,
        Contenido: contenido
    };

    var res = await comentar(idpost, ComentarioData)

}
