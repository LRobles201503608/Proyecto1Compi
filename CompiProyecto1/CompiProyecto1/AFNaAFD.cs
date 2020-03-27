using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace CompiProyecto1
{
    class AFNaAFD
    {
        public Automata afd;
        public List<string> regex;
        string rege = "";
        string nombre = "";
        public AFNaAFD(List<string> regex, string nombre)
        {
            this.regex = regex;
            this.nombre = nombre;
        }
        public void construct()
        {
            string reg = "";
            String sim = "";
            try
            {
                Stack pilaafd = new Stack();
                //Crea un automata por cada operacion
                for (
                    int i = 0; i < regex.Count(); i++)
                {
                    reg += regex.ElementAt(i);
                    string c = regex.ElementAt(i);
                    switch (c)
                    {
                        case "*":
                            Automata kleene = cerraduraKleene((Automata)pilaafd.Pop(), sim);
                            pilaafd.Push(kleene);
                            this.afd = kleene;
                            break;
                        case "+":
                            Automata repeat = UnoaMuchos((Automata)pilaafd.Pop(),sim);
                            pilaafd.Push(repeat);
                            this.afd = repeat;
                            break;
                        case "?":
                            Automata exclude = Omision((Automata)pilaafd.Pop(),sim);
                            pilaafd.Push(exclude);
                            this.afd = exclude;
                            break;
                        case ".":
                            Automata concat_param1 = (Automata)pilaafd.Pop();
                            Automata concat_param2 = (Automata)pilaafd.Pop();
                            Automata concat_result = concatenacion(concat_param1, concat_param2,sim);

                            pilaafd.Push(concat_result);
                            this.afd = concat_result;
                            break;

                        case "|":
                            Automata union_param1 = (Automata)pilaafd.Pop();
                            Automata union_param2 = (Automata)pilaafd.Pop();
                            Automata union_result = union(union_param1, union_param2,sim);
                            pilaafd.Push(union_result);

                            this.afd = union_result;
                            break;
                        default:
                            //crear un automata con cada simbolo
                            Automata simple = afdSimple((c));
                            //crea un nuevo string con el ultimo simbolo que se ingrese que no sea de los operadores
                            sim = c;
                            pilaafd.Push(simple);
                            this.afd = simple;
                            break;

                    }
                }
                this.afd.createAlfabeto(regex);
                this.afd.setTipo("afd");


            }
            catch (System.Exception e)
            {
                MessageBox.Show("Expresión mal ingresada");
            }
            try
            {
                generarDOT(reg, this.afd);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("no se pudo generar el dot");
            }
            
        }
        public Automata cerraduraKleene(Automata automatafd,String simbolo)
        {
            Automata afd_kleene = new Automata();

            //se crea un nuevo estado inicial
            Estado nuevoInicio = automatafd.getEstadoInicial();
            afd_kleene.addEstados(nuevoInicio);
            afd_kleene.setEstadoInicial(nuevoInicio);

            //Se crea un nuevo estado de aceptacion
            Estado nuevoFin = new Estado(automatafd.getEstados().Count() + 1);
            afd_kleene.addEstados(nuevoFin);
            afd_kleene.addEstadosAceptacion(nuevoFin);

            //definir estados clave para realizar la cerraduras
            Estado anteriorInicio = automatafd.getEstadoInicial();

            List<Estado> anteriorFin = automatafd.getEstadosAceptacion();

            // agregar transiciones desde el nuevo estado inicial
            nuevoInicio.getTransiciones().Add(new Transicion(nuevoInicio, anteriorInicio, simbolo));
            nuevoInicio.getTransiciones().Add(new Transicion(nuevoInicio, nuevoFin, simbolo));

            // agregar transiciones desde el anterior estado final
            for (int i = 0; i < anteriorFin.Count(); i++)
            {
                anteriorFin.ElementAt(i).getTransiciones().Add(new Transicion(anteriorFin.ElementAt(i), anteriorInicio, simbolo));
                anteriorFin.ElementAt(i).getTransiciones().Add(new Transicion(anteriorFin.ElementAt(i), nuevoFin, simbolo));
            }
            afd_kleene.setAlfabeto(automatafd.getAlfabeto());
            afd_kleene.setLenguajeR(automatafd.getLenguajeR());
            return afd_kleene;
        }
        public Automata UnoaMuchos(Automata automatafd,String simbolo)
        {
            Automata afd_kleene = new Automata();

            //se crea un nuevo estado inicial
            Estado nuevoInicio = automatafd.getEstadoInicial();
            afd_kleene.addEstados(nuevoInicio);
            afd_kleene.setEstadoInicial(nuevoInicio);

            //agregar todos los estados intermedio
            

            //Se crea un nuevo estado de aceptacion
            Estado nuevoFin = new Estado(automatafd.getEstados().Count() + 1);
            afd_kleene.addEstados(nuevoFin);
            afd_kleene.addEstadosAceptacion(nuevoFin);

            //definir estados clave para realizar la cerraduras
            Estado anteriorInicio = automatafd.getEstadoInicial();

            List<Estado> anteriorFin = automatafd.getEstadosAceptacion();

            // agregar transiciones desde el nuevo estado inicial
            nuevoInicio.getTransiciones().Add(new Transicion(nuevoInicio, anteriorInicio, simbolo));
            //nuevoInicio.getTransiciones().Add(new Transicion(nuevoInicio, nuevoFin, Form1.EPSILON));

            // agregar transiciones desde el anterior estado final
            for (int i = 0; i < anteriorFin.Count(); i++)
            {
                anteriorFin.ElementAt(i).getTransiciones().Add(new Transicion(anteriorFin.ElementAt(i), anteriorInicio, simbolo));
                anteriorFin.ElementAt(i).getTransiciones().Add(new Transicion(anteriorFin.ElementAt(i), nuevoFin, simbolo));
            }
            afd_kleene.setAlfabeto(automatafd.getAlfabeto());
            afd_kleene.setLenguajeR(automatafd.getLenguajeR());
            return afd_kleene;
        }
        public Automata Omision(Automata automatafd, String simbolo)
        {
            Automata afd_kleene = new Automata();

            //se crea un nuevo estado inicial
            Estado nuevoInicio = automatafd.getEstadoInicial();
            afd_kleene.addEstados(nuevoInicio);
            afd_kleene.setEstadoInicial(nuevoInicio);

            //Se crea un nuevo estado de aceptacion
            Estado nuevoFin = new Estado(automatafd.getEstados().Count() + 1);
            afd_kleene.addEstados(nuevoFin);
            afd_kleene.addEstadosAceptacion(nuevoFin);

            //definir estados clave para realizar la cerraduras
            Estado anteriorInicio = automatafd.getEstadoInicial();

            List<Estado> anteriorFin = automatafd.getEstadosAceptacion();

            // agregar transiciones desde el nuevo estado inicial
            nuevoInicio.getTransiciones().Add(new Transicion(nuevoInicio, anteriorInicio, simbolo));
            nuevoInicio.getTransiciones().Add(new Transicion(nuevoInicio, nuevoFin, simbolo));

            // agregar transiciones desde el anterior estado final
            for (int i = 0; i < anteriorFin.Count(); i++)
            {
                //anteriorFin.ElementAt(i).getTransiciones().Add(new Transicion(anteriorFin.ElementAt(i), anteriorInicio, Form1.EPSILON));
                anteriorFin.ElementAt(i).getTransiciones().Add(new Transicion(anteriorFin.ElementAt(i), nuevoFin, simbolo));
            }
            afd_kleene.setAlfabeto(automatafd.getAlfabeto());
            afd_kleene.setLenguajeR(automatafd.getLenguajeR());
            return afd_kleene;
        }
        public Automata concatenacion(Automata afd1, Automata afd2,String simbolo)
        {

            Automata afd_concat = new Automata();

            //se utiliza como contador para los estados del nuevo automata
            int i = 0;
            //agregar los estados del primer automata
            for (i = 0; i < afd2.getEstados().Count(); i++)
            {
                Estado tmp = (Estado)afd2.getEstados().ElementAt(i);
                tmp.setId(i);
                //se define el estado inicial
                if (i == 0)
                {
                    afd_concat.setEstadoInicial(tmp);
                }
                //cuando llega al último, concatena el ultimo con el primero del otro automata con un epsilon
                if (i == afd2.getEstados().Count() - 1)
                {
                    //se utiliza un ciclo porque los estados de aceptacion son un array
                    for (int k = 0; k < afd2.getEstadosAceptacion().Count(); k++)
                    {
                        tmp.setTransiciones(new Transicion((Estado)afd2.getEstadosAceptacion().ElementAt(k), afd1.getEstadoInicial(), simbolo));
                    }
                }
                afd_concat.addEstados(tmp);

            }
            //termina de agregar los estados y transiciones del segundo automata
            for (int j = 0; j < afd1.getEstados().Count(); j++)
            {
                Estado tmp = (Estado)afd1.getEstados().ElementAt(j);
                tmp.setId(i);

                //define el ultimo con estado de aceptacion
                if (afd1.getEstados().Count() - 1 == j)
                    afd_concat.addEstadosAceptacion(tmp);
                afd_concat.addEstados(tmp);
                i++;
            }

            HashSet<object> alfabeto = new HashSet<object>();
            alfabeto.Add(afd1.getAlfabeto());
            alfabeto.Add(afd2.getAlfabeto());
            afd_concat.setAlfabeto(alfabeto);
            afd_concat.setLenguajeR(afd1.getLenguajeR() + " " + afd2.getLenguajeR());

            return afd_concat;
        }
        public Automata union(Automata afd1, Automata afd2,String Simbolo)
        {
            Automata afd_union = new Automata();
            //se crea un nuevo estado inicial
            Estado nuevoInicio = new Estado(0);
            //se crea una transicion del nuevo estado inicial al primer automata
            nuevoInicio.setTransiciones(new Transicion(nuevoInicio, afd2.getEstadoInicial(), Simbolo));

            afd_union.addEstados(nuevoInicio);
            afd_union.setEstadoInicial(nuevoInicio);
            int i = 0;//llevar el contador del identificador de estados
                      //agregar los estados del segundo automata
            for (i = 0; i < afd1.getEstados().Count(); i++)
            {
                Estado tmp = (Estado)afd1.getEstados().ElementAt(i);
                tmp.setId(i + 1);
                afd_union.addEstados(tmp);
            }
            //agregar los estados del primer automata
            for (int j = 0; j < afd2.getEstados().Count(); j++)
            {
                Estado tmp = (Estado)afd2.getEstados().ElementAt(j);
                tmp.setId(i + 1);
                afd_union.addEstados(tmp);
                i++;
            }

            //se crea un nuevo estado final
            Estado nuevoFin = new Estado(afd1.getEstados().Count() + afd2.getEstados().Count() + 1);
            afd_union.addEstados(nuevoFin);
            afd_union.addEstadosAceptacion(nuevoFin);


            Estado anteriorInicio = afd1.getEstadoInicial();
            List<Estado> anteriorFin = afd1.getEstadosAceptacion();
            List<Estado> anteriorFin2 = afd2.getEstadosAceptacion();

            // agregar transiciones desde el nuevo estado inicial
            nuevoInicio.getTransiciones().Add(new Transicion(nuevoInicio, anteriorInicio, Simbolo));

            // agregar transiciones desde el anterior afd 1 al estado final
            for (int k = 0; k < anteriorFin.Count(); k++)
                anteriorFin.ElementAt(k).getTransiciones().Add(new Transicion(anteriorFin.ElementAt(k), nuevoFin, Simbolo));
            // agregar transiciones desde el anterior afd 2 al estado final
            for (int k = 0; k < anteriorFin.Count(); k++)
                anteriorFin2.ElementAt(k).getTransiciones().Add(new Transicion(anteriorFin2.ElementAt(k), nuevoFin, Simbolo));

            HashSet<object> alfabeto = new HashSet<object>();
            alfabeto.Add(afd1.getAlfabeto());
            alfabeto.Add(afd2.getAlfabeto());
            afd_union.setAlfabeto(alfabeto);
            afd_union.setLenguajeR(afd1.getLenguajeR() + " " + afd2.getLenguajeR());
            return afd_union;
        }

        public Automata afdSimple(string simboloRegex)
        {
            Automata automatafd = new Automata();
            //definir los nuevos estados
            Estado inicial = new Estado(0);
            //Estado aceptacion = new Estado(0);
            //crear una transicion unica con el simbolo
            //Transicion tran = new Transicion(inicial, aceptacion, simboloRegex);
            //inicial.setTransiciones(tran);
            //agrega los estados creados
            automatafd.addEstados(inicial);
            //automatafd.addEstados(aceptacion);
            //colocar los estados iniciales y de aceptacion
            automatafd.setEstadoInicial(inicial);
            //automatafd.addEstadosAceptacion(aceptacion);
            automatafd.setLenguajeR(simboloRegex + "");
            return automatafd;

        }
        public void generarDOT(string nombreArchivo, Automata automataFinito)
        {
            string texto = "digraph automata_finito {\n";

            texto += "\trankdir=LR;" + "\n";

            texto += "\tgraph [label=\"" + nombre + "\", labelloc=t, fontsize=20]; \n";
            texto += "\tnode [shape=doublecircle, style = filled,color = mediumseagreen];";
            //listar estados de aceptación
            for (int i = 0; i < automataFinito.getEstadosAceptacion().Count(); i++)
            {
                texto += " \"" + automataFinito.getEstadosAceptacion().ElementAt(i).id + "\"";
            }
            //
            texto += ";" + "\n";
            texto += "\tnode [shape=circle];" + "\n";
            texto += "\tnode [color=midnightblue,fontcolor=white];\n" + "	edge [color=red];" + "\n";

            texto += "\tsecret_node [style=invis];\n" + "	secret_node -> \"" + automataFinito.getEstadoInicial().id + "\" [label=\"inicio\"];" + "\n";
            //transiciones
            for (int i = 0; i < automataFinito.getEstados().Count(); i++)
            {
                List<Transicion> t = automataFinito.getEstados().ElementAt(i).getTransiciones();
                for (int j = 0; j < t.Count(); j++)
                {
                    texto += "\t" + t.ElementAt(j).DOT_String() + "\n";
                }

            }
            texto += "}";




            FileStream archivo = new FileStream(nombre + ".dot", FileMode.Create, FileAccess.Write);
            StreamWriter TextOut;

            try
            {
                TextOut = new StreamWriter(archivo);
                TextOut.WriteLine(texto);

                TextOut.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }

            generarArchivo();
        }
        public void generarArchivo()
        {
            try
            {
                var command = string.Format("dot -Tjpg " + nombre + ".dot -o " + nombre + ".jpg");
                var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/C " + command);
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                proc.WaitForExit();
                Thread.Sleep(2000);
                Process.Start(@"" + nombre + ".jpg");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
