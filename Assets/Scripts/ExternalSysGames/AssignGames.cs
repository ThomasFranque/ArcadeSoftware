using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;


namespace ExternalSystemGames
{
    // Source idea from
    // https://answers.unity.com/questions/25271/how-to-load-images-from-given-folder.html
    public class AssignGames : MonoBehaviour
    {
        //// [SerializeField] private GameObject _gameButtonPrefab = null;
        //// [SerializeField] private Transform _contentsTransform = null;

        ////private ExternalGameManager _EGM;

        private MainCanvas _mainCanvas;
        [SerializeField] private LoadingCanvas _loadingCanvas;

        private void Awake()
        {
            Cursor.visible = false;

            _mainCanvas = GameObject.Find("Main Canvas").GetComponent<MainCanvas>();
            LoadFinished += _mainCanvas.FinishedButtonLoad;
            LoadFinished += _loadingCanvas.FinishedButtonLoad;
            LoadFinished += DestroySelf;

            ////_EGM = new ExternalGameManager();
        }

        private void Start()
        {
            StartCoroutine(LoadButtons());

        }

        private IEnumerator LoadButtons()
        {
            ExternalGameManager egm = new ExternalGameManager(_loadingCanvas);
            List<GameInfo> gInfos = egm.GamesInfo;

            // Get images
            Texture2D[] textures = new Texture2D[gInfos.Count];

            string pathPreFix = @"file://";

            int dummy = 0;
            foreach (GameInfo gi in gInfos)
            {
                yield return new WaitForSeconds(.5f);
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
                _loadingCanvas.NewGameImageSet();
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
                _loadingCanvas.NewGameOnCanvas();
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
