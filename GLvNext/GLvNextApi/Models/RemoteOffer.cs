using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GLvNextApi.Models
{
    public class Location
    {
        public object locationName { get; set; }
        public string addressLine { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string regionState { get; set; }
        public string postcode { get; set; }
        public string country { get; set; }
        public bool isDefault { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public object distanceInMetres { get; set; }
        public int locationType { get; set; }
        public object metadata { get; set; }
        public object code { get; set; }
        public string key { get; set; }
        public string lang { get; set; }
    }

    public class Category
    {
        public string categoryName { get; set; }
        public List<object> categories { get; set; }
        public List<object> images { get; set; }
        public List<object> items { get; set; }
        public int id { get; set; }
        public object metadata { get; set; }
        public object code { get; set; }
        public string key { get; set; }
        public string lang { get; set; }
    }

    public class Summary
    {
        public string text { get; set; }
        public string richText { get; set; }
    }

    public class Image
    {
        public string imageName { get; set; }
        public string imageUrl { get; set; }
        public bool isDefault { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string dimensionUnit { get; set; }
        public object tags { get; set; }
        public string caption { get; set; }
    }

    public class RemoteOffer
    {
        public DateTime validFrom { get; set; }
        public DateTime validTo { get; set; }
        public List<Location> locations { get; set; }
        public object pricesFrom { get; set; }
        public List<Category> categories { get; set; }
        public string key { get; set; } 
        public string name { get; set; } //title
        public Summary summary { get; set; }  //Description
        public List<object> tags { get; set; }
        public string detailsUrl { get; set; }
        public List<Image> Images { get; set; }
        public bool IsFeatured { get; set; }
    }

    public class RemoteOfferRoot
    {
        public List<RemoteOffer> data { get; set; }
    }
}
