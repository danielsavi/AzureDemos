using Newtonsoft.Json;

namespace CosmosGettingStartedTutorial
{
    public class CosmosDBModel
    {
        public string id { get; set; }
        public Purchase[] purchases { get; set; }
        public bool isRegistered { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class Purchase
    {
        public string sku { get; set; }
        public string description { get; set; }
    }
}
