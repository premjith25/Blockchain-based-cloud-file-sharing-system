(function () {
  // Each page says which menu it needs: <body data-role="user"> or "admin"
  var role = document.body.getAttribute("data-role") || "user";
  var isPublic = role === "public";

  var menus = {
    user: [
      ["MyFiles.html", "My Files"],
      ["Upload.html", "Upload"],
      ["SharedFiles.html", "Shared With Me"],
      ["Logs.html", "Activity Logs"],
      ["Feedback.html", "Feedback"],
      ["Profile.html", "Profile"]
    ],
    admin: [
      ["AdminUsers.html", "Users"],
      ["AdminFiles.html", "Tampered Files"],
      ["AdminLogs.html", "Logs"],
      ["AdminFeedback.html", "Feedback"]
    ]
  };

  var logoutPage = role === "admin" ? "AdminLogin.html" : "Login.html";
  var title = role === "admin" ? "SecureCloud Share - Admin" : "SecureCloud Share";

  // Highlight the link of the page you are currently on
  var current = location.pathname.split("/").pop();

  var links = (menus[role] || []).map(function (item) {
    var cls = item[0] === current ? ' class="active"' : "";
    return '<a href="' + item[0] + '"' + cls + ">" + item[1] + "</a>";
  }).join("");

  // Placeholder name. The real login will store the user's name here.
  var username = sessionStorage.getItem("username") || "Guest";

  var header =
    '<header class="topbar">' +
      '<div class="brand">' + title + "</div>" +
      (isPublic ? "" :
        '<nav class="nav">' + links + "</nav>" +
        '<div class="userbox">Hello, ' + username +
          ' <a href="' + logoutPage + '" id="logoutLink">Logout</a></div>') +
    "</header>";

  var footer =
    '<footer class="footer">Blockchain-based Cloud File Sharing System | CA 3 Project</footer>';

  document.body.insertAdjacentHTML("afterbegin", header);
  document.body.insertAdjacentHTML("beforeend", footer);

  // Logout clears the stored name
  var logoutLink = document.getElementById("logoutLink");
  if (logoutLink) {
    logoutLink.addEventListener("click", function () {
      sessionStorage.removeItem("username");
    });
  }
})();