export function getById(id) {
    const element = document.getElementById(id);
    if (element === null)
        throw new Error(`Could not find element with id '${id}'.`);
    return element;
}

export function getByClass(container, className) {
    const element = container.querySelector(`.${className}`);
    if (element === null)
        throw new Error(`Could not find element with class name '${className}'.`);
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
