using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToCatchAGremlin.UI.UIs;

namespace ToCatchAGremlin.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
           //Gremlin_List_ProgramUI UI = new Gremlin_List_ProgramUI();
           Employee_DictionaryUI UI = new Employee_DictionaryUI();
            UI.Run();
        }
    }
}
