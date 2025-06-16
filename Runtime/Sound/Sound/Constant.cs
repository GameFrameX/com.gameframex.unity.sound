// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！


namespace GameFrameX.Sound.Runtime
{
    /// <summary>
    /// 声音相关常量。
    /// </summary>
    internal static class Constant
    {
        /// <summary>
        /// 默认播放位置。
        /// </summary>
        internal const float DefaultTime = 0f;

        /// <summary>
        /// 默认是否静音。
        /// </summary>
        internal const bool DefaultMute = false;

        /// <summary>
        /// 默认是否循环播放。
        /// </summary>
        internal const bool DefaultLoop = false;

        /// <summary>
        /// 默认优先级。
        /// </summary>
        internal const int DefaultPriority = 0;

        /// <summary>
        /// 默认音量。
        /// </summary>
        internal const float DefaultVolume = 1f;

        /// <summary>
        /// 默认声音淡入时间，以秒为单位。
        /// </summary>
        internal const float DefaultFadeInSeconds = 0f;

        /// <summary>
        /// 默认声音淡出时间，以秒为单位。
        /// </summary>
        internal const float DefaultFadeOutSeconds = 0f;

        /// <summary>
        /// 默认声音音调。
        /// </summary>
        internal const float DefaultPitch = 1f;

        /// <summary>
        /// 默认声音立体声声相。
        /// </summary>
        internal const float DefaultPanStereo = 0f;

        /// <summary>
        /// 默认声音空间混合量。
        /// </summary>
        internal const float DefaultSpatialBlend = 0f;

        /// <summary>
        /// 默认声音最大距离。
        /// </summary>
        internal const float DefaultMaxDistance = 100f;

        /// <summary>
        /// 默认声音多普勒等级。
        /// </summary>
        internal const float DefaultDopplerLevel = 1f;
    }
}