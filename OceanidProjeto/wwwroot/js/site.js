
function toggleTheme() {
    const html = document.documentElement;
    const current = html.getAttribute("data-bs-theme") || "light";
    const icon = document.getElementById("themeToggle");
    const image = document.getElementById("themeImage");
    const imgmaquiagem = document.getElementById("themeImgmaquiagem");
    const imgcabelo = document.getElementById("themeImgcabelo");
    const imgperfume = document.getElementById("themeImgperfume");

    if (current === "dark") {
        html.setAttribute("data-bs-theme", "light");
        localStorage.setItem("theme", "light");
        if (icon) icon.className = "bi bi-moon";
        if (image) image.src = "/img/skincare_light.png";
        if (imgmaquiagem) imgmaquiagem.src = "/img/maquiagem_light.png";
        if (imgcabelo) imgcabelo.src = "/img/cabelo_light.png";
        if (imgperfume) imgperfume.src = "/img/perfume_light.png";
    } else {
        html.setAttribute("data-bs-theme", "dark");
        localStorage.setItem("theme", "dark");
        if (icon) icon.className = "bi bi-sun";
        if (image) image.src = "/img/skincare_dark.png";
        if (imgmaquiagem) imgmaquiagem.src = "/img/maquiagem_dark.png";
        if (imgcabelo) imgcabelo.src = "/img/cabelo_dark.png";
        if (imgperfume) imgperfume.src = "/img/perfume_dark.png";
    }
}

function applySavedTheme() {
    const savedTheme = localStorage.getItem("theme") || "light";
    const html = document.documentElement;
    html.setAttribute("data-bs-theme", savedTheme);

    const icon = document.getElementById("themeToggle");
    const image = document.getElementById("themeImage");
    const imgmaquiagem = document.getElementById("themeImgmaquiagem");
    const imgcabelo = document.getElementById("themeImgcabelo");
    const imgperfume = document.getElementById("themeImgperfume");

    if (savedTheme === "dark") {
        if (icon) icon.className = "bi bi-sun";
        if (image) image.src = "/img/skincare_dark.png";
        if (imgmaquiagem) imgmaquiagem.src = "/img/maquiagem_dark.png";
        if (imgcabelo) imgcabelo.src = "/img/cabelo_dark.png";
        if (imgperfume) imgperfume.src = "/img/perfume_dark.png";
    } else {
        if (icon) icon.className = "bi bi-moon";
        if (image) image.src = "/img/skincare_light.png";
        if (imgmaquiagem) imgmaquiagem.src = "/img/maquiagem_light.png";
        if (imgcabelo) imgcabelo.src = "/img/cabelo_light.png";
        if (imgperfume) imgperfume.src = "/img/perfume_light.png";
    }
}

document.addEventListener("DOMContentLoaded", applySavedTheme);



var carrossel = document.querySelector('#carouselExample');
var instanciaCarrossel = new bootstrap.Carousel(carrossel, {
    interval: 4000,
    ride: 'carousel'
});

function menuLogin() {
    const menu = document.getElementById("menu-login");
    menu.classList.toggle("active");
}

function abrirCadastro() {
    document.getElementById("menu-cad").classList.add("show");
    document.getElementById("tudo-cad").style.display = "block";
    document.getElemntById("menu-login").style.display = 'none';
}

function fecharCadastro() {
    document.getElementById("menu-cad").classList.remove("show");
    document.getElementById("overlay-cad").style.display = "none";
}

const popup = document.getElementById('popupLogin');
popup.style.display = 'block';

setTimeout(() => {
    popup.style.display = 'none';
}, 5000); 


const popupCad = document.getElementById('popupCad');
popupCad.style.display = 'block'; 
setTimeout(() => {
    popupCadastro.style.display = 'none'; 
}, 5000);

function abrirSacola() {
    document.getElementById('menu-sac').classList.add('aberta');
    document.getElementById('sacola-overlay').style.display = 'block';
}

function fecharSacola() {
    document.getElementById('menu-sac').classList.remove('aberta');
    document.getElementById('sacola-overlay').style.display = 'none';
}


function toggleFavorito(event, element) {
    event.preventDefault(); 

    var img = element.querySelector('img');

    var iconeNormal = "/img/favs-icon.png";
    var iconePreenchido = "/img/favsP-icon.png";

    if (img.src.includes("favsP-icon.png")) {
        img.src = iconeNormal;
    } else {
        img.src = iconePreenchido;
    }
}


function toggleSenha() {
    var input = document.getElementById("senhaCliente");
    if (input.type === "password") {
        input.type = "text";
    } else {
        input.type = "password";
    }
}

function toggleSenhaLog() {
    var input = document.getElementById("senhaCliente2");
    if (input.type === "password") {
        input.type = "text";
    } else {
        input.type = "password";
    }
}



function toggleMobileMenu() {
    var menu = document.getElementById("mobileMenu");
    var body = document.body;

    if (menu.classList.contains("show")) {
        menu.classList.remove("show");
        body.style.overflow = "auto";
    } else {
        menu.classList.add("show");
        body.style.overflow = "hidden";
    }
}

function abrirMenuLogin(event) {
    event.preventDefault();
    toggleMobileMenu();     // fecha o menu hambúrguer
    menuLogin();            // exibe o menu de login
}
