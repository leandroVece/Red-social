export const obtenerDeLocalStorage = (clave) => {
    const datos = localStorage.getItem(clave);
    return datos ? JSON.parse(datos) : null;
};

export const guardarEnLocalStorage = (clave, valor) => {
    localStorage.setItem(clave, JSON.stringify(valor));
};

export const API_BASE = "http://192.168.x.x/api";
