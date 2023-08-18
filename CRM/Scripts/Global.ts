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
interface Date {
    addDays(days: number): void;
    toISOStringDate(): string;
}
Date.prototype.addDays = function (days) {
    this.setDate(this.getDate() + days);
}
Date.prototype.toISOStringDate = function () {
    const year = this.getFullYear();
    var month = this.getMonth() + 1;
    var day = this.getDate();

    if (day < 10) {
        day = '0' + day;
    }
    if (month < 10) {
        month = '0' + month;
    }

    return year + '-' + month + '-' + day;
}

function updateSelect(select: HTMLSelectElement) {
    if (select.value == "default") {
        select.style.color = "gray";
    }
    else {
        select.style.color = "black";
    }
}

function getData(url: string): any {
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

function postData(url: string, body: Object): any {
    return fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(body)
    })
        .then(response => response)
        .catch(function (error) {
            console.error("Error:", error);
        });
}