using UnityEngine;

namespace Game
{
    /// <summary>
    /// Objeto Scriptable de los mazos
    /// Tiene el modelo y el icono
    /// </summary>
    [CreateAssetMenu(fileName = ("New Deck"), menuName = "Deck")]
    public class DeckAsset : ScriptableObject
    {
        public GameObject DeckPrefab;
        public Sprite DeckIcon;
    }
}