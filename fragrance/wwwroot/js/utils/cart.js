import { showNotification} from './notify.js';

const cartCountEl = document.querySelector(".cart-count");
const cartEls = document.querySelectorAll(".cart-items");
const cartTotalPriceEls = document.querySelectorAll(".cart-total-price");

export default class Cart {
    static cartItems = JSON.parse(sessionStorage.getItem("cart")) || [];
    static totalPrice = 0;

    static render() {
        let cartHtml = '';

        this.cartItems.forEach(item => {
            cartHtml += `
                <div class="border-b border-b-lightGrey flex py-6">
                    <div class="relative mx-4">
                        <img class="h-32 w-24" src="${item.fragranceImage}" alt="parfume"/>
                        <button class="absolute -top-3 -left-3 rounded-full w-6 h-6 bg-white shadow-xl border border-lightGrey remove-from-cart" data-id="${item.fragranceId}">X</button>
                    </div>
                    <div>
                        <h3 class="font-bold mb-1">${item.fragranceBrand} ${item.fragranceLine}</h3>
                        <p>${item.count} x $${item.fragrancePrice}</p>
                    </div>
                </div>
            `;
        });

        cartCountEl.innerText = this.cartItems.length;
        cartEls.forEach(cartEl => cartEl.innerHTML = cartHtml);
        
        this.updateTotalPrice();

        document.querySelectorAll(".remove-from-cart").forEach(btn => {
            btn.addEventListener("click", () => {
                this.removeFromCart(btn.getAttribute("data-id"));
            });
        });
    }

    static addToCart(fragrance, count) {
        if (count > fragrance.fragranceStock) {
            return showNotification('error', 'Out of stock!');
        };

        const cartItemIndex = this.cartItems.findIndex(item => item.fragranceId == fragrance.fragranceId);

        if (cartItemIndex >= 0) {
            if (this.cartItems[cartItemIndex].count + count > fragrance.fragranceStock) {
                return showNotification('error', 'Out of stock!', 1500);
            }

            this.cartItems[cartItemIndex].count += count;
        } else {
            this.cartItems.push({ ...fragrance, count });
        }

        showNotification('success', 'Added to cart!');
        this.updateSessionStorage();
        this.render();
    }

    static removeFromCart(fragranceId) {
        this.cartItems = this.cartItems.filter(item => item.fragranceId != fragranceId);
        showNotification('success', 'Removed from cart!');
        this.updateSessionStorage();
        this.render();
    }

    static updateTotalPrice() {
        this.totalPrice = 0;

        this.cartItems.forEach(item => {
            this.totalPrice = this.totalPrice + (item.count * item.fragrancePrice);
        });

        cartTotalPriceEls.forEach(cartTotalPriceEl => cartTotalPriceEl.innerText = "$" + this.totalPrice);
    }

    static clearCart() {
        this.cartItems = [];
        this.totalPrice = 0;
        this.updateSessionStorage();
    }

    static updateSessionStorage() {
        sessionStorage.setItem("cart", JSON.stringify(this.cartItems));
    }
}
