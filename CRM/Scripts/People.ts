function loadUsers() {
    getData("/people")
        .then((people: Person[]) => people.forEach(person => {
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
    const inputs = getPersonInputs();

    if (person != undefined && person.username.length > 0) {
        inputs.username.disabled = true;
        document.querySelector(".btn-save").setAttribute("onclick", "editPerson()");

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