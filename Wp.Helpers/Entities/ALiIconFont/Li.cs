﻿using System.Collections.Generic;

namespace Wp.Helpers.Entities.ALiIconFont
{
    public class Li
    {
        public string Class { get; set; }
        public Content Span { get; set; } = new Content();
        public List<Content> Div { get; set; } = new List<Content>();
    }
}