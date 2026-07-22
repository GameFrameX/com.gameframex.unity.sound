## [1.3.1](https://github.com/gameframex/com.gameframex.unity.sound/compare/1.3.0...1.3.1) (2026-07-22)


### Bug Fixes

* **sound:** 触发 PR 5 补丁版本更新 ([63fb27b](https://github.com/gameframex/com.gameframex.unity.sound/commit/63fb27b3dde757f01c7ddb6fa56b20fe82d49527)), closes [#5](https://github.com/gameframex/com.gameframex.unity.sound/issues/5)

# [1.3.0](https://github.com/gameframex/com.gameframex.unity.sound/compare/1.2.2...1.3.0) (2026-07-03)


### Features

* **sound:** 添加音频组件初始化兜底机制 ([e5a5a5e](https://github.com/gameframex/com.gameframex.unity.sound/commit/e5a5a5e340bbc553f50756d4c15d83cc73b596f3))

## [1.2.2](https://github.com/gameframex/com.gameframex.unity.sound/compare/1.2.1...1.2.2) (2026-06-08)


### Bug Fixes

* **sound:** 修复事件回调直接传递原始 EventArgs 的问题 ([8c2ef33](https://github.com/gameframex/com.gameframex.unity.sound/commit/8c2ef33e9078b738020d4b3e31a9281781bdafe8))

## [1.2.1](https://github.com/gameframex/com.gameframex.unity.sound/compare/1.2.0...1.2.1) (2026-06-07)


### Bug Fixes

* 补全包规范文件（LICENSE/CHANGELOG/URL 字段/unity 字段） ([688bac6](https://github.com/gameframex/com.gameframex.unity.sound/commit/688bac62000c48157d9cc1726076aef677dca867))

# [1.2.0](https://github.com/gameframex/com.gameframex.unity.sound/compare/1.1.2...1.2.0) (2026-06-01)


### Bug Fixes

* **sound:** 修复测试编译错误 ([9fa9a2f](https://github.com/gameframex/com.gameframex.unity.sound/commit/9fa9a2f848e04745919eba913df696628ae83e1f))
* **sound:** 取消注释 EventArgs 的 ReferencePool.Release 调用 ([1ceded7](https://github.com/gameframex/com.gameframex.unity.sound/commit/1ceded713d562c4a8c80c51422a8cd01357b0784))
* **sound:** 同步 ISoundManager 接口与 int? serialId ([7fe45dc](https://github.com/gameframex/com.gameframex.unity.sound/commit/7fe45dc65c8c17f16bf01a7187aa9490b681de58))
* **sound:** 处理 PlaySound 异步回调中的同步完成情况 ([8f8faea](https://github.com/gameframex/com.gameframex.unity.sound/commit/8f8faea7195535b6178b9ff1d4f6e3a3af501c62))
* **sound:** 将 GameFrameX.Entity.Runtime 添加到测试 asmdef ([d481507](https://github.com/gameframex/com.gameframex.unity.sound/commit/d481507f2f0e01a004769b386e309526179c4b93))
* **sound:** 恢复 ReleaseSoundAsset 实现 ([81eb379](https://github.com/gameframex/com.gameframex.unity.sound/commit/81eb379e520939199bc758effbf94515b74f29f1))
* **sound:** 用 Constant.DefaultPriority 替换 SoundComponent.DefaultPriority ([04fe62d](https://github.com/gameframex/com.gameframex.unity.sound/commit/04fe62d7a7f04a08308aaa6f87893b938f02e825))
* **sound:** 用精确的 StopCoroutine 替换 StopAllCoroutines ([c7a13ab](https://github.com/gameframex/com.gameframex.unity.sound/commit/c7a13abf4ded17a35e9d1b34bf1104e850cea83b))
* **sound:** 补充 SoundPlayOptions 重载中缺失的 await ([c070540](https://github.com/gameframex/com.gameframex.unity.sound/commit/c070540adee8c1add47aef64d9480fd0867270ac))
* **sound:** 防止 Update 中重复触发 ResetSoundAgent 事件 ([d6bdb77](https://github.com/gameframex/com.gameframex.unity.sound/commit/d6bdb778c309900020a56c6545ebb9f60eb51f93))


### Features

* **sound:** 添加 SoundPlayOptions 参数对象 ([4caa256](https://github.com/gameframex/com.gameframex.unity.sound/commit/4caa25622e507319bfe834118d834b7027f9c00a))

## [1.1.2](https://github.com/gameframex/com.gameframex.unity.sound/compare/1.1.1...1.1.2) (2026-05-28)


### Bug Fixes

* **ci:** 统一 .github 工作流配置 ([e513ab7](https://github.com/gameframex/com.gameframex.unity.sound/commit/e513ab7b29b988e06c6f61582ab59ca361c961d6))
* **sound:** 补充 package.json 中缺失的 Entity 包依赖 ([5d6ae02](https://github.com/gameframex/com.gameframex.unity.sound/commit/5d6ae027c70bb5e357140bd2256e0fa36fedd5ca))

## [1.1.1](https://github.com/gameframex/com.gameframex.unity.sound/compare/1.1.0...1.1.1) (2026-03-16)


### Bug Fixes

* 修复获取资源加载耗时的属性名错误 ([317cac4](https://github.com/gameframex/com.gameframex.unity.sound/commit/317cac44b8cc21da5e7b8e0b38d0fae755038aa8))

# [1.1.0](https://github.com/gameframex/com.gameframex.unity.sound/compare/1.0.6...1.1.0) (2025-12-23)


### Features

* **ci:** change ci ([37d15f4](https://github.com/gameframex/com.gameframex.unity.sound/commit/37d15f4db842915fd5da70ee4598e6dfb491e88e))

# Changelog

## [1.0.6](https://github.com/GameFrameX/com.gameframex.unity.sound/tree/1.0.6) (2025-10-25)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.sound/compare/1.0.5...1.0.6)

**Merged pull requests:**

- \[修复\]  修复调用Pause接口后，会被 Reset 数据 bug [\#2](https://github.com/GameFrameX/com.gameframex.unity.sound/pull/2) ([DreamChaseAndVera](https://github.com/DreamChaseAndVera))

## [1.0.5](https://github.com/GameFrameX/com.gameframex.unity.sound/tree/1.0.5) (2025-06-21)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.sound/compare/1.0.4...1.0.5)

## [1.0.4](https://github.com/GameFrameX/com.gameframex.unity.sound/tree/1.0.4) (2025-06-01)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.sound/compare/1.0.3...1.0.4)

## [1.0.3](https://github.com/GameFrameX/com.gameframex.unity.sound/tree/1.0.3) (2025-05-31)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.sound/compare/1.0.2...1.0.3)

## [1.0.2](https://github.com/GameFrameX/com.gameframex.unity.sound/tree/1.0.2) (2025-05-30)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.sound/compare/1.0.1...1.0.2)

**Merged pull requests:**

- \[增加\]1. 播放声音功能传递的播放声音参数 [\#1](https://github.com/GameFrameX/com.gameframex.unity.sound/pull/1) ([aflycat](https://github.com/aflycat))

## [1.0.1](https://github.com/GameFrameX/com.gameframex.unity.sound/tree/1.0.1) (2024-11-09)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.sound/compare/1.0.0...1.0.1)

## [1.0.0](https://github.com/GameFrameX/com.gameframex.unity.sound/tree/1.0.0) (2024-04-10)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.sound/compare/29a5bc0adff0ef29f2f3be6ecc8234f802d966ed...1.0.0)



\* *This Changelog was automatically generated by [github_changelog_generator](https://github.com/github-changelog-generator/github-changelog-generator)*
