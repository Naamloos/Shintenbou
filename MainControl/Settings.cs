using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shintenbou.MainControl
{
    public struct Settings
    {
        [JsonProperty("enableRpc")]
        public bool EnableRpc { get; private set; }

        [JsonProperty("savePageState")]
        public bool SavePageState { get; private set; }
    }
}
