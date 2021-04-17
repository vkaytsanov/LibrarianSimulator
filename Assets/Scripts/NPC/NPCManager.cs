using System;
using System.Globalization;
using Book;
using UnityEngine;

namespace NPC
{
    public class NPCManager : MonoBehaviour
    {
        [SerializeField] 
        private Sprite[] sprites;
        [SerializeField] 
        private GameObject npcPrefab;
        public NPC _npcComponent;
        
        private SpriteRenderer _npcSpriteRenderer;

        private Vector3 _spawnVector;
        
        public static NPCManager Instance { get; private set; }
        
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        private void Start()
        {
            _spawnVector = Camera.main.ViewportToWorldPoint(new Vector3(-0.5f, 0.75f, 0.5f));
            _npcComponent = npcPrefab.GetComponent<NPC>();
            Spawn();
        }
        

        public void Spawn()
        {
            GenerateRandomNPC();
            GenerateRandomAction();

            _npcComponent.SetToComing();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Spawn();
        }

        private void GenerateRandomNPC()
        {
            
        }

        private void GenerateRandomAction()
        {
            // TODO
            _npcComponent.action = NPCAction.WantingBook;

            if (_npcComponent.action == NPCAction.WantingBook)
            {
                _npcComponent.actionInfo = BooksDB.GetRandomFictionBookCharacteristics().title;
            }
        }

        public bool DoTitleMatch(string text)
        {
            return String.Compare(_npcComponent.actionInfo, text, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols) == 0;
        }
    }
}