// Mensaje cuando se agrega producto
function mostrarMensaje(nombre) {
    alert(nombre + " agregado al carrito 🛒");
}

// efecto en botones agregar carrito
document.addEventListener("DOMContentLoaded", function () {

    const botones = document.querySelectorAll(".btn-agregar");

    botones.forEach(btn => {

        btn.addEventListener("click", function () {

            const nombre = this.getAttribute("data-nombre");

            // alerta
            mostrarMensaje(nombre);

            // efecto visual
            this.innerText = "Agregado ✔";
            this.classList.add("btn-primary");

            setTimeout(() => {
                this.innerText = "Agregar al Carrito";
                this.classList.remove("btn-primary");
            }, 1200);

        });

    });

});