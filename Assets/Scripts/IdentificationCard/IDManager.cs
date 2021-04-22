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
        [SerializeField] private TextMeshProUGUI npcUniversity;
        [SerializeField] private TextMeshProUGUI npcNumber;
        [SerializeField] private SpriteRenderer npcPhoto;
        
        private IDCard _idCardComponent;


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
            _idCardComponent = idPrefab.GetComponent<IDCard>();
        }

        public void Spawn(Vector3 spawnVector, Sprite photo)
        {
            idPrefab.SetActive(true);
            idPrefab.transform.position = spawnVector;
            _idCardComponent.currentState = ObjectState.InitialFalling;
            SetupIdentity(photo);
        }

        private void SetupIdentity(Sprite photo) {
            npcName.text = _idCardComponent.IDCharacteristics.LastName + ",\n" +
                           _idCardComponent.IDCharacteristics.FirstName;

            npcUniversity.text = _idCardComponent.IDCharacteristics.University;

            npcNumber.text = _idCardComponent.IDCharacteristics.Number;

            npcPhoto.sprite = photo;
        }
        

        private Sprite GenerateRandomSprite()
        {
            return sprites[Random.Range(0, sprites.Length)];
        }
    }
}