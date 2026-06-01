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

## Features

- **Sound Groups** — Organize sounds into named groups (BGM, SFX, Voice, etc.) with independent volume, mute, and agent count settings
- **Priority-based Playback** — Async playback via UniTask with automatic low-priority eviction when all agents are busy
- **Full Lifecycle Control** — Play, stop, pause, resume with optional fade-in/fade-out durations
- **Spatial Audio** — Bind sounds to game entities or place them at specific world positions
- **AudioMixer Integration** — Route sound groups to specific AudioMixer groups for fine-grained mixing
- **Event-driven Notifications** — Success, failure, and update events dispatched through the GameFrameX Event system
- **Reference Pooling** — All event args and play parameters are pooled to minimize GC allocation

## Quick Start

### Installation (choose one)

1. Add to `manifest.json` dependencies:
   ```json
   {"com.gameframex.unity.sound": "https://github.com/GameFrameX/com.gameframex.unity.sound.git"}
   ```

2. Unity Package Manager → `Add package from git URL`: https://github.com/GameFrameX/com.gameframex.unity.sound.git

3. Download and place in your project's `Packages` directory.

### Basic Usage

```csharp
var sound = GameEntry.GetComponent<SoundComponent>();

// Play a sound in the "BGM" group
int serialId = await sound.PlaySound("Assets/Audio/bgm.mp3", "BGM");

// Stop with fade-out
sound.StopSound(serialId, fadeOutSeconds: 1f);

// Pause / Resume
sound.PauseSound(serialId);
sound.ResumeSound(serialId, fadeInSeconds: 0.5f);

// Set group volume
sound.SetVolume("BGM", 0.5f);
```

### Using SoundPlayOptions

For complex play requests, use `SoundPlayOptions` instead of matching a specific overload:

```csharp
int serialId = await sound.PlaySound("Assets/Audio/explosion.mp3", "SFX",
    new SoundPlayOptions
    {
        Loop = false,
        Volume = 0.8f,
        Priority = 5,
        BindingEntity = enemyEntity,
        FadeInSeconds = 0.2f,
        Pitch = 1.2f,
        SpatialBlend = 1.0f
    });
```

### Spatial Audio

```csharp
// Bind to an entity — AudioSource follows the entity's transform
int serialId = await sound.PlaySound("Assets/Audio/footstep.mp3", "SFX",
    bindingEntity: playerEntity);

// Play at a world position
int serialId = await sound.PlaySound("Assets/Audio/explosion.mp3", "SFX",
    worldPosition: explosionPos);
```

### Listening to Events

```csharp
// Subscribe via EventComponent
var eventComponent = GameEntry.GetComponent<EventComponent>();
eventComponent.Subscribe(PlaySoundSuccessEventArgs.EventId, OnPlaySuccess);
eventComponent.Subscribe(PlaySoundFailureEventArgs.EventId, OnPlayFailure);

void OnPlaySuccess(object sender, GameEventArgs e)
{
    var args = (PlaySoundSuccessEventArgs)e;
    Debug.Log($"Sound playing: {args.SoundAssetName}, duration: {args.Duration}s");
}

void OnPlayFailure(object sender, GameEventArgs e)
{
    var args = (PlaySoundFailureEventArgs)e;
    Debug.LogWarning($"Sound failed: {args.SoundAssetName}, error: {args.ErrorMessage}");
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
