# Migration Guide

## Migrating a Repository to Use build-standards

### Step 1: Add the Submodule

```bash
cd your-repo
git submodule add https://github.com/jas88/build-standards.git build-standards
```

### Step 2: Update Directory.Build.props

**Before (repo-specific):**
```xml
<Project>
  <PropertyGroup>
    <LangVersion>12.0</LangVersion>
    <Nullable>enable</Nullable>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    <!-- ... many more properties ... -->
  </PropertyGroup>
</Project>
```

**After (using shared standards):**
```xml
<Project>
  <Import Project="build-standards/Directory.Build.props.shared" />

  <PropertyGroup>
    <MinVerMinimumMajorMinor>0.1</MinVerMinimumMajorMinor>
    <Product>CsvHandler</Product>
    <Description>High-performance CSV library</Description>
    <PackageProjectUrl>https://github.com/jas88/CsvHandler</PackageProjectUrl>
    <RepositoryUrl>https://github.com/jas88/CsvHandler</RepositoryUrl>
    <PackageTags>csv;parser</PackageTags>
  </PropertyGroup>
</Project>
```

### Step 3: Replace .editorconfig

```bash
rm .editorconfig
ln -s build-standards/.editorconfig .editorconfig
# Or copy if symlinks cause issues:
# cp build-standards/.editorconfig .editorconfig
```

### Step 4: Update Workflow

Replace your existing workflow with a call to the reusable workflow:

**Before (.github/workflows/build.yml - 150+ lines):**
```yaml
name: Build
on:
  push:
    branches: [main]
# ... 150+ lines of workflow definition
```

**After (.github/workflows/build.yml - 30 lines):**
```yaml
name: Build
on:
  push:
    branches: [main]
  pull_request:
    branches: [main]
  release:
    types: [published]

jobs:
  build:
    uses: jas88/.github/.github/workflows/reusable-dotnet-build.yml@main
    with:
      dotnet-versions: '["8.0.x", "9.0.x"]'
      run-tests: true
      upload-coverage: true
    secrets:
      CODECOV_TOKEN: ${{ secrets.CODECOV_TOKEN }}

  security:
    uses: jas88/.github/.github/workflows/reusable-security.yml@main

  publish:
    needs: [build, security]
    if: github.event_name == 'release'
    uses: jas88/.github/.github/workflows/reusable-publish.yml@main
    secrets:
      NUGET_API_KEY: ${{ secrets.NUGET_API_KEY }}
```

### Step 5: Add Pre-commit Hooks (Optional)

```bash
cp build-standards/.pre-commit-config.yaml .pre-commit-config.yaml
pip install pre-commit
pre-commit install
```

### Step 6: Update .gitmodules

Your repo will have a new `.gitmodules` file:
```ini
[submodule "build-standards"]
    path = build-standards
    url = https://github.com/jas88/build-standards.git
```

### Step 7: Commit Changes

```bash
git add -A
git commit -m "Migrate to shared build-standards

- Use jas88/build-standards submodule
- Simplify Directory.Build.props
- Use reusable GitHub Actions workflows"
```

## Updating Standards

When build-standards is updated:

```bash
git submodule update --remote build-standards
git add build-standards
git commit -m "Update build-standards"
```

## Cloning Repos with Submodules

```bash
git clone --recurse-submodules https://github.com/jas88/YourRepo.git
# Or after cloning:
git submodule update --init --recursive
```
