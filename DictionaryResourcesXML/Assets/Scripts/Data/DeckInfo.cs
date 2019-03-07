using System.Collections.Generic;

namespace Game
{
    /// <summary>
    /// Actua como una plantilla de todos los datos de mazos que queremos guardar
    /// </summary>
    [System.Serializable]
    public class DeckInfo
    {
        public int DeckID;
        public List<int> DeckCardIDs;
        public DeckAsset DeckAsset;

        public DeckInfo(int id, List<int> cardsIDs, DeckAsset asset)
        {
            this.DeckID = id;
            this.DeckCardIDs = cardsIDs;
            this.DeckAsset = asset;
        }

        public DeckInfo(DeckInfo other)
        {
            this.DeckID = other.DeckID;
            this.DeckCardIDs = new List<int>(other.DeckCardIDs);
            this.DeckAsset = other.DeckAsset;
        }
    }
}
