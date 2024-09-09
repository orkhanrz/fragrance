import Cart from './cart.js';
import Fragrance from './fragrance.js';
import { showNotification } from './notify.js';

export default class Favorite {
    static async getFavorites() {
        try {
            const res = await fetch('/api/favorites');
            const data = await res.json();
            return data;
        } catch (err) {
            console.log(err);
        }
    };

    static async addToFavorites(fragranceId) {
        try {
            const res = await fetch('/api/favorites/' + fragranceId, { method: "POST" });
            const data = await res.json();
            
            showNotification(data.success ? 'success' : 'error', data.message);
        } catch (err) {
            console.log(err);
        } 
    }

    static async render() {
        let favoritesHtml = ``;
        const data = await this.getFavorites();

        data.forEach(f => {
            favoritesHtml += `
                <div class="fragrance-item" id="${f.fragranceId}">
                    <div class="cursor-pointer relative group mb-6">
                        <img class="product-item-image h-80 object-cover w-full" src="${f.fragranceImage}" alt="" />
                        <div class="icons flex absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 shadow-md opacity-0 transition-opacity duration-300 group-hover:opacity-100">
                            <a href="/products/${f.fragranceId}" class="bg-white block w-12 h-12 border border-gray border-r-0 flex justify-center items-center">
                                <img class="w-4 h-4" src="/images/eye.svg" alt="eye" />
                            </a>
                            <span class="bg-white block w-12 h-12 border border-gray border-r-0 flex justify-center items-center remove-from-favorites" data-id="${f.fragranceId}">
                                <img class="w-4 h-4" src="/images/heart.svg" alt="heart" />
                            </span>
                            <span class="bg-white block w-12 h-12 border border-gray border-r-0 flex justify-center items-center add-to-cart" data-id="${f.fragranceId}">
                                <img class="w-4 h-4" src="/images/cart.svg" alt="cart" />
                            </span>
                            <span class="bg-white block w-12 h-12 border border-gray flex justify-center items-center">
                                <img class="w-4 h-4" src="/images/arrows.svg" alt="arrows" />
                            </span>
                        </div>
                    </div>
                    <a href="/products/@item.FragranceId" class="info text-center">
                        <h3 class="product-item-title uppercase font-bold text-sm mb-1">${f.fragranceBrand} ${f.fragranceLine}</h3>
                        <div class="prices">
                            <span class="product-item-price text-sm mr-1">$${f.fragrancePrice}</span>
                            ${f.fragranceOldPrice ? `<span class="text-xs text-primary line-through">$${f.fragranceOldPrice}</span>` : ""}
                        </div>
                    </a>
                </div>
            `;
        });

        document.querySelector('.fragrance-items').innerHTML = favoritesHtml;

        document.querySelectorAll('.add-to-cart').forEach(f => {
            f.addEventListener('click', async () => {
                const fId = f.getAttribute("data-id");
                const fragrance = await Fragrance.getById(fId);
                Cart.addToCart(fragrance, 1);
            });
        });

        document.querySelectorAll('.remove-from-favorites').forEach(btn => {
            btn.addEventListener('click', function () {
                const fId = this.getAttribute('data-id');
                Favorite.addToFavorites(fId);
                document.querySelector(`.fragrance-item[id="${fId}"]`).remove();
            });
        });
    }
}