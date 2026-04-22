namespace ContactManager.Core.UILayer;

public record MenuOption(string Key, string Label, Func<bool> Handler);
