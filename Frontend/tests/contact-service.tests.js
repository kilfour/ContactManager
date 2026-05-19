import { describe, expect, it } from "vitest";
import { addContact, removeContact, searchContacts } from "../src/contact-service.js";

describe("contact-service", () => {
  it("adds a contact", () => {
    const contacts = addContact([], "Ada");

    expect(contacts).toHaveLength(1);
    expect(contacts[0].name).toBe("Ada");
  });

  it("does not add an empty contact", () => {
    expect(addContact([], "   ")).toEqual([]);
  });

  it("searches contacts by name", () => {
    const contacts = [
      { id: "1", name: "Ada Lovelace" },
      { id: "2", name: "Grace Hopper" }
    ];

    expect(searchContacts(contacts, "ada")).toEqual([
      { id: "1", name: "Ada Lovelace" }
    ]);
  });

  it("removes a contact", () => {
    const contacts = [
      { id: "1", name: "Ada Lovelace" },
      { id: "2", name: "Grace Hopper" }
    ];

    expect(removeContact(contacts, "1")).toEqual([
      { id: "2", name: "Grace Hopper" }
    ]);
  });
});
