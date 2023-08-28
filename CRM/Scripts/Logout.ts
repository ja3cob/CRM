function logout() {
    postData(urls.apiLogout, null).then(() => {
        window.location.reload();
    });
}