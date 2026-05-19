import { getByClass, notConfigured, onClick } from "./_utils/ui.js";
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
        onDelete: () => notConfigured('delete-contact', 'onDelete')
    };

    function removeContact() {
        deleteContact(contact);
        elements.dialog.close();
        component.onDelete();
    }

    onClick(elements.confirmButton, () => removeContact());

    return component;
}