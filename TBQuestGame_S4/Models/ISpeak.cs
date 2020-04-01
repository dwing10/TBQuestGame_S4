using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame_S1.Models
{
    interface ISpeak
    {
        List<string> Messages { get; set; }

        string Speak();
    }
}
