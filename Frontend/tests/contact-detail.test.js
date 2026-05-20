import { describe, it, expect, beforeEach } from 'vitest';
import { setupDialog } from './_test-tools.js';
import { html } from '../src/_utils/fabrication-facility.js';
import { initializeContactDetail } from '../src/contact-detail.js'

const contactJos = { name: 'jos', email: 'jos@jos.com', phone: '12345' };
const contactFred = { name: 'fred', email: 'fred@fred.com', phone: '6789' };

function setup(contact) {
    setupDialog();
    const name = html('input', { class: 'name' });
    const email = html('input', { class: 'email' });
    const phone = html('input', { class: 'phone' });
    const editButton = html('button', { class: 'edit' });
    const deleteButton = html('button', { class: 'delete' });
    const container = html('dialog', name, email, phone, editButton, deleteButton);
    const component = initializeContactDetail(container);
    component.onEditClicked = () => { };
    component.onDeleteClicked = () => { };
    return { container, name, email, phone, editButton, deleteButton, component };
}

describe('Delete Contact:', () => {

    it('- showDialog opens the dialog with inputs filled.', () => {
        const sut = setup()
        sut.component.showDialog(contactJos);
        expect(sut.container.open).toBe(true);
        expect(sut.name.value).toBe('jos');
        expect(sut.email.value).toBe('jos@jos.com');
        expect(sut.phone.value).toBe('12345');
    });

    it('- hideDialog closes the dialog.', () => {
        const sut = setup()
        sut.component.showDialog(contactJos);
        expect(sut.container.open).toBe(true);
        sut.component.hideDialog();
        expect(sut.container.open).toBe(false);
    });

    it('- refresh updates the input values.', () => {
        const sut = setup()
        sut.component.showDialog(contactJos);
        sut.component.refresh(contactFred)
        expect(sut.container.open).toBe(true);
        expect(sut.name.value).toBe('fred');
        expect(sut.email.value).toBe('fred@fred.com');
        expect(sut.phone.value).toBe('6789');
    });

    it('- Clicking the edit button calls onEditClicked.', () => {
        let called = false;
        const sut = setup();
        sut.component.onEditClicked = () => called = true;
        sut.component.showDialog(contactJos);
        sut.editButton.click();
        expect(called).toBe(true);
    });

    it('- Clicking the delete button calls onDeleteClicked.', () => {
        let called = false;
        const sut = setup();
        sut.component.onDeleteClicked = () => called = true;
        sut.component.showDialog(contactJos);
        sut.deleteButton.click();
        expect(called).toBe(true);
    });

});