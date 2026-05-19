import { getByClass, bindDialogForm } from "./_utils/ui.js";
import { updateContact } from "./storage.js";

let component = {};
let elements = {};
let contact = {};

export function initializeEditContact(element) {
    createComponent();
    bindElements(element)
    bindForm();
    return component;
}

function createComponent() {
    component = {
        showDialog: a => showDialog(a),
        onConfirmed: null
    };
}

function bindElements(element) {
    elements = {
        dialog: element,
        form: getByClass(element, 'addContactForm'),
        name: getByClass(element, "name"),
        email: getByClass(element, "email"),
        phone: getByClass(element, "phone"),
    };
}

function bindForm() {
    bindDialogForm(
        elements.dialog,
        elements.form,
        a => { a.id = contact.id; contact = a; updateContact(a); },
        () => component.onConfirmed(contact));
}

function showDialog(contactToShow) {
    contact = contactToShow;
    elements.name.value = contact.name;
    elements.email.value = contact.email;
    elements.phone.value = contact.phone;
    elements.dialog.showModal();
}