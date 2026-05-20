import { describe, it, expect, beforeEach } from 'vitest';
import { setupDialog } from './_test-tools.js';
import { html } from '../src/_utils/fabrication-facility.js';
import { initializeEditContact } from '../src/edit-contact.js';


const contactJos = { id: 42, name: 'jos', email: 'jos@jos.com', phone: '12345' };

function setup(updateContact) {
    setupDialog();
    const name = html('input', { class: 'name', name: 'name' });
    const email = html('input', { class: 'email', name: 'email' });
    const phone = html('input', { class: 'phone', name: 'phone' });
    const form = html('form', { class: 'contact-form' }, name, email, phone);
    const container = html('dialog', form);
    const component = initializeEditContact(container, updateContact ?? (a => { }));
    component.onUpdate = () => { };
    return { container, form, name, email, phone, component };
}

describe('Edit Contact:', () => {

    it('- showDialog opens the dialog with inputs filled.', () => {
        const sut = setup()
        sut.component.showDialog(contactJos);
        expect(sut.container.open).toBe(true);
        expect(sut.name.value).toBe('jos');
        expect(sut.email.value).toBe('jos@jos.com');
        expect(sut.phone.value).toBe('12345');
    });

    it('- Form.submit passes form data to storage.', () => {
        let data = null;
        const sut = setup(a => data = a);
        sut.component.showDialog(contactJos);
        sut.name.value = 'long neck';
        sut.email.value = 'giraffe@savanna.com';
        sut.phone.value = '666';
        sut.form.dispatchEvent(new Event('submit', { bubbles: true, cancelable: true }));
        expect(data.id).toBe(42);
        console.log(data);
        expect(data.name).toBe('long neck');
        expect(data.email).toBe('giraffe@savanna.com');
        expect(data.phone).toBe('666');
    });

    it('- Form.submit calls onUpdate.', () => {
        let called = false;
        const sut = setup(a => { });
        sut.component.showDialog(contactJos);
        sut.component.onUpdate = () => called = true;
        sut.form.dispatchEvent(new Event('submit', { bubbles: true }));
        expect(called).toBe(true);
    });

});