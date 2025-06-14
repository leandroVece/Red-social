import { comentar, eliminarComentario } from "../Services/ComentarioServices.js";
import { eliminarPost } from "../Services/PostServices.js";
import { obtenerDeLocalStorage } from "../Services/Auth.js";

// Crea un elemento con clase y contenido opcional
export function createElement(tag, className, textContent) {
    const el = document.createElement(tag);
    if (className) el.className = className;
    if (textContent) el.textContent = textContent;
    return el;
}

// Crea el bloque de contenido del post
export function createPostContent(userName, content) {
    const postContent = createElement("div", "post-content");
    const strong = createElement("strong", null, userName + ":");
    const p = createElement("p", null, content);
    postContent.appendChild(strong);
    postContent.appendChild(p);
    return postContent;
}

// Crea una lista de comentarios
export function createComments(id, commentsArray, user) {
    console.log(commentsArray)
    const commentsContainer = createElement("div", "comments");
    commentsArray.forEach(c => {
        const comment = createElement("div", "comment");
        const strong = createElement("strong", null, c.userName + ":");
        const text = document.createTextNode(" " + c.contenido);

        if (user && user.id === c.idUser) {
            const btnEliminar = createDeleteButton(handleDeleteComentario(id, c, comment));
            comment.appendChild(btnEliminar);
        }

        comment.appendChild(strong);
        comment.appendChild(text);
        commentsContainer.appendChild(comment);
    });
    return commentsContainer;
}

// Crea el formulario para comentar
export function createCommentForm(onSubmitHandler) {
    const form = createElement("form", "comment-form");
    const input = createElement("input");
    input.type = "text";
    input.placeholder = "Escribe un comentario...";
    const button = createElement("button", null, "Comentar");
    button.type = "submit";

    form.appendChild(input);
    form.appendChild(button);

    if (onSubmitHandler) {
        form.addEventListener("submit", onSubmitHandler);
    }

    return form;
}


async function AddComentario(e, comments, idPost, user) {
    const newComment = e.target.querySelector("input").value;
    if (newComment.trim()) {
        const usuario = obtenerDeLocalStorage("userLogin");
        const fechaCreacion = new Date().toISOString();
        const comentarioData = {
            idUser: usuario.id,
            userName: usuario.nombre,
            fechaCreacion: fechaCreacion,
            contenido: newComment
        };
        const res = await comentar(idPost, comentarioData);

        //agregar si el comentario se guardo correctamente
        if (res) {
            const newCommentElement = createComments(idPost, [comentarioData], user);
            comments.appendChild(newCommentElement.firstChild);
            e.target.reset();
        } else {
            alert("Error al guardar el comentario");
        }
    }
}

function createDeleteButton(onClick) {
    const btn = createElement("button", "btn-eliminar", "X");
    btn.onclick = onClick;
    return btn;
}

function handleDeletePost(postId, postElement) {
    return async () => {
        if (confirm("¿Seguro que quieres eliminar este post?")) {
            const res = await eliminarPost(postId);
            if (res) {
                postElement.remove();
            } else {
                alert("No se pudo eliminar el post");
            }
        }
    };
}
function handleDeleteComentario(postId, commnets, CommnesHtmlElement) {
    return async () => {
        if (confirm("¿Seguro que quieres eliminar este coentario?")) {
            const res = await eliminarComentario(postId, commnets);
            if (res) {
                CommnesHtmlElement.remove();
            } else {
                alert("No se pudo eliminar el post");
            }
        }
    };
}


export function createPostComponent(data) {
    const usuario = obtenerDeLocalStorage("userLogin");
    data.forEach(postData => {
        const post = createElement("div", "post");
        const postContent = createPostContent(postData.userName, postData.contenido);
        if (usuario && usuario.id === postData.idUser) {
            const btnEliminar = createDeleteButton(handleDeletePost(postData.id, post));
            post.appendChild(btnEliminar);
        }
        const comments = createComments(postData.id, postData.comentarios, usuario);

        const commentForm = createCommentForm(async (e) => {
            e.preventDefault();
            await AddComentario(e, comments, postData.id, usuario);
        });
        post.appendChild(postContent);
        post.appendChild(comments);
        post.appendChild(commentForm);
        document.getElementById("main-content").appendChild(post);
    });
}