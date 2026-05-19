const storageKey = "contact-manager-contacts";

let contacts = loadContacts();

export function getAllContacts(filter = '') {
    const normalized = filter.toLowerCase();
    return contacts.filter(contact =>
        contact.name.toLowerCase().includes(normalized) ||
        contact.email.toLowerCase().includes(normalized) ||
        contact.phone.toLowerCase().includes(normalized));
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


