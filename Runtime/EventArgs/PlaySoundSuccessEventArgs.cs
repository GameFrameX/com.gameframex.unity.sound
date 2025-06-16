// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！


using GameFrameX.Event.Runtime;
using GameFrameX.Runtime;

namespace GameFrameX.Sound.Runtime
{
    /// <summary>
    /// 播放声音成功事件。
    /// </summary>
    public sealed class PlaySoundSuccessEventArgs : GameEventArgs
    {
        /// <summary>
        /// 初始化播放声音成功事件的新实例。
        /// </summary>
        public PlaySoundSuccessEventArgs()
        {
            SerialId = 0;
            SoundAssetName = null;
            SoundAgent = null;
            Duration = 0f;
            UserData = null;
        }

        /// <summary>
        /// 获取声音的序列编号。
        /// </summary>
        public int SerialId { get; private set; }

        /// <summary>
        /// 获取声音资源名称。
        /// </summary>
        public string SoundAssetName { get; private set; }

        /// <summary>
        /// 获取用于播放的声音代理。
        /// </summary>
        public ISoundAgent SoundAgent { get; private set; }

        /// <summary>
        /// 获取加载持续时间。
        /// </summary>
        public float Duration { get; private set; }

        /// <summary>
        /// 获取用户自定义数据。
        /// </summary>
        public object UserData { get; private set; }

        /// <summary>
        /// 创建播放声音成功事件。
        /// </summary>
        /// <param name="serialId">声音的序列编号。</param>
        /// <param name="soundAssetName">声音资源名称。</param>
        /// <param name="soundAgent">用于播放的声音代理。</param>
        /// <param name="duration">加载持续时间。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>创建的播放声音成功事件。</returns>
        public static PlaySoundSuccessEventArgs Create(int serialId, string soundAssetName, ISoundAgent soundAgent, float duration, object userData)
        {
            PlaySoundSuccessEventArgs playSoundSuccessEventArgs = ReferencePool.Acquire<PlaySoundSuccessEventArgs>();
            playSoundSuccessEventArgs.SerialId = serialId;
            playSoundSuccessEventArgs.SoundAssetName = soundAssetName;
            playSoundSuccessEventArgs.SoundAgent = soundAgent;
            playSoundSuccessEventArgs.Duration = duration;
            playSoundSuccessEventArgs.UserData = userData;
            return playSoundSuccessEventArgs;
        }

        /// <summary>
        /// 清理播放声音成功事件。
        /// </summary>
        public override void Clear()
        {
            SerialId = 0;
            SoundAssetName = null;
            SoundAgent = null;
            Duration = 0f;
            UserData = null;
        }

        /// <summary>
        /// 播放声音成功事件编号。
        /// </summary>
        public static readonly string EventId = typeof(PlaySoundSuccessEventArgs).FullName;

        public override string Id
        {
            get { return EventId; }
        }
    }
}