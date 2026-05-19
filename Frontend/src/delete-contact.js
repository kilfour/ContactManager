import { getByClass } from "./_utils/ui.js";
import { deleteContact } from "./storage.js";

let component = {};
let elements = {};
let contact = {};

export function initializeDeleteContact(element) {
    createComponent();
    bindElements(element)
    bindButtons();
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
        confirmButton: getByClass(element, 'confirm')
    };
}

function bindButtons() {
    elements.confirmButton.addEventListener("click", () => removeContact());
}

function showDialog(contactToShow) {
    contact = contactToShow;
    elements.dialog.showModal();
}

function removeContact() {
    deleteContact(contact);
    elements.dialog.close();
    component.onConfirmed();
}