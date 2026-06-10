# The Yeti

The Yeti is a Unity-built arcade game for iPhone and iPad.

This repository contains the Unity project, supporting documentation, and production materials used for maintaining and developing the game.

## Status

The game is currently live on the iOS App Store.

Current project version: 3.32

## Repository Structure

text yeti-game/   unity/     Yeti/       Assets/       Packages/       ProjectSettings/   production/     app-store/     source-assets/     marketing/     references/   docs/   builds/ 

## Unity Project

The Unity project is located at:

text unity/Yeti 

Open this folder in Unity Hub:

text yeti-game/unity/Yeti 

The Unity project should contain:

text Assets/ Packages/ ProjectSettings/ 

Unity-generated folders such as Library, Temp, Obj, Logs, UserSettings, and local build folders should not be committed.

## Production Files

The production/ directory is used for files that support the game but do not need to live inside the Unity project.

Examples include:

text production/app-store/      App icons, screenshots, release copy, store assets production/source-assets/  Source artwork, editable design files, audio masters production/marketing/      Trailers, promo images, social content production/references/     Moodboards, competitor notes, visual references 

Only exported assets that Unity needs at runtime should be placed inside the Unity Assets/ folder.

## Documentation

The docs/ directory is intended for planning and maintenance notes, such as:

text docs/roadmap.md docs/changelog.md docs/release-checklist.md docs/game-design.md docs/technical-notes.md 

## Builds

The builds/ directory may be used locally for exported builds, archives, and test builds.

Build outputs are not intended to be committed unless explicitly required.

## Development Notes

This project was originally built and released as a complete iOS game. The repository is being reorganised to make future updates, refactoring, and feature development easier.

Main development goals:

- Keep the Unity project isolated and easy to open.
- Keep source artwork and production materials outside the Unity project.
- Reduce generated Unity clutter in version control.
- Make future updates easier to plan, test, and release.
- Preserve the original released game while improving the project structure.

## License

This repository is not open source.

The code, artwork, audio, branding, documentation, and other project files are provided for viewing only.

No permission is granted to copy, modify, distribute, reuse, sell, sublicense, or create derivative works from this repository without prior written permission.

See LICENSE for details.

[Online Version](https://bejewelled-souffle-c18d77.netlify.app)