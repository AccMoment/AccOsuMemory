// See https://aka.ms/new-console-template for more information

using System.Net.Http.Json;
using AccOsuMemory.Core.OsuApi.V1;
using AccOsuMemory.Core.OsuApi.V1.Enum;
using AccOsuMemory.Core.OsuApi.V1.Model.Beatmap;
using AccOsuMemory.Core.OsuApi.V1.Model.Beatmap.Score;
using AccOsuMemory.Core.OsuApi.V1.Parameters.BeatMap;

// HttpClient hc =new HttpClient();
// string url = "https://osu.ppy.sh/api/get_user_recent?k=c79b5e03926c8013656913718b7cb609a726470b&m=0&limit=1&u=accmoment";
// var scores = await hc.GetFromJsonAsync<List<UserRecentScore>>(url);
//
// var score = new UserRecentScore();
var date = new DateTime(2021, 1, 1);
var api = new OsuApiV1("c79b5e03926c8013656913718b7cb609a726470b");
var para = new BeatMapParams();
Console.Write(123);
Console.ReadKey();
