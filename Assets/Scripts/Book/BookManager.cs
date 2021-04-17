using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MiscUtil.Collections.Extensions;
using NPC;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Book
{
    public class BookManager : MonoBehaviour
    {
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
            
        }
        
        public void OnSearchButtonClick(TMP_InputField field)
        {
            if (NPCManager.Instance.DoTitleMatch(field.text))
            {
                Spawn();
                Debug.Log("Book found.");
                field.image.color = Color.green;
            }
            else
            {
                Debug.Log("Book not found.");
                field.image.color = Color.red;
                
            }

            StartCoroutine(WaitAndResetFieldColor(field, 1.0f));
        }

        private IEnumerator WaitAndResetFieldColor(TMP_InputField field, float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                field.image.color = Color.white;
            }
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