﻿// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！


using System;
using GameFrameX.Runtime;

namespace GameFrameX.Sound.Runtime
{
    public sealed partial class SoundManager : GameFrameworkModule, ISoundManager
    {
        /// <summary>
        /// 声音代理。
        /// </summary>
        private sealed class SoundAgent : ISoundAgent
        {
            /// <summary>
            /// 所在的声音组。
            /// </summary>
            private readonly SoundGroup m_SoundGroup;

            /// <summary>
            /// 声音辅助器。
            /// </summary>
            private readonly ISoundHelper m_SoundHelper;

            /// <summary>
            /// 声音代理辅助器。
            /// </summary>
            private readonly ISoundAgentHelper m_SoundAgentHelper;

            /// <summary>
            /// 声音的序列编号。
            /// </summary>
            private int m_SerialId;

            /// <summary>
            /// 声音资源。
            /// </summary>
            private object m_SoundAsset;

            /// <summary>
            /// 设置声音资源的时间。
            /// </summary>
            private DateTime m_SetSoundAssetTime;

            /// <summary>
            /// 在声音组内是否静音。
            /// </summary>
            private bool m_MuteInSoundGroup;

            /// <summary>
            /// 在声音组内音量大小。
            /// </summary>
            private float m_VolumeInSoundGroup;

            /// <summary>
            /// 初始化声音代理的新实例。
            /// </summary>
            /// <param name="soundGroup">所在的声音组。</param>
            /// <param name="soundHelper">声音辅助器接口。</param>
            /// <param name="soundAgentHelper">声音代理辅助器接口。</param>
            public SoundAgent(SoundGroup soundGroup, ISoundHelper soundHelper, ISoundAgentHelper soundAgentHelper)
            {
                if (soundGroup == null)
                {
                    throw new GameFrameworkException("Sound group is invalid.");
                }

                if (soundHelper == null)
                {
                    throw new GameFrameworkException("Sound helper is invalid.");
                }

                if (soundAgentHelper == null)
                {
                    throw new GameFrameworkException("Sound agent helper is invalid.");
                }

                m_SoundGroup = soundGroup;
                m_SoundHelper = soundHelper;
                m_SoundAgentHelper = soundAgentHelper;
                m_SoundAgentHelper.ResetSoundAgent += OnResetSoundAgent;
                m_SerialId = 0;
                m_SoundAsset = null;
                Reset();
            }

            /// <summary>
            /// 获取所在的声音组。
            /// </summary>
            public ISoundGroup SoundGroup
            {
                get { return m_SoundGroup; }
            }

            /// <summary>
            /// 获取或设置声音的序列编号。
            /// </summary>
            public int SerialId
            {
                get { return m_SerialId; }
                set { m_SerialId = value; }
            }

            /// <summary>
            /// 获取当前是否正在播放。
            /// </summary>
            public bool IsPlaying
            {
                get { return m_SoundAgentHelper.IsPlaying; }
            }

            /// <summary>
            /// 获取声音长度。
            /// </summary>
            public float Length
            {
                get { return m_SoundAgentHelper.Length; }
            }

            /// <summary>
            /// 获取或设置播放位置。
            /// </summary>
            public float Time
            {
                get { return m_SoundAgentHelper.Time; }
                set { m_SoundAgentHelper.Time = value; }
            }

            /// <summary>
            /// 获取是否静音。
            /// </summary>
            public bool Mute
            {
                get { return m_SoundAgentHelper.Mute; }
            }

            /// <summary>
            /// 获取或设置在声音组内是否静音。
            /// </summary>
            public bool MuteInSoundGroup
            {
                get { return m_MuteInSoundGroup; }
                set
                {
                    m_MuteInSoundGroup = value;
                    RefreshMute();
                }
            }

            /// <summary>
            /// 获取或设置是否循环播放。
            /// </summary>
            public bool Loop
            {
                get { return m_SoundAgentHelper.Loop; }
                set { m_SoundAgentHelper.Loop = value; }
            }

            /// <summary>
            /// 获取或设置声音优先级。
            /// </summary>
            public int Priority
            {
                get { return m_SoundAgentHelper.Priority; }
                set { m_SoundAgentHelper.Priority = value; }
            }

            /// <summary>
            /// 获取音量大小。
            /// </summary>
            public float Volume
            {
                get { return m_SoundAgentHelper.Volume; }
            }

            /// <summary>
            /// 获取或设置在声音组内音量大小。
            /// </summary>
            public float VolumeInSoundGroup
            {
                get { return m_VolumeInSoundGroup; }
                set
                {
                    m_VolumeInSoundGroup = value;
                    RefreshVolume();
                }
            }

            /// <summary>
            /// 获取或设置声音音调。
            /// </summary>
            public float Pitch
            {
                get { return m_SoundAgentHelper.Pitch; }
                set { m_SoundAgentHelper.Pitch = value; }
            }

            /// <summary>
            /// 获取或设置声音立体声声相。
            /// </summary>
            public float PanStereo
            {
                get { return m_SoundAgentHelper.PanStereo; }
                set { m_SoundAgentHelper.PanStereo = value; }
            }

