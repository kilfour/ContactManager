import { describe, it, expect, beforeEach } from 'vitest';
import { setupDialog } from './_test-tools.js';
import { html } from '../src/_utils/fabrication-facility.js';
import { initializeDeleteContact } from '../src/delete-contact.js'

function setup(deleteContact) {
    setupDialog();
    const button = html('button', { class: 'confirm' })
    const container = html('dialog', button);
    const component = initializeDeleteContact(container, deleteContact ?? (a => { }));
    component.onDelete = () => { };
    return { container, button, component };
}

describe('Delete Contact:', () => {

    it('- showDialog does what it says on the tin.', () => {
        const sut = setup()
        sut.component.showDialog();
        expect(sut.container.open).toBe(true);
    });

    it('- Clicking the confirm button calls storage.', () => {
        let actual = null;
        const sut = setup(a => actual = a)
        const contact = { name: 'jos' }
        sut.component.showDialog(contact);
        sut.button.click();
        expect(actual).toBe(contact);
    });

    it('- Clicking the confirm button calls onDelete.', () => {
        let called = false;
        const sut = setup();
        sut.component.onDelete = () => called = true;
        sut.component.showDialog();
        sut.button.click();
        expect(called).toBe(true);
    });

    it('- Clicking the confirm button closes the dialog.', () => {
        const sut = setup();
        sut.component.showDialog();
        expect(sut.container.open).toBe(true);
        sut.button.click();
        expect(sut.container.open).toBe(false);
    });

});