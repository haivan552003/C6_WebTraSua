document.addEventListener("DOMContentLoaded", function () {
    window.addEventListener("scroll", function () {
        if (window.scrollY <= 40) {
            document.getElementById("menu").style.padding = "20px 0";
        } else {
            document.getElementById("menu").style.padding = "0";
        }
        document.getElementById("menu").style.transition = "all .5s ease-out";
    });
});

//session
window.sessionStorageHelper = {
    setItem: function (key, value) {
        sessionStorage.setItem(key, value);
    },
    getItem: function (key) {
        return sessionStorage.getItem(key);
    },
    removeItem: function (key) {
        sessionStorage.removeItem(key);
    }
};
