const openPopUp = document.getElementById("open_pop_up");
const closePopUp = document.getElementById("pop_up_close");
const popUp = document.getElementById("pop_up");

openPopUp.addEventListener("click", function (e) {
  e.preventDefault();
  popUp.classList.add("active");
});

closePopUp.addEventListener("click", () => {
  popUp.classList.remove("active");
});

const form = document.querySelector(".js-form");
const submitBtn = form.querySelector("button");

const nameInput = form.querySelector('input[type="text"][placeholder="Ім\'я"]');
const surnameInput = form.querySelector(
  'input[type="text"][placeholder="Прізвище"]'
);
const phoneInput = form.querySelector(
  'input[type="tel"][placeholder="Телефон"]'
);
const emailInput = form.querySelector(
  'input[type="email"][placeholder="Емейл"]'
);
const passwordInput = form.querySelector(
  'input[type="password"][placeholder="Пароль"]'
);

form.addEventListener("submit", function (e) {
  e.preventDefault();

  if (
    nameInput.value === "" ||
    surnameInput.value === "" ||
    phoneInput.value === "" ||
    emailInput.value === "" ||
    passwordInput.value === ""
  ) {
    alert("Будь ласка, заповніть усі поля форми!");
    return;
  }

  if (!/^\d+$/.test(phoneInput.value)) {
    alert("Будь ласка, введіть лише цифри у полі телефон!");
    return;
  }

  if (
    !/^(?=.*[A-Z])[a-zA-Z0-9~`!@#$%^&*()_+={[}\]|:;"'<,>.?/]+$/.test(
      passwordInput.value
    )
  ) {
    alert(
      "Пароль повинен містити хоча б одну велику латинську літеру і складатися лише з латинських та додаткових символів!"
    );
    return;
  }

  form.submit();
});

phoneInput.addEventListener("input", function () {
  this.value = this.value.replace(/[^0-9]/g, "");
});

passwordInput.addEventListener("input", function () {
  this.value = this.value.toUpperCase();
});
