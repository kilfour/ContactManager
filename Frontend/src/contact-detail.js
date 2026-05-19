import { getByClass, onClick } from "./_utils/ui.js";


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

    onClick(elements.editButton, () => component.onEditClicked(contact));
    onClick(elements.deleteButton, () => component.onDeleteClicked(contact));

    return component;
}


