import { html, htmlList } from './_utils/fabrication-facility.js';
import { getAllContacts } from "./storage.js";

export function initializeContactList(element, search) {

    let searchFilter = '';

    const elements = {
        container: element,
        search: search,
        addButton: html('button', { onclick: () => component.onAddButtonClicked(), class: 'addButton' }, '+')
    };

    const component = {
        refresh() {
            let contacts = getAllContacts(searchFilter);
            let cards = contacts.map(
                contact => html('div', { class: 'contactCard' },
                    html('div', { class: 'cardHeader' },
                        html('h2', contact.name),
                        html('button', { onclick: () => component.onShowDetail(contact) }, '⚙')
                    ),
                    html('p', `e-mail: ${contact.email}`),
                    html('p', `Phone nr: ${contact.phone}`)
                ));
            elements.container.innerHTML = '';
            elements.container.append(...cards);
            elements.container.append(elements.addButton);
        },
        onShowDetail: () => notConfigured('contact-list', 'onShowDetail'),
        onAddButtonClicked: () => notConfigured('contact-list', 'onAddButtonClicked')
    };

    function searchContacts(filter) {
        searchFilter = filter;
        component.refresh();
    }

    elements.search.addEventListener("input", () => searchContacts(elements.search.value.toLowerCase()));

    return component;
}