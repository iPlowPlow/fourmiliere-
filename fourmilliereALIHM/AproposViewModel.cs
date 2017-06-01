using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAbstraite
{
    public class AproposViewModel : ViewModelBase
    {
        public string CopyRight { get { return "Metagenia"; } }
        public string Date { get { return System.DateTime.Now.ToString(); } }
        public string Author { get { return "Pierre Lochouarn"; } }
    }
}
