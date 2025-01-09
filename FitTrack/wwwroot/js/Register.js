function RegisterViewModel() {
    // Observables for form fields
    this.username = ko.observable("");
    this.email = ko.observable("");
    this.password = ko.observable("");

    // Registration function
    this.register = async function (event) {
        // Prevent the form's default submit behavior
        if (event && typeof event.preventDefault === "function") {
            event.preventDefault();
        }

        const data = {
            Username: this.username(),
            Email: this.email(),
            Password: this.password()
        };

        const response = await fetch("/api/AccountApi/register", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(data)
        });

        if (response.ok) {
            alert("Registration successful!");
            window.location.href = "/Account/Login";
        } else {
            const result = await response.text();
            alert("Error: " + result);
        }
    };
}

// Expose the view model to the global scope
window.registerViewModel = new RegisterViewModel();

// Apply Knockout bindings
ko.applyBindings(new RegisterViewModel());