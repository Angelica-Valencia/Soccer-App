using System;
using System.Collections.Generic;

namespace Soccer_App.Model
{
    public class DatumMedia
    {
        public int id { get; set; }
        public string source_type { get; set; }
        public int? source_id { get; set; }
        public int? type { get; set; }
        public Title title { get; set; }
        public string sub_title { get; set; }
        public string url { get; set; }
        public string source_url { get; set; }
        public string thumbnail_url { get; set; }
    }

    public class MetaMedia
    {
        public int current_page { get; set; }
        public int from { get; set; }
        public int per_page { get; set; }
        public int to { get; set; }
    }

    public class RootMedia
    {
        public DatumMedia[] data { get; set; }
        public MetaMedia meta { get; set; }
    }

    public class Title
    {
        public string en { get; set; }
    }
}

