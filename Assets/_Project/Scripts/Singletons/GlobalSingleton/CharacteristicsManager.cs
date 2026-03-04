using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Characteristics
{
    public class CharacteristicsManager : SingletonBase<CharacteristicsManager>
    {
        private CharacteristicsSaver saver => CharacteristicsSaver.Instance;

        private void Start()
        {
            PrepareUpgrades();
        }
        
        private void PrepareUpgrades()
        {

        }
    }
}