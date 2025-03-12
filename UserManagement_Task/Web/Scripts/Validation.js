// Function to validate a text field with max length
function validateField(fieldId, maxLength) {
    let field = document.getElementById(fieldId);
    let errorSpan = document.getElementById(fieldId + "Error");

    if (!field.value.trim()) {
        errorSpan.innerText = `${fieldId.replace(/([A-Z])/g, ' $1')} is required.`;
        return false;
    }
    if (field.value.length > maxLength) {
        errorSpan.innerText = `Maximum ${maxLength} characters allowed.`;
        return false;
    }

    // Username validation: No special characters
    if (fieldId === "Username") {
        let usernamePattern = /^[a-zA-Z0-9_]+$/;  
        if (!usernamePattern.test(field.value)) {
            errorSpan.innerText = "Username can only contain letters, numbers, and underscores.";
            return false;
        }
    }

    // Mobile Number validation: Numeric only
    if (fieldId === "MobileNumber") {
        let mobilePattern = /^[0-9]{10,15}$/;  
        if (!mobilePattern.test(field.value)) {
            errorSpan.innerText = "Enter a valid mobile number (10-15 digits).";
            return false;
        }
    }

    errorSpan.innerText = "";
    return true;
}

// Function to validate email format
function validateEmail() {
    let email = document.getElementById("Email");
    let errorSpan = document.getElementById("EmailError");
    let emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;  

    if (!email.value.trim()) {
        errorSpan.innerText = "Email is required.";
        return false;
    }
    if (!emailPattern.test(email.value)) {
        errorSpan.innerText = "Invalid email format.";
        return false;
    }

    errorSpan.innerText = "";
    return true;
}

// Function to validate Profile Picture URL  
function validateProfilePicture() {
    let profilePic = document.getElementById("ProfilePicture");
    let errorSpan = document.getElementById("ProfilePictureError");
    let urlPattern = /^(https?:\/\/.*\.(?:png|jpg|jpeg|gif|svg|webp))$/i;  

    if (profilePic.value.trim() && !urlPattern.test(profilePic.value)) {
        errorSpan.innerText = "Enter a valid image URL (.png, .jpg, .jpeg, .gif, .svg, .webp)";
        return false;
    }

    errorSpan.innerText = "";
    return true;
}

// Function to validate entire form before submission
function validateForm() {
    let isValid = true;

     isValid &= validateField("Username", 50);
    isValid &= validateField("Name", 255);
    isValid &= validateEmail();
    isValid &= validateField("MobileNumber", 20);
    isValid &= validateProfilePicture();

    return isValid;  
}
