using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompiProyecto1
{
    public partial class Form1 : Form
    {
        public static String EPSILON = "ε";
        public static char EPSILON_CHAR = EPSILON.ElementAt(0);
        Analizador an = new Analizador("");
        List<RichTextBox> editor = new List<RichTextBox>();
        RichTextBox areatexto = new RichTextBox();
        static string direccion;
        int numeropestana;
        TabPage pestana;
        public String texto = "";
        public Form1()
        {
            numeropestana = -1;
            InitializeComponent();
        }
        public void cambiarcolor()
        {
            RichTextBox cam = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
            cam.TextChanged += (ob, ev) =>
            {

                string types = @"\b(Console)\b";
                MatchCollection typeMatches = Regex.Matches(cam.Text, types);

                // getting comments (inline or multiline)
                string comments = @"(\/\/.+?$|<!(.|[\n\t\r])+?!>)";
                MatchCollection commentMatches = Regex.Matches(cam.Text, comments, RegexOptions.Multiline);

                // getting strings
                string strings = "\".+?\"";
                MatchCollection stringMatches = Regex.Matches(cam.Text, strings);

                string stringschar = @"\'.\'";
                MatchCollection stringcarateres = Regex.Matches(cam.Text, stringschar);
                // saving the original caret position + forecolor
                int originalIndex = cam.SelectionStart;
                int originalLength = cam.SelectionLength;
                Color originalColor = Color.Black;

                // MANDATORY - focuses a label before highlighting (avoids blinking)
                lblconteo.Focus();   // este es un truco para evitar los parpadeos
                // removes any previous highlighting (so modified words won't remain highlighted)
                cam.SelectionStart = 0;
                cam.SelectionLength = cam.Text.Length;
                cam.SelectionColor = originalColor;
                // scanning...
                foreach (Match m in typeMatches)
                {
                    cam.SelectionStart = m.Index;
                    cam.SelectionLength = m.Length;
                    cam.SelectionColor = Color.DarkCyan;
                }


                foreach (Match m in commentMatches)
                {
                    cam.SelectionStart = m.Index;
                    cam.SelectionLength = m.Length;
                    cam.SelectionColor = Color.Gray;
                }

                foreach (Match m in stringMatches)
                {
                    cam.SelectionStart = m.Index;
                    cam.SelectionLength = m.Length;
                    cam.SelectionColor = Color.Brown;
                }

                foreach (Match m in stringcarateres)
                {
                    cam.SelectionStart = m.Index;
                    cam.SelectionLength = m.Length;
                    cam.SelectionColor = Color.Red;
                }
                // restoring the original colors, for further writing
                cam.SelectionStart = originalIndex;
                cam.SelectionLength = originalLength;
                cam.SelectionColor = originalColor;
                // giving back the focus
                cam.Focus();
            };
        }
        public void obtenerLineasColumnas()
        {

            var a = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
            a.PreviewKeyDown += (ob, ev) =>
            {
                //-----Obtener Fila-------
                int inicio = a.SelectionStart;
                int linea = a.GetLineFromCharIndex(inicio);

                //-----Obtener Columna----
                int primero = a.GetFirstCharIndexFromLine(linea);
                int columna = inicio - primero;
                this.lblconteo.Text = "Columna: " + columna + " Linea: " + linea;
            };

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void GUARDARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Archivo ER (*.er)|*.er|Archivo txt (*.txt) |*.txt|All files (*.*)|*.*";
                open.Title = "Abrir Archivos";
                //  abrir.FileName = "Archivo TXT";
                var resultado = open.ShowDialog();//guarda resultado de clic en variable resultado
                if (resultado == DialogResult.OK)//si hace click en abrir
                {
                    StreamReader leer = new StreamReader(open.FileName);
                    RichTextBox caja = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];

                    Font f = new Font("Arial", 10, FontStyle.Regular);
                    caja.Font = f;
                    caja.Height = tabControl1.Height - 30;
                    caja.Width = tabControl1.Width - 10;
                    caja.Text = leer.ReadToEnd();
                    pestana.Text = open.FileName;
                    tabControl1.SelectedTab = pestana;
                    leer.Close();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("No existe ninguna pestaña");
            }
        }

        private void HECHOPORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("JUAN LUIS ROBLES MOLINA 201503608");
        }

        private void ABRIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            numeropestana += 1;
            pestana = new TabPage("Pestaña " + numeropestana);
            Font f = new Font("Arial", 10, FontStyle.Regular);
            /*
            editor.Add(areatexto);
            editor[numeropestana] = new RichTextBox();
            editor[numeropestana].Font = f;
            editor[numeropestana].Height = tabControl1.Height - 30;
            editor[numeropestana].Width = tabControl1.Width - 10;
            editor[numeropestana].Multiline=true;
            editor[numeropestana].ShowSelectionMargin=true;
             pestana.Controls.Add( editor[numeropestana]);
           
             */
            RichTextBox ed = new RichTextBox();
            ed.Font = f;
            ed.Height = tabControl1.Height - 30;
            ed.Width = tabControl1.Width - 10;
            ed.Multiline = true;
            ed.ShowSelectionMargin = true;
            pestana.Controls.Add(ed);
            tabControl1.TabPages.Add(pestana);
            tabControl1.ShowToolTips = true;
            //tabControl1.SelectedTab = pestana;
            obtenerLineasColumnas();
            cambiarcolor();
        }

        private void SALIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                direccion = tabControl1.SelectedTab.Text;//captura direccion de tab actual 
                if (!File.Exists(direccion) || File.Exists(direccion))
                {
                    SaveFileDialog guardar = new SaveFileDialog();
                    guardar.Filter = "Archivo ER (*.er)|*.er|Archivo txt (*.txt)|*.txt|All files (*.*)|*.*";

                    guardar.Title = "Guardar Como";
                    guardar.FileName = "";
                    var resultado = guardar.ShowDialog();
                    if (resultado == DialogResult.OK)
                    {
                        StreamWriter escribe = new StreamWriter(guardar.FileName);
                        var caja = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                        foreach (object line in caja.Lines)
                        {
                            escribe.WriteLine(line);
                        }
                        escribe.Close();
                    }
                }
            }
            catch (Exception err) {
                MessageBox.Show(err.ToString());
            }
        }

        private void SALIRToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void EJECUTARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex >= 0)
            {
                var caja = (RichTextBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                texto = caja.Text;
                an.AnalisisLexico(texto);
            }
        }

        private void ERRORESToolStripMenuItem_Click(object sender, EventArgs e)
        {

            webBrowser1.Navigate(@"C:\Users\jlrob\source\repos\CompiProyecto1\CompiProyecto1\bin\Debug\Errores.html");
        }

        private void REPORTESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(@"C:\Users\jlrob\source\repos\CompiProyecto1\CompiProyecto1\bin\Debug\Tokens.html");
        }
    }
}