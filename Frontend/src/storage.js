export const StorageKey = "contact-manager-contacts";

export function initializeStorage() {
    let contacts = loadContacts();
    return {
        getFilteredContacts(filter = '') {
            const normalized = filter.toLowerCase();
            return contacts.filter(contact =>
                contact.name.toLowerCase().includes(normalized))
        },
        createContact(contact) {
            contact.id = crypto.randomUUID();
            contacts = [...contacts, contact];
            saveContacts();
        },
        updateContact(contact) {
            contacts = contacts.map(a => a.id == contact.id ? contact : a);
            saveContacts();
        },
        deleteContact(contact) {
            contacts = contacts.filter(a => a.id !== contact.id);
            saveContacts();
        }
    }

    function loadContacts() {
        const json = localStorage.getItem(StorageKey);
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
        localStorage.setItem(StorageKey, JSON.stringify(contacts));
    }
}




