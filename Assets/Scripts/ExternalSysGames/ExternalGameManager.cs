using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://stackoverflow.com/questions/38123183/c-sharp-read-folder-names-from-directory
namespace ExternalSystemGames
{
	public class ExternalGameManager
	{
		private const string _GAME_DIR_NAME = "games";
		private const string _DEFAULT_DESCRIPTION = "No game description provided.\n\\[T]/";
		private readonly string _dirPath;

		public List<GameInfo> GamesInfo { get; }

		public ExternalGameManager()
		{
			_dirPath = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
				_GAME_DIR_NAME);
			GamesInfo = new List<GameInfo>();

			if (!SaveDirExists())
				CreateGameDir();

			GamesInfo = GetStoredGamesInfo();
		}

		private List<GameInfo> GetStoredGamesInfo()
		{
			DirectoryInfo d = new DirectoryInfo(_dirPath);
			DirectoryInfo[] dirs = d.GetDirectories();

			List<GameInfo> gInf = new List<GameInfo>();

			foreach (DirectoryInfo dir in dirs)
			{
				// Get description
				FileInfo[] txts = dir.GetFiles("*.txt");
				string gameDescription = _DEFAULT_DESCRIPTION;
				if (txts.Length > 0)
					gameDescription = ExtractTextFromFile(txts[0]);
				else 
                    Debug.LogWarning($".txt Description file missing from {dir.Name}.");

				// Get image
				FileInfo[] pngs = dir.GetFiles("*.png");
				FileInfo finalPng = null;
				foreach(FileInfo i in pngs)
					finalPng = i;
				if (finalPng == null)
                    Debug.LogWarning($".png Image file missing from {dir.Name}.");

				// Get exe
				FileInfo[] exes = dir.GetFiles("*.exe");
				FileInfo finalExeInf = null;
				foreach(FileInfo f in exes)
					if (!f.Name.StartsWith("UnityCrashHandler"))
						finalExeInf = f;
				if (finalExeInf == null)
                    Debug.LogWarning($".exe Executable missing from {dir.Name}.");

				gInf.Add(new GameInfo(dir.Name, dir.FullName, gameDescription, finalExeInf, finalPng));
			}
			

            Debug.LogWarning($"Successfully read game files.");
			return gInf;
		}

		private string ExtractTextFromFile(FileInfo textFile)
		{
			return textFile.OpenText().ReadToEnd();
		}

		private bool SaveDirExists() =>
			Directory.Exists(_dirPath);

		private void CreateGameDir()
		{
			Directory.CreateDirectory(_dirPath);
		}
	}
}
