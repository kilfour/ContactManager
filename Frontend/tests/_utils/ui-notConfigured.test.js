import { describe, expect, it } from "vitest";
import { notConfigured } from '../../src/_utils/ui.js';

describe('notConfigured', () => {
    it('throws a custom exception, used as guard at runtime for missing config', () => {
        try {
            notConfigured('module-name', 'event-name');
            expect.fail('Should throw, but did not.');
        } catch (err) {
            expect(err).toBeInstanceOf(Error);
            expect(err.message).toBe('event-name was not configured in module module-name.');
        }
    });
});