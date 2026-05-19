import { getByClass } from "./_utils/ui.js";


export function initializeContactDetail(element) {

    let contact = null;

    const elements = {
        dialog: element,
        name: getByClass(element, "name"),
        email: getByClass(element, "email"),
        phone: getByClass(element, "phone"),
        editButton: getByClass(element, "edit"),
        deleteButton: getByClass(element, "delete")
    };

    function refresh(contactToShow) {
        contact = contactToShow;
        elements.name.value = contact.name;
        elements.email.value = contact.email;
        elements.phone.value = contact.phone;
    }

    const component = {
        refresh,
        showDialog(contactToShow) {
            refresh(contactToShow);
            elements.dialog.showModal();
        },
        hideDialog() {
            elements.dialog.close();
        },
        onEditClicked: () => notConfigured('contact-detail', 'onEditClicked'),
        onDeleteClicked: () => notConfigured('contact-detail', 'onDeleteClicked')
    };

    elements.editButton.addEventListener("click", () => component.onEditClicked(contact));
    elements.deleteButton.addEventListener("click", () => component.onDeleteClicked(contact));

    return component;
}


