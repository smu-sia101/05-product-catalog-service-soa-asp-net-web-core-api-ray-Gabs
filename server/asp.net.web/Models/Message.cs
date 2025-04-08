using Google.Cloud.Firestore;

namespace asp.net.web.Models
{
    [FirestoreData]
    public class Message
    {
        [FirestoreProperty]
        public string id { get; set; }
        public string name {  get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public int stock {  get; set; }
        public string ImageUrl { get; set; }
    }
}
