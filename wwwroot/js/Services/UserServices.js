import { API_BASE } from "./Auth.js";


export async function loginOrCreateUser(data) {
    try {
        const response = await fetch(`${API_BASE}/user/login`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        });

        // const result = await response.text();
        if (response.ok) {
            const result = await response.json();
            // alert("Entraste a la mejor red social");
            return result;
        } else {
            alert('Error al iniciar sesión o crear usuario');
        }
    } catch (error) {
        console.error('Error:', error);
        alert('No se pudo conectar con el servidor');
    }
}

export async function logoutUser(nombre) {
    try {
        const response = await fetch(`${API_BASE}/user/logout`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ nombre })
        });

        const result = await response.text();
        if (response.ok) {
            alert(result);
        } else {
            alert('Error al cerrar sesión');
        }
    } catch (error) {
        console.error('Error:', error);
        alert('No se pudo conectar con el servidor');
    }
}
