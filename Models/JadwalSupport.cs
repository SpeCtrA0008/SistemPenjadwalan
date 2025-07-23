using System.Collections.Generic;

namespace AdminDashboardApp.Models
{
    public class JadwalSupport
    {
        public int JadwalSupportId { get; set; }

        public ICollection<JenisSupportJadwal>? JenisSupportJadwals { get; set; }
    }
}
