import { getById } from "./_utils/ui.js";

export function getElements() {
    return {
        contactList:   /**/ getById("contact-list"),
        search:        /**/ getById("search-field"),
        addContact:    /**/ getById("add-contact-dialog"),
        contactDetail: /**/ getById("inspect-contact-dialog"),
        editContact:   /**/ getById("edit-contact-dialog"),
        deleteContact: /**/ getById("delete-contact-dialog"),
    };
}

