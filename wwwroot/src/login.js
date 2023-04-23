const openPopUp2 = document.getElementById("open_pop_up_2");
const popUp2 = document.getElementById("pop_up_2");
const popUpBody2 = document.getElementById("pop_up_body_2");
const popUpClose2 = document.getElementById("pop_up_close_2");

openPopUp2.addEventListener("click", () => {
  popUp2.classList.add("active");
});

popUpClose2.addEventListener("click", () => {
  popUp2.classList.remove("active");
});
popUp2.addEventListener("click", (e) => {
  if (e.target === popUp2) {
    popUp2.classList.remove("active");
  }
});
