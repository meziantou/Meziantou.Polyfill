**Any code you commit SHOULD compile, and new and existing tests related to the change SHOULD pass.**

You MUST make your best effort to ensure your changes satisfy those criteria before committing. If for any reason you were unable to build or test the changes, you MUST report that. You MUST NOT claim success unless all builds and tests pass as described above.

Do not complete without checking the relevant code builds and relevant tests still pass after the last edits you make. Do not simply assume that your changes fix test failures you see, actually build and run those tests again to confirm.
Also, do not assume that tests pass just because you did not see any failures in your last test run; verify that all relevant tests were actually run.

Before testing your changes, be sure to run the following commands:
- `dotnet run --project ./Meziantou.Polyfill.Generator/Meziantou.Polyfill.Generator.csproj` to regenerate any generated code
- `dotnet build` to build the solution
- `dotnet test` to run all tests
Also, the there must be no warnings during build.

When creating a new a polyfill, you must:
- Generate the XML Comment Identifier for the new member
- Create a new file in the `Meziantou.Polyfill.Editor` folder named after the XML Comment Identifier (`<xml documentation id>.cs`)
- All polyfills must be self contained. Use a `file class` if needed to create helpers.
- If you need to generate a file only when another polyfill is generated, add `// when <xml documentation id>` in the file
- If xml documentation id is too long, you can use `// XML-DOC: <xml documentation id>` in the file
- All polyfill must be in a partial class named `PolyfillExtensions`
- If using `extension` keyword, the class name can be suffixed with `PolyfillExtensions_<type>` (e.g., `PolyfillExtensions_Int32`) when there is a risk of name collision
- Add tests in the `Meziantou.Polyfill.Tests` project
- Increment the patch component of the version in `Meziantou.Polyfill/Meziantou.Polyfill.csproj`
- Remove all extra `using` directives in the new file
- Use the global namespace (do not declare a namespace)

Documentation about XML documentation identifiers: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/#id-strings.

What can be polyfilled:
- Instance methods using extension methods
- Static methods using `extension` keyword ([extension members](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14#extension-members) introduced in C# 14)
- Properties using `extension` keyword ([extension members](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14#extension-members) introduced in C# 14)
- Static properties using `extension` keyword ([extension members](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14#extension-members) introduced in C# 14)
- Operators using `extension` keyword ([extension members](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14#extension-members) introduced in C# 14)

```c#
// Extension method
partial class PolyfillExtensions
{
    public static void Sample(this string? value)
    {
    }
}

// Extension static method
partial class PolyfillExtensions
{
    extension(int)
    {
        public static int Sample()
        {
        }
    }
}

// Extension static method on generic type
partial class PolyfillExtensions
{
    extension<T>(int) where T : class
    {
        public static int Sample()
        {
        }
    }1
}
```
