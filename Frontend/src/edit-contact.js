import { bindDialogForm, getByClass, notConfigured } from "./_utils/ui.js";


export function initializeEditContact(element, updateContact) {

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
        a => { contact = { ...a, id: contact.id }; updateContact(contact); },
        () => component.onUpdate(contact));

    return component;
}