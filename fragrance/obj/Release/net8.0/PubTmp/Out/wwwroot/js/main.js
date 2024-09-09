import Cart from './utils/cart.js';

const account = document.getElementById("account");
const accountBtn = document.getElementById("account-btn");
const logoutBtn = document.getElementById('logout-btn');
const cartEl = document.querySelector("#cart");
const cartActions = document.querySelectorAll(".cart-action");

accountBtn.addEventListener("click", function () {
	account.classList.toggle("active");
});

cartActions.forEach(a => {
	a.addEventListener('click', () => cartEl.classList.toggle("active"));
});

if (logoutBtn) {
	logoutBtn.addEventListener('click', () => {
		Cart.clearCart();
		location.href = "/auth/logout";
	});
};

Cart.render();