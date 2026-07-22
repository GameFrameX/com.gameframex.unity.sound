using System;
using GameFrameX.Runtime;
using GameFrameX.Sound.Runtime;
using NUnit.Framework;

namespace GameFrameX.Sound.Tests
{
    /// <summary>
    /// 声音模块单元测试。
    /// </summary>
    /// <remarks>
    /// 覆盖可脱离 Unity 运行时独立测试的纯 C# 逻辑。
    /// Unity 运行时依赖的类（SoundManager、SoundComponent、AgentHelper）
    /// 需要 Unity Test Framework + PlayMode 测试，不在本文件覆盖。
    /// </remarks>
    internal class UnitTests
    {
        #region PlaySoundErrorCode

        /// <summary>
        /// 验证枚举值定义完整性。
        /// </summary>
        [Test]
        public void PlaySoundErrorCode_HasAllExpectedValues()
        {
            Assert.That(Enum.IsDefined(typeof(PlaySoundErrorCode), PlaySoundErrorCode.Unknown), Is.True);
            Assert.That(Enum.IsDefined(typeof(PlaySoundErrorCode), PlaySoundErrorCode.SoundGroupNotExist), Is.True);
            Assert.That(Enum.IsDefined(typeof(PlaySoundErrorCode), PlaySoundErrorCode.SoundGroupHasNoAgent), Is.True);
            Assert.That(Enum.IsDefined(typeof(PlaySoundErrorCode), PlaySoundErrorCode.LoadAssetFailure), Is.True);
            Assert.That(Enum.IsDefined(typeof(PlaySoundErrorCode), PlaySoundErrorCode.IgnoredDueToLowPriority), Is.True);
            Assert.That(Enum.IsDefined(typeof(PlaySoundErrorCode), PlaySoundErrorCode.SetSoundAssetFailure), Is.True);
        }

        /// <summary>
        /// 验证 Unknown 默认值为 0。
        /// </summary>
        [Test]
        public void PlaySoundErrorCode_UnknownIsZero()
        {
            Assert.That((int)PlaySoundErrorCode.Unknown, Is.EqualTo(0));
        }

        #endregion

        #region PlaySoundParams

        /// <summary>
        /// 验证 Create() 返回非 null 实例。
        /// </summary>
        [Test]
        public void PlaySoundParams_Create_ReturnsNonNull()
        {
            PlaySoundParams sut = PlaySoundParams.Create();
            Assert.That(sut, Is.Not.Null);
            ReferencePool.Release(sut);
        }

        /// <summary>
        /// 验证 Create() 的默认值与 Constant 一致。
        /// </summary>
        [Test]
        public void PlaySoundParams_Create_DefaultsMatchConstants()
        {
            PlaySoundParams sut = PlaySoundParams.Create();
            Assert.That(sut.Time, Is.EqualTo(0f));
            Assert.That(sut.MuteInSoundGroup, Is.False);
            Assert.That(sut.Loop, Is.False);
            Assert.That(sut.Priority, Is.EqualTo(0));
            Assert.That(sut.VolumeInSoundGroup, Is.EqualTo(1f));
            Assert.That(sut.FadeInSeconds, Is.EqualTo(0f));
            Assert.That(sut.Pitch, Is.EqualTo(1f));
            Assert.That(sut.PanStereo, Is.EqualTo(0f));
            Assert.That(sut.SpatialBlend, Is.EqualTo(0f));
            Assert.That(sut.MaxDistance, Is.EqualTo(100f));
            Assert.That(sut.DopplerLevel, Is.EqualTo(1f));
            ReferencePool.Release(sut);
        }

        /// <summary>
        /// 验证 Create(true) 设置 Loop = true。
        /// </summary>
        [Test]
        public void PlaySoundParams_CreateWithLoop_SetsLoop()
        {
            PlaySoundParams sut = PlaySoundParams.Create(true);
            Assert.That(sut.Loop, Is.True);
            ReferencePool.Release(sut);
        }

        /// <summary>
        /// 验证属性 get/set 的正确性。
        /// </summary>
        [Test]
        public void PlaySoundParams_Properties_SetAndGet()
        {
            PlaySoundParams sut = PlaySoundParams.Create();

            sut.Time = 1.5f;
            Assert.That(sut.Time, Is.EqualTo(1.5f));

            sut.MuteInSoundGroup = true;
            Assert.That(sut.MuteInSoundGroup, Is.True);

            sut.Loop = true;
            Assert.That(sut.Loop, Is.True);

            sut.Priority = 128;
            Assert.That(sut.Priority, Is.EqualTo(128));

            sut.VolumeInSoundGroup = 0.5f;
            Assert.That(sut.VolumeInSoundGroup, Is.EqualTo(0.5f));

            sut.FadeInSeconds = 0.3f;
            Assert.That(sut.FadeInSeconds, Is.EqualTo(0.3f));

            sut.Pitch = 2f;
            Assert.That(sut.Pitch, Is.EqualTo(2f));

            sut.PanStereo = 0.5f;
            Assert.That(sut.PanStereo, Is.EqualTo(0.5f));

            sut.SpatialBlend = 1f;
            Assert.That(sut.SpatialBlend, Is.EqualTo(1f));

            sut.MaxDistance = 500f;
            Assert.That(sut.MaxDistance, Is.EqualTo(500f));

            sut.DopplerLevel = 0f;
            Assert.That(sut.DopplerLevel, Is.EqualTo(0f));

            ReferencePool.Release(sut);
        }

