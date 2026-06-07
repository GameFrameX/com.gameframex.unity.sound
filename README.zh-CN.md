<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Sound

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.sound)](https://github.com/GameFrameX/com.gameframex.unity.sound/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.sound)](https://github.com/GameFrameX/com.gameframex.unity.sound/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使

<br />

[文档](https://gameframex.doc.alianblank.com) · [快速开始](#快速开始) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | **简体中文** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## 功能特性

- **声音分组** — 将声音组织为命名分组（BGM、SFX、Voice 等），每组拥有独立的音量、静音和代理数量设置
- **优先级播放** — 通过 UniTask 实现异步播放，当所有代理忙碌时自动驱逐低优先级声音
- **完整生命周期控制** — 播放、停止、暂停、恢复，支持可选的淡入/淡出时长
- **空间音频** — 将声音绑定到游戏实体，或放置在指定的世界坐标位置
- **AudioMixer 集成** — 将声音分组路由到特定的 AudioMixer 分组，实现精细化混音
- **事件驱动通知** — 通过 GameFrameX 事件系统分发播放成功、失败和更新事件
- **引用池化** — 所有事件参数和播放参数均使用引用池，最小化 GC 分配

## 快速开始

### 安装

编辑 Unity 项目的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：

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

`scopes` 控制哪些包通过此注册表解析。只有以 `com.gameframex` 开头的包才会从这个注册表获取。

Then add the package to `dependencies`:

```json
{
  "dependencies": {
    "com.gameframex.unity.sound": "1.2.0"
  }
}
```


## 架构

```
SoundComponent (MonoBehaviour)
 └── SoundManager (引擎无关的逻辑层)
      └── SoundGroup (命名分组：BGM, SFX, ...)
           └── SoundAgent (单个发声通道)
                └── SoundAgentHelper (AudioSource 封装)
```

| 概念 | 说明 |
|------|------|
| **SoundGroup** | 代理的命名集合。每个分组拥有独立的音量、静音状态和可配置的代理数量。 |
| **SoundAgent** | 表示一个并发发声通道。每组的代理数量决定了可同时播放的声音数量。 |
| **SoundPlayContext** | 承载播放请求的绑定实体、世界坐标和用户数据。 |
| **PlaySoundParams** | 池化的音频属性参数对象（音量、音调、淡入淡出、空间混合等）。 |

## 依赖

| 包 | 说明 |
|----|------|
| `com.gameframex.unity` | 核心框架（模块系统、引用池、工具类） |
| `com.gameframex.unity.asset` | 资源加载（YooAsset 集成） |
| `com.gameframex.unity.entity` | 实体系统，用于空间音频绑定 |
| `com.gameframex.unity.event` | 事件系统，用于播放通知 |

## 文档与资源

- [官方文档](https://gameframex.doc.alianblank.com)

## 社区与支持

- QQ 群：[加入](https://qm.qq.com/q/3dIpogITg)

## 更新日志

查看 [Releases](https://github.com/GameFrameX/com.gameframex.unity.sound/releases) 了解更新日志。

## 开源协议

详见 [LICENSE.md](LICENSE.md) 文件。
