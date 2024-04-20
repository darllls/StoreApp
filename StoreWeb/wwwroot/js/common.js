window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operation Successful", { timeOut: 5000 });
    }
    if (type === "error") {
        toastr.error(message, "Operation Failed", { timeOut: 5000 });
    }
}

window.ShowSweetAlert = (type, message) => {
    if (type === "success") {
        Swal.fire({
            title: "Success",
            text: "Operation Successful",
            icon: "success"
        });
    }
    if (type === "error") {
        Swal.fire({
            icon: "error",
            title: "Failure",
            text: "Operation Failed",
        });
    }
}

function ShowDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('show');
}

function HideDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('hide');
}