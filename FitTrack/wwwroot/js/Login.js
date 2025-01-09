function LoginViewModel() {
    this.email = ko.observable("");
    this.password = ko.observable("");

    this.login = async function (event) {
        // Prevent the form's default submit behavior
        if (event && typeof event.preventDefault === "function") {
            event.preventDefault();
        }

        const data = {
            Email: this.email(),
            Password: this.password()
        };

        const response = await fetch("/api/AccountApi/login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(data)
        });

        if (response.ok) {
            const result = await response.json();

            // Redirect based on role
            if (result.role === "Admin") {
                window.location.href = "/Admin/Dashboard";  // Redirect to Admin Dashboard
            } else {
                window.location.href = "/User/Dashboard";  // Redirect to User Dashboard
            }
        } else {
            const result = await response.text();
            alert("Error: " + result)
        }
    }
}

// Expose the view model to the global scope
window.loginViewModel = new LoginViewModel();

// Apply Knockout bindings
ko.applyBindings(loginViewModel);