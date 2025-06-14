    CREATE DATABASE IF NOT EXISTS social;
    USE social;

    -- Tabla de usuarios
    CREATE TABLE usuarios (
        id_user INT AUTO_INCREMENT PRIMARY KEY,
        nombre VARCHAR(100) NOT NULL,
        profesion VARCHAR(100),
        password VARCHAR(100) NOT NULL,
        img VARCHAR(255), -- Ruta o URL de imagen
        activo BOOLEAN DEFAULT FALSE
    );

    -- Tabla de posts
    CREATE TABLE posts (
        id_post INT AUTO_INCREMENT PRIMARY KEY,
        id_user INT NOT NULL,
        contenido TEXT NOT NULL,
        fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
        FOREIGN KEY (id_user) REFERENCES usuarios(id_user)
    );

    -- Tabla de comentarios
    CREATE TABLE comentarios (
        id_comentario INT AUTO_INCREMENT PRIMARY KEY,
        id_post INT NOT NULL,
        id_user INT NOT NULL,
        contenido TEXT NOT NULL,
        fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
        FOREIGN KEY (id_post) REFERENCES posts(id_post),
        FOREIGN KEY (id_user) REFERENCES usuarios(id_user)
    );

datos de prueva

    INSERT INTO usuarios (nombre, profesion, password, img) VALUES
     ('Juan Pérez', 'Desarrollador Web', '1234Juan', 'avart-m.png'),
     ('Ana Gómez', 'Analista de Datos', '1234Ana', 'avart-f.png'),
     ('Carlos Ruiz', 'Diseñador Grafico', '1234Carlos', 'avart-m.png'),
     ('Diego Torres', 'El diego', '1234Diego', 'avart-m.png');# Red-social
