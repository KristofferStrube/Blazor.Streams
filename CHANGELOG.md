# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]
### Added
- `ReadableStream` now extends `Stream` and can be read from by other .NET streams using `CopyToAsync`.
- `WritableStream` now extends `Stream` and can be written to by other .NET streams using `CopyToAsync`.

## [0.1.0] - 2022-10-30
### Added
- Made the initial implementation that covers most of the API.