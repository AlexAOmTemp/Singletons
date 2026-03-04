using System;
using System.Collections.Generic;
using UnityEngine;

namespace Characteristics
{
    [Serializable]
    public class CharacteristicsSaver : SingletonBase<CharacteristicsSaver>
    {
        private CharacteristicsManager characteristicsManager => CharacteristicsManager.Instance;
        
        public void PrepareData()
        {
  
        }
    }
}