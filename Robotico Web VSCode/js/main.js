document.addEventListener("DOMContentLoaded", () => {

  // ===== Secciones por grado =====
  const seccionesPorGrado = {
    "setimo": [1,2,3,4,5,6,7,8,9,10,11],
    "octavo": [1,2,3,4,5,6,7,8,9],
    "noveno": [1,2,3,4,5,6,7,8,9],
    "decimo": [1,2,3,4,5,6],
    "undecimo": [1,2,3,4,5]
  };

  const gradoSelect = document.getElementById('grado');
  const seccionSelect = document.getElementById('seccion');
  const inscripcionForm = document.getElementById("inscripcionForm");
  const consentModal = new bootstrap.Modal(document.getElementById("consentModal"));
  const confirmConsentBtn = document.getElementById("confirmConsent");
  const consentSelect = document.getElementById("consent");

  // ===== Función para normalizar texto =====
  function normalizarTexto(texto) {
    return texto.toLowerCase().normalize("NFD").replace(/[\u0300-\u036f]/g, "");
  }

  // ===== Actualizar secciones al cambiar de grado =====
  gradoSelect.addEventListener('change', () => {
    const gradoSeleccionado = normalizarTexto(gradoSelect.value);

    // Limpiar opciones actuales
    seccionSelect.innerHTML = '<option value="">Seleccione sección</option>';

    // Agregar solo las secciones disponibles para el grado seleccionado
    if(seccionesPorGrado[gradoSeleccionado]) {
      seccionesPorGrado[gradoSeleccionado].forEach(num => {
        const opcion = document.createElement('option');
        opcion.value = num;
        opcion.textContent = num;
        seccionSelect.appendChild(opcion);
      });
    }
  });

  // ===== Enviar formulario de inscripción =====
  inscripcionForm.addEventListener("submit", function(e) {
    e.preventDefault(); // evitar envío real
    consentModal.show(); // mostrar modal de consentimiento
  });

  // ===== Confirmar consentimiento =====
  confirmConsentBtn.addEventListener("click", () => {
    const valor = consentSelect.value;

    if(valor === "si") {
      consentModal.hide(); // cerramos el modal
      alert("Gracias, su inscripción y consentimiento han sido registrados.");
      // Después de aceptar el alert, redirigimos
      window.location.href = "index.html";
    } else if(valor === "no") {
      alert("No se puede completar la inscripción sin el consentimiento.");
    } else {
      alert("Por favor seleccione Sí o No para continuar.");
    }
  });

});