        /// <summary>
        /// 验证 Clear() 重置所有属性为默认值。
        /// </summary>
        [Test]
        public void PlaySoundParams_Clear_ResetsDefaults()
        {
            PlaySoundParams sut = PlaySoundParams.Create();

            sut.Time = 1.5f;
            sut.Priority = 128;
            sut.VolumeInSoundGroup = 0.5f;

            sut.Clear();

            Assert.That(sut.Time, Is.EqualTo(0f));
            Assert.That(sut.Priority, Is.EqualTo(0));
            Assert.That(sut.VolumeInSoundGroup, Is.EqualTo(1f));

            ReferencePool.Release(sut);
        }

        /// <summary>
        /// 验证 ReferencePool 的 Acquire/Release 循环复用。
        /// </summary>
        [Test]
        public void PlaySoundParams_ReferencePoolReusesInstance()
        {
            PlaySoundParams first = PlaySoundParams.Create();
            first.Priority = 99;
            int firstHash = first.GetHashCode();
            ReferencePool.Release(first);

            PlaySoundParams second = PlaySoundParams.Create();
            Assert.That(second.Priority, Is.EqualTo(0), "Clear should reset after Release");
            Assert.That(second.GetHashCode(), Is.EqualTo(firstHash), "ReferencePool should reuse the same instance");
            ReferencePool.Release(second);
        }

        #endregion

        #region SoundPlayContext

        /// <summary>
        /// 验证创建和属性赋值。
        /// </summary>
        [Test]
        public void SoundPlayContext_Create_SetsProperties()
        {
            object userData = "test";
            SoundPlayContext sut = SoundPlayContext.Create(null, UnityEngine.Vector3.one, userData);

            Assert.That(sut.BindingEntity, Is.Null);
            Assert.That(sut.WorldPosition, Is.EqualTo(UnityEngine.Vector3.one));
            Assert.That(sut.UserData, Is.EqualTo("test"));

            ReferencePool.Release(sut);
        }

        /// <summary>
        /// 验证 Clear() 重置属性。
        /// </summary>
        [Test]
        public void SoundPlayContext_Clear_ResetsProperties()
        {
            object userData = "test";
            SoundPlayContext sut = SoundPlayContext.Create(null, UnityEngine.Vector3.one, userData);

            sut.Clear();

            Assert.That(sut.BindingEntity, Is.Null);
            Assert.That(sut.WorldPosition, Is.EqualTo(UnityEngine.Vector3.zero));
            Assert.That(sut.UserData, Is.Null);

            ReferencePool.Release(sut);
        }

        #endregion

        #region PlaySoundFailureEventArgs

        /// <summary>
        /// 验证创建和属性赋值。
        /// </summary>
        [Test]
        public void PlaySoundFailureEventArgs_Create_SetsProperties()
        {
            PlaySoundParams soundParams = PlaySoundParams.Create();
            object userData = "test";

            PlaySoundFailureEventArgs sut = PlaySoundFailureEventArgs.Create(
                1, "test_asset", "test_group", soundParams,
                PlaySoundErrorCode.SoundGroupNotExist, "Group not found", userData);

            Assert.That(sut.SerialId, Is.EqualTo(1));
            Assert.That(sut.SoundAssetName, Is.EqualTo("test_asset"));
            Assert.That(sut.SoundGroupName, Is.EqualTo("test_group"));
            Assert.That(sut.PlaySoundParams, Is.SameAs(soundParams));
            Assert.That(sut.ErrorCode, Is.EqualTo(PlaySoundErrorCode.SoundGroupNotExist));
            Assert.That(sut.ErrorMessage, Is.EqualTo("Group not found"));
            Assert.That(sut.UserData, Is.EqualTo("test"));
            Assert.That(sut.Id, Is.EqualTo(typeof(PlaySoundFailureEventArgs).FullName));

            ReferencePool.Release(sut);
            ReferencePool.Release(soundParams);
        }

        /// <summary>
        /// 验证 Clear() 重置属性。
        /// </summary>
        [Test]
        public void PlaySoundFailureEventArgs_Clear_ResetsProperties()
        {
            PlaySoundParams soundParams = PlaySoundParams.Create();
            PlaySoundFailureEventArgs sut = PlaySoundFailureEventArgs.Create(
                1, "test", "group", soundParams,
                PlaySoundErrorCode.Unknown, "error", null);

            sut.Clear();

            Assert.That(sut.SerialId, Is.EqualTo(0));
            Assert.That(sut.SoundAssetName, Is.Null);
            Assert.That(sut.SoundGroupName, Is.Null);
            Assert.That(sut.PlaySoundParams, Is.Null);
            Assert.That(sut.ErrorCode, Is.EqualTo(PlaySoundErrorCode.Unknown));
            Assert.That(sut.ErrorMessage, Is.Null);
            Assert.That(sut.UserData, Is.Null);

            ReferencePool.Release(soundParams);
            ReferencePool.Release(sut);
        }

