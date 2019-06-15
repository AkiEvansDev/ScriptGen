using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            DataDictionary.App app = new DataDictionary.App();
            app.GenerateDataDictionary(new ScriptGenPlugin.Model.TableInfo[2] { new ScriptGenPlugin.Model.TableInfo() { Fields = new ScriptGenPlugin.Model.FieldInfo[2] }, new ScriptGenPlugin.Model.TableInfo() { Fields = new ScriptGenPlugin.Model.FieldInfo[2] } }, "");
        }
    }
}
