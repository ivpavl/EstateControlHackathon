document.querySelector("form").addEventListener("submit", function (event) {
  event.preventDefault();

  var cardNumber = document.querySelector(".card-number-input").value.trim();
  var cardHolder = document.querySelector(".card-holder-input").value.trim();
  var month = document.querySelector(".month-input").value;
  var year = document.querySelector(".year-input").value;
  var cvv = document.querySelector(".cvv-input").value.trim();

  var cardNumberRegex = /^[0-9]{16}$/;
  if (!cardNumberRegex.test(cardNumber)) {
    alert("Будь ласка, введіть правильний номер картки");
    return false;
  }

  var cardHolderRegex = /^[а-ЯА-Я ]{1,}$/;
  if (!cardHolderRegex.test(cardHolder)) {
    alert("Будь ласка, введіть правильне ім'я власника картки");
    return false;
  }

  var date = new Date();
  var currentYear = date.getFullYear();
  var currentMonth = date.getMonth() + 1;
  if (year < currentYear || (year == currentYear && month < currentMonth)) {
    alert("Будь ласка, введіть правильну дату закінчення терміну дії картки");
    return false;
  }

  var cvvRegex = /^[0-9]{3,4}$/;
  if (!cvvRegex.test(cvv)) {
    alert("Будь ласка, введіть правильний CVV-код");
    return false;
  }
});

const form = document.querySelector("form");
const cardNumberInput = document.querySelector(".card-number-input");
const cardHolderInput = document.querySelector(".card-holder-input");
const monthInput = document.querySelector(".month-input");
const yearInput = document.querySelector(".year-input");
const cvvInput = document.querySelector(".cvv-input");

form.addEventListener("submit", async (event) => {
  event.preventDefault();

  if (
    !cardNumberInput.checkValidity() ||
    !cardHolderInput.checkValidity() ||
    !monthInput.checkValidity() ||
    !yearInput.checkValidity() ||
    !cvvInput.checkValidity()
  ) {
    alert("Будь ласка, заповніть форму коректно");
    return;
  }

  const data = {
    cardNumber: cardNumberInput.value,
    cardHolderName: cardHolderInput.value,
    expiration: {
      month: monthInput.value,
      year: yearInput.value,
    },
    cvv: cvvInput.value,
  };

  const response = await fetch("/api/payment", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    const errorMessage = await response.text();
    alert(`Помилка: ${errorMessage}`);
    return;
  }

  alert("Дані успішно відправлено!");
  form.reset();
});
