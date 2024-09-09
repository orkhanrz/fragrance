import Cart from '../utils/cart.js';
import Fragrance from '../utils/fragrance.js';
import Favorite from '../utils/favorite.js';
import { showNotification } from '../utils/notify.js';

const reduceProductBtn = document.querySelector('#reduce-product');
const increaseProductBtn = document.querySelector('#increase-product');
const productAmountInput = document.querySelector('#product-amount');
const addToCartBtn = document.querySelector(".add-to-cart");
const addToFavoritesBtn = document.querySelector('.add-to-favorites');
const productInfoBtns = document.querySelectorAll('.product-info-btn');
const productInfoSections = document.querySelectorAll('.product-info-section');
const openReviewBtn = document.querySelector('#open-review-btn');
const cancelReviewBtn = document.querySelector('#cancel-review-btn');
const submitReviewBtn = document.querySelector('#submit-review-btn');
const writeReviewDiv = document.querySelector('#write-review');
const writeReviewForm = document.querySelector('#write-review-form');
const pathnameArr = location.pathname.split("/");
const productId = pathnameArr[pathnameArr.length - 1];
const stars = document.querySelectorAll('.stars .star');
const starInputs = document.querySelectorAll('.star-inputs input');
const fragrance = await Fragrance.getById(productId);
const inStock = fragrance.fragranceStock;

function highlightStars(count) {
    stars.forEach((star, idx) => {
        if (idx < count) {
            star.src = '/images/star-filled.svg'; 
        } else {
            star.src = '/images/star.svg';
        }
    });
}

function changeClasses(elements, className, action) {
    if (action === 'add') {
        elements.forEach(el => el.classList.add(className));
    };

    if (action === 'remove') {
        elements.forEach(el => el.classList.remove(className));
    }
}

function closeReview() {
    openReviewBtn.innerText = 'Write a review';
    writeReviewDiv.classList.add('hidden');
}

starInputs.forEach((star, index) => {
    star.addEventListener('mouseenter', () => {
        highlightStars(index + 1);
    });

    star.addEventListener('mouseleave', () => {
        const checkedInput = document.querySelector('.star-inputs input:checked');
        highlightStars(checkedInput ? parseInt(checkedInput.value) : 0);
    });

    star.addEventListener('click', () => {
        starInputs[index].checked = true;
        highlightStars(index + 1);
    });
});

reduceProductBtn.addEventListener('click', () => {
    if (+productAmountInput.value > 1) {
        productAmountInput.value = +productAmountInput.value - 1;
    }
});

increaseProductBtn.addEventListener('click', () => {
    if (+productAmountInput.value < inStock) {
        productAmountInput.value = +productAmountInput.value + 1;
    }
});

productInfoBtns.forEach(btn => {
    btn.addEventListener('click', () => {
        const id = btn.getAttribute('data-id');

        changeClasses(productInfoBtns, 'active', 'remove');
        btn.classList.add('active');

        changeClasses(productInfoSections, 'hidden', 'add');
        document.querySelector(`.product-info-section[id="${id}"]`).classList.remove('hidden');
    });
});

openReviewBtn.addEventListener('click', () => {
    writeReviewDiv.classList.toggle('hidden');
    writeReviewDiv.scrollIntoView({ behavior: 'smooth' });

    if (writeReviewDiv.classList.contains('hidden')) {
        openReviewBtn.innerText = 'Write a review';
    } else {
        openReviewBtn.innerText = 'Cancel a review';
    }
});

cancelReviewBtn.addEventListener('click', closeReview);

writeReviewForm.addEventListener('submit', (e) => {
    e.preventDefault();
    const formData = new FormData(writeReviewForm);
    const formBody = {};

    for (const [key, value] of formData.entries()) {
        formBody[key] = value;
    }

    fetch('/api/reviews', { method: "POST", body: JSON.stringify(formBody), headers: {"Content-Type": "application/json"} })
        .then(res => res.json())
        .then(data => {
            if (data.success) {
                submitReviewBtn.disabled = true;
                closeReview();
                showNotification('success', data.message);
                setTimeout(() => {
                    location.reload();
                }, 1500);

                return;
            };

            showNotification('error', data.message);
        })
        .catch(err => showNotification('error', err.message));
});

addToCartBtn.addEventListener('click', ()  => Cart.addToCart(fragrance, +productAmountInput.value));
addToFavoritesBtn.addEventListener('click', () => Favorite.addToFavorites(productId));