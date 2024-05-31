using System.Net.Http.Json;
using Top10Streams.Data.TopList;

namespace Top10Streams.Data
{
    public class ListService
    {
        protected readonly HttpClient HttpClient;
        protected Dictionary<string, TopList.TopList>? LoadedLists;
        protected Dictionary<string, AvailableTopList>? LoadableLists;

        public ListService(HttpClient http)
        {
            HttpClient = http;
        }

        public async Task<TopList.TopList> GetTopList(string code)
        {
            await LoadLists();

            if(LoadedLists.TryGetValue(code, out TopList.TopList? topList))
                return topList;

            if(!LoadableLists.TryGetValue(code, out AvailableTopList? loadableList))
                throw new Exception($"Unknown List: {code}");

            await LoadList(loadableList);
            
            var exists = LoadedLists.TryGetValue(code, out TopList.TopList? list);
            if(exists && list != null)
                return list;

            throw new Exception($"Failed to Load List: {code}");
        }

        public async Task<List<AvailableTopList>> GetAvailableLists()
        {
            await LoadLists();
            return LoadableLists.Values.ToList();
        }

        protected async Task LoadLists()
        {
            if (LoadableLists != null) return;
            LoadedLists = [];

            var lists = await HttpClient.GetFromJsonAsync<AvailableTopList[]>($"{HttpClient.BaseAddress}/lists/lists.json");

            if (lists == null)
                throw new Exception($"Failed to download list of lists.");

            Console.WriteLine($"Got {lists.Length} lists");

            LoadableLists = [];
            foreach (var list in lists)
            {
                Console.WriteLine(list.Code);
                LoadableLists.Add(list.Code, list);
            }
        }

        protected async Task LoadList(AvailableTopList list)
        {
            var listItems = await HttpClient.GetFromJsonAsync<TopListItem[]>($"lists/{list.Code}.json");

            if (listItems == null || listItems.Length <= 0)
                throw new Exception($"Download Failed for List: {list.Code}");

            LoadedLists.Add(list.Code, new TopList.TopList(list.Name, list.Code, listItems.ToList()));
        }
    }
}
