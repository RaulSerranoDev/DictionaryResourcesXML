using System.Collections.Generic;

namespace Game
{
    /// <summary>
    /// Actua como una plantilla de todos los datos que queremos guardar en la carta
    /// </summary>
    [System.Serializable]
    public class DeckInfo
    {
        public int DeckID;
        public List<int> DeckCardIDs;

        public DeckInfo(int id, List<int> cardsIDs)
        {
            this.DeckID = id;
            this.DeckCardIDs = cardsIDs;
        }

        public DeckInfo(DeckInfo other)
        {
            this.DeckID = other.DeckID;
            this.DeckCardIDs = new List<int>(other.DeckCardIDs);
        }
    }
}
