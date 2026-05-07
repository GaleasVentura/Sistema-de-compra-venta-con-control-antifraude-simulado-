document.addEventListener("DOMContentLoaded", () => {

    const form = document.getElementById("loginForm");

    form.addEventListener("submit", function (e) {

        const correo = document.getElementById("correo").value.trim();
        const password = document.getElementById("password").value.trim();

        if (correo === "" || password === "") {
            alert("⚠️ Completa todos los campos");
            e.preventDefault();
            return;
        }

        if (!correo.includes("@")) {
            alert("⚠️ Correo inválido");
            e.preventDefault();
            return;
        }

    });

});