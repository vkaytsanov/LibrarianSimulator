using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] private GameObject dialogBox;
        [SerializeField] private TextMeshProUGUI dialogText;

        private bool _isDialoging;

        private const float DialogChangeTime = 4.0f;
        private float _currentTime = DialogChangeTime;


        private readonly Queue<string> _sentences = new Queue<string>();

        public static DialogManager Instance { get; private set; }

        // Start is called before the first frame update
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

        // Update is called once per frame
        void Update()
        {
            if (_isDialoging)
            {
                _currentTime += Time.deltaTime;
                if (_currentTime > DialogChangeTime)
                {
                    _currentTime = 0.0f;
                    DisplayNextSentence();
                }
            }
        }

        public void StartDialog(Dialog dialog)
        {
            _sentences.Clear();

            foreach (var dialogSentence in dialog.sentences)
            {
                _sentences.Enqueue(dialogSentence);
            }

            dialogBox.SetActive(true);
            _isDialoging = true;
        }

        private void DisplayNextSentence()
        {
            if (_sentences.Count == 0)
            {
                dialogBox.SetActive(false);
                dialogText.text = "";
                _isDialoging = false;
                _currentTime = DialogChangeTime;
            }
            else
            {
                dialogText.text = _sentences.Dequeue();
            }
        }
    }
}