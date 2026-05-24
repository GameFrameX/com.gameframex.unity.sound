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
  獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使
</p>

<p align="center">
  <a href="https://gameframex.doc.alianblank.com">文檔</a> ·
  <a href="#快速開始">快速開始</a> ·
  <a href="https://qm.qq.com/q/3dIpogITg">QQ群</a> ·
  語言: <a href="README.md">English</a> ·
  <a href="README.zh-CN.md">简体中文</a> ·
  **繁體中文** ·
  <a href="README.ja.md">日本語</a> ·
  <a href="README.ko.md">한국어</a>
</p>

---

## 項目簡介

GameFrameX.Sound 是 GameFrameX 框架的聲音管理組件。提供音訊播放、暫停、停止、音量控制等常用音訊操作。支援 MP3、WAV、OGG 等多種音訊格式，採用現代 C# async/await 模式，使音訊操作更加簡單高效。

## 快速開始

### 安裝方式（任選其一）

1. 直接在 `manifest.json` 的 `dependencies` 節點下加入以下內容：
   ```json
   {"com.gameframex.unity.sound": "https://github.com/GameFrameX/com.gameframex.unity.sound.git"}
   ```

2. 在 Unity 的 `Packages Manager` 中使用 `Git URL` 的方式添加庫，地址為：https://github.com/GameFrameX/com.gameframex.unity.sound.git

3. 直接下載倉庫放置到 Unity 專案的 `Packages` 目錄下，會自動載入識別。

### 使用範例

```csharp
// 獲取聲音組件
var soundComponent = GameEntry.GetComponent<SoundComponent>();

// 播放音訊
soundComponent.PlaySound("audio_path", "audio_group");

// 停止音訊
soundComponent.StopSound("audio_path");

// 暫停音訊
soundComponent.PauseSound("audio_path");

// 恢復音訊
soundComponent.ResumeSound("audio_path");

// 設定音量
soundComponent.SetVolume("audio_group", 0.5f);
```

## 文檔與資源

- [官方文檔](https://gameframex.doc.alianblank.com)

## 社區與支援

- QQ群: [加入](https://qm.qq.com/q/3dIpogITg)

## 更新日誌

查看 [Releases](https://github.com/GameFrameX/com.gameframex.unity.sound/releases) 了解更新日誌。

## 開源協議

本專案基於 MIT 協議開源 - 詳見 [LICENSE](https://github.com/GameFrameX/com.gameframex.unity.sound/blob/main/LICENSE) 檔案。
