using System;
using Data.Saving;
using DI.Signals;
using TMPro;
using UI.Base;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace UI.Canvases
{
    public class FlappyBirdGameOverCanvas : MonoBehaviour, ICanvas
    {
        [SerializeField] private TMP_Text _scoreValueText;
        [SerializeField] private TMP_Text _scoreBestValueText;
        [SerializeField] private TMP_Text _commentText;
        [SerializeField] private Button _okButton;
        
        [Inject]
        public void Init(SignalBus signalBus)
        {
            _okButton.onClick.AddListener(() => SceneManager.LoadScene("Game"));
            
            //TODO Добавить посередник между канвасами
            signalBus.Subscribe<BirdDiedSignal>(x =>
            {
                gameObject.SetActive(true);
                Update();
            });
        }

        public void Update()
        {
            _scoreValueText.text = GlobalPrefs.CurrentScore.ToString();
            _scoreBestValueText.text = GlobalPrefs.BestScore.ToString();
        }
    }
}