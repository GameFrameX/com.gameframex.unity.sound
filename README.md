<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Sound

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.sound)](https://github.com/GameFrameX/com.gameframex.unity.sound/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.sound)](https://github.com/GameFrameX/com.gameframex.unity.sound/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams

<br />

[Documentation](https://gameframex.doc.alianblank.com) · [Quick Start](#quick-start) · QQ Group: 467608841 / 233840761

<br />

**English** | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## Features

- **Sound Groups** — Organize sounds into named groups (BGM, SFX, Voice, etc.) with independent volume, mute, and agent count settings
- **Priority-based Playback** — Async playback via UniTask with automatic low-priority eviction when all agents are busy
- **Full Lifecycle Control** — Play, stop, pause, resume with optional fade-in/fade-out durations
- **Spatial Audio** — Bind sounds to game entities or place them at specific world positions
- **AudioMixer Integration** — Route sound groups to specific AudioMixer groups for fine-grained mixing
- **Event-driven Notifications** — Success, failure, and update events dispatched through the GameFrameX Event system
- **Reference Pooling** — All event args and play parameters are pooled to minimize GC allocation

## Quick Start

### Installation

Edit your Unity project's `Packages/manifest.json` and add the `scopedRegistries` section:

```json
{
  "scopedRegistries": [
    {
      "name": "GameFrameX",
      "url": "https://gameframex.upm.alianblank.uk",
      "scopes": [
        "com.gameframex"
      ]
    }
  ]
}
```

`scopes` controls which packages are resolved through this registry. Only packages whose names start with `com.gameframex` will be fetched from it.

Then add the package to `dependencies`:

```json
{
  "dependencies": {
    "com.gameframex.unity.sound": "1.2.0"
  }
}
```


## Architecture

```
SoundComponent (MonoBehaviour)
 └── SoundManager (engine-agnostic logic)
      └── SoundGroup (named group: BGM, SFX, ...)
           └── SoundAgent (single voice slot)
                └── SoundAgentHelper (AudioSource wrapper)
```

| Concept | Description |
|---------|-------------|
| **SoundGroup** | A named collection of agents. Each group has its own volume, mute state, and configurable agent count. |
| **SoundAgent** | Represents one concurrent voice. The number of agents per group determines how many sounds can play simultaneously. |
| **SoundPlayContext** | Carries binding entity, world position, and user data for a play request. |
| **PlaySoundParams** | Pooled parameter object for audio properties (volume, pitch, fade, spatial blend, etc.). |

## Dependencies

| Package | Description |
|---------|-------------|
| `com.gameframex.unity` | Core framework (module system, reference pool, utilities) |
| `com.gameframex.unity.asset` | Asset loading (YooAsset integration) |
| `com.gameframex.unity.entity` | Entity system for spatial audio binding |
| `com.gameframex.unity.event` | Event system for playback notifications |

## Documentation & Resources

- [Official Documentation](https://gameframex.doc.alianblank.com)

## Community & Support

- QQ Group: [Join](https://qm.qq.com/q/3dIpogITg)

## Changelog

See [Releases](https://github.com/GameFrameX/com.gameframex.unity.sound/releases) for changelog.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE.md) file for details.
