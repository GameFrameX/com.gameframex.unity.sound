// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！


using GameFrameX.Runtime;

namespace GameFrameX.Sound.Runtime
{
    public sealed partial class SoundManager : GameFrameworkModule, ISoundManager
    {
        /// <summary>
        /// 播放声音信息。
        /// </summary>
        private sealed class PlaySoundInfo : IReference
        {
            /// <summary>
            /// 序列编号。
            /// </summary>
            private int m_SerialId;

            /// <summary>
            /// 声音组。
            /// </summary>
            private SoundGroup m_SoundGroup;

            /// <summary>
            /// 播放声音参数。
            /// </summary>
            private PlaySoundParams m_PlaySoundParams;

            /// <summary>
            /// 用户自定义数据。
            /// </summary>
            private object m_UserData;

            /// <summary>
            /// 初始化播放声音信息的新实例。
            /// </summary>
            public PlaySoundInfo()
            {
                m_SerialId = 0;
                m_SoundGroup = null;
                m_PlaySoundParams = null;
                m_UserData = null;
            }

            /// <summary>
            /// 获取序列编号。
            /// </summary>
            public int SerialId
            {
                get { return m_SerialId; }
            }

            /// <summary>
            /// 获取声音组。
            /// </summary>
            public SoundGroup SoundGroup
            {
                get { return m_SoundGroup; }
            }

            /// <summary>
            /// 获取播放声音参数。
            /// </summary>
            public PlaySoundParams PlaySoundParams
            {
                get { return m_PlaySoundParams; }
            }

            /// <summary>
            /// 获取用户自定义数据。
            /// </summary>
            public object UserData
            {
                get { return m_UserData; }
            }

            /// <summary>
            /// 创建播放声音信息。
            /// </summary>
            /// <param name="serialId">序列编号。</param>
            /// <param name="soundGroup">声音组。</param>
            /// <param name="playSoundParams">播放声音参数。</param>
            /// <param name="userData">用户自定义数据。</param>
            /// <returns>创建的播放声音信息。</returns>
            public static PlaySoundInfo Create(int serialId, SoundGroup soundGroup, PlaySoundParams playSoundParams, object userData)
            {
                PlaySoundInfo playSoundInfo = ReferencePool.Acquire<PlaySoundInfo>();
                playSoundInfo.m_SerialId = serialId;
                playSoundInfo.m_SoundGroup = soundGroup;
                playSoundInfo.m_PlaySoundParams = playSoundParams;
                playSoundInfo.m_UserData = userData;
                return playSoundInfo;
            }

            /// <summary>
            /// 清理播放声音信息。
            /// </summary>
            public void Clear()
            {
                m_SerialId = 0;
                m_SoundGroup = null;
                m_PlaySoundParams = null;
                m_UserData = null;
            }
        }
    }
}