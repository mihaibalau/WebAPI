# C# Coding Style
1. Use tabs for indentation (no 4 spaces)
2. Use `this.` everywhere possibile.
3. Always specify the visibility, even if it's the default case. (e.g. `private string _foo` not `string _foo`).
4. Visibility should be the first modifier (e.g. `public abstract` not `abstract public`).
5. Avoid more than one blank line at one time. For example, do not have two blank lines between any lines of code).
6. Avoid spurious free spaces. For example avoid `if (some_var == 0)   {`. (use `if (some_var == 0) {`;
7. Only use `var` when the type is explicitly named on the right-hand side, tipically due to either `new` or an explicit cast, e.g. `var stream = new FileStream(...)` not `var stream = OpenStandardInput()`.
8. We use language keywords instead of BCL types (e.g. `int, string, float` instead of `Int32, String, Single`, etc) for both type references as well as method calls (e.g. `int.Parse` instead of `Int32.Parse`).
9. Use `readonly` where possible. When used on static fields, `readonly` should come after `static` (e.g. `static readonly`).
9. Use `snake_case` for all fields and variables.
10. Prefix internal and private fields and local variables with `_` and static fields with `s_`.
11. Use `camelCase` for all method names, including local functions. 
12. Use `PascalCase` for all class and interface names.
13. Fields should be specified at the top within type declarations.
14. When including non-ASCII characters in the source code use Unicode escape sequences (\uXXXX) instead of literal characters.
15. When using a single-statement if, if possible, use a single-line form: `if (my_variable == 5) return true;`.
16. Using braces is always accepted, and required for any nested blocks or if a single statement body spans multiple lines.
17. No MAGIC NUMBERS, always use `enums`!
