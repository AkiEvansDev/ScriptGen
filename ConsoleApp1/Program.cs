using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xceed.Words.NET;


namespace ConsoleApp1
{
    class Program
    {
        static DocX document;
        static Table wordTable;
        static int row = 1;

        static void Main(string[] args)
        {
            document = DocX.Create(@"C:\Users\Aku\Desktop\tt1.docx");
            wordTable = document.AddTable(200, 4);

            Rec(new DirectoryInfo(@"X:\Users\Aku\Flag\app\src\main\rr"));

            document.InsertParagraph().InsertTableAfterSelf(wordTable);
            document.Save();
        }

        private static void Rec(DirectoryInfo d)
        {
            GetInfo(d.GetFiles());

            foreach (DirectoryInfo sd in d.GetDirectories())
                Rec(sd);
        }
        private static void GetInfo(FileInfo[] files)
        {
            files = files.Where(f => f.Extension.EndsWith("java") || f.Extension.EndsWith("xml")).ToArray();
            foreach (FileInfo f in files)
            {
                List<string> strs = File.ReadAllLines(f.FullName).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

                string[] comment = strs.Where(s => s.Trim().StartsWith("///") && !s.Contains("<summary>")).Select(s => s.Trim(' ', '*', '\\', '/')).ToArray();

                string op = comment.Length > 0 ? comment[0] : "";

                document.InsertParagraph(row.ToString() + ". " + f.Name + ". " + op + ".");

                foreach (string s in strs)
                    document.InsertParagraph(s);

                wordTable.Rows[row].Cells[0].Paragraphs[0].Append("// ");
                wordTable.Rows[row].Cells[1].Paragraphs[0].Append(f.Name);
                wordTable.Rows[row].Cells[2].Paragraphs[0].Append(op);
                wordTable.Rows[row].Cells[3].Paragraphs[0].Append("Авторский");

                row++;
            }
        }

       private static void GetInfo1(FileInfo[] files)
        {
            files = files.Where(f => f.Extension.EndsWith("java") || f.Extension.EndsWith("xml")).ToArray();
            foreach (FileInfo f in files)
            {
                wordTable.Rows[row].Cells[0].Paragraphs[0].Append(row.ToString());
                wordTable.Rows[row].Cells[1].Paragraphs[0].Append(f.Name);
                wordTable.Rows[row].Cells[2].Paragraphs[0].Append(File.ReadAllLines(f.FullName).Length.ToString());

                int v = (int)Math.Round(f.Length / 1024d);
                if (v <= 0)
                    v = 1;
                
                wordTable.Rows[row].Cells[3].Paragraphs[0].Append(v.ToString());

                row++;
            }
        }
    }
}
