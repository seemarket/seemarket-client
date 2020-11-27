using System;

namespace Model
{
    [Serializable]
    public class Product
    {
        public int id;
        public string title;
        public string type;
        public string prefab_url;
        public string description;
        public string price;
        public string thumbnail_url;
    }
}