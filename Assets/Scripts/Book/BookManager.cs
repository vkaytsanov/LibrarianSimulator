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


        private SpriteRenderer bookSpriteRenderer;

        private Vector3 spawnVector;

        // Start is called before the first frame update
        void Start()
        {
            bookSpriteRenderer = bookPrefab.GetComponent<SpriteRenderer>();
            spawnVector = Camera.main.ViewportToWorldPoint(new Vector3(0.65f, 1.2f, 0.5f));

            button.onClick.AddListener(() => { Spawn(); });
            Spawn();
        }

        void Spawn()
        {
            bookSpriteRenderer.sprite = GenerateRandomSprite();
            Instantiate(bookPrefab, spawnVector, Quaternion.identity);
        }


        Sprite GenerateRandomSprite()
        {
            return sprites[Random.Range(0, sprites.Length)];
        }
    }
}