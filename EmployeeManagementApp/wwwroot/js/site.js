const connection = new signalR.HubConnectionBuilder()
    .withUrl("/NotificationHub")
    .build();

connection.on("RecieveAddEmployeeMessage", (employeeName, employeeSurname) => {
    alert(employeeName + " " + employeeSurname + " was added to your department.");
    var message = employeeName + " " + employeeSurname + " was added to your department.";
    var d = document.createElement("a");
    d.className = "dropdown-item";
    d.innerText = message;
    document.getElementById("notificationList").appendChild(d);
    document.getElementById("notiBadge").style.visibility = "visible";
});
connection.on("RecieveEditProfileMessage", (name) => {
    alert("Employee " + name + " edited their profile.");
    var d = document.createElement("a");
    d.className = "dropdown-item";
    d.innerText = "Employee " + name + " edited their profile.";
    document.getElementById("notificationList").appendChild(d);
    document.getElementById("notiBadge").style.visibility = "visible";
});
connection.on("RecieveAddDepartmentMessage", (deptName) => {
    alert(deptName + " department was added by the Admin.");
    var d = document.createElement("a");
    d.className = "dropdown-item";
    d.innerText = deptName + " department was added by the Admin.";
    document.getElementById("notificationList").appendChild(d);
    document.getElementById("notiBadge").style.visibility = "visible";
});
try {
    connection.start();
    console.log("connected");
} catch (err) {
    console.log(err);
}
if (document.getElementById("dropdownMenuButton")) {
    document.getElementById("dropdownMenuButton").addEventListener("click", function (event) {
        document.getElementById("notiBadge").style.visibility= "hidden";
        });
    };
//if (document.getElementById("editEmployeeButton")) {
//    document.getElementById("editEmployeeButton").addEventListener("click", function (event) {
//        var name = document.getElementById("nameInput").value;
//        connection.invoke("SendEditProfileMessage", name).catch(function (err) {
//            return console.error(err.toString());
//        });
//    });
//}
//if (document.getElementById("createEmployeeButton")) {
//    document.getElementById("createEmployeeButton").addEventListener("click", function (event) {
//        var name = document.getElementById("nameInput").value;
//        var surname = document.getElementById("surnameInput").value;
//        connection.invoke("SendAddEmployeeMessage", name, surname).catch(function (err) {
//            return console.error(err.toString());
//        });
//    });
//}


connection.onclose(async () => {
    await start();
});
