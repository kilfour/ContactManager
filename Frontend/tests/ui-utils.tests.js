import { describe, expect, it } from "vitest";
import { normalizeSearchText } from "../src/ui-utils.js";

describe("normalizeSearchText", () => {
  it("trims spaces and converts to lowercase", () => {
    expect(normalizeSearchText("  Tom  ")).toBe("tom");
  });
});
