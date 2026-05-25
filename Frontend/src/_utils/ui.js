export function getByClass(container, className) {
    const element = container.querySelector(`.${className}`);
    if (element === null)
        throw new Error(`Could not find element with class name '${className}'.`);
    return element;
}

export function getByName(container, name) {
    const element = container.querySelector(`[name='${name}']`);
    if (element === null)
        throw new Error(`Could not find element with attribute name of '${name}'.`);
    return element;
}

export function bindDialogForm(dialog, form, onSubmit, onClose) {
    form.addEventListener("submit", function (event) {
        event.preventDefault();
        onSubmit(Object.fromEntries(new FormData(form)));
        form.reset();
        dialog.close();
        onClose();
    });
}

export function notConfigured(module, name) {
    throw new Error(`${name} was not configured in module ${module}.`);
}

export function onInput(element, action) {
    element.addEventListener("input", () => action(element.value));
}

export function onClick(element, action) {
    element.addEventListener("click", () => action());
}
