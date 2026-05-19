import { getByClass, notConfigured } from "./_utils/ui.js";
import { deleteContact } from "./storage.js";

export function initializeDeleteContact(element) {

    let contact = null;

    const elements = {
        dialog: element,
        confirmButton: getByClass(element, 'confirm')
    };

    const component = {
        showDialog(contactToShow) {
            contact = contactToShow;
            elements.dialog.showModal();
        },
        onConfirmed: () => notConfigured('delete-contact', 'onConfirmed')
    };

    function removeContact() {
        deleteContact(contact);
        elements.dialog.close();
        component.onConfirmed();
    }

    elements.confirmButton.addEventListener("click", () => removeContact());

    return component;
}