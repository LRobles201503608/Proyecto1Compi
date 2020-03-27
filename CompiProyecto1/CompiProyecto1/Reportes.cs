using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiProyecto1
{
    class Reportes
    {
        public Reportes()
        {

        }
        public void GenerarHTMLTokens(List<String>tokens)
        {
            String texto = "";
            texto += "<html>";
            texto += "\n<head><title>Tokens</title></head>\n";
            texto += "<body> \n <table border=\"2\">\n";
            texto += "<tr><td>TOKEN</tr></td>";
            for (int i = 0; i < tokens.Count(); i++)
            {
                texto += "<tr><td>";
                texto += tokens.ElementAt(i);
                texto += "</tr></td>";
            }
            texto += "</table>\n</body>\n</html>";
            string fileName = "Tokens.html";
            StreamWriter writer = File.CreateText(fileName);
            writer.WriteLine(texto);
            writer.Close();
        }
        public void GenerarHTMLErrores(List<String> error)
        {
            String texto = "";
            texto += "<html>";
            texto += "\n<head><title>Errores</title></head>\n";
            texto += "<body> \n <table border=\"2\">\n";
            texto += "<tr><td>ERROR</tr></td>";
            for (int i=0; i<error.Count();i++)
            {
                texto += "<tr><td>";
                texto += error.ElementAt(i);
                texto += "</tr></td>";
            }
            texto += "</table>\n</body>\n</html>";
            string fileName = "Errores.html";
            StreamWriter writer = File.CreateText(fileName);
            writer.WriteLine(texto);
            writer.Close();
        }
    }
}
