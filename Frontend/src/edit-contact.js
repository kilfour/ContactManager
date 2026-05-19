import { bindDialogForm, getByClass, notConfigured } from "./_utils/ui.js";
import { updateContact } from "./storage.js";


export function initializeEditContact(element) {

    let contact = null;

    const elements = {
        dialog: element,
        form: getByClass(element, 'contact-form'),
        name: getByClass(element, "name"),
        email: getByClass(element, "email"),
        phone: getByClass(element, "phone"),
    };

    const component = {
        showDialog(contactToShow) {
            contact = contactToShow;
            elements.name.value = contact.name;
            elements.email.value = contact.email;
            elements.phone.value = contact.phone;
            elements.dialog.showModal();
        },
        onUpdate: () => notConfigured('edit-contact', 'onUpdate')
    };

    bindDialogForm(
        elements.dialog,
        elements.form,
        a => { a.id = contact.id; contact = a; updateContact(a); },
        () => component.onUpdate(contact));

    return component;
}