using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GLvNextApi.Models
{
    public class OfferContext : DbContext
    {
        public OfferContext(DbContextOptions<OfferContext> options)
            : base(options)
        {
        }

        public DbSet<Offer> Offers { get; set; }
    }
}
