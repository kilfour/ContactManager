import { getByClass, bindDialogForm } from "./_utils/ui.js";
import { createContact } from "./storage.js";

let component = {};
let elements = {};
let contact = {};

export function initializeAddContact(element) {
    createComponent();
    bindElements(element);
    bindForm();
    return component;
}

function createComponent() {
    component = {
        showDialog: a => elements.dialog.showModal(a),
        onUpdate: null
    };
}

function bindElements(element) {
    elements = {
        dialog: element,
        form: getByClass(element, 'addContactForm')
    };
}

function bindForm() {
    bindDialogForm(
        elements.dialog,
        elements.form,
        a => createContact(a),
        () => component.onCreate());
}