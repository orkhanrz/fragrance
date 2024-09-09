const accountForm = document.getElementById("account-form");
const accountFormInputs = document.querySelectorAll("#account-form input");
const accountFormEditBtns = document.querySelectorAll(".account-edit");

function toggleInputs(inputs) {
	inputs.forEach((input, index) => {
		console.log(input, index);
		if (input.getAttribute("readonly")) {
			input.removeAttribute("readonly");
			input.classList.add("border-b-primary");


			if (index == 0) {
				input.focus();
				input.setSelectionRange(input.value.length, input.value.length);
			}
		} else {
			input.setAttribute("readonly", "readonly");
			input.classList.remove("border-b-primary");
		}
	});
};

accountFormEditBtns.forEach((btn) => {
	btn.addEventListener("click", function (e) {
		e.preventDefault();
		this.innerText = this.innerText === "Edit" ? "Save" : "Edit";

		const grandparent = this.parentElement.parentElement;
		const inputs = grandparent.querySelectorAll("input[type='text'], input[type='password']");

		toggleInputs(inputs);
	});
});

toggleInputs(accountFormInputs);