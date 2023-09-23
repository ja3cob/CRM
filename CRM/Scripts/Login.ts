document.querySelector("input[name=password]").addEventListener("keypress", e => {
    if ((<KeyboardEvent>e).key == "Enter") {
        login();
    }
})

function getLoginInputs() {
    return {
        username: (<HTMLInputElement>document.querySelector("input[name=username]")).value,
        password: (<HTMLInputElement>document.querySelector("input[name=password]")).value
    }
}
function login() {
    const inputs = getLoginInputs();
    postData(urls.apiLogin, { Username: inputs.username, Password: inputs.password }).then(response => {
        if (response.ok) {
            window.location.replace("/");
        }
    });
}