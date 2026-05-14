import { parseArguments } from "./parse-arguments";

export function htmlList(tag, items, renderItem) {
    const container = document.createElement(tag);
    for (const item of items) {
        container.appendChild(renderItem(item));
    }
    return container;
}

export function html(tag, ...args) {
    const { attrs, children } = parseArguments(args);
    const node = document.createElement(tag);
    applyAttributes(node, attrs);
    appendChildren(node, children);
    return node;
}

const booleanProps = new Set(['checked', 'selected', 'disabled']);

function applyAttributes(node, attrs) {
    for (const [key, value] of Object.entries(attrs)) {
        if (key.startsWith('on') && typeof value === 'function') {
            const eventName = key.slice(2).toLowerCase();
            node.addEventListener(eventName, value);
        } else if (key === 'style' && typeof value === 'object') {
            Object.assign(node.style, value);
        } else if (booleanProps.has(key)) {
            node[key] = Boolean(value);
        } else {
            node.setAttribute(key, value);
        }
    }
}

function appendChildren(node, children) {
    for (const child of children.flat()) {
        node.append(child instanceof Node ? child : document.createTextNode(child));
    }
}

export const __only_for_test = { applyAttributes };
