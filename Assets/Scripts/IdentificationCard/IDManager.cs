using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IdentificationCard
{
    public class IDManager : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private GameObject idPrefab;
        
        private SpriteRenderer _idSpriteRenderer;
        
        private static IDManager _instance;

        public static IDManager Instance
        {
            get => _instance;
        }
        
        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }
        
        private void Start()
        {
            _idSpriteRenderer = idPrefab.GetComponent<SpriteRenderer>();
        }

        public void Spawn(Vector3 spawnVector)
        {
            _idSpriteRenderer.sprite = GenerateRandomSprite();
            idPrefab.SetActive(true);
            idPrefab.transform.position = spawnVector;
            idPrefab.GetComponent<IDCard>().currentState = ObjectState.InitialFalling;
        }
        
        private Sprite GenerateRandomSprite()
        {
            return sprites[Random.Range(0, sprites.Length)];
        }
        
    }
}