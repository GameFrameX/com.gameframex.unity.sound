// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！


using GameFrameX.Sound;
using System;
using UnityEngine;
using UnityEngine.Audio;

namespace GameFrameX.Sound.Runtime
{
    /// <summary>
    /// 声音代理辅助器基类。
    /// </summary>
    public abstract class SoundAgentHelperBase : MonoBehaviour, ISoundAgentHelper
    {
        /// <summary>
        /// 获取当前是否正在播放。
        /// </summary>
        public abstract bool IsPlaying { get; }

        /// <summary>
        /// 获取声音长度。
        /// </summary>
        public abstract float Length { get; }

        /// <summary>
        /// 获取或设置播放位置。
        /// </summary>
        public abstract float Time { get; set; }

        /// <summary>
        /// 获取或设置是否静音。
        /// </summary>
        public abstract bool Mute { get; set; }

        /// <summary>
        /// 获取或设置是否循环播放。
        /// </summary>
        public abstract bool Loop { get; set; }

        /// <summary>
        /// 获取或设置声音优先级。
        /// </summary>
        public abstract int Priority { get; set; }

        /// <summary>
        /// 获取或设置音量大小。
        /// </summary>
        public abstract float Volume { get; set; }

        /// <summary>
        /// 获取或设置声音音调。
        /// </summary>
        public abstract float Pitch { get; set; }

        /// <summary>
        /// 获取或设置声音立体声声相。
        /// </summary>
        public abstract float PanStereo { get; set; }

        /// <summary>
        /// 获取或设置声音空间混合量。
        /// </summary>
        public abstract float SpatialBlend { get; set; }

        /// <summary>
        /// 获取或设置声音最大距离。
        /// </summary>
        public abstract float MaxDistance { get; set; }

        /// <summary>
        /// 获取或设置声音多普勒等级。
        /// </summary>
        public abstract float DopplerLevel { get; set; }

        /// <summary>
        /// 获取或设置声音代理辅助器所在的混音组。
        /// </summary>
        public abstract AudioMixerGroup AudioMixerGroup { get; set; }

        /// <summary>
        /// 重置声音代理事件。
        /// </summary>
        public abstract event EventHandler<ResetSoundAgentEventArgs> ResetSoundAgent;

        /// <summary>
        /// 播放声音。
        /// </summary>
        /// <param name="fadeInSeconds">声音淡入时间，以秒为单位。</param>
        public abstract void Play(float fadeInSeconds);

        /// <summary>
        /// 停止播放声音。
        /// </summary>
        /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
        public abstract void Stop(float fadeOutSeconds);

        /// <summary>
        /// 暂停播放声音。
        /// </summary>
        /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
        public abstract void Pause(float fadeOutSeconds);

        /// <summary>
        /// 恢复播放声音。
        /// </summary>
        /// <param name="fadeInSeconds">声音淡入时间，以秒为单位。</param>
        public abstract void Resume(float fadeInSeconds);

        /// <summary>
        /// 重置声音代理辅助器。
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// 设置声音资源。
        /// </summary>
        /// <param name="soundAsset">声音资源。</param>
        /// <returns>是否设置声音资源成功。</returns>
        public abstract bool SetSoundAsset(object soundAsset);

        /// <summary>
        /// 设置声音绑定的实体。
        /// </summary>
        /// <param name="bindingEntity">声音绑定的实体。</param>
        public abstract void SetBindingEntity(Entity.Runtime.Entity bindingEntity);

        /// <summary>
        /// 设置声音所在的世界坐标。
        /// </summary>
        /// <param name="worldPosition">声音所在的世界坐标。</param>
        public abstract void SetWorldPosition(Vector3 worldPosition);
    }
}