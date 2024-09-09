import Cart from "./cart.js";
import Favorite from './favorite.js';
import { showNotification } from './notify.js';
    
export default class Fragrance {
    static fragrances = [];

    static async fetch(params = {}) {
        let url = "/api/fragrances?";
        let keys = Object.keys(params);

        keys.forEach((key, index) => {
            if (index != keys.length - 1) {
                url += `${key}=${params[key]}&`
            } else {
                url += `${key}=${params[key]}`;
            }
        });

        const res = await fetch(url);
        const data = await res.json();
        this.fragrances = data;
        this.render();
    }

    static async getById(id) {
        const res = await fetch('/api/fragrances/' + id);
        const data = await res.json();
        return data;
    }

    static render() {
        const fragrancesDiv = document.querySelector(".fragrance-items");
        let fragrancesHtml = '';

        this.fragrances.forEach(f => {
            fragrancesHtml += `
                <div class="fragrance-item" id="${f.fragranceId}">
                    <div class="cursor-pointer relative group mb-6">
                        <img class="product-item-image h-80 object-cover w-full" src="${f.fragranceImage}" alt="" />
                        <div class="icons flex absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 shadow-md opacity-0 transition-opacity duration-300 group-hover:opacity-100">
                            <a href="/products/${f.fragranceId}" class="bg-white block w-12 h-12 border border-gray border-r-0 flex justify-center items-center">
                                <img class="w-4 h-4" src="/images/eye.svg" alt="eye" />
                            </a>
                            <span class="bg-white block w-12 h-12 border border-gray border-r-0 flex justify-center items-center add-to-favorites" data-id="${f.fragranceId}">
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
        })

        fragrancesDiv.innerHTML = fragrancesHtml;

        document.querySelectorAll('.add-to-cart').forEach(f => {
            f.addEventListener('click', async () => {
                const fId = f.getAttribute("data-id");
                const fragrance = await this.getById(fId);
                Cart.addToCart(fragrance, 1);
            });
        });

        document.querySelectorAll('.add-to-favorites').forEach(f => {
            f.addEventListener('click', async () => {
                const fId = f.getAttribute("data-id");
                console.log(fId);
                await Favorite.addToFavorites(fId);
            });
        });
    }
}
