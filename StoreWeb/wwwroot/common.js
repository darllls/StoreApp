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

function ShowSupplierModal() {
    $('#supplierInfoModal').modal('show');
};

function HideSupplierModal() {
    $('#supplierInfoModal').modal('hide');
};

function ShowProductModal() {
    $('#productDetailsModal').modal('show');
};

function HideProductModal() {
    $('#productDetailsModal').modal('hide');
};

function ShowOrderModal() {
    $('#orderItemsModal').modal('show');
};

function HideOrderModal() {
    $('orderItemsModal').modal('hide');
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

function ShowSupplyModal() {
    $('#supplyDetailsModal').modal('show');
};

function HideSupplyModal() {
    $('#supplyDetailsModal').modal('hide');
};

function ShowShipmentModal() {
    $('#shipmentInvoicesModal').modal('show');
};

function HideShipmentModal() {
    $('#shipmentInvoicesModal').modal('hide');
};

function ShowSupFactModal() {
    $('#supplyDetailsFactModal').modal('show');
};

function HideSupFactModal() {
    $('#supplyDetailsFactModal').modal('hide');
};

function ShowOrdFactModal() {
    $('#orderDetailsFactModal').modal('show');
};

function HideOrdFactModal() {
    $('#orderDetailsFactModal').modal('hide');
};