# Use mise to activate the project's pinned SDK if it's installed; otherwise fall through to
# whatever `dotnet` is on PATH. The prefix is empty when mise is missing, so commands still run.
PREP := if command -v mise >/dev/null 2>&1; then eval "$$(mise activate bash)"; fi

SLN := ./src/DeFuncto.sln
CORE := ./src/DeFuncto.Core/DeFuncto.Core.csproj
ASSERTIONS := ./src/DeFuncto.Assertions/DeFuncto.Assertions.csproj

.PHONY: setup restore build test test-ci test-no-build pack

# One-time bootstrap: trust the project's mise.toml and install the pinned SDK.
setup:
	@if ! command -v mise >/dev/null 2>&1; then \
		echo "mise is not installed — see https://mise.jdx.dev/getting-started.html"; \
		exit 1; \
	fi
	mise trust
	mise install

restore:
	$(PREP) && dotnet restore $(SLN)

build: restore
	$(PREP) && dotnet build -c Release --no-restore $(SLN)

# Dev/TDD target: let `dotnet test` rebuild on every invocation so source edits are picked up.
# Release configuration matches what ships; Debug only changes optimisations + symbols.
test:
	$(PREP) && dotnet test -c Release $(SLN)

# CI target: build the whole solution once, then run the tests without rebuilding.
test-ci: build test-no-build

# Same suite as test-ci but assumes the solution is already built.
test-no-build:
	$(PREP) && dotnet test -c Release --no-build --no-restore $(SLN)

# Produce the NuGet packages for both shipped projects.
pack: build
	$(PREP) && dotnet pack -c Release -o . --no-restore $(CORE)
	$(PREP) && dotnet pack -c Release -o . --no-restore $(ASSERTIONS)
