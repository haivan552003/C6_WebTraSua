var nav = document.getElementById("nav");

window.addEventListener("scroll", function (event) {
  event.preventDefault();

  if (window.scrollY <= 40 || window.scrollY <= 40) {
    nav.style.padding = "20px 0 20px 0";
    nav.style.transition = "all .5s ease-out"
  } else {
    nav.style.padding = "0";
    nav.style.transition = "all .5s ease-out"
  }
});
