import { html, htmlList } from './_utils/fabrication-facility.js';
import { getAllContacts } from "./storage.js";

let component = {};
let elements = {};
let searchFilter = '';

export function initializeContactList(element, search) {
    createComponent();
    bindElements(element, search)
    bindSearch();
    return component;
}

function createComponent() {
    component = {
        refresh: () => refresh(),
        onShowDetail: null,
        onAddButtonClicked: null
    };
}

function bindElements(element, search) {
    elements = {
        container: element,
        search: search,
        addButton: html('button', { onclick: () => component.onAddButtonClicked(), class: 'addButton' }, '+'),
    };
}

function bindSearch() {
    elements.search.addEventListener("input", () => searchContacts(elements.search.value.toLowerCase()));
}

function searchContacts(filter) {
    searchFilter = filter;
    refresh();
}

function refresh() {
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
}