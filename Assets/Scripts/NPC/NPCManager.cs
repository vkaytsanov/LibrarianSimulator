using System;
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
        private NPC _npcComponent;
        
        private SpriteRenderer _npcSpriteRenderer;

        private Vector3 _spawnVector;
        
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
    }
}