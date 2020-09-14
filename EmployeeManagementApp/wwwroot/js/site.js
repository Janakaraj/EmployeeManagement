const connection = new signalR.HubConnectionBuilder()
    .withUrl("/NotificationHub")
    .build();
connection.on("RecieveAddEmployeeMessage", (employeeName, employeeSurname) => {
    alert(employeeName + " " + employeeSurname + " was added.");
});
connection.on("RecieveEditProfileMessage", (name) => {
    alert(name + " edited their profile.");
});
connection.on("RecieveAddDepartmentMessage", (deptName) => {
    alert(deptName + " was added by the Admin.");
});
try {
    connection.start();
    console.log("connected");
} catch (err) {
    console.log(err);
}
if (document.getElementById("createDepartmentButton")) {
    document.getElementById("createDepartmentButton").addEventListener("click", function (event) {
        var name = document.getElementById("nameInput").value;
        connection.invoke("SendAddDepartmentMessage", name).catch(function (err) {
            return console.error(err.toString());
        });
    });
}
if (document.getElementById("editEmployeeButton")) {
    document.getElementById("editEmployeeButton").addEventListener("click", function (event) {
        var name = document.getElementById("nameInput").value;
        connection.invoke("SendEditProfileMessage", name).catch(function (err) {
            return console.error(err.toString());
        });
    });
}
if (document.getElementById("createEmployeeButton")) {
    document.getElementById("createEmployeeButton").addEventListener("click", function (event) {
        var name = document.getElementById("nameInput").value;
        var surname = document.getElementById("surnameInput").value;
        connection.invoke("SendAddEmployeeMessage", name, surname).catch(function (err) {
            return console.error(err.toString());
        });
    });
}


connection.onclose(async () => {
    await start();
});
