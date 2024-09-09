import Fragrance from '../utils/fragrance.js';

const applyFiltersBtn = document.getElementById("apply-filters");
const filtrationForm = document.getElementById('filtration-form');

applyFiltersBtn.addEventListener('click', (e) => {
    e.preventDefault();
    let filtrationObj = {};
    const formData = new FormData(filtrationForm);

    for (const key of formData.keys()) {
        const value = formData.get(key);

        if (value) {
            filtrationObj[key] = value;
        };
    }

    Fragrance.fetch(filtrationObj);
});


Fragrance.fetch();

