﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFrameX.Runtime;
using UnityEngine;

namespace GameFrameX.Sound.Runtime
{
    internal sealed class PlaySoundInfo : IReference
    {
        private Entity.Runtime.Entity m_BindingEntity;
        private Vector3 m_WorldPosition;
        private object m_UserData;

        public PlaySoundInfo()
        {
            m_BindingEntity = null;
            m_WorldPosition = Vector3.zero;
            m_UserData = null;
        }

        public Entity.Runtime.Entity BindingEntity
        {
            get
            {
                return m_BindingEntity;
            }
        }

        public Vector3 WorldPosition
        {
            get
            {
                return m_WorldPosition;
            }
        }

        public object UserData
        {
            get
            {
                return m_UserData;
            }
        }

        public static PlaySoundInfo Create(Entity.Runtime.Entity bindingEntity, Vector3 worldPosition, object userData)
        {
            PlaySoundInfo playSoundInfo = ReferencePool.Acquire<PlaySoundInfo>();
            playSoundInfo.m_BindingEntity = bindingEntity;
            playSoundInfo.m_WorldPosition = worldPosition;
            playSoundInfo.m_UserData = userData;
            return playSoundInfo;
        }

        public void Clear()
        {
            m_BindingEntity = null;
            m_WorldPosition = Vector3.zero;
            m_UserData = null;
        }
    }
}
