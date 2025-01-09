function LoginViewModel() {
}

// Expose the view model to the global scope
window.loginViewModel = new LoginViewModel();

// Apply Knockout bindings
ko.applyBindings(loginViewModel);