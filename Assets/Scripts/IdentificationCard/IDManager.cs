using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IdentificationCard
{
    public class IDManager : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private GameObject idPrefab;
        [SerializeField] private TextMeshProUGUI npcName;
        [SerializeField] private TextMeshProUGUI npcNumber;

        private SpriteRenderer _idSpriteRenderer;


        public static IDManager Instance { get; private set; }

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