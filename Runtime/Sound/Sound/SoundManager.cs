﻿// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！


using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFrameX.Asset.Runtime;
using GameFrameX.Runtime;
using UnityEngine;
using YooAsset;

namespace GameFrameX.Sound.Runtime
{
    /// <summary>
    /// 声音管理器。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public sealed partial class SoundManager : GameFrameworkModule, ISoundManager
    {
        private readonly Dictionary<string, SoundGroup> m_SoundGroups;
        private readonly List<int> m_SoundsBeingLoaded;

        private readonly HashSet<int> m_SoundsToReleaseOnLoad;

        // private readonly LoadAssetCallbacks m_LoadAssetCallbacks;
        private IAssetManager _assetManager;
        private ISoundHelper m_SoundHelper;
        private int m_Serial;
        private EventHandler<PlaySoundSuccessEventArgs> m_PlaySoundSuccessEventHandler;
        private EventHandler<PlaySoundFailureEventArgs> m_PlaySoundFailureEventHandler;
        // private EventHandler<PlaySoundUpdateEventArgs> m_PlaySoundUpdateEventHandler;

        /// <summary>
        /// 初始化声音管理器的新实例。
        /// </summary>
        public SoundManager()
        {
            m_SoundGroups = new Dictionary<string, SoundGroup>(StringComparer.Ordinal);
            m_SoundsBeingLoaded = new List<int>();
            m_SoundsToReleaseOnLoad = new HashSet<int>();
            // m_LoadAssetCallbacks = new LoadAssetCallbacks(LoadAssetSuccessCallback, LoadAssetFailureCallback, LoadAssetUpdateCallback);
            _assetManager = null;
            m_SoundHelper = null;
            m_Serial = 0;
            m_PlaySoundSuccessEventHandler = null;
            m_PlaySoundFailureEventHandler = null;
            // m_PlaySoundUpdateEventHandler = null;
        }

        /// <summary>
        /// 获取声音组数量。
        /// </summary>
        public int SoundGroupCount
        {
            get { return m_SoundGroups.Count; }
        }

        /// <summary>
        /// 播放声音成功事件。
        /// </summary>
        public event EventHandler<PlaySoundSuccessEventArgs> PlaySoundSuccess
        {
            add { m_PlaySoundSuccessEventHandler += value; }
            remove { m_PlaySoundSuccessEventHandler -= value; }
        }

        /// <summary>
        /// 播放声音失败事件。
        /// </summary>
        public event EventHandler<PlaySoundFailureEventArgs> PlaySoundFailure
        {
            add { m_PlaySoundFailureEventHandler += value; }
            remove { m_PlaySoundFailureEventHandler -= value; }
        }

        /*/// <summary>
        /// 播放声音更新事件。
        /// </summary>
        public event EventHandler<PlaySoundUpdateEventArgs> PlaySoundUpdate
        {
            add { m_PlaySoundUpdateEventHandler += value; }
            remove { m_PlaySoundUpdateEventHandler -= value; }
        }*/

        /// <summary>
        /// 声音管理器轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// 关闭并清理声音管理器。
        /// </summary>
        protected override void Shutdown()
        {
            StopAllLoadedSounds();
            m_SoundGroups.Clear();
            m_SoundsBeingLoaded.Clear();
            m_SoundsToReleaseOnLoad.Clear();
        }

        /// <summary>
        /// 设置资源管理器。
        /// </summary>
        /// <param name="assetManager">资源管理器。</param>
        public void SetResourceManager(IAssetManager assetManager)
        {
            if (assetManager == null)
            {
                throw new GameFrameworkException("Resource manager is invalid.");
            }

            _assetManager = assetManager;
        }

        /// <summary>
        /// 设置声音辅助器。
        /// </summary>
        /// <param name="soundHelper">声音辅助器。</param>
        public void SetSoundHelper(ISoundHelper soundHelper)
        {
            if (soundHelper == null)
            {
                throw new GameFrameworkException("Sound helper is invalid.");
            }

            m_SoundHelper = soundHelper;
        }

        /// <summary>
        /// 是否存在指定声音组。
        /// </summary>
        /// <param name="soundGroupName">声音组名称。</param>
        /// <returns>指定声音组是否存在。</returns>
        public bool HasSoundGroup(string soundGroupName)
        {
            if (string.IsNullOrEmpty(soundGroupName))
            {
                throw new GameFrameworkException("Sound group name is invalid.");
            }

            return m_SoundGroups.ContainsKey(soundGroupName);
        }

        /// <summary>
        /// 获取指定声音组。
        /// </summary>
        /// <param name="soundGroupName">声音组名称。</param>
        /// <returns>要获取的声音组。</returns>
        public ISoundGroup GetSoundGroup(string soundGroupName)
        {
            if (string.IsNullOrEmpty(soundGroupName))
            {
                throw new GameFrameworkException("Sound group name is invalid.");
            }

            if (m_SoundGroups.TryGetValue(soundGroupName, out var soundGroup))
            {
                return soundGroup;
            }

            return null;
        }

        /// <summary>
        /// 获取所有声音组。
        /// </summary>
        /// <returns>所有声音组。</returns>
        public ISoundGroup[] GetAllSoundGroups()
        {
            int index = 0;
            ISoundGroup[] results = new ISoundGroup[m_SoundGroups.Count];
            foreach (KeyValuePair<string, SoundGroup> soundGroup in m_SoundGroups)
            {
                results[index++] = soundGroup.Value;
            }

            return results;
        }

        /// <summary>
        /// 获取所有声音组。
        /// </summary>
        /// <param name="results">所有声音组。</param>
        public void GetAllSoundGroups(List<ISoundGroup> results)
        {
            if (results == null)
            {
                throw new GameFrameworkException("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<string, SoundGroup> soundGroup in m_SoundGroups)
            {
                results.Add(soundGroup.Value);
            }
        }

        /// <summary>
        /// 增加声音组。
        /// </summary>
        /// <param name="soundGroupName">声音组名称。</param>
        /// <param name="soundGroupHelper">声音组辅助器。</param>
        /// <returns>是否增加声音组成功。</returns>
        public bool AddSoundGroup(string soundGroupName, ISoundGroupHelper soundGroupHelper)
        {
            return AddSoundGroup(soundGroupName, false, Constant.DefaultMute, Constant.DefaultVolume, soundGroupHelper);
        }

        /// <summary>
        /// 增加声音组。
        /// </summary>
        /// <param name="soundGroupName">声音组名称。</param>
        /// <param name="soundGroupAvoidBeingReplacedBySamePriority">声音组中的声音是否避免被同优先级声音替换。</param>
        /// <param name="soundGroupMute">声音组是否静音。</param>
        /// <param name="soundGroupVolume">声音组音量。</param>
        /// <param name="soundGroupHelper">声音组辅助器。</param>
        /// <returns>是否增加声音组成功。</returns>
        public bool AddSoundGroup(string soundGroupName, bool soundGroupAvoidBeingReplacedBySamePriority, bool soundGroupMute, float soundGroupVolume, ISoundGroupHelper soundGroupHelper)
        {
            if (string.IsNullOrEmpty(soundGroupName))
            {
                throw new GameFrameworkException("Sound group name is invalid.");
            }

            if (soundGroupHelper == null)
            {
                throw new GameFrameworkException("Sound group helper is invalid.");
            }

            if (HasSoundGroup(soundGroupName))
            {
                return false;
            }

            SoundGroup soundGroup = new SoundGroup(soundGroupName, soundGroupHelper)
            {
                AvoidBeingReplacedBySamePriority = soundGroupAvoidBeingReplacedBySamePriority,
                Mute = soundGroupMute,
                Volume = soundGroupVolume
            };

            m_SoundGroups.Add(soundGroupName, soundGroup);

            return true;
        }

        /// <summary>
        /// 增加声音代理辅助器。
        /// </summary>
        /// <param name="soundGroupName">声音组名称。</param>
        /// <param name="soundAgentHelper">要增加的声音代理辅助器。</param>
        public void AddSoundAgentHelper(string soundGroupName, ISoundAgentHelper soundAgentHelper)
        {
            if (m_SoundHelper == null)
            {
                throw new GameFrameworkException("You must set sound helper first.");
            }

            SoundGroup soundGroup = (SoundGroup)GetSoundGroup(soundGroupName);
            if (soundGroup == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Sound group '{0}' is not exist.", soundGroupName));
            }

            soundGroup.AddSoundAgentHelper(m_SoundHelper, soundAgentHelper);
        }

        /// <summary>
        /// 获取所有正在加载声音的序列编号。
        /// </summary>
        /// <returns>所有正在加载声音的序列编号。</returns>
        public int[] GetAllLoadingSoundSerialIds()
        {
            return m_SoundsBeingLoaded.ToArray();
        }

        /// <summary>
        /// 获取所有正在加载声音的序列编号。
        /// </summary>
        /// <param name="results">所有正在加载声音的序列编号。</param>
        public void GetAllLoadingSoundSerialIds(List<int> results)
        {
            if (results == null)
            {
                throw new GameFrameworkException("Results is invalid.");
            }

            results.Clear();
            results.AddRange(m_SoundsBeingLoaded);
        }

        /// <summary>
        /// 是否正在加载声音。
        /// </summary>
        /// <param name="serialId">声音序列编号。</param>
        /// <returns>是否正在加载声音。</returns>
        public bool IsLoadingSound(int serialId)
        {
            return m_SoundsBeingLoaded.Contains(serialId);
        }

        /// <summary>
        /// 播放声音。
        /// </summary>
        /// <param name="soundAssetName">声音资源名称。</param>
        /// <param name="soundGroupName">声音组名称。</param>
        /// <returns>声音的序列编号。</returns>
        public UniTask<int> PlaySound(string soundAssetName, string soundGroupName)
        {
            return PlaySound(soundAssetName, soundGroupName, Constant.DefaultPriority, null, null);
        }

        /// <summary>
        /// 播放声音。
        /// </summary>
        /// <param name="soundAssetName">声音资源名称。</param>
        /// <param name="soundGroupName">声音组名称。</param>
        /// <param name="priority">加载声音资源的优先级。</param>
        /// <returns>声音的序列编号。</returns>
        public UniTask<int> PlaySound(string soundAssetName, string soundGroupName, int priority)
        {
            return PlaySound(soundAssetName, soundGroupName, priority, null, null);
        }

        /// <summary>
        /// 播放声音。
        /// </summary>
        /// <param name="soundAssetName">声音资源名称。</param>
        /// <param name="soundGroupName">声音组名称。</param>
        /// <param name="playSoundParams">播放声音参数。</param>
        /// <returns>声音的序列编号。</returns>
        public UniTask<int> PlaySound(string soundAssetName, string soundGroupName, PlaySoundParams playSoundParams)
        {
            return PlaySound(soundAssetName, soundGroupName, Constant.DefaultPriority, playSoundParams, null);
        }

        /// <summary>
        /// 播放声音。
        /// </summary>
        /// <param name="soundAssetName">声音资源名称。</param>
        /// <param name="soundGroupName">声音组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>声音的序列编号。</returns>
        public UniTask<int> PlaySound(string soundAssetName, string soundGroupName, object userData)
        {
            return PlaySound(soundAssetName, soundGroupName, Constant.DefaultPriority, null, userData);
        }

        /// <summary>
        /// 播放声音。
        /// </summary>
        /// <param name="soundAssetName">声音资源名称。</param>
        /// <param name="soundGroupName">声音组名称。</param>
        /// <param name="priority">加载声音资源的优先级。</param>
        /// <param name="playSoundParams">播放声音参数。</param>
        /// <returns>声音的序列编号。</returns>
        public UniTask<int> PlaySound(string soundAssetName, string soundGroupName, int priority, PlaySoundParams playSoundParams)
        {
            return PlaySound(soundAssetName, soundGroupName, priority, playSoundParams, null);
        }

        /// <summary>
        /// 播放声音。并设置指定的序列编号
        /// </summary>
        /// <param name="soundAssetName">声音资源名称。</param>
        /// <param name="soundGroupName">声音组名称。</param>
        /// <param name="serialId">加载声音资源的优先级。</param>
        /// <returns>声音的序列编号。</returns>
        public UniTask<int> PlaySoundBySerialId(string soundAssetName, string soundGroupName, int serialId)
        {
            return PlaySound(soundAssetName, soundGroupName, Constant.DefaultPriority, null, null, serialId);
        }

        /// <summary>
        /// 播放声音。
        /// </summary>
        /// <param name="soundAssetName">声音资源名称。</param>
        /// <param name="soundGroupName">声音组名称。</param>
        /// <param name="priority">加载声音资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>声音的序列编号。</returns>
        public UniTask<int> PlaySound(string soundAssetName, string soundGroupName, int priority, object userData)
        {
            return PlaySound(soundAssetName, soundGroupName, priority, null, userData);
        }

        /// <summary>
        /// 播放声音。
        /// </summary>
        /// <param name="soundAssetName">声音资源名称。</param>
        /// <param name="soundGroupName">声音组名称。</param>
        /// <param name="playSoundParams">播放声音参数。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>声音的序列编号。</returns>
        public async UniTask<int> PlaySound(string soundAssetName, string soundGroupName, PlaySoundParams playSoundParams, object userData)
        {
            return await PlaySound(soundAssetName, soundGroupName, Constant.DefaultPriority, playSoundParams, userData);
        }

        /// <summary>
        /// 播放声音。
        /// </summary>
        /// <param name="soundAssetName">声音资源名称。</param>
        /// <param name="soundGroupName">声音组名称。</param>
        /// <param name="priority">加载声音资源的优先级。</param>
        /// <param name="playSoundParams">播放声音参数。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="serialId">序列编号</param>
        /// <returns>声音的序列编号。</returns>
        public async UniTask<int> PlaySound(string soundAssetName, string soundGroupName, int priority, PlaySoundParams playSoundParams, object userData, int serialId = -1)
        {
            if (_assetManager == null)
            {
                throw new GameFrameworkException("You must set resource manager first.");
            }

            if (m_SoundHelper == null)
            {
                throw new GameFrameworkException("You must set sound helper first.");
            }

            if (playSoundParams == null)
            {
                playSoundParams = PlaySoundParams.Create();
            }

            int newSerialId;
            if (serialId >= 0)
            {
                newSerialId = serialId;
            }
            else
            {
                newSerialId = ++m_Serial;
            }


            PlaySoundErrorCode? errorCode = null;
            string errorMessage = null;
            SoundGroup soundGroup = (SoundGroup)GetSoundGroup(soundGroupName);
            if (soundGroup == null)
            {
                errorCode = PlaySoundErrorCode.SoundGroupNotExist;
                errorMessage = Utility.Text.Format("Sound group '{0}' is not exist.", soundGroupName);
            }
            else if (soundGroup.SoundAgentCount <= 0)
            {
                errorCode = PlaySoundErrorCode.SoundGroupHasNoAgent;
                errorMessage = Utility.Text.Format("Sound group '{0}' is have no sound agent.", soundGroupName);
            }

            if (errorCode.HasValue)
            {
                if (m_PlaySoundFailureEventHandler != null)
                {
                    PlaySoundFailureEventArgs playSoundFailureEventArgs = PlaySoundFailureEventArgs.Create(newSerialId, soundAssetName, soundGroupName, playSoundParams, errorCode.Value, errorMessage, userData);
                    m_PlaySoundFailureEventHandler(this, playSoundFailureEventArgs);
                    // ReferencePool.Release(playSoundFailureEventArgs);

                    if (playSoundParams.Referenced)
                    {
                        ReferencePool.Release(playSoundParams);
                    }

                    return newSerialId;
                }

                throw new GameFrameworkException(errorMessage);
            }

            m_SoundsBeingLoaded.Add(newSerialId);
            var assetOperationHandle = await _assetManager.LoadAssetAsync<AudioClip>(soundAssetName);

            void OnAssetOperationHandleOnCompleted(AssetHandle assetHandle)
            {
                var assetObject = assetHandle.GetAssetObject<AudioClip>();
                LoadAssetSuccessCallback(soundAssetName, assetObject, assetHandle.Duration, PlaySoundInfo.Create(newSerialId, soundGroup, playSoundParams, userData));
            }

            assetOperationHandle.Completed += OnAssetOperationHandleOnCompleted;
            return newSerialId;
        }

        /// <summary>
        /// 停止播放声音。
        /// </summary>
        /// <param name="serialId">要停止播放声音的序列编号。</param>
        /// <returns>是否停止播放声音成功。</returns>
        public bool StopSound(int serialId)
        {
            return StopSound(serialId, Constant.DefaultFadeOutSeconds);
        }

        /// <summary>
        /// 停止播放声音。
        /// </summary>
        /// <param name="serialId">要停止播放声音的序列编号。</param>
        /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
        /// <returns>是否停止播放声音成功。</returns>
        public bool StopSound(int serialId, float fadeOutSeconds)
        {
            if (IsLoadingSound(serialId))
            {
                m_SoundsToReleaseOnLoad.Add(serialId);
                m_SoundsBeingLoaded.Remove(serialId);
                return true;
            }

            foreach (KeyValuePair<string, SoundGroup> soundGroup in m_SoundGroups)
            {
                if (soundGroup.Value.StopSound(serialId, fadeOutSeconds))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 停止所有已加载的声音。
        /// </summary>
        public void StopAllLoadedSounds()
        {
            StopAllLoadedSounds(Constant.DefaultFadeOutSeconds);
        }

        /// <summary>
        /// 停止所有已加载的声音。
        /// </summary>
        /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
        public void StopAllLoadedSounds(float fadeOutSeconds)
        {
            foreach (KeyValuePair<string, SoundGroup> soundGroup in m_SoundGroups)
            {
                soundGroup.Value.StopAllLoadedSounds(fadeOutSeconds);
            }
        }

        /// <summary>
        /// 停止所有正在加载的声音。
        /// </summary>
        public void StopAllLoadingSounds()
        {
            foreach (int serialId in m_SoundsBeingLoaded)
            {
                m_SoundsToReleaseOnLoad.Add(serialId);
            }
        }

        /// <summary>
        /// 暂停播放声音。
        /// </summary>
        /// <param name="serialId">要暂停播放声音的序列编号。</param>
        public void PauseSound(int serialId)
        {
            PauseSound(serialId, Constant.DefaultFadeOutSeconds);
        }

        /// <summary>
        /// 暂停播放声音。
        /// </summary>
        /// <param name="serialId">要暂停播放声音的序列编号。</param>
        /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
        public void PauseSound(int serialId, float fadeOutSeconds)
        {
            foreach (KeyValuePair<string, SoundGroup> soundGroup in m_SoundGroups)
            {
                if (soundGroup.Value.PauseSound(serialId, fadeOutSeconds))
                {
                    return;
                }
            }

            throw new GameFrameworkException(Utility.Text.Format("Can not find sound '{0}'.", serialId));
        }

        /// <summary>
        /// 恢复播放声音。
        /// </summary>
        /// <param name="serialId">要恢复播放声音的序列编号。</param>
        public void ResumeSound(int serialId)
        {
            ResumeSound(serialId, Constant.DefaultFadeInSeconds);
        }

        /// <summary>
        /// 恢复播放声音。
        /// </summary>
        /// <param name="serialId">要恢复播放声音的序列编号。</param>
        /// <param name="fadeInSeconds">声音淡入时间，以秒为单位。</param>
        public void ResumeSound(int serialId, float fadeInSeconds)
        {
            foreach (KeyValuePair<string, SoundGroup> soundGroup in m_SoundGroups)
            {
                if (soundGroup.Value.ResumeSound(serialId, fadeInSeconds))
                {
                    return;
                }
            }

            throw new GameFrameworkException(Utility.Text.Format("Can not find sound '{0}'.", serialId));
        }

        private void LoadAssetSuccessCallback(string soundAssetName, object soundAsset, float duration, object userData)
        {
            PlaySoundInfo playSoundInfo = (PlaySoundInfo)userData;
            if (playSoundInfo == null)
            {
                throw new GameFrameworkException("Play sound info is invalid.");
            }

            if (m_SoundsToReleaseOnLoad.Contains(playSoundInfo.SerialId))
            {
                m_SoundsToReleaseOnLoad.Remove(playSoundInfo.SerialId);
                if (playSoundInfo.PlaySoundParams.Referenced)
                {
                    ReferencePool.Release(playSoundInfo.PlaySoundParams);
                }

                ReferencePool.Release(playSoundInfo);
                m_SoundHelper.ReleaseSoundAsset(soundAsset);
                return;
            }

            m_SoundsBeingLoaded.Remove(playSoundInfo.SerialId);

            ISoundAgent soundAgent = playSoundInfo.SoundGroup.PlaySound(playSoundInfo.SerialId, soundAsset, playSoundInfo.PlaySoundParams, out var errorCode);
            if (soundAgent != null)
            {
                if (m_PlaySoundSuccessEventHandler != null)
                {
                    PlaySoundSuccessEventArgs playSoundSuccessEventArgs = PlaySoundSuccessEventArgs.Create(playSoundInfo.SerialId, soundAssetName, soundAgent, duration, playSoundInfo.UserData);
                    m_PlaySoundSuccessEventHandler(this, playSoundSuccessEventArgs);
                    // ReferencePool.Release(playSoundSuccessEventArgs);
                }

                if (playSoundInfo.PlaySoundParams.Referenced)
                {
                    ReferencePool.Release(playSoundInfo.PlaySoundParams);
                }

                ReferencePool.Release(playSoundInfo);
                return;
            }

            m_SoundsToReleaseOnLoad.Remove(playSoundInfo.SerialId);
            m_SoundHelper.ReleaseSoundAsset(soundAsset);
            string errorMessage = Utility.Text.Format("Sound group '{0}' play sound '{1}' failure.", playSoundInfo.SoundGroup.Name, soundAssetName);
            if (m_PlaySoundFailureEventHandler != null)
            {
                PlaySoundFailureEventArgs playSoundFailureEventArgs = PlaySoundFailureEventArgs.Create(playSoundInfo.SerialId, soundAssetName, playSoundInfo.SoundGroup.Name,
                                                                                                       playSoundInfo.PlaySoundParams, errorCode.Value, errorMessage, playSoundInfo.UserData);
                m_PlaySoundFailureEventHandler(this, playSoundFailureEventArgs);
                // ReferencePool.Release(playSoundFailureEventArgs);

                if (playSoundInfo.PlaySoundParams.Referenced)
                {
                    ReferencePool.Release(playSoundInfo.PlaySoundParams);
                }

                ReferencePool.Release(playSoundInfo);
                return;
            }

            if (playSoundInfo.PlaySoundParams.Referenced)
            {
                ReferencePool.Release(playSoundInfo.PlaySoundParams);
            }

            ReferencePool.Release(playSoundInfo);
            throw new GameFrameworkException(errorMessage);
        }

        /*
        private void LoadAssetFailureCallback(string soundAssetName, LoadResourceStatus status, string errorMessage, object userData)
        {
            PlaySoundInfo playSoundInfo = (PlaySoundInfo)userData;
            if (playSoundInfo == null)
            {
                throw new GameFrameworkException("Play sound info is invalid.");
            }

            if (m_SoundsToReleaseOnLoad.Contains(playSoundInfo.SerialId))
            {
                m_SoundsToReleaseOnLoad.Remove(playSoundInfo.SerialId);
                if (playSoundInfo.PlaySoundParams.Referenced)
                {
                    ReferencePool.Release(playSoundInfo.PlaySoundParams);
                }

                return;
            }

            m_SoundsBeingLoaded.Remove(playSoundInfo.SerialId);
            string appendErrorMessage = Utility.Text.Format("Load sound failure, asset name '{0}', status '{1}', error message '{2}'.", soundAssetName, status, errorMessage);
            if (m_PlaySoundFailureEventHandler != null)
            {
                PlaySoundFailureEventArgs playSoundFailureEventArgs = PlaySoundFailureEventArgs.Create(playSoundInfo.SerialId, soundAssetName, playSoundInfo.SoundGroup.Name,
                    playSoundInfo.PlaySoundParams, PlaySoundErrorCode.LoadAssetFailure, appendErrorMessage, playSoundInfo.UserData);
                m_PlaySoundFailureEventHandler(this, playSoundFailureEventArgs);
                ReferencePool.Release(playSoundFailureEventArgs);

                if (playSoundInfo.PlaySoundParams.Referenced)
                {
                    ReferencePool.Release(playSoundInfo.PlaySoundParams);
                }

                return;
            }

            throw new GameFrameworkException(appendErrorMessage);
        }*/

        /*
        private void LoadAssetUpdateCallback(string soundAssetName, float progress, object userData)
        {
            PlaySoundInfo playSoundInfo = (PlaySoundInfo)userData;
            if (playSoundInfo == null)
            {
                throw new GameFrameworkException("Play sound info is invalid.");
            }

            if (m_PlaySoundUpdateEventHandler != null)
            {
                PlaySoundUpdateEventArgs playSoundUpdateEventArgs = PlaySoundUpdateEventArgs.Create(playSoundInfo.SerialId, soundAssetName, playSoundInfo.SoundGroup.Name,
                    playSoundInfo.PlaySoundParams, progress, playSoundInfo.UserData);
                m_PlaySoundUpdateEventHandler(this, playSoundUpdateEventArgs);
                ReferencePool.Release(playSoundUpdateEventArgs);
            }
        }*/
    }
}