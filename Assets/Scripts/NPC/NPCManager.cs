using System;
using UnityEngine;

namespace NPC
{
    public class NPCManager : MonoBehaviour
    {
        [SerializeField] 
        private Sprite[] sprites;
        [SerializeField] 
        private GameObject prefab;

        private GameObject current;
        private NPC _npcComponent;
        
        private SpriteRenderer npcSpriteRenderer;

        private Vector3 spawnVector;
        
        private void Start()
        {
            spawnVector = Camera.main.ViewportToWorldPoint(new Vector3(-0.5f, 0.75f, 0.5f));
            current = Instantiate(prefab, spawnVector, Quaternion.identity);
            _npcComponent = current.GetComponent<NPC>();
            Spawn();
        }
        

        public void Spawn()
        {
            GenerateRandomNPC();
            _npcComponent.SetToComing();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Spawn();
        }

        private void GenerateRandomNPC()
        {
            
        }
        
        public string GenerateRandomDialogue()
        {
            throw new NotImplementedException();
        }
    }
}