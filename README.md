<p align="center">
  <img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="GameFrameX Logo" width="160" />
</p>

<h1 align="center">GameFrameX Sound</h1>

<p align="center">
  <a href="https://github.com/GameFrameX/com.gameframex.unity.sound/releases">
    <img src="https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.sound?style=flat-square" alt="Version" />
  </a>
  <a href="https://github.com/GameFrameX/com.gameframex.unity.sound/blob/main/LICENSE">
    <img src="https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.sound?style=flat-square" alt="License" />
  </a>
  <a href="https://gameframex.doc.alianblank.com">
    <img src="https://img.shields.io/badge/Documentation-online-blue?style=flat-square" alt="Documentation" />
  </a>
</p>

<p align="center">
  All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams
</p>

<p align="center">
  <a href="https://gameframex.doc.alianblank.com">Documentation</a> ·
  <a href="#quick-start">Quick Start</a> ·
  <a href="https://qm.qq.com/q/3dIpogITg">QQ Group</a> ·
  Language: **English** ·
  <a href="README.zh-CN.md">简体中文</a> ·
  <a href="README.zh-TW.md">繁體中文</a> ·
  <a href="README.ja.md">日本語</a> ·
  <a href="README.ko.md">한국어</a>
</p>

---

## Project Overview

GameFrameX.Sound is the Sound component for the GameFrameX framework. It provides audio playback, pause, stop, volume control, and other common audio operations. It supports multiple audio formats including MP3, WAV, OGG, and uses modern C# async/await patterns for efficient audio management.

## Quick Start

### Installation (choose one)

1. Add the following to the `dependencies` section of your `manifest.json`:
   ```json
   {"com.gameframex.unity.sound": "https://github.com/GameFrameX/com.gameframex.unity.sound.git"}
   ```

2. In Unity's Package Manager, use `Git URL` to add the package: https://github.com/GameFrameX/com.gameframex.unity.sound.git

3. Download the repository and place it in your Unity project's `Packages` directory. It will be loaded automatically.

### Usage

```csharp
// Get the Sound component
var soundComponent = GameEntry.GetComponent<SoundComponent>();

// Play audio
soundComponent.PlaySound("audio_path", "audio_group");

// Stop audio
soundComponent.StopSound("audio_path");

// Pause audio
soundComponent.PauseSound("audio_path");

// Resume audio
soundComponent.ResumeSound("audio_path");

// Set volume
soundComponent.SetVolume("audio_group", 0.5f);
```

## Documentation & Resources

- [Official Documentation](https://gameframex.doc.alianblank.com)

## Community & Support

- QQ Group: [Join](https://qm.qq.com/q/3dIpogITg)

## Changelog

See [Releases](https://github.com/GameFrameX/com.gameframex.unity.sound/releases) for changelog.

## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/GameFrameX/com.gameframex.unity.sound/blob/main/LICENSE) file for details.
