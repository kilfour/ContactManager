import { getById } from "./_utils/ui.js";

export function getElements() {
    return {
        contactList:   /**/ getById("contact-list"),
        search:        /**/ getById("searchField"),
        addContact:    /**/ getById("addContactDialog"),
        contactDetail: /**/ getById("inspectContactDialog"),
        editContact:   /**/ getById("editContactDialog"),
        deleteContact: /**/ getById("deleteContactDialog"),
    };
}

