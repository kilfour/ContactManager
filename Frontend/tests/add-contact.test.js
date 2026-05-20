import { describe, it, expect, beforeEach } from 'vitest';
import { setupDialog } from './_test-tools.js';
import { html } from '../src/_utils/fabrication-facility.js';
import { initializeAddContact } from '../src/add-contact.js'

function setup(inputValue, createContact) {
    setupDialog();
    const input = html('input', { name: 'name', value: inputValue });
    const form = html('form', { class: 'contact-form' }, input);
    const container = html('dialog', form);
    const component = initializeAddContact(container, createContact);
    return { container, form, input, component };
}

describe('Add Contact:', () => {

    it('- showDialog does what it says on the tin.', () => {
        const sut = setup()
        sut.component.showDialog();
        expect(sut.container.open).toBe(true);
    });

    it('- Form.submit passes form data to storage.', () => {
        let data = null;
        const sut = setup('jos', a => data = a)
        sut.component.onCreate = () => { };
        sut.form.dispatchEvent(new Event('submit', { bubbles: true }));
        expect(data).toBeTruthy({ name: 'jos' });
    });

    it('- Form.submit calls onCreate.', () => {
        let called = false;
        const sut = setup('jos', a => { })
        sut.component.onCreate = () => called = true;
        sut.form.dispatchEvent(new Event('submit', { bubbles: true }));
        expect(called).toBe(true);
    });
});