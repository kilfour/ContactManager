export function wire(component) {
    return {
        from(selector) {
            return {
                to(handler) {
                    const name = extract(selector);
                    component[name] = handler;
                    return wire(component);
                }
            };
        }
    };
}

function extract(selector) {
    const proxy = new Proxy({}, {
        get(_, prop) {
            return prop;
        }
    });
    return selector(proxy);
}