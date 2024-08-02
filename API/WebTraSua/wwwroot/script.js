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

/*details*/
function changeMainImage(src) {
    document.getElementById('mainImage').src = src;
}

function attachQuantityHandlers() {
    const decreaseBtn = document.querySelector('#decreaseBtn');
    const increaseBtn = document.querySelector('#increaseBtn');
    const quantityInput = document.querySelector('#quantityInput');

    decreaseBtn.addEventListener('click', function () {
        let currentValue = parseInt(quantityInput.value);
        if (currentValue > 1) {
            quantityInput.value = currentValue - 1;
        }
    });

    increaseBtn.addEventListener('click', function () {
        let currentValue = parseInt(quantityInput.value);
        if (currentValue < 10) {  // Giới hạn tối đa là 10
            quantityInput.value = currentValue + 1;
        }
    });
}

