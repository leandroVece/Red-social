export async function comentar(idPost, body) {
    try {
        const res = await fetch(`http://localhost:5047/api/post/${idPost}/comentarios`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(body),
        });

        const data = await res.text();
        if (!res.ok) throw new Error(data);

        return true;
    } catch (err) {
        console.error("Error al comentar:", err.message);
        return false;
    }
}

export async function eliminarComentario(idPost, body) {
    try {
        const res = await fetch(`http://localhost:5047/api/post/${idPost}/comentarios`, {
            method: "DELETE",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(body),
        });

        if (!res.ok) throw new Error("No se pudo eliminar el comentario");
        return true;
    } catch (err) {
        console.error("Error al eliminar comentario:", err.message);
        return false;
    }
}
