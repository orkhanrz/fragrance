import Cart from '../utils/cart.js';
import { showNotification } from '../utils/notify.js';

const checkoutForm = document.getElementById('checkout-form');

checkoutForm.addEventListener('submit', async (e) => {
    e.preventDefault();
    const cartItems = Cart.cartItems.map(cartItem => ({
        fragranceId: cartItem.fragranceId,
        fragrancePrice: cartItem.fragrancePrice,
        count: cartItem.count,
    }));

    try {
        const response = await fetch('/api/checkout', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(cartItems)
        });

        if (response.ok) {
            console.log('Order created:');
            showNotification('success', 'Order has been created!');

            setTimeout(() => {
                Cart.clearCart();
                location.href = '/';
            }, 1000);

        } else {
            showNotification('error', 'Failed to create order!');
        }
    } catch (err) {
        showNotification('error', 'Something went wrong!');
    }
});

