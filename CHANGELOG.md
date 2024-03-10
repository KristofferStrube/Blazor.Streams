# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.4.1] - 2024-03-10
### Fixed
- Fixed that `ReadableStreamBYOBReaderInProcess` did not invoke read on the `JSReference` itself.

## [0.4.0] - 2024-03-10
### Deprecated
- Deprecated `ArrayBufferView` as the `IArrayBufferView` interface from `Blazor.WebIDL` should be used instead.
### Changed
- Changed the version of `Blazor.WebIDL` to use the newest version which is `0.5.0`.
### Added
- Added `Signal` property to `StreamPipeOptions` using the `AbortSignal` type from `Blazor.DOM`.
- Added `ReadableStreamBYOBReaderReadOptions` parameter to `ReadableStreamBYOBReader.ReadAsync` method and in-process version.

## [0.3.0] - 2023-03-15
### Changed
- Changed .NET version to `7.0`.
- Marked all `Create` methods as `Obsolete` and suggests to use `CreateAsync` instead.
### Added
- Added `IGenericTransformStream` and `IGenericTransformStreamInProcess` interfaces for use with the `PipeThroughAsync` method.
- Added the generation of a documentation file packaging all XML comments with the package.
- Added `CreateAsync` methods to all wrapper instances as an alternative to the previous synchronous `Create` methods.

## [0.2.2] - 2022-11-09
### Fixed
- Fixed that `WritableStream`' did not call `CloseAsync` when invoking `DisposeAsync`.

## [0.2.1] - 2022-11-09
### Fixed
- Fixed that `WritableStream`'s constructor was internal and made it protected instead.

## [0.2.0] - 2022-10-31
### Added
- `ReadableStream` now extends `Stream` and can be read from by other .NET streams using `CopyToAsync`.
- `WritableStream` now extends `Stream` and can be written to by other .NET streams using `CopyToAsync`.

## [0.1.0] - 2022-10-30
### Added
- Made the initial implementation that covers most of the API.