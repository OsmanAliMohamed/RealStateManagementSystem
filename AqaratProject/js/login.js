function changeSection1(){
    document.querySelector('.login').style.display = 'none'
    document.querySelector('.signup').style.display = 'block'
    document.querySelector('.form2').style.opacity = '0'
    window.setTimeout(function() {
      document.querySelector('.signup').style.left = '50%'
      document.querySelector('.login').style.left = '50%'
      document.querySelector('.form2').style.opacity = '1'
    }, 50)
  }
  function changeSection2(){
    document.querySelector('.signup').style.display = 'none'
    document.querySelector('.login').style.display = 'block'
    document.querySelector('.form1').style.opacity = '0' 
    window.setTimeout(function() {
      document.querySelector('.login').style.left = '0%'
      document.querySelector('.signup').style.left = '0%'
      document.querySelector('.form1').style.opacity = '1'
    }, 50)
  }


  $(function () {
    $("#toggle_pwd").click(function () {
        $(this).toggleClass("fa-eye fa-eye-slash");
       var type = $(this).hasClass("fa-eye-slash") ? "text" : "password";
        $("#txtPassword").attr("type", type);
    });
});


document.addEventListener("DOMContentLoaded", () => {
  const input = document.getElementById("citizenid");
  const btn = document.getElementById("button");
  const error = document.getElementById("errorMessage");
  const success = document.getElementById("successMessage");
  const mask = new IMask(input, { mask: "000-0000-00-00000" });

  input.addEventListener("keyup", event => {
    validateInput(event, input.value.replace(/-/g, ""));
  });

  input.addEventListener("keypress", event => {
    if (event.keyCode === 14) {
      event.preventDefault();
      return false; // Disable enter to submit for UX
    }
  });

  btn.addEventListener("click", event => {
    event.preventDefault();
    event.stopImmediatePropagation();
    // handle submit here
    alert("Your national ID submit value is: " + input.value.replace(/-/g, ""));
  });

  function validateInput(event, value) {
    const maxLength = 14;
    const regex = /^[0-9]\d*$/;
    const char =
      String.fromCharCode(event.keyCode) || String.fromCharCode(event.which);

    if (
      value !== undefined &&
      value.toString().length == maxLength &&
      value.match(regex) &&
      validNationalID(value)
    ) {
      btn.disabled = false;
      input.setAttribute("aria-invalid", false);
      error.setAttribute("aria-hidden", true);
      success.setAttribute("aria-hidden", false);
      error.style.display = "none";
      success.style.display = "block";
    } else if (
      value !== undefined &&
      value.toString().length == maxLength &&
      value.match(regex) &&
      !validNationalID(value)
    ) {
      btn.disabled = true;
      input.setAttribute("aria-invalid", true);
      error.setAttribute("aria-hidden", false);
      success.setAttribute("aria-hidden", true);
      error.style.display = "block";
      success.style.display = "none";
    } else {
      btn.disabled = true;
      input.setAttribute("aria-invalid", true);
      error.setAttribute("aria-hidden", false);
      success.setAttribute("aria-hidden", true);
      error.style.display = "none";
      success.style.display = "none";
    }
  }

  function validNationalID(id) {
    if (id.length != 14) return false;
    // STEP 1 - get only first 12 digits
    for (i = 0, sum = 0; i < 13; i++) {
      // STEP 2 - multiply each digit with each index (reverse)
      // STEP 3 - sum multiply value together
      sum += parseInt(id.charAt(i)) * (14 - i);
    }
    // STEP 4 - mod sum with 11
    let mod = sum % 12;
    // STEP 5 - subtract 11 with mod, then mod 10 to get unit
    let check = (12 - mod) % 10;
    // STEP 6 - if check is match the digit 13th is correct
    if (check == parseInt(id.charAt(13))) {
      return true;
    }
    return false;
  }
});
