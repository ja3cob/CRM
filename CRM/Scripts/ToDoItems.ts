﻿function clearEditor() {
    document.querySelector(".task-editor-title").innerHTML = "Stwórz zadanie";

    const inputs = document.querySelectorAll(".task-editor-form input, .input-text");
    inputs.forEach(input => (<HTMLInputElement>input).value = "");

    const assignedPerson = (<HTMLSelectElement>document.querySelector(".select-assigned-person"));
    assignedPerson.value = "default";

    const inputProgress = (<HTMLInputElement>document.querySelector(".input-progress"));
    inputProgress.value = "0";

    updateProgressValue(inputProgress.value);
    updateSelect(assignedPerson);
}

function updateProgressValue(progress) {
    document.querySelector(".input-progress-value").innerHTML = progress + '%';
}

﻿function loadDays(days: Date[], calendarBody: Element) {    
    for(let week = 0, arrayIndex = 0; week < daysInCalendar / daysInWeek; week++) {
        let tr = document.createElement("tr");

        for(let dayInWeek = 0; dayInWeek < daysInWeek; dayInWeek++, arrayIndex++) {
            let td = document.createElement("td");
            
            let mainDiv = document.createElement("div");
            mainDiv.classList.add("calendar-day");
            if(days[arrayIndex].getMonth() != days[15].getMonth()) {
                mainDiv.classList.add("different-month");
            }

            let p = document.createElement("p");
            p.innerHTML = days[arrayIndex].getDate().toString();
            mainDiv.appendChild(p);

            let toDoItemsDiv = document.createElement("div");
            toDoItemsDiv.classList.add("todoitems");
            toDoItemsDiv.id = days[arrayIndex].toISOStringDate();

            mainDiv.appendChild(toDoItemsDiv);
            td.appendChild(mainDiv);
            tr.appendChild(td);
        }

        calendarBody.appendChild(tr);
    }
}

function getCurrentDate() {
    const urlParams = new URLSearchParams(window.location.search);
    var year = urlParams.get('year');
    var month = urlParams.get('month');
    if (year == undefined || month == undefined
        || isNaN(Number(year)) || isNaN(Number(month))) {
        let currentDate = new Date();
        year = currentDate.getFullYear().toString();
        month = (currentDate.getMonth() + 1).toString();
    }
    return new Date(Number(year), Number(month) - 1, 1);
}

function postTask(inputs) {
    const item: ToDoItem = {
        id: inputs.id.value,
        date: inputs.date.value,
        startTime: inputs.startTime.value,
        endTime: inputs.endTime.value,
        text: inputs.text.value,
        progress: inputs.progress.value,
        assignedToId: Number(inputs.assignedTo.value),
        createdById: 1
    }

    if (item.id.toString().length < 1 || isNaN(Number(item.id))) {
        item.id = null;
    }
    if (isNaN(item.assignedToId)) {
        item.assignedToId = null;
    }

    postData(urls.apiToDoItems, item).then(response => {
        if (!response.ok) {
            console.error("Failed to save data.");
        }
        else {
            clearEditor();
            loadToDoItems(getCurrentDate())
        }
    });
}

function loadToDoItems(date: Date) {
    const apiUrl = urls.apiToDoItems+`?year=${date.getFullYear()}&month=${date.getMonth() + 1}`;

    getData(apiUrl).then(toDoItems => {

        const allToDoItems = document.querySelectorAll(".todoitems");
        allToDoItems.forEach(item => item.innerHTML = "");

        toDoItems.forEach((toDoItem: ToDoItem) => {
            const day = document.getElementById(`${toDoItem.date}`);
            if (day != undefined) {
                day.appendChild(generateToDoItemDiv(toDoItem, false));
            }
        });
    });
}

function generateToDoItemDiv(toDoItem: ToDoItem, differentMonth: boolean): Element {
    const toDoItemDiv = document.createElement("div");
    toDoItemDiv.classList.add("todoitems-item");
    toDoItemDiv.setAttribute("onmouseover", "displayTodoitemDetails(this)");
    toDoItemDiv.setAttribute("onmouseout", "hideTodoitemDetails(this)");
    toDoItemDiv.setAttribute("onclick", `populateEditor(${JSON.stringify(toDoItem)})`);

    const id = document.createElement("span");
    id.style.display = "none";
    id.innerHTML = toDoItem.id.toString();
    toDoItemDiv.appendChild(id);

    const timeRange = document.createElement("p");
    timeRange.classList.add("todoitems-item-time");
    timeRange.innerHTML = toDoItem.startTime + " - " + toDoItem.endTime;
    toDoItemDiv.appendChild(timeRange);

    const text = document.createElement("p");
    text.classList.add("todoitems-item-text");
    text.innerHTML = toDoItem.text;
    toDoItemDiv.appendChild(text);

    if(!differentMonth) {
        toDoItemDiv.appendChild(generateToDoItemDetails(toDoItem));
    }

    return toDoItemDiv;
}

function generateToDoItemDetails(toDoItem: ToDoItem): Element {
    const detailsDiv = document.createElement("div");
    detailsDiv.classList.add("todoitems-item-details")

    const header = document.createElement("div");
    header.classList.add("todoitems-item-details-header");

    const timeRange = document.createElement("span");
    timeRange.classList.add("todoitems-item-details-time");
    timeRange.innerHTML = toDoItem.startTime + " - " + toDoItem.endTime;
    header.appendChild(timeRange);

    header.appendChild(generatePercentageCircle(toDoItem.progress, true));
    detailsDiv.appendChild(header);

    const text = document.createElement("p");
    text.classList.add("todoitems-item-details-text");
    text.innerHTML = toDoItem.text;
    detailsDiv.appendChild(text);

    const assignedTo = document.createElement("p");
    assignedTo.classList.add("todoitems-item-details-assignedto");
    assignedTo.innerHTML = "<b>Przydzielony: </b><br>";
    if (toDoItem.assignedToId == null) {
        assignedTo.innerHTML += "brak";
    } else {
        getPerson(toDoItem.assignedToId)
            .then((person: Person) => assignedTo.innerHTML += person.username);
    }
    detailsDiv.appendChild(assignedTo);

    const createdBy = document.createElement("p");
    createdBy.classList.add("todoitems-item-details-createdby");
    createdBy.innerHTML = "<b>Utworzył: </b><br>";
    getPerson(toDoItem.createdById)
        .then((person: Person) => createdBy.innerHTML += person.username);
    detailsDiv.appendChild(createdBy);

    return detailsDiv;
}

function generatePercentageCircle(progress: number, small: boolean): Element {
    const circleDiv = document.createElement("div");
    circleDiv.classList.add("c100");
    circleDiv.classList.add(`p${progress}`);
    if(small) {
        circleDiv.classList.add("small");
    }

    const percentage = document.createElement("span");
    percentage.innerHTML = progress + "%";
    circleDiv.appendChild(percentage);

    const sliceDiv = document.createElement("div");
    sliceDiv.classList.add("slice");

    const bar = document.createElement("div");
    bar.classList.add("bar");
    sliceDiv.appendChild(bar);

    const fill = document.createElement("div");
    fill.classList.add("fill");
    sliceDiv.appendChild(fill);

    circleDiv.appendChild(sliceDiv);

    return circleDiv;
}

function loadAssignedTo() {
    const dropDown = document.querySelector('.select-assigned-person');
    getData(urls.apiPeople).then(people => {
        people.forEach((person: Person) => {
            const option = document.createElement('option');
            option.setAttribute('value', person.id.toString());
            option.innerHTML = person.username;
            dropDown.appendChild(option);
        });
    });
}