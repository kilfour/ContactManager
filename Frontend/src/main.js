import { getElements } from "./elements.js";
import { initializeContactDetail } from "./contact-detail.js";
import { initializeContactList } from "./contact-list.js";
import { initializeAddContact } from "./add-contact.js";
import { initializeEditContact } from "./edit-contact.js";
import { initializeDeleteContact } from "./delete-contact.js";

const elements      /**/ = getElements();

// Create Components
const contactList   /**/ = initializeContactList(elements.contactList, elements.search);
const addContact    /**/ = initializeAddContact(elements.addContact);
const contactDetail /**/ = initializeContactDetail(elements.contactDetail);
const editContact   /**/ = initializeEditContact(elements.editContact);
const deleteContact /**/ = initializeDeleteContact(elements.deleteContact);

// Link them up
contactList.onShowDetail = contactDetail.showDialog;
contactList.onAddButtonClicked = addContact.showDialog;
contactDetail.onEditClicked = editContact.showDialog;
contactDetail.onDeleteClicked = deleteContact.showDialog;
editContact.onConfirmed = a => { contactList.refresh(); contactDetail.refresh(a); };
addContact.onCreate = contactList.refresh;
deleteContact.onConfirmed = () => { contactDetail.hideDialog(); contactList.refresh(); }











contactList.refresh();