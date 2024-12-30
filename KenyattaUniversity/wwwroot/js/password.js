// JavaScript to dynamically set the Password field
document.getElementById('SchoolID').addEventListener('input', function () {
    const schoolID = this.value; // Get the value of SchoolID field
    document.getElementById('Password').value = schoolID; // Set Password to SchoolID value
});