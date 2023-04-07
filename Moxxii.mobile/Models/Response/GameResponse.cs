using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moxxii.mobile.Models.Response
{
    public partial class GameResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("thumbnail")]
        public Uri Thumbnail { get; set; }

        [JsonProperty("short_description")]
        public string ShortDescription { get; set; }

        [JsonProperty("game_url")]
        public Uri GameUrl { get; set; }

        [JsonProperty("genre")]
        public Genre Genre { get; set; }

        [JsonProperty("platform")]
        public Platform Platform { get; set; }

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("developer")]
        public string Developer { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("freetogame_profile_url")]
        public Uri FreetogameProfileUrl { get; set; }
    }

    public enum Genre { ActionRpg, Arpg, BattleRoyale, CardGame, Fantasy, Fighting, GenreMmorpg, Mmo, Mmoarpg, Mmorpg, Moba, Racing, Shooter, Social, Sports, Strategy };
    public enum Platform { PcWindows, PcWindowsWebBrowser, WebBrowser };
}
