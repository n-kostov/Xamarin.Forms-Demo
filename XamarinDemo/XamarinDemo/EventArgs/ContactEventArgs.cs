using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinDemo
{
    public class ContactEventArgs:EventArgs
    {
        public Contact Contact { get; set; }

        public ContactEventArgs(Contact contact)
        {
            this.Contact = contact;
        }
    }
}
