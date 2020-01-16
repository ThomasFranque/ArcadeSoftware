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
		private readonly string _dirPath;

		public List<GameInfo> GamesInfo { get; }

		public ExternalGameManager()
		{
			_dirPath = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
				_GAME_DIR_NAME);
			GamesInfo = new List<GameInfo>();

			if (!SaveDirExists())
				CreateSaveDir();

			GamesInfo = GetStoredGamesInfo();
		}

		private List<GameInfo> GetStoredGamesInfo()
		{
			DirectoryInfo d = new DirectoryInfo(_dirPath);
			DirectoryInfo[] dirs = d.GetDirectories();

			List<GameInfo> gInf = new List<GameInfo>();

			foreach (DirectoryInfo dir in dirs)
			{
				FileInfo[] exes = dir.GetFiles("*.exe");
				FileInfo finalExeInf = null;
				foreach(FileInfo f in exes)
				{
					if (!f.Name.StartsWith("UnityCrashHandler"))
						finalExeInf = f;
				}

				gInf.Add(new GameInfo(dir.Name, dir.FullName, ExtractTextFromFile(dir.GetFiles("*.txt")[0]),finalExeInf, dir.GetFiles("*.png")[0]));
			}

			return gInf;
		}

		private string ExtractTextFromFile(FileInfo textFile)
		{
			return textFile.OpenText().ReadToEnd();
		}

		private bool SaveDirExists() =>
			Directory.Exists(_dirPath);

		private void CreateSaveDir()
		{
			Directory.CreateDirectory(_dirPath);
		}
	}
}
