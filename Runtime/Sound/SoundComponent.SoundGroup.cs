// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！


using System;
using GameFrameX.Runtime;
using UnityEngine;

namespace GameFrameX.Sound.Runtime
{
    public sealed partial class SoundComponent : GameFrameworkComponent
    {
        /// <summary>
        /// 声音组。
        /// </summary>
        [Serializable]
        private sealed class SoundGroup
        {
            /// <summary>
            /// 声音组名称。
            /// </summary>
            [SerializeField] private string m_Name = null;

            /// <summary>
            /// 是否避免被同优先级声音替换。
            /// </summary>
            [SerializeField] private bool m_AvoidBeingReplacedBySamePriority = false;

            /// <summary>
            /// 是否静音。
            /// </summary>
            [SerializeField] private bool m_Mute = false;

            /// <summary>
            /// 音量大小。
            /// </summary>
            [SerializeField, Range(0f, 1f)] private float m_Volume = 1f;

            /// <summary>
            /// 声音代理辅助器数量。
            /// </summary>
            [SerializeField] private int m_AgentHelperCount = 1;

            /// <summary>
            /// 获取声音组名称。
            /// </summary>
            public string Name
            {
                get { return m_Name; }
            }

            /// <summary>
            /// 获取是否避免被同优先级声音替换。
            /// </summary>
            public bool AvoidBeingReplacedBySamePriority
            {
                get { return m_AvoidBeingReplacedBySamePriority; }
            }

            /// <summary>
            /// 获取是否静音。
            /// </summary>
            public bool Mute
            {
                get { return m_Mute; }
            }

            /// <summary>
            /// 获取音量大小。
            /// </summary>
            public float Volume
            {
                get { return m_Volume; }
            }

            /// <summary>
            /// 获取声音代理辅助器数量。
            /// </summary>
            public int AgentHelperCount
            {
                get { return m_AgentHelperCount; }
            }
        }
    }
}