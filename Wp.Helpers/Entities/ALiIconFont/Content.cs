using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Entities.ALiIconFont
{
    public class Content
    {
        [Description("Class")]
        public string Class { get; set; }

        [Description("Text")]
        public string Text { get; set; }

        [Description("Test")]
        public bool Test { get; set; }
    }
}