        #endregion

        #region PlaySoundSuccessEventArgs

        /// <summary>
        /// 验证创建和属性赋值（ISoundAgent 传 null 测试属性传递）。
        /// </summary>
        [Test]
        public void PlaySoundSuccessEventArgs_Create_SetsProperties()
        {
            PlaySoundSuccessEventArgs sut = PlaySoundSuccessEventArgs.Create(
                42, "success_asset.wav", null, 1.5f, "user");

            Assert.That(sut.SerialId, Is.EqualTo(42));
            Assert.That(sut.SoundAssetName, Is.EqualTo("success_asset.wav"));
            Assert.That(sut.SoundAgent, Is.Null);
            Assert.That(sut.Duration, Is.EqualTo(1.5f));
            Assert.That(sut.UserData, Is.EqualTo("user"));
            Assert.That(sut.Id, Is.EqualTo(typeof(PlaySoundSuccessEventArgs).FullName));

            ReferencePool.Release(sut);
        }

        /// <summary>
        /// 验证 Clear() 重置属性。
        /// </summary>
        [Test]
        public void PlaySoundSuccessEventArgs_Clear_ResetsProperties()
        {
            PlaySoundSuccessEventArgs sut = PlaySoundSuccessEventArgs.Create(
                42, "asset", null, 1.5f, "user");

            sut.Clear();

            Assert.That(sut.SerialId, Is.EqualTo(0));
            Assert.That(sut.SoundAssetName, Is.Null);
            Assert.That(sut.SoundAgent, Is.Null);
            Assert.That(sut.Duration, Is.EqualTo(0f));
            Assert.That(sut.UserData, Is.Null);

            ReferencePool.Release(sut);
        }

        #endregion

        #region PlaySoundUpdateEventArgs

        /// <summary>
        /// 验证创建和属性赋值。
        /// </summary>
        [Test]
        public void PlaySoundUpdateEventArgs_Create_SetsProperties()
        {
            PlaySoundParams soundParams = PlaySoundParams.Create();
            PlaySoundUpdateEventArgs sut = PlaySoundUpdateEventArgs.Create(
                3, "loading_asset", "bgm", soundParams, 0.75f, "ctx");

            Assert.That(sut.SerialId, Is.EqualTo(3));
            Assert.That(sut.SoundAssetName, Is.EqualTo("loading_asset"));
            Assert.That(sut.SoundGroupName, Is.EqualTo("bgm"));
            Assert.That(sut.PlaySoundParams, Is.SameAs(soundParams));
            Assert.That(sut.Progress, Is.EqualTo(0.75f));
            Assert.That(sut.UserData, Is.EqualTo("ctx"));
            Assert.That(sut.Id, Is.EqualTo(typeof(PlaySoundUpdateEventArgs).FullName));

            ReferencePool.Release(sut);
            ReferencePool.Release(soundParams);
        }

        /// <summary>
        /// 验证 Clear() 重置属性。
        /// </summary>
        [Test]
        public void PlaySoundUpdateEventArgs_Clear_ResetsProperties()
        {
            PlaySoundParams soundParams = PlaySoundParams.Create();
            PlaySoundUpdateEventArgs sut = PlaySoundUpdateEventArgs.Create(
                3, "asset", "group", soundParams, 0.75f, "ctx");

            sut.Clear();

            Assert.That(sut.SerialId, Is.EqualTo(0));
            Assert.That(sut.SoundAssetName, Is.Null);
            Assert.That(sut.SoundGroupName, Is.Null);
            Assert.That(sut.PlaySoundParams, Is.Null);
            Assert.That(sut.Progress, Is.EqualTo(0f));
            Assert.That(sut.UserData, Is.Null);

            ReferencePool.Release(soundParams);
            ReferencePool.Release(sut);
        }

        #endregion

        #region ResetSoundAgentEventArgs

        /// <summary>
        /// 验证创建和 Id。
        /// </summary>
        [Test]
        public void ResetSoundAgentEventArgs_Create_ReturnsNonNull()
        {
            ResetSoundAgentEventArgs sut = ResetSoundAgentEventArgs.Create();
            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.Id, Is.EqualTo(typeof(ResetSoundAgentEventArgs).FullName));
            ReferencePool.Release(sut);
        }

        /// <summary>
        /// 验证 Clear() 不抛出异常（当前无字段）。
        /// </summary>
        [Test]
        public void ResetSoundAgentEventArgs_Clear_DoesNotThrow()
        {
            ResetSoundAgentEventArgs sut = ResetSoundAgentEventArgs.Create();
            Assert.DoesNotThrow(() => sut.Clear());
            ReferencePool.Release(sut);
        }

        #endregion
    }
}