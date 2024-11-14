using JetBrains.Annotations;
using UnityEngine;

namespace Practices.MVC_Example.DataBace
{
    [CreateAssetMenu(fileName = "ItemSpec", menuName = "Scriptable Objects/ItemSpec")]
    public class ItemSpec : ScriptableObject
    {
        [field: SerializeField] public int id { get; private set; }
        [field: SerializeField] public string description { get; private set; }
        [field: SerializeField] public int price { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public GameObject prefeb { get; private set; }
    }
}

