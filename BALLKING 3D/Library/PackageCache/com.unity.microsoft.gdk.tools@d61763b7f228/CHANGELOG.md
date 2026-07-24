# Changelog
All notable changes to this package will be documented in this file (reverse chronological order).

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/) and this project adheres
 to [Semantic Versioning](http://semver.org/spec/v2.0.0.html) expressed as MAJOR.MINOR.PATCH:

- Different MAJOR: there is at least one breaking change, and neither version of the package can be substituted for the other. For example, versions 1.2.3 and 2.0.0 are not compatible and cannot be used interchangeably.
- Same MAJOR, different MINOR: the highest MINOR introduces functionality in a backward-compatible way. For example, version 1.3.0 can be used to fulfill a dependency on 1.2.0 because 1.3.0 is backward-compatible; however because it introduces new features, the reverse is not true.
- Same MAJOR.MINOR, different PATCH: the highest PATCH introduces bug fixes without changing the API at all, in a backward-compatible way. For example, versions 1.3.0 and 1.3.1 should be interchangeable as they have the same API, although 1.3.1 contains a bug fix not present in 1.3.0.



## [1.7.0] - 2026-07-01
### Added
- Added automatic Submission Validator update via the MakePkg `/updatesubval` flag (requires
  GDK October 2025 or later). When packaging, MakePkg checks for a newer
  `SubmissionValidator.dll` and downloads it into the build output directory for validation.
  The GDK installation is not modified. If the machine is offline, the existing DLL from the
  GDK installation is used as a fallback.
### Fixed
- Fix unused field warning when project is opened on none Windows platform.
- Fix `kBatFileTemplate` where extra MakePkg arguments (e.g. `/contentid`) were incorrectly
  placed between `/f` and the layout XML path, breaking argument parsing in the CreateSolution
  build path.
### Changed
- `Create Microsoft Store Package` and `Sideloadable Package`options in `Project Settings -> Microsoft GDK`, are disabled if the Build and Run deployment method is set to Xbox PC Toolbox
- Minimum supported Unity editor version changed to 6000.0


## [1.6.0] - 2026-03-01
### Fixed
- Fix build failure when building a package and the `Assets/Plugins/GDK` path does not exist  
- Fix "Generate Shell Visuals" failure with exception when MGC contains an empty ShellVisuals element

### Changed
- Changes needed to support Intelligent Delivery package.


## [1.5.1] - 2026-02-01
### Added
- Added a prebuild check to ensure GDK builds only target Intel 64-bit. Both Intel 32-bit and ARM 64-bit architectures are unsupported for GDK builds.
- Added documentation on how to update Shell Visuals resource files for new language-locale pairs.
- Added new "GDK Sideloaded Packages" window allowing see, run, teminate and remove packages installed via "Sideloadable Package" on GDK Settings.
### Fixed
- Fixed pre/post build processors to skip execution on platforms that do not support GDK.
- Selecting "Sideloadable package" on GDK Settings should attempt to sideload the package at build. 
### Changed
- Refactored GDKPlatformManager to reduce boilerplate code and improve maintainability.
- Improved code structure in pre-build and post-build processors with extracted validation methods.
- Added comprehensive XML documentation to some public APIs.
- Changed min supported Unity editor version to: 2022.3.
- Documentation updates based on user feedback.
- Game Config Schema updated for GDK 251000.
- GetAssetPath(int instanceId) has been marked obsolete for newer editor streams.
### Removed
- Removed repetitive foreach patterns in platform management code.


## [1.4.4] - 2025-07-01
### Added
- Changed the way the GDK SDK is discovered. Now uses the `com.unity.microsoft.gdk.discovery` package.
- Support for using Microsoft Game Development Kit (GDK) without install. See package documentation.
- Added manual editing of Sandbox entries in the Microsoft Game Config file through the Inspector window. Sandboxes must still be configured in the Microsoft Partner Center and retrieved by associating the MGC file with the Microsoft Store.
### Fixed
- Removed UGUI package as dependency as it wasn't being used.
- UI added to the Windows Standalone GDK Settings to support GDK Edition switching. NOTE: The selection here is **not** stored in the GDKSettings asset, instead it writes directly to the `Assets/Plugins/GDK/GdkEdition.json` tracking file to prompt an update of the GDK assemblies. **This is intended for use in conjunction with the Microsoft GDK API package (com.unity.microsoft.gdk).**
- Fixed null reference error when attempting to generate a GDK settings script.
- Fixed incorrect executable name being written to the Microsoft Game Config file when using BuildPlayerOptions with locationPathName.
- Fixed shell visuals in Microsoft Game Config missing `PublisherDisplayName` element on generation requiring manual addition of element for submission. NOTE: This fix only applies to newly generated shell visuals.
- Fixed a bug which greyed out the switch sandbox menu option when the build target was not Windows
### Changed
- Change `Pre` and `Post` process build handling callback order. The order has been increased as the previous value of 0 was causing issues for some users.
- Improved the Sandbox Switcher window UI design and functionality.
### Removed
- Removed old GDK SDK discovery code.


## [1.3.0] - 2024-11-12
### Added
- Added support for Stable Key (lk) encryption when building the Windows standalone package using MakePkg.exe.

### Fixed
- "VC14" added as a "KnownDependency" in the final MGC to avoid the "ILI_HasMissingDependency" submission validation error.

### Changed
- The `Sideload Package` GDK Setting UI has changed to `Sideloadable Package`. Selecting this setting will still generate a test signed package as before however the package will now only be installed into the local Microsoft Store when using 'Build and Run'. This change makes it possible to generate a test signed package without it being installed automatically by using 'Build'. This is more appropriate for CI environments.
- `GdkSettingsAsset.createScriptingDefine` is now marked as obsolete, please do not use. Changing this field will no longer take effect. The creation of `MICROSOFT_GDK_SETTINGS` is now controlled by 'Build for GDK'.
- The 'Build for GDK' setting now automatically adds or removes the `MICROSOFT_GDK_SUPPORT` pre-processor.
- Changing the state of 'Build for GDK' will now prompt for a domain reload as this is required for the `MICROSOFT_GDK_SUPPORT` pre-processor change to take immediate effect.
- Pre-existing GdkSettingsAssets which have previously disabled `Build for GDK` on platforms that can only support GDK may find this setting disabled and unchecked. To re-enable the GdkSettingsAsset may need to be re-created.
- The Windows Standalone post-process now deploys the `.DLLs` located in the `Plugins/GDK` folder when `Build for GDK` is enabled (automatically imported DLLs will be marked Editor only by default).

### Removed
- The UI for 'Define MICROSOFT_GDK_SUPPORT' has been removed, this pre-processor is now added or removed based on 'Build for GDK'

### Known Issues
-


## [1.2.3] - 2024-09-09
### Added
- Added documentation on creating and publishing an app to Microsoft Partner Center.
- When building for GDK, invalid Microsoft Game Config will throw an exception.
- Added new status and actions log messages on Microsoft Game Config.
- Added Microsoft Game Config Identity properties Name, Publisher, and Version to `GameConfigAsset`.
- Add an info box when `Create Microsoft Store Package` is enabled to warn about Shell Visuals requirement

### Fixed
- Building GDK for Windows using `Create Visual Studio Solution` and GDK Settings' `Create Microsoft Store Package` is now working.
- makepri.exe fails to build game config localized resources when Windows Driver Framework is installed.
- `MakePri.exe` Game Config localization build can fail when using BuildPipeline.BuildPlayer due to incorrect usage of `GetBuildLocation()`.
- Create/OpenKey Access denied error on Windows 11.

### Changed
- Marked `GameConfigAsset` properties as obsolete, do not use to set values. Instead see: [change the active GDK platform settings](xref:modify-gdk-settings).
- Changed pre-build step to check if the `Microsoft GDK` SDK is installed. The Build will fail when not available and "Build for Microsoft GDK" is enabled.
- Changed text on `GDK Settings` inspector window to correctly reference the `Activate GDK Settings` button.

### Removed
-
### Known Issues
-

## [1.1.0] - 2024-06-17
### Added
- Added documentation on package version numbering requirement in the Issues and limitations section.
- When building for GDK, invalid Microsoft Game Config will throw an exception.
- Add new status and actions log messages on Microsoft Game Config.
- Exposed API to [change the active MGC XML](xref:edit-mgc-file#usecsharpscript) directly.
- Exposed API to [change the active GDK platform settings](xref:modify-gdk-settings).

### Fixed
- 'Generate Shell Visuals Resources' is now correctly saving `en-US` configuration to the '.mgc'.
- Microsoft Game Config asset created using the context menu is now correctly validated.
- Updating Shell Visual Resources no longer generate placeholder images without user consent.
- Valid '.mgc' files are now validated on import.
- Renamed placeholder image `default480x480.png` to `Square480x480Logo.png`.
- Microsoft Game Config asset properties edit messages is now correctly  displaying 'oldValue' and 'newValue'.
- Fixed MGC asset validation log format in Unity 6.
- Fixes MGC asset validation safeguard in Unity 6.

### Changed
- Replaced the Microsoft Game Config Log 'Clear' button by a 'Refresh' button.
- Hide `ImportedObject` section at the bottom of Microsoft Game Config Inspector.
- API Documentation for GameConfigAsset.cs
- Microsoft Game Config schema class updated - class Game.

## [1.0.0] - 2024-03-28
### Added
- Create dedicated documentation on Unity Editor Play mode support.

### Fixed
- Platform tabs on `Microsoft GDK - Project Settings` window maintains predictable order.

### Changed
- Changed default values for newly created Microsoft Game Config (MGC) asset.
- Bump package version.

### Removed
- Remove 'Copy MGC alongside Unity.exe'.


## [1.0.0-pre.3] - 2024-03-18
### Fixed
- Creating a Microsoft Game Config asset using the context menu with a pre-existing asset selected results in exception.
- Generating Shell Visual Resources spams console with errors (CONSOLE-192).
- Generate 'GdkPlatformSettings.cs' UI is causing confusion (PLAT-7934).
- Don't set asset icons in code.
- Debug folders included in package builds when using IL2CPP/Burst (CONSOLE-196).

### Changed
- Bumped version to keep inline with other related packages.
- Revised documentation.
- Rename all occurrences of 'Microsoft GDK Settings' -> 'GDK Settings'.
- Handle renamed XGameRuntimeThunks.DLL -> XGameRuntime.Thunks.DLL.


## [0.2.0] - 2024-02-06
### Changed
- Bumped version to keep inline with other related packages.


## [0.1.0] - 2024-01-31
### Added
- Preview GDK workflow support.
- Preview documentation.
- Microsoft Game Config files with the '.mgc' extension will be treated as an asset.
- Microsoft Game Config 'Asset' supported from the asset inspector allowing changes for most common config elements.
- Added Microsoft GDK Settings, this is used to store the settings used by the GDK Tools package when generating a build.
- Shell Visuals can be managed by Microsoft Game Config inspector view.


#### This is the first release of *Unity Package com.unity.microsoft.gdk.tools*.

*Initial release.*