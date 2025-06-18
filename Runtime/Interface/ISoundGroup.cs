// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！


namespace GameFrameX.Sound.Runtime
{
    /// <summary>
    /// 声音组接口。
    /// </summary>
    public interface ISoundGroup
    {
        /// <summary>
        /// 获取声音组名称。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 获取声音代理数。
        /// </summary>
        int SoundAgentCount { get; }

        /// <summary>
        /// 获取或设置声音组中的声音是否避免被同优先级声音替换。
        /// </summary>
        bool AvoidBeingReplacedBySamePriority { get; set; }

        /// <summary>
        /// 获取或设置声音组静音。
        /// </summary>
        bool Mute { get; set; }

        /// <summary>
        /// 获取或设置声音组音量。
        /// </summary>
        float Volume { get; set; }

        /// <summary>
        /// 获取声音组辅助器。
        /// </summary>
        ISoundGroupHelper Helper { get; }

        /// <summary>
        /// 是否正在播放声音。
        /// </summary>
        /// <param name="serialId">声音的序列编号。</param>
        /// <returns>正在播放则返回Ture,否则返回False,找不到指定的序列编号也会返回False</returns>
        bool IsPlaying(int serialId);

        /// <summary>
        /// 停止所有已加载的声音。
        /// </summary>
        void StopAllLoadedSounds();

        /// <summary>
        /// 停止所有已加载的声音。
        /// </summary>
        /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
        void StopAllLoadedSounds(float fadeOutSeconds);
    }
}