const Home = {
    text: 'Home',
    link: 'home'
}
const Tickets = {
    text: 'Tickets',
    submenu: [
        {
            text: 'Escanear tickets',
            link: 'tickets/scan',
            roles: ["Acomodador"]
        },
        {
            text: 'Vender tickets',
            link: 'tickets/vender',
            roles: ["Vendedor"]
        },
    ]
}

const Admin = {
    text: 'Administracion',
    submenu: [
        {
            text: 'Artistas',
            link: 'administracion/artistas',
            roles: ["Administrador"]
        },
        {
            text: 'Bandas',
            link: 'administracion/bandas',
            roles: ["Administrador"]
        },
        {
            text: 'Conciertos',
            link: 'administracion/conciertos',
            roles: ["Administrador"]
        },
        {
            text: 'Importar/Exportar conciertos',
            link: 'administracion/importarexportar',
            roles: ["Administrador"]
        },
        {
            text: 'Géneros',
            link: 'administracion/generos',
            roles: ["Administrador"]
        },
        {
            text: 'Solistas',
            link: 'administracion/solistas',
            roles: ["Administrador"]
        },
        {
            text: 'Usuarios',
            link: 'administracion/usuarios',
            roles: ["Administrador"]
        },
    ]
};

const Protagonistas = {
    text: 'Protagonistas',
    submenu: [
        {
            text: 'Bandas',
            link: 'protagonistas/bandas',
        },
        {
            text: 'Solistas',
            link: 'protagonistas/solistas',
        },
    ]
}

export const menu = [
    Home,
    Tickets,
    Admin,
    Protagonistas
]