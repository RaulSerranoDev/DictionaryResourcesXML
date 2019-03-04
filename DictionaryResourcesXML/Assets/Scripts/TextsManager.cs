using UnityEngine;
using System.Xml;
using System.Collections.Generic;

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
        /// Inicializa valores del GameManager en los que va a guardar los datos del XML y empieza la lectura
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
                    switch(levelInfo.Name)
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

                foreach (XmlNode levelCardInfo in levelCard.ChildNodes)    //cardInfo
                {
                    switch(levelCardInfo.Name)
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
                    }
                }

                //Se añade la carta a la lista
                cards.Add(new CardInfo(id, name, cost, sprite));
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

                foreach (XmlNode levelCardID in levelDeck.ChildNodes)    //cardIDs
                    decksIDs.Add(int.Parse(levelCardID.InnerText));

                //Se añade el mazo a la lista
                decks.Add(new DeckInfo(id,decksIDs));
            }

            GameManager.Instance.Decks = decks;
        }

    }
}
