const adressEl = document.querySelector("#inputAdress");
const areaEl = document.querySelector("#inputArea");
const roomsEl = document.querySelector("#inputRooms");
const priceEl = document.querySelector("#inputPrice");
const notesEl = document.querySelector("#inputNotes");

const isRequired = (value) => (value === "" ? false : true);
const isBetween = (length, min, max) =>
  length < min || length > max ? false : true;

const showError = (input, message) => {
  const formField = input.parentElement;
  formField.classList.remove("success");
  formField.classList.add("error");
  const error = formField.querySelector("small");
  error.textContent = message;
};

const showSuccess = (input) => {
  const formField = input.parentElement;
  formField.classList.remove("error");
  formField.classList.add("success");
  const error = formField.querySelector("small");
  error.textContent = "";
};

const checkAdress = () => {
  let valid = false;
  const min = 5,
    max = 100;
  const adress = adressEl.value.trim();
  if (!isRequired(adress)) {
    showError(adressEl, "Поле Адреса не може бути пустим.");
  } else if (!isBetween(adress.length, min, max)) {
    showError(adressEl, `Поле Адреса має містити від ${min} до ${max} знаків.`);
  } else {
    showSuccess(adressEl);
    valid = true;
  }
  return valid;
};

const checkArea = () => {
  let valid = false;
  const area = areaEl.value.trim();
  if (!isRequired(area)) {
    showError(areaEl, "Поле Площа не може бути пустим.");
  } else {
    showSuccess(areaEl);
    valid = true;
  }
  return valid;
};

const checkRooms = () => {
  let valid = false;
  const rooms = roomsEl.value.trim();
  if (!isRequired(rooms)) {
    showError(roomsEl, "Поле Кількість кімнат не може бути пустим.");
  } else {
    showSuccess(roomsEl);
    valid = true;
  }
  return valid;
};

const checkPrice = () => {
  let valid = false;
  const price = priceEl.value.trim();
  if (!isRequired(price)) {
    showError(priceEl, "Поле Ціна не може бути пустим.");
  } else {
    showSuccess(priceEl);
    valid = true;
  }
  return valid;
};

function sendJSON(data) {
  let xhr = new XMLHttpRequest();
  let url = "/api/Estate/add";

  xhr.open("POST", url, true);
  xhr.setRequestHeader("Content-Type", "application/json");

  xhr.send(data);
}

function handleFormSubmit() {
  const data = new FormData(event.target);

  const formJSON = Object.fromEntries(data.entries());

  const results = JSON.stringify(formJSON, null, 2);

  sendJSON(results);
}

const form = document.querySelector(".new-object-form");
form.addEventListener("submit", function (e) {
  e.preventDefault();
  let isAdressValid = checkAdress(),
    isAreaValid = checkArea(),
    isRoomsValid = checkRooms(),
    isPriceValid = checkPrice();
  let isFormValid =
    isAdressValid && isAreaValid && isRoomsValid && isPriceValid;
  if (isFormValid) {
    handleFormSubmit();
  }
});
