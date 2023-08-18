﻿function addPersonToList(person: Person) {
    const row = document.createElement("div");
    row.classList.add("people-row");

    const username = document.createElement("div");
    username.classList.add("row-username");
    username.innerHTML = person.username;
    row.appendChild(username);

    const firstName = document.createElement("div");
    firstName.classList.add("row-firstname");
    firstName.innerHTML = person.firstName;
    row.appendChild(firstName);

    const lastName = document.createElement("div");
    lastName.classList.add("row-lastname");
    lastName.innerHTML = person.lastName;
    row.appendChild(lastName);

    const role = document.createElement("div");
    role.classList.add("row-role");
    role.innerHTML = person.role.toString();
    row.appendChild(role);

    const buttons = document.createElement("div");
    buttons.classList.add("row-buttons");

    const editButton = document.createElement("button");
    editButton.classList.add("btn-edit");
    editButton.classList.add("btn");
    editButton.classList.add("btn--outline");
    editButton.setAttribute("onclick", `showPersonEditor(${JSON.stringify(person)})`);
    editButton.innerHTML = "Edytuj";
    buttons.appendChild(editButton);

    const deleteButton = document.createElement("button");
    deleteButton.classList.add("btn-delete");
    deleteButton.classList.add("btn");
    deleteButton.classList.add("btn--primary");
    deleteButton.innerHTML = "Usuń";
    buttons.appendChild(deleteButton);

    row.appendChild(buttons);


    document.querySelector(".people-list").appendChild(row);
}

function loadUsers() {
    getData("/people")
        .then((people: Person[]) => people.forEach(person => {
            addPersonToList(person);
    }));
}

function getPersonInputs() {
    return {
        username: <HTMLInputElement>document.querySelector("input[name=username]"),
        firstName: <HTMLInputElement>document.querySelector("input[name=firstName]"),
        lastName: <HTMLInputElement>document.querySelector("input[name=lastName]"),
        role: <HTMLSelectElement>document.querySelector("select[name=role]"),
        password: <HTMLInputElement>document.querySelector("input[name=password]")
    }
}

function populatePersonEditor(person: Person) {
    const inputs = getPersonInputs();
    inputs.username.value = person.username;
    inputs.firstName.value = person.firstName;
    inputs.lastName.value = person.lastName;
    inputs.role.value = person.role.toString();

    updateSelect(inputs.role);
}

function resetPersonEditor() {
    const inputs = getPersonInputs();
    inputs.username.value = "";
    inputs.firstName.value = "";
    inputs.lastName.value = "";
    inputs.role.value = "default";
    inputs.password.value = "";

    updateSelect(inputs.role);
}

function showPersonEditor(person: Person) {
    if (person != undefined && person.username.length > 0) {
        document.querySelector(".btn-save").setAttribute("onclick", `editPerson('${person.username}')`);

        const title = document.querySelector(".person-editor-title");
        title.innerHTML = title.innerHTML.replace("Dodaj", "Edytuj");

        populatePersonEditor(person);
    }
    else {
        document.querySelector(".btn-save").setAttribute("onclick", "addPerson()");

        const title = document.querySelector(".person-editor-title");
        title.innerHTML = title.innerHTML.replace("Edytuj", "Dodaj");

        resetPersonEditor();
    }

    document.querySelector(".person-editor-container").classList.remove("hidden");
}

function hidePersonEditor() {
    document.querySelector(".person-editor-container").classList.add("hidden");
}

function addPerson() {
    const inputs = getPersonInputs();
    const person: Person = {
        username: inputs.username.value,
        firstName: inputs.firstName.value,
        lastName: inputs.lastName.value,
        role: Number(inputs.role.value),
        password: inputs.password.value
    };

    postData("/people", person).then(response => {
        if (response.ok) {
            addPersonToList(person);
            hidePersonEditor();
        }
    });
}

function updatePersonData(username: string, person: Person) {
    const rows = document.querySelectorAll(".people-row");
    rows.forEach(row => {
        if (row.querySelector(".row-username").innerHTML == username) {
            row.querySelector(".row-username").innerHTML = person.username;
            row.querySelector(".row-firstName").innerHTML = person.firstName;
            row.querySelector(".row-lastName").innerHTML = person.lastName;
            row.querySelector(".row-role").innerHTML = person.role.toString();
        }
    })
}

function editPerson(username: string) {
    const inputs = getPersonInputs();
    const person: Person = {
        username: inputs.username.value,
        firstName: inputs.firstName.value,
        lastName: inputs.lastName.value,
        role: Number(inputs.role.value),
        password: inputs.password.value
    };

    postData(`/people/${username}`, person).then(response => {
        if (response.ok) {
            updatePersonData(username, person);
            hidePersonEditor();
        }
    });
}