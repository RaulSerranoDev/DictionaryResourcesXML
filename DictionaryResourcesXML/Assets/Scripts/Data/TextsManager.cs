using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using System.Globalization;

namespace Game
{
    /// <summary>
    /// Se encarga de cargar los datos del XML y se destruye
    /// </summary>
    public class TextsManager : MonoBehaviour
    {
        [Header("XML")]
        public TextAsset GameDataText;

        /// <summary>
        /// Empieza la lectura de datos
        /// </summary>
        private void Start()
        {
            LoadTexts();
        }

        /// <summary>
        /// Parsea un string a float (1.5)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static float FloatParse(string number)
        {
            return float.Parse(number, CultureInfo.InvariantCulture.NumberFormat);
        }

        /// <summary>
        /// Inicializa valores del GameManager en los que va a guardar los datos del XML y empieza la lectura, se destruye cuando acaba
        /// </summary>
        private void LoadTexts()
        {
            GameManager.Instance.Cards = new List<CardInfo>();
            GameManager.Instance.Decks = new List<DeckInfo>();

            GetGameDataInfo();

            Destroy(this.gameObject);
        }

        /// <summary>
        /// Obtiene los datos del GameDataText y los guarda en un atributo en GameManager
        /// </summary>
        private void GetGameDataInfo()
        {
            TextAsset currentFile = GameDataText;

            //Cargamos el XML
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(currentFile.text);

            XmlNodeList levelsList = xmlDoc.GetElementsByTagName("gameData"); //Se coloca en el primer tag

            foreach (XmlNode levelIndex in levelsList)//gameDatas
            {
                foreach (XmlNode levelInfo in levelIndex)//cards, decks..
                {
                    switch (levelInfo.Name)
                    {
                        case "cards":
                            GetCardsInfo(levelInfo);
                            break;

                        case "decks":
                            GetDecksInfo(levelInfo);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Lee las cartas
        /// </summary>
        /// <param name="levelInfo"></param>
        private void GetCardsInfo(XmlNode levelInfo)
        {
            List<CardInfo> cards = new List<CardInfo>();

            foreach (XmlNode levelCard in levelInfo.ChildNodes) //card
            {
                int id = int.Parse(levelCard.Attributes["key"].Value);
                string name = null;
                int cost = 0;
                Sprite sprite = null;
                bool taunt = false;
                bool charge = false;

                foreach (XmlNode levelCardInfo in levelCard.ChildNodes)    //cardInfo
                {
                    switch (levelCardInfo.Name)
                    {
                        case "name":
                            name = levelCardInfo.InnerText;
                            break;
                        case "cost":
                            cost = int.Parse(levelCardInfo.InnerText);
                            break;
                        case "sprite":
                            sprite = Resources.Load<Sprite>(levelCardInfo.InnerText);
                            break;
                        case "mechanics":
                            string[] mechanics = levelCardInfo.InnerText.Split(',');
                            for (int i = 0; i < mechanics.Length; i++)
                            {
                                switch (mechanics[i])
                                {
                                    case "TAUNT":
                                        taunt = true;
                                        break;
                                    case "CHARGE":
                                        charge = true;
                                        break;                              
                                }
                            }
                            break;

                    }
                }

                //Se añade la carta a la lista
                cards.Add(new CardInfo(id, name, cost, sprite,taunt,charge));
            }

            GameManager.Instance.Cards = cards;
        }

        private void GetDecksInfo(XmlNode levelInfo)
        {
            List<DeckInfo> decks = new List<DeckInfo>();

            foreach (XmlNode levelDeck in levelInfo.ChildNodes) //deck
            {
                int id = int.Parse(levelDeck.Attributes["key"].Value);
                List<int> decksIDs = new List<int>();
                DeckAsset asset = Resources.Load<DeckAsset>("DeckAssets/deck" + id);

                foreach (XmlNode levelCardID in levelDeck.ChildNodes)    //cardIDs
                    decksIDs.Add(int.Parse(levelCardID.InnerText));

                //Se añade el mazo a la lista
                decks.Add(new DeckInfo(id, decksIDs, asset));
            }

            GameManager.Instance.Decks = decks;
        }

    }
}
