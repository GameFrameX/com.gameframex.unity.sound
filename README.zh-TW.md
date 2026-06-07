<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Sound

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.sound)](https://github.com/GameFrameX/com.gameframex.unity.sound/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.sound)](https://github.com/GameFrameX/com.gameframex.unity.sound/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使

<br />

[文檔](https://gameframex.doc.alianblank.com) · [快速開始](#快速開始) · QQ群: 467608841 / 233840761

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

### 安裝

選擇以下任一方式：

1. 編輯 Unity 專案的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：
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
     ],
     "dependencies": {
       "com.gameframex.unity.sound": "1.2.0"
     }
   }
   ```

   `scopes` 控制哪些套件透過此註冊表解析。只有以 `com.gameframex` 開頭的套件才會從這個註冊表取得。

2. 直接在 `manifest.json` 的 `dependencies` 節點下添加以下內容：
   ```json
   {
      "com.gameframex.unity.sound": "https://github.com/gameframex/com.gameframex.unity.sound.git"
   }
   ```
3. 在 Unity 的 `Package Manager` 中使用 `Git URL` 的方式添加庫，地址為：`https://github.com/gameframex/com.gameframex.unity.sound.git`
4. 直接下載倉庫放置到 Unity 專案的 `Packages` 目錄下，會自動載入識別。
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
