document.addEventListener('DOMContentLoaded', () => {
  const form = document.querySelector('#adminForm');
  const passwordInput = document.querySelector('#password');
  const modalMessage = document.getElementById('modalMessage');
  const passwordModal = new bootstrap.Modal(document.getElementById('passwordModal'));

  const correctPassword = "1234"; // Cambia esta por tu contraseña real

  form.addEventListener('submit', (e) => {
    e.preventDefault();
    const enteredPassword = passwordInput.value;

    if(enteredPassword === correctPassword){
      modalMessage.textContent = "Contraseña correcta, generando Excel...";
    } else {
      modalMessage.textContent = "Contraseña incorrecta, vuelva a intentar.";
    }

    passwordModal.show();
    passwordInput.value = ""; // limpia el input
  });
});
