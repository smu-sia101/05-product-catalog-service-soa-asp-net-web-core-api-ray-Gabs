using Google.Cloud.Firestore;

namespace asp.net.web.Models
{
    [FirestoreData]
    public class Product
    {   
        public string id { get; set; }
        public string name { get; set; }
        public double price { get; set; } // Change from decimal to double
        public string description { get; set; }
        public string category { get; set; }
        public int stock { get; set; }
        public string imageUrl { get; set; }
    }
}
