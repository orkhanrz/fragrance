export function showNotification(icon, title, timerMs = 1000) {
    const Toast = Swal.mixin({
        toast: true,
        position: "top-end",
        showConfirmButton: false,
        timer: timerMs,
        timerProgressBar: true,
    });
    Toast.fire({
        icon,
        title
    });
};

export function showAdminNotification(title, text, icon) {
    Swal.fire({
        title,
        text,
        icon
    });
};