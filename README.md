# build-standards

Shared build configuration for jas88 .NET projects.

## Usage

### Adding to a Repository

```bash
# Add as a submodule
git submodule add https://github.com/jas88/build-standards.git build-standards
git commit -m "Add build-standards submodule"
```

### Directory.Build.props

Create or update your repo's `Directory.Build.props`:

```xml
<Project>
  <Import Project="build-standards/Directory.Build.props.shared" />

  <!-- Repo-specific overrides -->
  <PropertyGroup>
    <MinVerMinimumMajorMinor>1.0</MinVerMinimumMajorMinor>
    <Product>YourProject</Product>
    <Description>Your project description</Description>
    <PackageProjectUrl>https://github.com/jas88/YourProject</PackageProjectUrl>
    <RepositoryUrl>https://github.com/jas88/YourProject</RepositoryUrl>
  </PropertyGroup>
</Project>
```

### .editorconfig

Symlink or copy the shared `.editorconfig`:

```bash
ln -s build-standards/.editorconfig .editorconfig
# Or copy if symlinks cause issues
cp build-standards/.editorconfig .editorconfig
```

### Pre-commit Hooks

```bash
cp build-standards/.pre-commit-config.yaml .pre-commit-config.yaml
cp build-standards/.markdownlint.yaml .markdownlint.yaml
pip install pre-commit
pre-commit install
```

`.markdownlint.yaml` turns off three markdownlint rules: line length, inline HTML, and a top-level heading as the first line. It also lets a heading repeat under a different parent heading, as the version sections of a Keep a Changelog CHANGELOG do. The markdownlint hook and editors read it from the repo root. Turn rules off there, not with `--disable` in the hook's `args`: `--disable` takes every argument after it, including the file names pre-commit appends, so markdownlint checks nothing.

`pre-commit install` sets up the pre-commit, pre-push and commit-msg hooks, because the config's `default_install_hook_types` lists all three. If a clone installed the hooks before that setting was added, run `pre-commit install` again to add the pre-push and commit-msg hooks.

### global.json

```bash
cp build-standards/global.json.template global.json
```

The template pins the .NET SDK and sets `test.runner` to `Microsoft.Testing.Platform`. `Directory.Build.props.shared` switches test projects to Microsoft Testing Platform, and on the .NET 10 SDK and later `dotnet test` only runs those projects when `global.json` opts in to that runner; without it, `dotnet test` fails with "Testing with VSTest target is no longer supported by Microsoft.Testing.Platform". A repo that stays on VSTest (`UseMicrosoftTestingPlatform=false`) must remove the `test` section from its `global.json`.

## Updating

```bash
git submodule update --remote build-standards
git add build-standards
git commit -m "Update build-standards"
```

## Files

| File | Purpose |
| ------ | --------- |
| `Directory.Build.props.shared` | Common MSBuild properties |
| `.editorconfig` | Code style rules |
| `.pre-commit-config.yaml` | Pre-commit hook configuration |
| `.markdownlint.yaml` | markdownlint rule settings, for the hook and editors |
| `global.json.template` | .NET SDK version pinning |
| `nuget.config` | NuGet package source configuration |
| `analyzers/.globalconfig` | Global analyzer severity settings |
| `validation/`, `build-standards.slnx` | Builds the templates to check them; not for copying |
