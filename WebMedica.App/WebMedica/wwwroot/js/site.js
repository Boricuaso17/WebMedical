document.addEventListener("DOMContentLoaded", function () {
    var deleteModalElement = document.getElementById("deleteConfirmationModal");
    var deleteElementLabel = document.getElementById("deleteConfirmationElement");
    var confirmDeleteButton = document.getElementById("confirmDeleteButton");
    var cancelDeleteButton = document.querySelector(".delete-confirm-cancel");
    var pendingDeleteForm = null;

    if (!deleteModalElement || !deleteElementLabel || !confirmDeleteButton) {
        return;
    }

    var deleteModal = bootstrap.Modal.getOrCreateInstance(deleteModalElement);

    document.addEventListener("submit", function (event) {
        var form = event.target;

        if (!(form instanceof HTMLFormElement) || !form.querySelector(".btnDelete")) {
            return;
        }

        event.preventDefault();
        pendingDeleteForm = form;

        var submitButton = event.submitter || form.querySelector(".btnDelete");
        var elementName = form.dataset.deleteName || submitButton.dataset.deleteName || "this item";

        deleteElementLabel.textContent = elementName;
        deleteModal.show();
    });

    deleteModalElement.addEventListener("shown.bs.modal", function () {
        if (cancelDeleteButton) {
            cancelDeleteButton.focus();
        }
    });

    deleteModalElement.addEventListener("hidden.bs.modal", function () {
        pendingDeleteForm = null;
    });

    confirmDeleteButton.addEventListener("click", function () {
        if (!pendingDeleteForm) {
            return;
        }

        var form = pendingDeleteForm;
        pendingDeleteForm = null;
        deleteModal.hide();
        form.submit();
    });
});
