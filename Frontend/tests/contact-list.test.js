import { describe, it, expect, beforeEach } from 'vitest';
import { html } from '../src/_utils/fabrication-facility.js';
import { initializeContactList } from '../src/contact-list.js'

function setup(contacts) {
    const container = html('div');
    const search = html('input');
    const component = initializeContactList(container, search, contacts ?? (a => []));
    return { container, search, component };
}

describe('Contact List:', () => {

    it('- Contains the add button.', () => {
        const sut = setup()
        sut.component.refresh();
        expect(sut.container.children.length).toBe(1);
        const button = sut.container.children[0];
        expect(button).toBeInstanceOf(HTMLButtonElement);
    });

    it('- Add button calls onAddButtonClicked when clicked.', () => {
        let clicked = false;
        const sut = setup()
        sut.component.onAddButtonClicked = () => clicked = true;
        sut.component.refresh();
        sut.container.children[0].click();
        expect(clicked).toBe(true);
    });

    it('- Renders contacts as cards.', () => {
        const sut = setup(a => [{ name: 'jos', email: '', phone: '' }])
        sut.component.refresh();
        expect(sut.container.children.length).toBe(2); // 1 card + add button
        const card = sut.container.children[0];
        expect(card.innerHTML).toBe('<div class="cardHeader"><h2>jos</h2><button>⚙</button></div><p>e-mail: </p><p>Phone nr: </p>');
    });

    it('- Calls onShowDetail when contact detail button clicked.', () => {
        let contact = null;
        const sut = setup(a => [{ name: 'jos', email: '', phone: '' }])
        sut.component.onShowDetail = a => contact = a;
        sut.component.refresh();
        expect(sut.container.children.length).toBe(2); // 1 card + add button
        const button = sut.container.children[0].querySelector('button');
        button.click();
        expect(contact.name).toBe('jos');
    });

    it('- Typing in the search input passes the value as argument to storage.', () => {
        let filter = null;
        const sut = setup(a => { filter = a; return [] });
        sut.component.refresh();
        expect(filter).toBe('');
        sut.search.value = 'looking';
        sut.search.dispatchEvent(new Event('input', { bubbles: true }));
        expect(filter).toBe('looking');
    });
});