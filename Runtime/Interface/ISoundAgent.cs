// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！


namespace GameFrameX.Sound.Runtime
{
    /// <summary>
    /// 声音代理接口。
    /// </summary>
    public interface ISoundAgent
    {
        /// <summary>
        /// 获取所在的声音组。
        /// </summary>
        ISoundGroup SoundGroup { get; }

        /// <summary>
        /// 获取声音的序列编号。
        /// </summary>
        int SerialId { get; }

        /// <summary>
        /// 获取当前是否正在播放。
        /// </summary>
        bool IsPlaying { get; }

        /// <summary>
        /// 获取声音长度。
        /// </summary>
        float Length { get; }

        /// <summary>
        /// 获取或设置播放位置。
        /// </summary>
        float Time { get; set; }

        /// <summary>
        /// 获取或设置是否静音。
        /// </summary>
        bool Mute { get; }

        /// <summary>
        /// 获取或设置在声音组内是否静音。
        /// </summary>
        bool MuteInSoundGroup { get; set; }

        /// <summary>
        /// 获取或设置是否循环播放。
        /// </summary>
        bool Loop { get; set; }

        /// <summary>
        /// 获取或设置声音优先级。
        /// </summary>
        int Priority { get; set; }

        /// <summary>
        /// 获取音量大小。
        /// </summary>
        float Volume { get; }

        /// <summary>
        /// 获取或设置在声音组内音量大小。
        /// </summary>
        float VolumeInSoundGroup { get; set; }

        /// <summary>
        /// 获取或设置声音音调。
        /// </summary>
        float Pitch { get; set; }

        /// <summary>
        /// 获取或设置声音立体声声相。
        /// </summary>
        float PanStereo { get; set; }

        /// <summary>
        /// 获取或设置声音空间混合量。
        /// </summary>
        float SpatialBlend { get; set; }

        /// <summary>
        /// 获取或设置声音最大距离。
        /// </summary>
        float MaxDistance { get; set; }

        /// <summary>
        /// 获取或设置声音多普勒等级。
        /// </summary>
        float DopplerLevel { get; set; }

        /// <summary>
        /// 获取声音代理辅助器。
        /// </summary>
        ISoundAgentHelper Helper { get; }

        /// <summary>
        /// 播放声音。
        /// </summary>
        void Play();

        /// <summary>
        /// 播放声音。
        /// </summary>
        /// <param name="fadeInSeconds">声音淡入时间，以秒为单位。</param>
        void Play(float fadeInSeconds);

        /// <summary>
        /// 停止播放声音。
        /// </summary>
        void Stop();

        /// <summary>
        /// 停止播放声音。
        /// </summary>
        /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
        void Stop(float fadeOutSeconds);

        /// <summary>
        /// 暂停播放声音。
        /// </summary>
        void Pause();

        /// <summary>
        /// 暂停播放声音。
        /// </summary>
        /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
        void Pause(float fadeOutSeconds);

        /// <summary>
        /// 恢复播放声音。
        /// </summary>
        void Resume();

        /// <summary>
        /// 恢复播放声音。
        /// </summary>
        /// <param name="fadeInSeconds">声音淡入时间，以秒为单位。</param>
        void Resume(float fadeInSeconds);

        /// <summary>
        /// 重置声音代理。
        /// </summary>
        void Reset();
    }
}