
// CONFIRMACIONES

function confirmarAccion(mensaje) {
    return confirm(mensaje);
}

function confirmarEliminacion() {
    return confirm("¿Seguro que deseas eliminar este usuario?");
}

function confirmarVolver() {
    return confirm("¿Deseas regresar a la lista?");
}

// MENSAJES

function mostrarMensaje(texto) {
    alert(texto);
}

// ANIMACIÓN DE CARDS

document.addEventListener("DOMContentLoaded", () => {

    const cards = document.querySelectorAll(".card");

    cards.forEach((card, i) => {
        card.style.opacity = 0;
        card.style.transform = "translateY(20px)";

        setTimeout(() => {
            card.style.transition = "all 0.5s ease";
            card.style.opacity = 1;
            card.style.transform = "translateY(0)";
        }, i * 100);
    });


    //  VALIDACIÓN EMPLEADO

    const form = document.getElementById("formEmpleado");

    if (form) {
        form.addEventListener("submit", function (e) {

            const nombre = document.getElementById("nombre").value.trim();
            const correo = document.getElementById("correo").value.trim();
            const password = document.getElementById("password").value.trim();

            if (nombre === "" || correo === "" || password === "") {
                alert("⚠️ Todos los campos son obligatorios");
                e.preventDefault();
                return;
            }

            if (!correo.includes("@")) {
                alert("⚠️ Correo inválido");
                e.preventDefault();
                return;
            }

            if (password.length < 4) {
                alert("⚠️ La contraseña debe tener al menos 4 caracteres");
                e.preventDefault();
                return;
            }

            return confirm("¿Crear nuevo empleado?");
        });
    }
});

//  PRODUCTOS


function confirmarEliminarProducto() {
    return confirm("¿Eliminar producto?");
}