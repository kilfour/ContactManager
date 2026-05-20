import { notConfigured, getByClass, bindDialogForm } from "./_utils/ui.js";
import { createContact } from "./storage.js";

export function initializeAddContact(element) {

    const elements = {
        dialog: element,
        form: getByClass(element, 'contact-form')
    };

    const component = {
        showDialog: () => elements.dialog.showModal(),
        onCreate: () => notConfigured('add-contact', 'onCreate')
    };

    bindDialogForm(
        elements.dialog,
        elements.form,
        a => createContact(a),
        component.onCreate);

    return component;
}