            /// <summary>
            /// 获取或设置声音空间混合量。
            /// </summary>
            public float SpatialBlend
            {
                get { return m_SoundAgentHelper.SpatialBlend; }
                set { m_SoundAgentHelper.SpatialBlend = value; }
            }

            /// <summary>
            /// 获取或设置声音最大距离。
            /// </summary>
            public float MaxDistance
            {
                get { return m_SoundAgentHelper.MaxDistance; }
                set { m_SoundAgentHelper.MaxDistance = value; }
            }

            /// <summary>
            /// 获取或设置声音多普勒等级。
            /// </summary>
            public float DopplerLevel
            {
                get { return m_SoundAgentHelper.DopplerLevel; }
                set { m_SoundAgentHelper.DopplerLevel = value; }
            }

            /// <summary>
            /// 获取声音代理辅助器。
            /// </summary>
            public ISoundAgentHelper Helper
            {
                get { return m_SoundAgentHelper; }
            }

            /// <summary>
            /// 获取声音创建时间。
            /// </summary>
            internal DateTime SetSoundAssetTime
            {
                get { return m_SetSoundAssetTime; }
            }

            /// <summary>
            /// 播放声音。
            /// </summary>
            public void Play()
            {
                m_SoundAgentHelper.Play(Constant.DefaultFadeInSeconds);
            }

            /// <summary>
            /// 播放声音。
            /// </summary>
            /// <param name="fadeInSeconds">声音淡入时间，以秒为单位。</param>
            public void Play(float fadeInSeconds)
            {
                m_SoundAgentHelper.Play(fadeInSeconds);
            }

            /// <summary>
            /// 停止播放声音。
            /// </summary>
            public void Stop()
            {
                m_SoundAgentHelper.Stop(Constant.DefaultFadeOutSeconds);
            }

            /// <summary>
            /// 停止播放声音。
            /// </summary>
            /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
            public void Stop(float fadeOutSeconds)
            {
                m_SoundAgentHelper.Stop(fadeOutSeconds);
            }

            /// <summary>
            /// 暂停播放声音。
            /// </summary>
            public void Pause()
            {
                m_SoundAgentHelper.Pause(Constant.DefaultFadeOutSeconds);
            }

            /// <summary>
            /// 暂停播放声音。
            /// </summary>
            /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
            public void Pause(float fadeOutSeconds)
            {
                m_SoundAgentHelper.Pause(fadeOutSeconds);
            }

            /// <summary>
            /// 恢复播放声音。
            /// </summary>
            public void Resume()
            {
                m_SoundAgentHelper.Resume(Constant.DefaultFadeInSeconds);
            }

            /// <summary>
            /// 恢复播放声音。
            /// </summary>
            /// <param name="fadeInSeconds">声音淡入时间，以秒为单位。</param>
            public void Resume(float fadeInSeconds)
            {
                m_SoundAgentHelper.Resume(fadeInSeconds);
            }

            /// <summary>
            /// 重置声音代理。
            /// </summary>
            public void Reset()
            {
                if (m_SoundAsset != null)
                {
                    m_SoundHelper.ReleaseSoundAsset(m_SoundAsset);
                    m_SoundAsset = null;
                }

                m_SetSoundAssetTime = DateTime.MinValue;
                Time = Constant.DefaultTime;
                MuteInSoundGroup = Constant.DefaultMute;
                Loop = Constant.DefaultLoop;
                Priority = Constant.DefaultPriority;
                VolumeInSoundGroup = Constant.DefaultVolume;
                Pitch = Constant.DefaultPitch;
                PanStereo = Constant.DefaultPanStereo;
                SpatialBlend = Constant.DefaultSpatialBlend;
                MaxDistance = Constant.DefaultMaxDistance;
                DopplerLevel = Constant.DefaultDopplerLevel;
                m_SoundAgentHelper.Reset();
            }

            /// <summary>
            /// 设置声音资源。
            /// </summary>
            /// <param name="soundAsset">声音资源。</param>
            /// <returns>是否设置声音资源成功。</returns>
            internal bool SetSoundAsset(object soundAsset)
            {
                Reset();
                m_SoundAsset = soundAsset;
                m_SetSoundAssetTime = DateTime.UtcNow;
                return m_SoundAgentHelper.SetSoundAsset(soundAsset);
            }

            /// <summary>
            /// 刷新静音设置。
            /// </summary>
            internal void RefreshMute()
            {
                m_SoundAgentHelper.Mute = m_SoundGroup.Mute || m_MuteInSoundGroup;
            }

            /// <summary>
            /// 刷新音量设置。
            /// </summary>
            internal void RefreshVolume()
            {
                m_SoundAgentHelper.Volume = m_SoundGroup.Volume * m_VolumeInSoundGroup;
            }

            /// <summary>
            /// 重置声音代理事件的回调函数。
            /// </summary>
            /// <param name="sender">事件发送者。</param>
            /// <param name="e">事件参数。</param>
            private void OnResetSoundAgent(object sender, ResetSoundAgentEventArgs e)
            {
                Reset();
            }
        }
    }
}