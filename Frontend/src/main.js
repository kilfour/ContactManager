import { getById } from "./_utils/ui.js";
import { getFilteredContacts } from "./storage.js";
import { initializeContactDetail } from "./contact-detail.js";
import { initializeContactList } from "./contact-list.js";
import { initializeAddContact } from "./add-contact.js";
import { initializeEditContact } from "./edit-contact.js";
import { initializeDeleteContact } from "./delete-contact.js";


// ----------------------------------------------------------------------------------------------
// Retrieve the main elements
// ---
const elements =
{
    contactList:   /**/ getById("contact-list"),
    search:        /**/ getById("search-field"),
    addContact:    /**/ getById("add-contact-dialog"),
    contactDetail: /**/ getById("inspect-contact-dialog"),
    editContact:   /**/ getById("edit-contact-dialog"),
    deleteContact: /**/ getById("delete-contact-dialog"),
};

// ----------------------------------------------------------------------------------------------
// Create Some Components
// ---
const contactList   /**/ = initializeContactList(elements.contactList, elements.search, getFilteredContacts);
const addContact    /**/ = initializeAddContact(elements.addContact);
const contactDetail /**/ = initializeContactDetail(elements.contactDetail);
const editContact   /**/ = initializeEditContact(elements.editContact);
const deleteContact /**/ = initializeDeleteContact(elements.deleteContact);
// ----------------------------------------------------------------------------------------------


// ----------------------------------------------------------------------------------------------
// Link'm all up
// ---
// When clicking on the '+' in contact list, show the add dialog.
// ▼
contactList.onAddButtonClicked = addContact.showDialog;
// ---
// When a contact is succesfully created refresh the contact list.
// ▼
addContact.onCreate = contactList.refresh;
// ---
// When clicking on a '⚙' in contact list, show the detail dialog.
// ▼
contactList.onShowDetail = contactDetail.showDialog;
// ---
// When clicking on the edit button in the detail, show the edit dialog.
// ▼
contactDetail.onEditClicked = editContact.showDialog;
// ---
// When a contact is succesfully updated refresh both detail and the list.
// ▼
editContact.onUpdate = a => { contactList.refresh(); contactDetail.refresh(a); };
// ---
// When clicking on the delete button in the detail, show the delete dialog.
// ▼
contactDetail.onDeleteClicked = deleteContact.showDialog;
// ---
// When a contact is succesfully deleted close the detail dialog and refresh the list.
// ▼
deleteContact.onDelete = () => { contactDetail.hideDialog(); contactList.refresh(); }
// ----------------------------------------------------------------------------------------------


// ----------------------------------------------------------------------------------------------
// Start the Engines
contactList.refresh();
// ----------------------------------------------------------------------------------------------