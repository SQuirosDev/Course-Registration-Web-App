const hero = document.getElementById("hero");

hero.addEventListener("mousemove", (e) => {
  const rect = hero.getBoundingClientRect();
  const x = ((e.clientX - rect.left) / rect.width) * 100;
  const y = ((e.clientY - rect.top) / rect.height) * 100;

  // Fondo dinámico con degradado que sigue el mouse
  hero.style.background = `
    radial-gradient(circle at ${x}% ${y}%,
      #9de6fdff 0%,   /* verde LEGO */
      #00B8D9 70%,  /* azul LEGO */
      #1E1E1E 100%  /* casi negro */
    )
  `;
});
