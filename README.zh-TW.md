<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Sound

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.sound)](https://github.com/GameFrameX/com.gameframex.unity.sound/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.sound)](https://github.com/GameFrameX/com.gameframex.unity.sound/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使

<br />

[文檔](https://gameframex.doc.alianblank.com) · [快速開始](#快速開始) · [QQ群](https://qm.qq.com/q/3dIpogITg)

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | **繁體中文** | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>
## 功能特性

- **音效分組** — 將音效組織到命名分組中（BGM、SFX、Voice 等），每個分組可獨立設定音量、靜音和代理數量
- **基於優先級的播放** — 透過 UniTask 進行異步播放，當所有代理都忙碌時自動驅逐低優先級音效
- **完整的生命週期控制** — 播放、停止、暫停、恢復，支援可選的淡入/淡出時長
- **空間音訊** — 將音效綁定到遊戲實體或放置在特定的世界座標位置
- **AudioMixer 整合** — 將音效分組路由到特定的 AudioMixer 分組，實現精細的混音控制
- **事件驅動通知** — 透過 GameFrameX 事件系統分發成功、失敗和更新事件
- **引用池化** — 所有事件參數和播放參數均使用對象池，以最小化 GC 分配

## 快速開始

### 安裝（選擇一種方式）

1. 在 `manifest.json` 的 dependencies 中新增：
   ```json
   {"com.gameframex.unity.sound": "https://github.com/GameFrameX/com.gameframex.unity.sound.git"}
   ```

2. Unity Package Manager → `Add package from git URL`：https://github.com/GameFrameX/com.gameframex.unity.sound.git

3. 下載後放入專案的 `Packages` 目錄中。

### 基本用法

```csharp
var sound = GameEntry.GetComponent<SoundComponent>();

// 在 "BGM" 分組中播放音效
int serialId = await sound.PlaySound("Assets/Audio/bgm.mp3", "BGM");

// 帶淡出效果停止
sound.StopSound(serialId, fadeOutSeconds: 1f);

// 暫停 / 恢復
sound.PauseSound(serialId);
sound.ResumeSound(serialId, fadeInSeconds: 0.5f);

// 設定分組音量
sound.SetVolume("BGM", 0.5f);
```

### 使用 SoundPlayOptions

對於複雜的播放請求，可以使用 `SoundPlayOptions` 代替匹配特定的方法多載：

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

### 空間音訊

```csharp
// 綁定到實體 — AudioSource 會跟隨實體的 Transform
int serialId = await sound.PlaySound("Assets/Audio/footstep.mp3", "SFX",
    bindingEntity: playerEntity);

// 在世界座標位置播放
int serialId = await sound.PlaySound("Assets/Audio/explosion.mp3", "SFX",
    worldPosition: explosionPos);
```

### 監聽事件

```csharp
// 透過 EventComponent 訂閱
var eventComponent = GameEntry.GetComponent<EventComponent>();
eventComponent.Subscribe(PlaySoundSuccessEventArgs.EventId, OnPlaySuccess);
eventComponent.Subscribe(PlaySoundFailureEventArgs.EventId, OnPlayFailure);

void OnPlaySuccess(object sender, GameEventArgs e)
{
    var args = (PlaySoundSuccessEventArgs)e;
    Debug.Log($"音效播放中：{args.SoundAssetName}，時長：{args.Duration}s");
}

void OnPlayFailure(object sender, GameEventArgs e)
{
    var args = (PlaySoundFailureEventArgs)e;
    Debug.LogWarning($"音效播放失敗：{args.SoundAssetName}，錯誤：{args.ErrorMessage}");
}
```

## 架構

```
SoundComponent (MonoBehaviour)
 └── SoundManager (引擎無關的邏輯層)
      └── SoundGroup（命名分組：BGM、SFX、...）
           └── SoundAgent（單一發聲通道）
                └── SoundAgentHelper（AudioSource 封裝）
```

| 概念 | 說明 |
|------|------|
| **SoundGroup** | 命名的代理集合。每個分組擁有獨立的音量、靜音狀態和可設定的代理數量。 |
| **SoundAgent** | 代表一個併發發聲通道。每個分組的代理數量決定了可同時播放的音效數量。 |
| **SoundPlayContext** | 攜帶綁定實體、世界座標和使用者資料的播放請求上下文。 |
| **PlaySoundParams** | 用於音訊屬性（音量、音高、淡入淡出、空間混合等）的池化參數物件。 |

## 依賴

| 套件 | 說明 |
|------|------|
| `com.gameframex.unity` | 核心框架（模組系統、引用池、工具類） |
| `com.gameframex.unity.asset` | 資源載入（YooAsset 整合） |
| `com.gameframex.unity.entity` | 實體系統，用於空間音訊綁定 |
| `com.gameframex.unity.event` | 事件系統，用於播放通知 |

## 文檔與資源

- [官方文檔](https://gameframex.doc.alianblank.com)

## 社群與支援

- QQ 群：[加入](https://qm.qq.com/q/3dIpogITg)

## 更新日誌

請參閱 [Releases](https://github.com/GameFrameX/com.gameframex.unity.sound/releases) 頁面查看更新日誌。

## 授權

本專案採用 MIT 授權條款 — 詳情請參閱 [LICENSE](LICENSE.md) 檔案。
