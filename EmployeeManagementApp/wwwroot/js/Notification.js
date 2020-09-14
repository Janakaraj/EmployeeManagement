const connection = new signalR.HubConnectionBuilder()
    .withUrl("/NotificationHub")
    .build();
connection.on("RecieveMessage", (employeeName, employeeSurname) => {
    alert(employeeName + " " + employeeSurname+" was added.");
    //var heading = document.createElement("p");
    //heading.textContent = employeeName;
    //var p = document.createElement("p");
    //p.innerText = employeeSurname;

    //var div = document.createElement("div");
    //div.appendChild(heading);
    //div.appendChild(p);

    //document.getElementById("notify").appendChild(div);
});
try {
    connection.start();
    console.log("connected");
} catch (err) {
    console.log(err);
}
document.getElementById("createEmployeeButton").addEventListener("click", function (event) {
    var name = document.getElementById("nameInput").value;
    var surname = document.getElementById("surnameInput").value;
    var role = "Employee";
    connection.invoke("SendMessage", name, surname, role).catch(function (err) {
        return console.error(err.toString());
    });
});
document.getElementById("createDepartmentButton").addEventListener("click", function (event) {
    var name = document.getElementById("nameInput").value;
    var surname = " ";
    var role = "HR"
    connection.invoke("SendMessage", name, surname, role).catch(function (err) {
        return console.error(err.toString());
    });
});

connection.onclose(async () => {
    await start();
});
