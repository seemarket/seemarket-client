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
        public string product_type;

        public ProductType GETProductType()
        {
            switch (product_type)
            {
                case "SNACK":
                    return ProductType.SNACK;
                case "CEREAL":
                    return ProductType.CEREAL;
                case "SANDWICH":
                    return ProductType.SANDWICH;
                case "DRINK":
                    return ProductType.DRINK;
            }
            return ProductType.DRINK;
        }
    }

    public enum ProductType
    {
        SNACK, CEREAL, SANDWICH, DRINK
    }
}