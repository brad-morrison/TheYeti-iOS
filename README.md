# The Yeti

**The Yeti** is a fast-paced Unity arcade game for iPhone and iPad, built around quick reactions, score-chasing, unlockable costumes, and escalating pressure.

This repository contains the Unity project, supporting documentation, and production materials used to maintain, refactor, and extend the game.

> Public for portfolio, development transparency, and project documentation purposes.  
> This project is **not open source**. See [License](#license).

---

## Overview

The Yeti is a mobile arcade game designed around simple input, short sessions, and replayable score-based gameplay.

The player controls a yeti defending the mountain from incoming hikers. Each tap needs to be timed and directed correctly as the pace increases, special modes trigger, and the run becomes harder to sustain.

The project includes:

- A complete Unity iOS game project.
- Arcade-style tap input and score-based progression.
- Unlockable costumes.
- Special gameplay modes.
- Game Center leaderboard support.
- Persistent local player data.
- iPhone and iPad support.
- Production folders for App Store assets, source artwork, marketing files, and documentation.

---

## Status

| Item | Details |
|---|---|
| Platform | iOS |
| Devices | iPhone & iPad |
| Engine | Unity |
| Current Version | 3.32 |
| Project Type | Mobile arcade game |
| Repository Status | Active maintenance / refactor preparation |
| Source Visibility | Public, all rights reserved |

---

## Play Online

An online version of the game is available here:

[Play The Yeti Online](https://bejewelled-souffle-c18d77.netlify.app)

---

## Repository Structure

```text
yeti-game/
├── unity/
│   └── Yeti/
│       ├── Assets/
│       ├── Packages/
│       └── ProjectSettings/
├── production/
│   ├── app-store/
│   ├── source-assets/
│   ├── marketing/
│   └── references/
├── docs/
├── builds/
├── .gitignore
├── .gitattributes
├── .vsconfig
├── LICENSE
└── README.md
```

---

## Unity Project

The Unity project is located at:

```text
unity/Yeti
```

Open this folder in Unity Hub:

```text
yeti-game/unity/Yeti
```

The Unity project folder should contain:

```text
Assets/
Packages/
ProjectSettings/
```

Do **not** open the repository root as the Unity project.

---

## Project Layout

### Unity Project

```text
unity/Yeti/
├── Assets/
├── Packages/
└── ProjectSettings/
```

This is the actual Unity project and should be opened through Unity Hub.

Only files required by Unity at edit time or runtime should live inside the Unity project.

---

### Production Assets

```text
production/
├── app-store/
├── source-assets/
├── marketing/
└── references/
```

The `production/` directory is used for files that support the game but do not need to be imported by Unity.

Examples include:

```text
production/app-store/       App icons, screenshots, release notes, store copy
production/source-assets/   Editable artwork, source files, audio masters
production/marketing/       Trailers, promo images, social content
production/references/      Moodboards, competitor notes, visual references
```

Only exported, runtime-ready assets should be copied into the Unity `Assets/` folder.

Example workflow:

```text
production/source-assets/art/yeti-source.psd
        ↓ export
unity/Yeti/Assets/_Project/Art/yeti.png
```

---

### Documentation

```text
docs/
├── roadmap.md
├── changelog.md
├── release-checklist.md
├── game-design.md
└── technical-notes.md
```

The `docs/` directory is for planning, release management, technical notes, and future design work.

Suggested documentation:

| File | Purpose |
|---|---|
| `roadmap.md` | Planned updates, improvements, and feature ideas |
| `changelog.md` | Version-by-version development history |
| `release-checklist.md` | Repeatable pre-release and App Store checklist |
| `game-design.md` | Core loop, rules, modes, economy, and design notes |
| `technical-notes.md` | Architecture notes, refactor plans, platform notes |

---

### Builds

```text
builds/
└── .gitkeep
```

The `builds/` directory is reserved for local build outputs.

Build artefacts are not committed to the repository. They should be generated from the Unity project whenever needed.

Example local-only folders:

```text
builds/
├── ios/
├── testflight/
└── android/
```

Git stores the recipe, not the cake.

---

## Features

Current project features include:

- Fast one-touch arcade gameplay.
- Increasing difficulty over time.
- Score tracking and high score persistence.
- Unlockable yeti costumes.
- Local player progress storage.
- Special gameplay modes.
- Game Center leaderboard support.
- iPhone and iPad compatibility.
- App Store release support files.
- Unity-based production pipeline.

---

## Development Goals

This repository is currently being reorganised and prepared for future development.

Primary goals:

- Preserve the released version of the game.
- Clean up the repository structure.
- Separate Unity runtime assets from production/source assets.
- Improve version control hygiene.
- Make future development easier to plan and execute.
- Prepare the codebase for safer refactoring.
- Improve maintainability before adding new features.
- Create a clean foundation for future iOS updates.

---

## Version Control Strategy

The repository should track the files required to rebuild and maintain the project:

```text
unity/Yeti/Assets/
unity/Yeti/Packages/
unity/Yeti/ProjectSettings/
production/
docs/
```

The repository should not track generated Unity folders, IDE files, or build outputs such as:

```text
Library/
Temp/
Obj/
Logs/
UserSettings/
Builds/
builds/ios/
builds/testflight/
```

Unity-generated folders can be recreated by opening the project in Unity.

---

## Opening the Project

1. Clone the repository.
2. Open Unity Hub.
3. Select **Add project from disk**.
4. Choose the Unity project folder:

   ```text
   yeti-game/unity/Yeti
   ```

5. Allow Unity to import the project.
6. Open the required scene from the Unity `Assets/` directory.

---

## Release Workflow

A typical release process should be:

1. Make and test source changes in Unity.
2. Update version and build numbers.
3. Confirm release checklist items.
4. Commit the source changes.
5. Build the iOS project locally.
6. Open the generated iOS project in Xcode.
7. Archive and upload to App Store Connect.
8. Test through TestFlight.
9. Submit the build for App Store review.
10. Tag the release commit.

Example release tag:

```bash
git tag v3.32
git push origin v3.32
```

---

## Suggested Git Workflow

For future development:

```text
main          Stable release-ready branch
develop       Active development branch
feature/*     Individual features, fixes, or refactors
release/*     Release preparation branches
hotfix/*      Urgent fixes for live builds
```

Example:

```bash
git checkout -b feature/refactor-player-data
```

---

## Future Work

Potential areas for future updates:

- Codebase refactoring.
- Improved game state management.
- Cleaner save data handling.
- Expanded costume system.
- Additional gameplay modes.
- Improved onboarding/tutorial flow.
- New visual effects and game feel improvements.
- Updated App Store screenshots and marketing assets.
- More structured release pipeline.

---

## Notes for Reviewers

This repository represents an independently built and released Unity iOS game.

It is being kept public to show the development process, project organisation, production structure, and ongoing maintenance work behind a live mobile game.

The project is not intended to be reused, cloned, redistributed, or republished.

---

## License

This repository is **not open source**.

The code, artwork, audio, branding, documentation, and other project files are provided for viewing only.

No permission is granted to copy, modify, distribute, reuse, sell, sublicense, republish, or create derivative works from this repository without prior written permission.

See [`LICENSE`](./LICENSE) for details.