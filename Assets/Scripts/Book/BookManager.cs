using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MiscUtil.Collections.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Book
{
    public class BookManager : MonoBehaviour
    {
        public Button button;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private GameObject bookPrefab;
        [SerializeField] private GameObject bookSeal;


        private SpriteRenderer _bookSpriteRenderer;

        private Vector3 _spawnVector;

        // Start is called before the first frame update
        void Start()
        {
            _bookSpriteRenderer = bookPrefab.GetComponent<SpriteRenderer>();
            _spawnVector = Camera.main.ViewportToWorldPoint(new Vector3(0.65f, 1.2f, 0.5f));

            button.onClick.AddListener(() => { Spawn(); });
            Spawn();
        }

        void Spawn()
        {
            _bookSpriteRenderer.sprite = GenerateRandomSprite();
            bookSeal.SetActive(false);
            Instantiate(bookPrefab, _spawnVector, Quaternion.identity);
        }


        Sprite GenerateRandomSprite()
        {
            return sprites[Random.Range(0, sprites.Length)];
        }
    }
}