namespace Top10Streams.Data.TopList
{
    public class TopList
    {
        public string Name { get; protected set; }
        public string Code { get; protected set; }
        protected Dictionary<int, TopListItem> Items { get; set; }

        public TopList(string name, string code, List<TopListItem> items)
        {
            Name = name;
            Code = code;
            Items = [];
            foreach(var item in items)
                Items.Add(item.Id, item);
        }

        public List<TopListItem> GetItems() => Items.Values.ToList();

        public TopListItem GetItem(int id) => Items[id];
    }
}
