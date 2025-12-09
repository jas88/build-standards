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
pip install pre-commit
pre-commit install
```

### global.json

```bash
cp build-standards/global.json.template global.json
```

## Updating

```bash
git submodule update --remote build-standards
git add build-standards
git commit -m "Update build-standards"
```

## Files

| File | Purpose |
|------|---------|
| `Directory.Build.props.shared` | Common MSBuild properties |
| `.editorconfig` | Code style rules |
| `.pre-commit-config.yaml` | Pre-commit hook configuration |
| `global.json.template` | .NET SDK version pinning |
| `nuget.config` | NuGet package source configuration |
| `analyzers/.globalconfig` | Global analyzer severity settings |
