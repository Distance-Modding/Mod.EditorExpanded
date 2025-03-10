#if APP
#pragma warning disable IDE0063
#endif
using HarmonyLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace EditorExpanded
{
	//EditorAdditions
	public class TrackNodeColors
	{
		// When specifying connections between two spline types
		public const char ITEM_DELIMITER = '/';

		#region Events
		public event EventHandler OnFileReloaded;
		#endregion

		public readonly FileInfo file;
		private FileSystemWatcher watcher;
		private Data data;

		#region Data Fields (Set after json read)
		private Dictionary<string, Color> splineColors;
		private Dictionary<Regex, Color> keyRegexList;
		#endregion

		#region Constructors
		public TrackNodeColors(string fileName) : this(new FileInfo(fileName))
		{ }

		public TrackNodeColors(FileInfo file)
		{
			this.file = file;
			/* Not worrying about tracknodecolor stuff
			MakeWatcher();
			LoadData();
			*/
		}
		#endregion

		#region Static Factory Pattern
		public static TrackNodeColors FromFile(string fileName) => new TrackNodeColors(fileName);

		public static TrackNodeColors FromFile(FileInfo file) => new TrackNodeColors(file);

		public static TrackNodeColors FromSettings(string settingsFileName)
		{
			FileSystem fs = new FileSystem();

			// Get file name from Settings folder (inside mod directory)
			DirectoryInfo settingsPath = new DirectoryInfo(Path.Combine(fs.RootDirectory, "Settings"));

			if (!settingsPath.Exists) settingsPath.Create();

			string configPath = new FileInfo(Path.Combine(settingsPath.FullName, settingsFileName)).FullName;

			return new TrackNodeColors(configPath);
		}
		#endregion

		#region Indexer
		public Color? this[string key]
		{
			get
			{
				string lcaseKey = key.ToLower();
				if (string.IsNullOrEmpty(key)) return null;

				if (splineColors.ContainsKey(lcaseKey))
				{
					return splineColors[lcaseKey];
				}
				else
				{
					foreach (var item in keyRegexList)
					{
						if (item.Key.IsMatch(key))
						{
							return item.Value;
						}
					}

					return null;
				}
			}
		}

		#endregion

		private void MakeWatcher()
		{
			watcher = new FileSystemWatcher(file.Directory.FullName, file.Name)
			{
				EnableRaisingEvents = true
			};

			watcher.Changed += OnFileChanged;
		}

		private void OnFileChanged(object sender, FileSystemEventArgs e)
		{
			if (string.Equals(e.FullPath, file.FullName, StringComparison.InvariantCultureIgnoreCase))
			{
				Mod.Log.LogWarning($"Reloading file {file.FullName}");
				LoadData();

				OnFileReloaded?.Invoke(this, EventArgs.Empty);
			}
		}

		private void LoadData()
		{
			data = GetOrCreate(file, new Data());

			Mod.Log.LogInfo("Loading data");
			splineColors = new Dictionary<string, Color>();
			keyRegexList = new Dictionary<Regex, Color>();

			// Making sure colors that copy from other are set properly
			HashSet<string> toRemove = new HashSet<string>();
			Dictionary<string, JsonColor> rewrite = new Dictionary<string, JsonColor>();

			foreach (var item in data.spline_colors)
			{
				JsonColor color = item.Value;

				if (string.Equals(color.type, JsonColor.TYPE_COPY, StringComparison.InvariantCultureIgnoreCase))
				{
					if (data.spline_colors.ContainsKey(color.from))
					{
						rewrite.Add(item.Key, data.spline_colors[color.from]);
					}
					else
					{
						toRemove.Add(item.Key);
					}
				}
			}

			toRemove.Do(key => data.spline_colors.Remove(key));

			foreach (var item in rewrite)
			{
				data.spline_colors.Remove(item.Key);
				data.spline_colors.Add(item.Key, item.Value);
			}

			foreach (var item in data.spline_colors)
			{
				try
				{
					string[] subSplines = item.Key.Split(ITEM_DELIMITER);
					Array.Sort(subSplines);

					string lcaseKey = string.Join(string.Empty, subSplines).ToLower();

					Color color = item.Value;
					splineColors.Add(lcaseKey, color);
					keyRegexList.Add(new Regex(item.Key), color);

					Mod.Log.LogInfo($"{item.Key} ({ColorEx.ColorToHexUnity(color)})");
				}
				catch (Exception e)
				{
					Mod.Log.LogInfo(e);
					Mod.Log.LogError($"Could not read color data for spline_color \"{item.Key}\"");
				}
			}
		}

		#region Data Class
		[Serializable]
		private class Data
		{
			[JsonProperty("spline_colors")]
			public Dictionary<string, JsonColor> spline_colors = new Dictionary<string, JsonColor>();
		}

		[Serializable]
		public class JsonColor
		{
			public const string TYPE_HEX = "hex";
			public const string TYPE_RGB = "rgb";
			public const string TYPE_HSB = "hsb";
			public const string TYPE_COPY = "copy";

			/// <summary>
			/// Color type, valid values are "hex", "rgb", "hsb" and "copy"
			/// </summary>
			[JsonProperty("type", Required = Required.DisallowNull)]
			public string type = TYPE_HEX;

			/// <summary>
			/// Hexadecimal color (when type = "hex")
			/// </summary>
			[JsonProperty("hex")]
			public string hex = "";

			/// <summary>
			/// Represents the key of the spline color to copy values from
			/// </summary>
			[JsonProperty("from")]
			public string from = "";

			/// <summary>
			/// Hue value (0° - 360°)
			/// </summary>
			[JsonProperty("hue")]
			public int hue = 0;

			/// <summary>
			/// Saturation value (0.0 - 1.0)
			/// </summary>
			[JsonProperty("saturation")]
			public float saturation = 0.8f;

			/// <summary>
			/// Brightness value (0.0 - 1.0)
			/// </summary>
			[JsonProperty("brightness")]
			public float brightness = 1.0f;

			/// <summary>
			/// Red channel value (0 - 255)
			/// </summary>
			[JsonProperty("red")]
			public int red = 128;

			/// <summary>
			/// Green channel value (0 - 255)
			/// </summary>
			[JsonProperty("green")]
			public int green = 128;

			/// <summary>
			/// Blue channel value (0 - 255)
			/// </summary>
			[JsonProperty("blue")]
			public int blue = 128;

			public Color ToColor()
			{
				switch (type.ToLower())
				{
					case TYPE_HEX:
						return ColorEx.HexToColor(hex);
					case TYPE_RGB:
						return new Color
						(
							r: red / 255.0f,
							g: green / 255.0f,
							b: blue / 255.0f,
							a: 1.0f
						);
					case TYPE_HSB:
						return new ColorHSB
						(
							hue: hue / 360.0f,
							saturation: saturation,
							brightness: brightness,
							alpha: 1.0f
						).ToColor();
					default:
						throw new NotSupportedException($"Color type \"{type}\" not supported, valid values are: {TYPE_HEX}, {TYPE_RGB} and {TYPE_HSB}");
				}
			}

			public static implicit operator Color(JsonColor col) => col.ToColor();
		}
		#endregion

		#region Save
		public static void Save<TYPE>(string file, TYPE data, bool overwrite = true)
			=> Save(new FileInfo(file), data, overwrite);

		public static void Save<TYPE>(FileInfo file, TYPE data, bool overwrite = true)
		{
			if (file.Exists)
			{
				if (overwrite)
				{
					file.Delete();
				}
				else
				{
					throw new Exception($"File already exists: {file.FullName}");
				}
			}

			JsonSerializer serializer = new JsonSerializer()
			{
				NullValueHandling = NullValueHandling.Include
			};

			using (StreamWriter streamWriter = new StreamWriter(file.FullName))
			{
				using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter)
				{
					Formatting = Formatting.Indented
				})
				{
					serializer.Serialize(jsonWriter, data);
				}
			}
		}
		#endregion

		#region Load
		public static TYPE Load<TYPE>(string file) where TYPE : new()
			=> Load<TYPE>(new FileInfo(file));

		public static TYPE Load<TYPE>(string file, TYPE @default) where TYPE : new()
			=> Load(new FileInfo(file), @default);

		public static TYPE Load<TYPE>(FileInfo file) where TYPE : new()
			=> Load(file, new TYPE());

		public static TYPE Load<TYPE>(FileInfo file, TYPE @default) where TYPE : new()
		{
			if (file.Exists)
			{
				try
				{
					using (StreamReader streamReader = new StreamReader(file.FullName))
					{
						return JsonConvert.DeserializeObject<TYPE>(streamReader.ReadToEnd());
					}
				}
				catch (Exception)
				{
					return @default;
				}
			}

			return @default;
			//else
			//{
			//	throw new FileNotFoundException("The file does not exist", file.FullName);
			//}
		}
		#endregion

		#region Get or Create
		public static TYPE GetOrCreate<TYPE>(string file, TYPE @default) where TYPE : new()
			=> GetOrCreate(new FileInfo(file), @default);

		public static TYPE GetOrCreate<TYPE>(FileInfo file, TYPE @default) where TYPE : new()
		{
			if (!file.Exists)
			{
				Save(file, @default);
			}

			return Load<TYPE>(file);
		}
		#endregion
	}
}
