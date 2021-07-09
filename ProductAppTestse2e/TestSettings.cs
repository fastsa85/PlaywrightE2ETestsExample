using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Tests.e2e
{
    public class TestSettings
    {
        public string BaseUrl { get; set; }

        public PlaywrightSettings PlaywrightSettings { get; set; }
    }

    public class PlaywrightSettings
    {
        public bool Headless { get; set; }
        public int SlowMo { get; set; }
    }
}
