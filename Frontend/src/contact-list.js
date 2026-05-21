import { html } from './_utils/fabrication-facility.js';
import { notConfigured, onInput } from "./_utils/ui.js";


export function initializeContactList(container, search, getFilteredContacts) {

    let searchFilter = '';

    const elements = {
        container,
        search,
        addButton: html('button', { onclick: () => component.onAddButtonClicked(), class: 'add-button' }, '+')
    };

    const component = {
        refresh() {
            const contacts = getFilteredContacts(searchFilter);
            const children = contacts.map(contactCard);
            elements.container.replaceChildren(...children, elements.addButton);
        },
        onShowDetail: () => notConfigured('contact-list', 'onShowDetail'),
        onAddButtonClicked: () => notConfigured('contact-list', 'onAddButtonClicked')
    };

    const contactCard = (contact) =>
        html('div', { class: 'contactCard' },
            html('div', { class: 'card-header' },
                html('h2', contact.name),
                html('button', { onclick: () => component.onShowDetail(contact), class: 'edit-button' }, '⚙')
            ),
            html('p', `e-mail: ${contact.email}`),
            html('p', `Phone nr: ${contact.phone}`)
        );


    onInput(elements.search, a => searchContacts(a));

    function searchContacts(filter) {
        searchFilter = filter;
        component.refresh();
    }

    return component;
}