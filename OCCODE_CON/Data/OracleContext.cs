using Microsoft.EntityFrameworkCore;

namespace OCCODE_CON.Data
{
    public class OracleContext : DbContext
    {
        public OracleContext(
            DbContextOptions<OracleContext> options)
            : base(options)
        {
        }
    }
}