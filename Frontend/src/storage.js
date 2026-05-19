const storageKey = "contact-manager-contacts";

let contacts = loadContacts();

export function getAllContacts(filter) {
    return contacts.filter(a =>
        a.name.includes(filter) ||
        a.email.includes(filter) ||
        a.phone.includes(filter));
}

export function createContact(contact) {
    contact.id = crypto.randomUUID();
    contacts = [...contacts, contact];
    saveContacts();
}

export function updateContact(contact) {
    contacts = contacts.map(a => a.id == contact.id ? contact : a);
    saveContacts();
}

export function deleteContact(contact) {
    contacts = contacts.filter(a => a.id !== contact.id);
    saveContacts();
}

function loadContacts() {
    const json = localStorage.getItem(storageKey);
    if (json === null)
        return [];

    try {
        const contacts = JSON.parse(json);
        return Array.isArray(contacts) ? contacts : [];
    }
    catch {
        return [];
    }
}

function saveContacts() {
    localStorage.setItem(storageKey, JSON.stringify(contacts));
}


