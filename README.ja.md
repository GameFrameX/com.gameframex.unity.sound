<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Sound

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.sound)](https://github.com/GameFrameX/com.gameframex.unity.sound/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.sound)](https://github.com/GameFrameX/com.gameframex.unity.sound/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

インディゲーム開発者向けオールインワンソリューション · インディ開発者の夢を支援

<br />

[ドキュメント](https://gameframex.doc.alianblank.com) · [クイックスタート](#クイックスタート) · QQグループ: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | **日本語** | [한국어](README.ko.md)

</div>
## 特徴

- **サウンドグループ** — BGM、SFX、ボイスなど、名前付きグループでサウンドを整理。グループごとに独立した音量、ミュート、エージェント数を設定可能
- **優先度ベースの再生** — UniTask を使用した非同期再生。全エージェントがビジーの場合、低優先度のサウンドを自動的に除外
- **完全なライフサイクル制御** — 再生、停止、一時停止、再開。フェードイン/フェードアウト時間の指定にも対応
- **空間オーディオ** — ゲームエンティティにサウンドをバインド、または特定のワールド座標に配置
- **AudioMixer 統合** — サウンドグループを特定の AudioMixer グループにルーティングし、きめ細かなミキシングが可能
- **イベント駆動の通知** — 成功、失敗、更新イベントを GameFrameX イベントシステムを通じて配信
- **参照プーリング** — すべてのイベント引数と再生パラメータはプーリングされ、GC アロケーションを最小化

## クイックスタート

### インストール（いずれかを選択）

1. `manifest.json` の dependencies に追加:
   ```json
   {"com.gameframex.unity.sound": "https://github.com/GameFrameX/com.gameframex.unity.sound.git"}
   ```

2. Unity Package Manager → `Add package from git URL`: https://github.com/GameFrameX/com.gameframex.unity.sound.git

3. ダウンロードしてプロジェクトの `Packages` ディレクトリに配置。

### 基本的な使い方

```csharp
var sound = GameEntry.GetComponent<SoundComponent>();

// "BGM" グループでサウンドを再生
int serialId = await sound.PlaySound("Assets/Audio/bgm.mp3", "BGM");

// フェードアウト付きで停止
sound.StopSound(serialId, fadeOutSeconds: 1f);

// 一時停止 / 再開
sound.PauseSound(serialId);
sound.ResumeSound(serialId, fadeInSeconds: 0.5f);

// グループの音量を設定
sound.SetVolume("BGM", 0.5f);
```

### SoundPlayOptions の使用

複雑な再生リクエストには、特定のオーバーロードに合わせる代わりに `SoundPlayOptions` を使用します:

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

### 空間オーディオ

```csharp
// エンティティにバインド — AudioSource がエンティティの Transform に追従
int serialId = await sound.PlaySound("Assets/Audio/footstep.mp3", "SFX",
    bindingEntity: playerEntity);

// ワールド座標で再生
int serialId = await sound.PlaySound("Assets/Audio/explosion.mp3", "SFX",
    worldPosition: explosionPos);
```

### イベントの購読

```csharp
// EventComponent を通じて購読
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

## アーキテクチャ

```
SoundComponent (MonoBehaviour)
 └── SoundManager (エンジン非依存のロジック)
      └── SoundGroup (名前付きグループ: BGM, SFX, ...)
           └── SoundAgent (単一のボイススロット)
                └── SoundAgentHelper (AudioSource ラッパー)
```

| 概念 | 説明 |
|---------|-------------|
| **SoundGroup** | エージェントの名前付きコレクション。各グループは独自の音量、ミュート状態、設定可能なエージェント数を持ちます。 |
| **SoundAgent** | 1つの同時発音ボイスを表します。グループあたりのエージェント数により、同時に再生可能なサウンド数が決まります。 |
| **SoundPlayContext** | 再生リクエストに対するバインドエンティティ、ワールド座標、ユーザーデータを保持します。 |
| **PlaySoundParams** | オーディオプロパティ（音量、ピッチ、フェード、空間ブレンドなど）のためのプーリングされたパラメータオブジェクト。 |

## 依存パッケージ

| パッケージ | 説明 |
|---------|-------------|
| `com.gameframex.unity` | コアフレームワーク（モジュールシステム、参照プール、ユーティリティ） |
| `com.gameframex.unity.asset` | アセット読み込み（YooAsset 統合） |
| `com.gameframex.unity.entity` | 空間オーディオバインディング用エンティティシステム |
| `com.gameframex.unity.event` | 再生通知用イベントシステム |

## ドキュメントとリソース

- [公式ドキュメント](https://gameframex.doc.alianblank.com)

## コミュニティとサポート

- QQ グループ: [参加](https://qm.qq.com/q/3dIpogITg)

## 変更履歴

変更履歴については [Releases](https://github.com/GameFrameX/com.gameframex.unity.sound/releases) をご覧ください。

## ライセンス

このプロジェクトは MIT ライセンスの下でライセンスされています。詳細は [LICENSE](LICENSE.md) ファイルをご覧ください。
