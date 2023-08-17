﻿function getDataa(url: string): any {
    return fetch(url)
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => data)
        .catch(error => {
            console.error('Fetch error:', error);
        });
}

interface Person {
    username: string;
    firstName: string;
    lastName: string;
    role: number;
}

function loadUsers() {
    getDataa("/people")
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