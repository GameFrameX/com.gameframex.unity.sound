<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Sound

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.sound)](https://github.com/GameFrameX/com.gameframex.unity.sound/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.sound)](https://github.com/GameFrameX/com.gameframex.unity.sound/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

인디 게임 개발자를 위한 올인원 솔루션 · 인디 개발자의 꿈을 실현

<br />

[문서](https://gameframex.doc.alianblank.com) · [빠른 시작](#quick-start) · [QQ 그룹](https://qm.qq.com/q/3dIpogITg)

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | **한국어**

</div>
## 기능

- **사운드 그룹** — 사운드를 명명된 그룹(BGM, SFX, Voice 등)으로 구성하며, 독립적인 볼륨, 음소거 및 에이전트 수 설정을 지원합니다
- **우선순위 기반 재생** — UniTask를 통한 비동기 재생, 모든 에이전트가 사용 중일 때 낮은 우선순위 자동 교체
- **전체 수명주기 제어** — 재생, 정지, 일시정지, 재개 및 선택적 페이드인/페이드아웃 시간 설정
- **공간 오디오** — 사운드를 게임 엔티티에 바인딩하거나 특정 월드 위치에 배치
- **AudioMixer 통합** — 사운드 그룹을 특정 AudioMixer 그룹으로 라우팅하여 세밀한 믹싱 가능
- **이벤트 기반 알림** — 성공, 실패 및 업데이트 이벤트가 GameFrameX 이벤트 시스템을 통해 전달됩니다
- **참조 풀링** — 모든 이벤트 인수 및 재생 매개변수가 풀링되어 GC 할당을 최소화합니다

## 빠른 시작

### 설치 (택 1)

1. `manifest.json` 종속성에 추가:
   ```json
   {"com.gameframex.unity.sound": "https://github.com/GameFrameX/com.gameframex.unity.sound.git"}
   ```

2. Unity Package Manager → `Add package from git URL`: https://github.com/GameFrameX/com.gameframex.unity.sound.git

3. 다운로드하여 프로젝트의 `Packages` 디렉토리에 배치합니다.

### 기본 사용법

```csharp
var sound = GameEntry.GetComponent<SoundComponent>();

// "BGM" 그룹에서 사운드 재생
int serialId = await sound.PlaySound("Assets/Audio/bgm.mp3", "BGM");

// 페이드아웃으로 정지
sound.StopSound(serialId, fadeOutSeconds: 1f);

// 일시정지 / 재개
sound.PauseSound(serialId);
sound.ResumeSound(serialId, fadeInSeconds: 0.5f);

// 그룹 볼륨 설정
sound.SetVolume("BGM", 0.5f);
```

### SoundPlayOptions 사용

복잡한 재생 요청의 경우 특정 오버로드 대신 `SoundPlayOptions`를 사용하세요:

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

### 공간 오디오

```csharp
// 엔티티에 바인딩 — AudioSource가 엔티티의 Transform을 따라갑니다
int serialId = await sound.PlaySound("Assets/Audio/footstep.mp3", "SFX",
    bindingEntity: playerEntity);

// 월드 위치에서 재생
int serialId = await sound.PlaySound("Assets/Audio/explosion.mp3", "SFX",
    worldPosition: explosionPos);
```

### 이벤트 수신

```csharp
// EventComponent를 통해 구독
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

## 아키텍처

```
SoundComponent (MonoBehaviour)
 └── SoundManager (엔진 독립적 로직)
      └── SoundGroup (명명된 그룹: BGM, SFX, ...)
           └── SoundAgent (단일 보이스 슬롯)
                └── SoundAgentHelper (AudioSource 래퍼)
```

| 개념 | 설명 |
|------|------|
| **SoundGroup** | 에이전트의 명명된 컬렉션입니다. 각 그룹은 자체 볼륨, 음소거 상태 및 구성 가능한 에이전트 수를 가집니다. |
| **SoundAgent** | 하나의 동시 재생 보이스를 나타냅니다. 그룹당 에이전트 수는 동시에 재생할 수 있는 사운드 수를 결정합니다. |
| **SoundPlayContext** | 재생 요청에 대한 바인딩 엔티티, 월드 위치 및 사용자 데이터를 전달합니다. |
| **PlaySoundParams** | 오디오 속성(볼륨, 피치, 페이드, 공간 블렌드 등)을 위한 풀링된 매개변수 객체입니다. |

## 종속성

| 패키지 | 설명 |
|--------|------|
| `com.gameframex.unity` | 코어 프레임워크 (모듈 시스템, 참조 풀, 유틸리티) |
| `com.gameframex.unity.asset` | 에셋 로딩 (YooAsset 통합) |
| `com.gameframex.unity.entity` | 공간 오디오 바인딩을 위한 엔티티 시스템 |
| `com.gameframex.unity.event` | 재생 알림을 위한 이벤트 시스템 |

## 문서 및 자료

- [공식 문서](https://gameframex.doc.alianblank.com)

## 커뮤니티 및 지원

- QQ 그룹: [참여](https://qm.qq.com/q/3dIpogITg)

## 변경 이력

변경 이력은 [Releases](https://github.com/GameFrameX/com.gameframex.unity.sound/releases)를 참조하세요.

## 라이선스

이 프로젝트는 MIT 라이선스에 따라 라이선스가 부여됩니다 — 자세한 내용은 [LICENSE](LICENSE.md) 파일을 참조하세요.
