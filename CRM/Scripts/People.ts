function convertRole(role: number) {
    if (role == 0) {
        return "Admin";
    }
    else if (role == 1) {
        return "User";
    }
} 
function addPersonToList(person: Person) {
    const row = document.createElement("div");
    row.classList.add("people-row");

    const id = document.createElement("div");
    id.classList.add("row-id");
    id.innerHTML = person.id.toString();
    row.appendChild(id);

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
    role.innerHTML = convertRole(person.role);
    row.appendChild(role);

    const buttons = document.createElement("div");
    buttons.classList.add("row-buttons");

    const editButton = document.createElement("button");
    editButton.classList.add("btn-edit");
    editButton.classList.add("btn");
    editButton.classList.add("btn--outline");
    editButton.setAttribute("onclick", `showPersonEditor(${person.id})`);
    editButton.innerHTML = "Edytuj";
    buttons.appendChild(editButton);

    const deleteButton = document.createElement("button");
    deleteButton.classList.add("btn-delete");
    deleteButton.classList.add("btn");
    deleteButton.classList.add("btn--primary");
    deleteButton.setAttribute("onclick", `deletePerson('${person.username}')`);
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
    inputs.password.value = person.password == undefined ? "" : person.password;

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

function personEditorModeAdd() {
    document.querySelector(".btn-save").setAttribute("onclick", "addPerson()");

    const title = document.querySelector(".person-editor-title");
    title.innerHTML = title.innerHTML.replace("Edytuj", "Dodaj");

    resetPersonEditor();
}

function showPersonEditor(id: number) {
    if (id != undefined) {
        getPerson(id).then(person => {
            if (person != undefined && person.username.length > 0) {
                document.querySelector(".btn-save").setAttribute("onclick", `editPerson(${person.id})`);

                const title = document.querySelector(".person-editor-title");
                title.innerHTML = title.innerHTML.replace("Dodaj", "Edytuj");

                populatePersonEditor(person);
            }
            else {
                personEditorModeAdd();
            }
        });
    }
    else {
        personEditorModeAdd();
    }

    const editor = document.querySelector(".person-editor-container");
    editor.classList.add("shown");
}

function hidePersonEditor() {
    const editor = document.querySelector(".person-editor-container");
    editor.classList.remove("shown");
}

function addPerson() {
    const inputs = getPersonInputs();
    const person: Person = {
        id: null,
        username: inputs.username.value,
        firstName: inputs.firstName.value,
        lastName: inputs.lastName.value,
        role: Number(inputs.role.value),
        password: inputs.password.value
    };

    postData("/people", person).then(response => {
        person.password = null;
        if (response.ok) {
            addPersonToList(person);
            hidePersonEditor();
        }
    });
}

function updatePersonData(id: number, person: Person) {
    const rows = document.querySelectorAll(".people-row");
    rows.forEach(row => {
        const idSpan = row.querySelector(".row-id");
        if (idSpan != undefined && idSpan.innerHTML == id.toString()) {
            row.querySelector(".row-username").innerHTML = person.username;
            row.querySelector(".row-firstname").innerHTML = person.firstName;
            row.querySelector(".row-lastname").innerHTML = person.lastName;
            row.querySelector(".row-role").innerHTML = convertRole(person.role);
            row.querySelector(".btn-edit").setAttribute("onclick", `showPersonEditor(${person.id})`);
            row.querySelector(".btn-delete").setAttribute("onlick", `deletePerson('${person.username}')`);
        }
    })
}

function editPerson(id: number) {
    const inputs = getPersonInputs();
    const person: Person = {
        id: id,
        username: inputs.username.value,
        firstName: inputs.firstName.value,
        lastName: inputs.lastName.value,
        role: Number(inputs.role.value),
        password: inputs.password.value
    };

    postData(`/people/${id}`, person).then(response => {
        person.password = null;
        if (response.ok) {
            updatePersonData(id, person);
            hidePersonEditor();
        }
    });
}