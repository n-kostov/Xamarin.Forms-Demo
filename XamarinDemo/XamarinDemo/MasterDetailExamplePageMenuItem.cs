using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinDemo
{

    public class MasterDetailExamplePageMenuItem
    {
        public MasterDetailExamplePageMenuItem()
        {
            TargetType = typeof(MasterDetailExamplePageDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}