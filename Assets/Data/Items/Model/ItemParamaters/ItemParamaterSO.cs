using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class ItemParamaterSO : ScriptableObject
    {
        [field: SerializeField]
        public string paramaterName { get; private set; }
        

    }
}