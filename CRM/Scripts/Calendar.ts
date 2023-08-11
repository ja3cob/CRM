function loadDays(days: Date[], calendarBody: Element) {    
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

interface Person {
    username: string;
    firstName: string;
    lastName: string;
    role: number;
}
interface ToDoItem {
    id: number;
    date: string;
    startTime: string;
    endTime: string;
    text: string;
    progress: number;
    assignedToUsername: string;
    createdByUsername: string;
}
function postTask(inputs) {
    const item: ToDoItem = {
        id: inputs.id.value,
        date: inputs.date.value,
        startTime: inputs.startTime.value,
        endTime: inputs.endTime.value,
        text: inputs.text.value,
        progress: inputs.progress.value,
        assignedToUsername: inputs.assignedTo.value,
        createdByUsername: null
    }

    fetch("/todoitems", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(item)
    })
    .then(response => {
        if (!response.ok) {
            console.error("Failed to save data.");
        }
        else {
            clearEditor();
            loadToDoItems(getCurrentDate())
        }
    })
    .catch(function (error) {
        console.error("Error:", error);
    });
}

function loadToDoItems(date: Date) {
    const apiUrl = `/todoitems?year=${date.getFullYear()}&month=${date.getMonth() + 1}`;

    fetch(apiUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            data.forEach((toDoItem: ToDoItem) => {
                const day = document.getElementById(`${toDoItem.date}`);
                if(day != undefined) {
                    day.appendChild(generateToDoItemDiv(toDoItem, false));
                }
            });
        })
        .catch(error => {
            console.error('Fetch error:', error);
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
    assignedTo.innerHTML = "Przydzielony: ";
    if (toDoItem.assignedToUsername == null) {
        assignedTo.innerHTML += "brak";
    } else {
        assignedTo.innerHTML += toDoItem.assignedToUsername;
    }
    detailsDiv.appendChild(assignedTo);

    const createdBy = document.createElement("p");
    createdBy.classList.add("todoitems-item-details-createdby");
    createdBy.innerHTML = "Utworzył: " + toDoItem.createdByUsername;
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