using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;


namespace ExternalSystemGames
{
    // Source idea from
    // https://answers.unity.com/questions/25271/how-to-load-images-from-given-folder.html
    public class AssignGames: MonoBehaviour
    {
        //// [SerializeField] private GameObject _gameButtonPrefab = null;
        //// [SerializeField] private Transform _contentsTransform = null;

        private ExternalGameManager _EGM;

        private MainCanvas _mainCanvas;

        private void Awake()
        {
            Cursor.visible = false;

            _EGM = new ExternalGameManager();
            _mainCanvas = GameObject.Find("Main Canvas").GetComponent<MainCanvas>();
            StartCoroutine(LoadButtons(_EGM.GamesInfo));

            LoadFinished += _mainCanvas.FinishedButtonLoad;
            LoadFinished += DestroySelf;
        }

        private IEnumerator LoadButtons(List<GameInfo> gInfos)
        {
            // Get images
            Texture2D[] textures = new Texture2D[gInfos.Count];

            string pathPreFix = @"file://";

            int dummy = 0;
            foreach (GameInfo gi in gInfos)
            {
                if (gi.ImageFile != null)
                {
                    string pathTemp = pathPreFix + gi.ImageFile.FullName;
                    WWW www = new WWW(pathTemp);
                    yield return www;
                    while (!www.isDone)
                        yield return null;
                    textures[dummy] = www.texture;
                }
                else
                    textures[dummy] = null;
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
                // GameObject newButton =
                //     Instantiate(_gameButtonPrefab, _contentsTransform);
                // newButton.GetComponent<RawImage>().texture = textures[dummy];
                // GameButton buttonScript = newButton.GetComponent<GameButton>();
                // buttonScript.SetGameInfo(g);
                // _mainCanvas.AddGameButton(buttonScript);

                // if (dummy == 0) _eventSystem.SetSelectedGameObject(newButton);
                _mainCanvas.AddGameButton(g, textures[dummy]);
                dummy++;
            }
            OnLoadFinished();
        }

        private void OnLoadFinished()
        {
            Debug.LogWarning($"Successfully displaying game buttons.");
            LoadFinished?.Invoke();
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }

        private Action LoadFinished;
    }
}
