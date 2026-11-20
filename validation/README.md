# validation

Builds an executable-repo project and a library-repo project with the templates, imported the way a consuming repo imports them, so a broken template fails `dotnet build` and the pre-push hook. After it builds, ValidationApp also checks `global.json.template`. It exists only to validate the templates: don't copy, symlink or import anything from this folder.
