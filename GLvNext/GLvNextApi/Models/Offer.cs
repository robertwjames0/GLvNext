using System;

namespace GLvNextApi.Models
{


    public class Offer
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public String Description { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public Offer(){}

        public Offer(RemoteOffer remoteOffer)
        {
            Title = remoteOffer.name;
            Description = remoteOffer.summary.text;
            ValidFrom = remoteOffer.validFrom;
            ValidTo = remoteOffer.validTo;
            //TODO: add more fields if time allows
        }
    }
}