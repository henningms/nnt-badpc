using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace NRKNettTvBadPc.Models
{
    public class Playlist
    {
        public IList<PlaylistItem> Items { get; set; } 

        public Playlist()
        {
            Items = new List<PlaylistItem>();
        }

        public static Playlist FromFile(String path)
        {
            if (String.IsNullOrEmpty(path))
                return null;

            var reader = new StreamReader(path);

            var line = "";

            var playlist = new Playlist();

            while ((line = reader.ReadLine()) != null)
            {
                if (!line.StartsWith("#EXT-X-STREAM-INF")) continue;

                var item = new PlaylistItem();

                var infoLine = line.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);

                foreach (var keyValue in infoLine.Select(info => info.Split('=')))
                {
                    switch (keyValue[0])
                    {
                        case "BANDWIDTH":
                            item.Bandwidth = Convert.ToInt64(keyValue[1]);
                            break;
                        case "CODECS":
                            item.Codecs = keyValue[1];
                            break;
                        case "RESOLUTION":
                            item.Resolution = keyValue[1];
                            break;
                    }
                }

                if (String.IsNullOrEmpty(item.Resolution)) continue;

                var readLine = reader.ReadLine();
                if (readLine != null) item.URL = readLine.Replace("&id=", "");

                playlist.Items.Add(item);
            }

            reader.Close();

            return playlist;
        }
    }
}
