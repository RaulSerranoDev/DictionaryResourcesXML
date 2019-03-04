using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    /// <summary>
    /// Clase persistente entre escenas
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region Persistent_Singleton

        public static GameManager Instance;

        /// <summary>
        /// Persistent Singleton
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        #endregion Persistent_Singleton

        /// <summary>
        /// Lista con todas las cartas que leemos del XML
        /// </summary>
        public List<CardInfo> Cards;

        /// <summary>
        /// Mazos de la jugadores
        /// </summary>
        public List<DeckInfo> Decks;
    }
}
