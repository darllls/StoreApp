window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operation Successful", { timeOut: 5000 });
    }
    if (type === "error") {
        toastr.error(message, "Operation Failed", { timeOut: 5000 });
        $('#productModal').modal('show');
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

function ShowProductModal() {
    $('#productDetailsModal').modal('show');
};

function HideProductModal() {
    $('#productDetailsModal').modal('hide');
};

function ShowAvailableProductModal() {
    $('#productStoresModal').modal('show');
};

function HideAvailableProductModal() {
    $('#productStoresModal').modal('hide');
};

function ShowDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('show');
}

function HideDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('hide');
}

