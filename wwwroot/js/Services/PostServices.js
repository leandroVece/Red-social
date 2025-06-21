import { API_BASE } from "./Auth.js";


export async function crearPost(body) {
    try {
        const res = await fetch(`${API_BASE}/post`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(body),
        });

        const data = await res.text();
        if (!res.ok) {
            throw new Error(data);
        }

        return true;
    } catch (err) {
        console.error("Error al crear post:", err.message);
        return false;
    }
}

export async function eliminarPost(idPost) {
    try {
        const res = await fetch(`${API_BASE}/post/${idPost}`, {
            method: "DELETE"
        });

        if (!res.ok) throw new Error("No se pudo eliminar el post");
        return true;
    } catch (err) {
        console.error("Error al eliminar post:", err.message);
        return false;
    }
}

export async function obtenerPosts() {
    try {
        const res = await fetch(`${API_BASE}/post`);
        if (!res.ok) throw new Error("Error al obtener los posts");

        const data = await res.json();
        return data;
    } catch (err) {
        console.error("Error al obtener posts:", err.message);
        return [];
    }
}

