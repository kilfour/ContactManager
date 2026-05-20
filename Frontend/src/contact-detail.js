import { getByName, onClick } from "./_utils/ui.js";


export function initializeContactDetail(element) {

    let contact = null;

    const elements = {
        dialog: element,
        name: getByName(element, "name"),
        email: getByName(element, "email"),
        phone: getByName(element, "phone"),
        editButton: getByName(element, "edit"),
        deleteButton: getByName(element, "delete")
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


