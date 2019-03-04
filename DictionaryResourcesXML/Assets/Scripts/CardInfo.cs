﻿using UnityEngine;

namespace Game
{
    /// <summary>
    /// Actua como una plantilla de todos los datos que queremos guardar en la carta
    /// </summary>
    [System.Serializable]
    public class CardInfo
    {
        public int CardID;
        public string CardName;
        public int CardCost;
        public Sprite CardSprite;

        public CardInfo(int id, string name, int cost, Sprite sprite)
        {
            this.CardID = id;
            this.CardName = name;
            this.CardCost = cost;
            this.CardSprite = sprite;
        }

        public CardInfo(CardInfo other)
        {
            this.CardID = other.CardID;
            this.CardName = other.CardName;
            this.CardCost = other.CardID;
            this.CardSprite = other.CardSprite;
        }
    }
}
