# Changelog
All notable changes to this package will be documented in this file (reverse chronological order).

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/) and this project adheres
 to [Semantic Versioning](http://semver.org/spec/v2.0.0.html) expressed as MAJOR.MINOR.PATCH:

- Different MAJOR: there is at least one breaking change, and neither version of the package can be substituted for the other. For example, versions 1.2.3 and 2.0.0 are not compatible and cannot be used interchangeably.
- Same MAJOR, different MINOR: the highest MINOR introduces functionality in a backward-compatible way. For example, version 1.3.0 can be used to fulfill a dependency on 1.2.0 because 1.3.0 is backward-compatible; however because it introduces new features, the reverse is not true.
- Same MAJOR.MINOR, different PATCH: the highest PATCH introduces bug fixes without changing the API at all, in a backward-compatible way. For example, versions 1.3.0 and 1.3.1 should be interchangeable as they have the same API, although 1.3.1 contains a bug fix not present in 1.3.0.

## [1.7.0] - 2026-07-01
### Fixed
- Fix marshalling of XblSocialManagerEvent UsersAffected field for GDK versions later than October 2024 GDK.
### Changed
- Remove use of obsolete `BinaryFormatter` in Cloud Saves sample.
- Recommeded GDK updated to Microsoft GDK April 26 (260400).
- Minimum supported Unity editor version changed to 6000.0
### Fixed
- Fix NonSerialized warnings in SpeechSynthesizer sample when using newer editor streams. 
- Fix deprecation warnings when used in newer editor streams.
- Correct import of Microsoft GDK April 26 (260400) assemblies.

## [1.6.0] - 2026-03-01
### Fixed
- Fix incorrect debug DLL being imported from the GDK causing subval errors.


## [1.5.1] - 2026-02-01
### Fixed
- Fix images not displaying properly in samples documentation pages
- Fix documentation typos.
### Changed
- Changed min supported Unity editor version to: 2022.3.
- Documentation updates based on user feedback.
- Bump recommended GDK edition to 251000.
- Added support for the new October 25 GDK layout.


## [1.4.4] - 2025-07-01
### Added
- Changed the way the GDK SDK is discovered. Now uses the `com.unity.microsoft.gdk.discovery` package.
- Support for using Microsoft Game Development Kit (GDK) without install. See package documentation.
- Support for user specified GDK Edition (Windows Standalone platform only):
- Added a `GdkEdition.json` file to the `Assets/Plugins/GDK` path. This file tracks the GDK Edition from which the GDK assembles originated. Changing the edition number in this file will cause a re-import of the GDK Assemblies.
- Added `SDK.GetGdkEdition` to query the selected edition at runtime. This relies on an auto generated `GDKEdition.asset` being included in the `Resources/GDKEditionAutoGen` path (automatically removed from the project post build).
- Added GDK support for Play Mode, allowing developers to test their Xbox integration features directly within the editor (see known issues below).

### Fixed
- Fix pinvoke DllImportAttribute name for libHttpClient.GDK.dll apis and ensure the DLL is available for the active GDK.
- Fix missing SafeHandle support for HCWebsocketHandle
- Fix XblSocialManagerUser and XblSocialRelationship to support new member 'isFriend' for GDK versions later than October 2024 GDK. Earlier versions will also include the member but it will be set to false by default.

### Changed
- Updated for the `April 25 (250400)` release of the Microsoft GDK
- Modified the samples to no longer use Awaitables (on 6.0 or newer) and asynchronous Tasks (in older versions of Unity). For Awaitables, this is because they are dependent on the Time.timeScale setting, so the Awaitable.WaitForSecondsAsync call could take more than the specified time. For C# asynchronous Tasks, the worry is that because Tasks are a reference type, using them in the tight while loop to await for 32ms means a new Task object is created every iteration and needs to then be garbage collected once it is no longer used.
### Removed
- Removed old GDK SDK discovery code.

### Known Issues
- Under some circumstances the GDK runtime services may fail to initialize. If you are editing or switching the `Microsoft Game Config` settings or changing the active GDK Settings between in-editor play sessions and experience errors it may be necessary to restart the editor.


## [1.3.0] - 2024-11-12
### Added
- Added SafeHandle as a base class for all XBL Handles. Unclosed handles will autorelease once out of scope.
- Added support to October 2024 GDK new Extension Libraries paths.
- Generated GDK Settings script file now marks `gameConfigSandbox` as obsolete, it will be removed in a future release.

### Fixed
- Fix XPersistentLocalStorageGetPath interop miss-match for string path.
- XGamingRuntimeThunks.dll incorrectly included in some builds.

### Changed
- Automatically imported GDK assemblies are now marked as Editor ONLY by default. This makes the assemblies available for in Editor Playmode whilst avoiding the assemblies being deployed to none-compatible target platforms. When using the __Microsoft GDK API__ package without the __Microsoft GDK Tools__ package, the developer will be responsible for ensuring that these assemblies are correctly deployed.


## [1.2.3] - 2024-09-09
### Added
- Added public XblAchievementsAddAchievementProgressChangeHandler and XblAchievementsRemoveAchievementProgressChangeHandler

### Fixed
- Corrected samples and documentation that incorrectly recommend using coroutine to pull GDK events.
- Improved mouse handling in the samples.
- Updated sample installation documentation to mention dependency on the Input System package.
- Create/OpenKey Access denied error on Windows 11.
- Copy libHttpClient.GDK.dll to `Assets/Plugin/GDK` project folder when using June 2024 GDK.

### Changed
-
### Removed
-
### Known Issues
-

## [1.1.0] - 2024-06-17
### Added
- Added public HR constants to Interop.HR to maintain back compatibility with possible upgrade paths
- Added public constructor to XblTitleStorageBlobMetadata.
- Added public XGameSaveSubmitBlobWrite which accepts 'length' parameter.

### Fixed
- Fixed the XStoreQueryAssociatedProductsAsync wrapper always returning an error.
- Fixed the Speech Synthesizer Sample UI to not lock when the close button is pressed.
- Fixed Leaderboard sample: better handling of `Http429TooManyRequests`.
- Fixed XGameSaveInitializeProviderResult and XGameSaveInitializeProvider to bypass offline mode handles.
- Fixed XblSocialManagerPresenceTitleRecord struct interop missmatch.
- Fixed XblSocialManagerUserGroupGetLocalUser, XblSocialManagerPresenceTitleRecord and XblSocialManagerEvent to avoid XUserHandle autoreleases.
- Fixed abort pulling Gdk Dll's into the project when the GDK is not installed on the local machine.
- Fixed API's with optional parameters to accept null.
- Fixed XUserGetTokenAndSignatureUtf16Async to support null arrays passed to it for the Header and Body parameters.
- Fixed player could crash after XblSocialManagerCreateSocialUserGroupFromFilters call.

### Changed
- Updated the included fall-back XGameRuntime thunks DLL.

## [1.0.0] - 2024-03-28
### Fixed
- Speech Synthesizer Sample: Avoid trying to close already closed speech synthesizer.
- Speech Synthesizer Sample: Make UI active/inactive based on speech synthesizer state.

### Changed
- Bump package version


## [1.0.0-pre.3] - 2024-03-18
### Added
- Added support for XblPrivacyCheckPermissionForAnonymousUserAsync.

### Changed
- Bumped version to keep inline with other related packages.
- Revised documentation


## [0.2.0] - 2024-02-06
### Changed
- Bumped version to keep inline with other related packages.
- Updated APIs


## [0.1.0] - 2024-01-31
### Added
- Preview implementation of API support
- Preview samples
- Preview documentation


### This is the first release of *Unity Package com.unity.microsoft.gdk*.
*Initial release.*
