import { getByClass } from "./_utils/ui.js";

let component = {};
let elements = {};
let contact = {};

export function initializeContactDetail(element) {
    createComponent();
    bindElements(element)
    bindButtons();
    return component;
}

function createComponent() {
    component = {
        showDialog: a => showDialog(a),
        hideDialog: () => hideDialog(),
        refresh: a => refresh(a),
        onEditClicked: a => { },
        onDeleteClicked: a => { }
    };
}

function bindElements(element) {
    elements = {
        dialog: element,
        name: getByClass(element, "name"),
        email: getByClass(element, "email"),
        phone: getByClass(element, "phone"),
        editButton: getByClass(element, "edit"),
        deleteButton: getByClass(element, "delete"),
    };
}

function bindButtons() {
    elements.editButton.addEventListener("click", () => component.onEditClicked(contact));
    elements.deleteButton.addEventListener("click", () => component.onDeleteClicked(contact));
}

function showDialog(contactToShow) {
    refresh(contactToShow);
    elements.dialog.showModal();
}

function refresh(contactToShow) {
    contact = contactToShow;
    elements.name.value = contact.name;
    elements.email.value = contact.email;
    elements.phone.value = contact.phone;
}

function hideDialog() {
    elements.dialog.close();
}