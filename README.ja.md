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
  インディゲーム開発者向けオールインワンソリューション · インディ開発者の夢を支援
</p>

<p align="center">
  <a href="https://gameframex.doc.alianblank.com">ドキュメント</a> ·
  <a href="#クイックスタート">クイックスタート</a> ·
  <a href="https://qm.qq.com/q/3dIpogITg">QQグループ</a> ·
  言語: <a href="README.md">English</a> ·
  <a href="README.zh-CN.md">简体中文</a> ·
  <a href="README.zh-TW.md">繁體中文</a> ·
  **日本語** ·
  <a href="README.ko.md">한국어</a>
</p>

---

## プロジェクト概要

GameFrameX.Sound は GameFrameX フレームワークのサウンド管理コンポーネントです。オーディオの再生、一時停止、停止、音量調整など、一般的なオーディオ操作を提供します。MP3、WAV、OGG などの複数のオーディオフォーマットに対応し、最新の C# async/await パターンを採用しています。

## クイックスタート

### インストール（いずれかを選択）

1. `manifest.json` の `dependencies` セクションに以下を追加：
   ```json
   {"com.gameframex.unity.sound": "https://github.com/GameFrameX/com.gameframex.unity.sound.git"}
   ```

2. Unity の Package Manager で `Git URL` を使用してパッケージを追加：https://github.com/GameFrameX/com.gameframex.unity.sound.git

3. リポジトリをダウンロードして Unity プロジェクトの `Packages` ディレクトリに配置。自動的にロードされます。

### 使用例

```csharp
// サウンドコンポーネントを取得
var soundComponent = GameEntry.GetComponent<SoundComponent>();

// オーディオの再生
soundComponent.PlaySound("audio_path", "audio_group");

// オーディオの停止
soundComponent.StopSound("audio_path");

// オーディオの一時停止
soundComponent.PauseSound("audio_path");

// オーディオの再開
soundComponent.ResumeSound("audio_path");

// 音量の設定
soundComponent.SetVolume("audio_group", 0.5f);
```

## ドキュメントとリソース

- [公式ドキュメント](https://gameframex.doc.alianblank.com)

## コミュニティとサポート

- QQグループ: [参加](https://qm.qq.com/q/3dIpogITg)

## 変更履歴

変更履歴は [Releases](https://github.com/GameFrameX/com.gameframex.unity.sound/releases) をご覧ください。

## ライセンス

このプロジェクトは MIT ライセンスの下で公開されています - 詳細は [LICENSE](https://github.com/GameFrameX/com.gameframex.unity.sound/blob/main/LICENSE) ファイルをご覧ください。
