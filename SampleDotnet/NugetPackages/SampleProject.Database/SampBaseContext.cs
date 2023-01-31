using Microsoft.EntityFrameworkCore;
using SampleProject.Core.Entities;

namespace SampleProject.Core.Database
{
    public class SampBaseContext : DbContext
    {
        public SampBaseContext()
            : base()
        {
        }

        public SampBaseContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}