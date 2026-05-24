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
  인디 게임 개발자를 위한 올인원 솔루션 · 인디 개발자의 꿈을 실현
</p>

<p align="center">
  <a href="https://gameframex.doc.alianblank.com">문서</a> ·
  <a href="#빠른-시작">빠른 시작</a> ·
  <a href="https://qm.qq.com/q/3dIpogITg">QQ 그룹</a> ·
  언어: <a href="README.md">English</a> ·
  <a href="README.zh-CN.md">简体中文</a> ·
  <a href="README.zh-TW.md">繁體中文</a> ·
  <a href="README.ja.md">日本語</a> ·
  **한국어**
</p>

---

## 프로젝트 개요

GameFrameX.Sound는 GameFrameX 프레임워크의 사운드 관리 컴포넌트입니다. 오디오 재생, 일시 정지, 정지, 볼륨 제어 등 일반적인 오디오 작업을 제공합니다. MP3, WAV, OGG 등 여러 오디오 형식을 지원하며, 최신 C# async/await 패턴을 사용합니다.

## 빠른 시작

### 설치 (선택)

1. `manifest.json`의 `dependencies` 섹션에 다음을 추가:
   ```json
   {"com.gameframex.unity.sound": "https://github.com/GameFrameX/com.gameframex.unity.sound.git"}
   ```

2. Unity의 Package Manager에서 `Git URL`을 사용하여 패키지 추가: https://github.com/GameFrameX/com.gameframex.unity.sound.git

3. 리포지토리를 다운로드하여 Unity 프로젝트의 `Packages` 디렉토리에 배치하면 자동으로 로드됩니다.

### 사용 예시

```csharp
// 사운드 컴포넌트 가져오기
var soundComponent = GameEntry.GetComponent<SoundComponent>();

// 오디오 재생
soundComponent.PlaySound("audio_path", "audio_group");

// 오디오 정지
soundComponent.StopSound("audio_path");

// 오디오 일시 정지
soundComponent.PauseSound("audio_path");

// 오디오 재개
soundComponent.ResumeSound("audio_path");

// 볼륨 설정
soundComponent.SetVolume("audio_group", 0.5f);
```

## 문서 및 자료

- [공식 문서](https://gameframex.doc.alianblank.com)

## 커뮤니티 및 지원

- QQ 그룹: [가입](https://qm.qq.com/q/3dIpogITg)

## 변경 로그

변경 로그는 [Releases](https://github.com/GameFrameX/com.gameframex.unity.sound/releases)에서 확인하세요.

## 라이선스

이 프로젝트는 MIT 라이선스에 따라 배포됩니다 - 자세한 내용은 [LICENSE](https://github.com/GameFrameX/com.gameframex.unity.sound/blob/main/LICENSE) 파일을 참조하세요.
