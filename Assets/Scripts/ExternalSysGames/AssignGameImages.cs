﻿using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;


namespace ExternalSystemGames
{
    // Source idea from
    // https://answers.unity.com/questions/25271/how-to-load-images-from-given-folder.html
    public class AssignGameImages : MonoBehaviour
    {
        [SerializeField] private GameObject _gameButtonPrefab = null;
        [SerializeField] private Transform _contentsTransform = null;

        private EventSystem _eventSystem;
        private ExternalGameManager _EGM;


        private void Awake()
        {
            Cursor.visible = false;

            _EGM = new ExternalGameManager();
            _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            StartCoroutine(LoadButtons(_EGM.GamesInfo));
        }

        private IEnumerator LoadButtons(List<GameInfo> gInfos)
        {
            // Get images
            Texture2D[] textures = new Texture2D[gInfos.Count];

            string pathPreFix = @"file://";

            int dummy = 0;
            foreach (GameInfo gi in gInfos)
            {
                string pathTemp = pathPreFix + gi.ImageFile.FullName;
                WWW www = new WWW(pathTemp);
                yield return www;
                while (!www.isDone)
                    yield return null;
                textures[dummy] = www.texture;

                Debug.LogWarning($"{gi.Name} Loaded.");
                dummy++;
            }

            // Instantiate buttons
            PopulateCanvas(gInfos, textures);
        }

        private void PopulateCanvas(List<GameInfo> gInfos, Texture2D[] textures)
        {
            int dummy = 0;
            foreach (GameInfo g in gInfos)
            {
                GameObject newButton =
                    Instantiate(_gameButtonPrefab, _contentsTransform);
                newButton.GetComponent<RawImage>().texture = textures[dummy];
                newButton.GetComponent<GameButton>().SetGameInfo(g);

                if (dummy == 0) _eventSystem.SetSelectedGameObject(newButton);

                dummy++;
            }

            DestroySelf();
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
