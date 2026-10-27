// ---------- Helpers ----------
function setError(id, message) {
  var box = document.getElementById("err-" + id);
  if (box) box.textContent = message;
}

function clearErrors(form) {
  var spans = form.querySelectorAll(".error-text");
  for (var i = 0; i < spans.length; i++) spans[i].textContent = "";
}

function val(id) {
  return document.getElementById(id).value.trim();
}

// ---------- Login ----------
function validateLogin(form) {
  clearErrors(form);
  var ok = true;

  if (val("username") === "") {
    setError("username", "Please enter your username.");
    ok = false;
  }
  if (document.getElementById("password").value === "") {
    setError("password", "Please enter your password.");
    ok = false;
  }
  return ok;
}

// ---------- Register ----------
function validateRegister(form) {
  clearErrors(form);
  var ok = true;

  if (val("fullname").length < 3) {
    setError("fullname", "Name must be at least 3 characters.");
    ok = false;
  }
  if (!/^[A-Za-z0-9_]{4,}$/.test(val("username"))) {
    setError("username", "Username: at least 4 letters, numbers or underscores.");
    ok = false;
  }
  if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(val("email"))) {
    setError("email", "Enter a valid email address.");
    ok = false;
  }
  if (!/^[0-9]{10}$/.test(val("phone"))) {
    setError("phone", "Phone must be exactly 10 digits.");
    ok = false;
  }

  var pass = document.getElementById("password").value;
  if (pass.length < 6 || !/[0-9]/.test(pass)) {
    setError("password", "Password: at least 6 characters and one number.");
    ok = false;
  }
  if (document.getElementById("confirm").value !== pass) {
    setError("confirm", "Passwords do not match.");
    ok = false;
  }
  return ok;
}