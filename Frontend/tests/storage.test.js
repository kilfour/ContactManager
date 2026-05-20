import { describe, it, expect, vi, beforeEach } from 'vitest';
import { initializeStorage, StorageKey } from '../src/storage';


beforeEach(() => {
    localStorage.clear();
    vi.spyOn(crypto, 'randomUUID')
        .mockReturnValueOnce('id-1')
        .mockReturnValueOnce('id-2');
});

describe('createContact:', () => {

    it(' - Stores a contact with generated id', () => {
        const storage = initializeStorage();
        let contact = { name: 'jos', email: 'jos@jos.com', phone: '12345' };
        storage.createContact(contact);
        const result = JSON.parse(
            localStorage.getItem(StorageKey)
        );
        expect(result).toEqual([
            {
                id: 'id-1',
                name: 'jos',
                email: 'jos@jos.com',
                phone: '12345'
            }
        ]);
    });
});

describe('updateContact:', () => {

    it(' - Updates the contact with matching id', () => {

        let contact = { id: 'id-42', name: 'jos', email: 'jos@jos.com', phone: '12345' };
        localStorage.setItem(StorageKey, JSON.stringify([contact]));

        const storage = initializeStorage();

        let contactUpdate = { id: 'id-42', name: 'os', email: 'os@os.com', phone: '45' };
        storage.updateContact(contactUpdate);
        const result = JSON.parse(
            localStorage.getItem(StorageKey)
        );
        expect(result).toEqual([
            {
                id: 'id-42',
                name: 'os',
                email: 'os@os.com',
                phone: '45'
            }
        ]);
    });
});

describe('deleteContact:', () => {

    it(' - Deletes the contact with matching id', () => {

        let contact1 = { id: 'id-1', name: 'jos', email: '', phone: '' };
        let contact2 = { id: 'id-2', name: 'fred', email: '', phone: '' };
        localStorage.setItem(StorageKey, JSON.stringify([contact1, contact2]));
        const storage = initializeStorage();
        storage.deleteContact(contact1);
        const result = JSON.parse(
            localStorage.getItem(StorageKey)
        );
        expect(result).toEqual([{
            "id": "id-2",
            "name": "fred",
            "email": "",
            "phone": "",
        }]);
    });

});

describe('getFilteredContacts:', () => {

    it(' - Returns all contacts when no filter set', () => {

        let contact1 = { id: 'id-1', name: 'jos', email: '', phone: '' };
        let contact2 = { id: 'id-2', name: 'fred', email: '', phone: '' };
        localStorage.setItem(StorageKey, JSON.stringify([contact1, contact2]));

        const storage = initializeStorage();
        const result = storage.getFilteredContacts('');
        expect(result).toEqual([
            {
                "id": "id-1",
                "name": "jos",
                "email": "",
                "phone": "",
            },
            {
                "id": "id-2",
                "name": "fred",
                "email": "",
                "phone": "",
            },
        ]);
    });

    it(' - Returns matching contacts when filter set', () => {

        let contact1 = { id: 'id-1', name: 'jos', email: '', phone: '' };
        let contact2 = { id: 'id-2', name: 'fred', email: '', phone: '' };
        localStorage.setItem(StorageKey, JSON.stringify([contact1, contact2]));

        const storage = initializeStorage();
        const result = storage.getFilteredContacts('j');
        expect(result).toEqual([
            {
                "id": "id-1",
                "name": "jos",
                "email": "",
                "phone": "",
            }
        ]);
    });

});
