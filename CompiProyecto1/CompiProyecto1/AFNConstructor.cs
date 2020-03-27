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
//using java.lang;

namespace CompiProyecto1
{
    class AFNConstructor
    {
        public Automata afn;
        int pnombre = 0;
        public List<string> regex;
        string rege = "";
        string nombre = "";
        public AFNConstructor(List<string> regex,string nombre)
        {
            this.regex = regex;
            this.nombre = nombre;
        }
        public void construct()
        {
            string reg = "";
            try
            {
                Stack pilaAFN = new Stack();
                //Crea un automata por cada operacion
                for (
                    int i = 0; i < regex.Count(); i++)
                {
                    reg += regex.ElementAt(i);
                    string c = regex.ElementAt(i);
                    switch (c)
                    {
                        case "*":
                            Automata kleene = cerraduraKleene((Automata)pilaAFN.Pop());
                            pilaAFN.Push(kleene);
                            this.afn = kleene;
                            break;
                        case "+":
                            Automata repeat = UnoaMuchos((Automata)pilaAFN.Pop());
                            pilaAFN.Push(repeat);
                            this.afn = repeat;
                            break;
                        case "?":
                            Automata exclude = Omision((Automata)pilaAFN.Pop());
                            pilaAFN.Push(exclude);
                            this.afn = exclude;
                            break;
                        case ".":
                            Automata concat_param1 = (Automata)pilaAFN.Pop();
                            Automata concat_param2 = (Automata)pilaAFN.Pop();
                            Automata concat_result = concatenacion(concat_param1, concat_param2);

                            pilaAFN.Push(concat_result);
                            this.afn = concat_result;
                            break;

                        case "|":
                            Automata union_param1 = (Automata)pilaAFN.Pop();
                            Automata union_param2 = (Automata)pilaAFN.Pop();
                            Automata union_result = union(union_param1, union_param2);
                            pilaAFN.Push(union_result);

                            this.afn = union_result;
                            break;
                        default:
                            //crear un automata con cada simbolo
                            Automata simple = afnSimple((c));
                            pilaAFN.Push(simple);
                            this.afn = simple;
                            break;

                    }
                }
                this.afn.createAlfabeto(regex);
                this.afn.setTipo("AFN");


            }
            catch (System.Exception e)
            {
                MessageBox.Show("Expresión mal ingresada");
            }
            try
            {
                generarDOT(reg, this.afn);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("no se pudo generar el dot");
            }
            try
            {
                //AFDConstructor afd = new AFDConstructor();
                //afd.conversionAFN(this.afn);
                AFNaAFD cons = new AFNaAFD(this.regex,this.nombre+pnombre);
                pnombre++;
                cons.construct();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public Automata cerraduraKleene(Automata automataFN)
        {
            Automata afn_kleene = new Automata();

            //se crea un nuevo estado inicial
            Estado nuevoInicio = new Estado(0);
            afn_kleene.addEstados(nuevoInicio);
            afn_kleene.setEstadoInicial(nuevoInicio);

            //agregar todos los estados intermedio
            for (int i = 0; i < automataFN.getEstados().Count(); i++)
            {
                Estado tmp = (Estado)automataFN.getEstados().ElementAt(i);
                tmp.setId(i + 1);
                afn_kleene.addEstados(tmp);
            }

            //Se crea un nuevo estado de aceptacion
            Estado nuevoFin = new Estado(automataFN.getEstados().Count() + 1);
            afn_kleene.addEstados(nuevoFin);
            afn_kleene.addEstadosAceptacion(nuevoFin);

            //definir estados clave para realizar la cerraduras
            Estado anteriorInicio = automataFN.getEstadoInicial();

            List<Estado> anteriorFin = automataFN.getEstadosAceptacion();

            // agregar transiciones desde el nuevo estado inicial
            nuevoInicio.getTransiciones().Add(new Transicion(nuevoInicio, anteriorInicio, Form1.EPSILON));
            nuevoInicio.getTransiciones().Add(new Transicion(nuevoInicio, nuevoFin, Form1.EPSILON));

            // agregar transiciones desde el anterior estado final
            for (int i = 0; i < anteriorFin.Count(); i++)
            {
                anteriorFin.ElementAt(i).getTransiciones().Add(new Transicion(anteriorFin.ElementAt(i), anteriorInicio, Form1.EPSILON));
                anteriorFin.ElementAt(i).getTransiciones().Add(new Transicion(anteriorFin.ElementAt(i), nuevoFin, Form1.EPSILON));
            }
            afn_kleene.setAlfabeto(automataFN.getAlfabeto());
            afn_kleene.setLenguajeR(automataFN.getLenguajeR());
            return afn_kleene;
        }
        public Automata UnoaMuchos(Automata automataFN)
        {
            Automata afn_kleene = new Automata();

            //se crea un nuevo estado inicial
            Estado nuevoInicio = new Estado(0);
            afn_kleene.addEstados(nuevoInicio);
            afn_kleene.setEstadoInicial(nuevoInicio);

            //agregar todos los estados intermedio
            for (int i = 0; i < automataFN.getEstados().Count(); i++)
            {
                Estado tmp = (Estado)automataFN.getEstados().ElementAt(i);
                tmp.setId(i + 1);
                afn_kleene.addEstados(tmp);
            }

            //Se crea un nuevo estado de aceptacion
            Estado nuevoFin = new Estado(automataFN.getEstados().Count() + 1);
            afn_kleene.addEstados(nuevoFin);
            afn_kleene.addEstadosAceptacion(nuevoFin);

            //definir estados clave para realizar la cerraduras
            Estado anteriorInicio = automataFN.getEstadoInicial();

            List<Estado> anteriorFin = automataFN.getEstadosAceptacion();

            // agregar transiciones desde el nuevo estado inicial
            nuevoInicio.getTransiciones().Add(new Transicion(nuevoInicio, anteriorInicio, Form1.EPSILON));
            //nuevoInicio.getTransiciones().Add(new Transicion(nuevoInicio, nuevoFin, Form1.EPSILON));

            // agregar transiciones desde el anterior estado final
            for (int i = 0; i < anteriorFin.Count(); i++)
            {
                anteriorFin.ElementAt(i).getTransiciones().Add(new Transicion(anteriorFin.ElementAt(i), anteriorInicio, Form1.EPSILON));
                anteriorFin.ElementAt(i).getTransiciones().Add(new Transicion(anteriorFin.ElementAt(i), nuevoFin, Form1.EPSILON));
            }
            afn_kleene.setAlfabeto(automataFN.getAlfabeto());
            afn_kleene.setLenguajeR(automataFN.getLenguajeR());
            return afn_kleene;
        }
        public Automata Omision(Automata automataFN)
        {
            Automata afn_kleene = new Automata();

            //se crea un nuevo estado inicial
            Estado nuevoInicio = new Estado(0);
            afn_kleene.addEstados(nuevoInicio);
            afn_kleene.setEstadoInicial(nuevoInicio);

            //agregar todos los estados intermedio
            for (int i = 0; i < automataFN.getEstados().Count(); i++)
            {
                Estado tmp = (Estado)automataFN.getEstados().ElementAt(i);
                tmp.setId(i + 1);
                afn_kleene.addEstados(tmp);
            }

            //Se crea un nuevo estado de aceptacion
            Estado nuevoFin = new Estado(automataFN.getEstados().Count() + 1);
            afn_kleene.addEstados(nuevoFin);
            afn_kleene.addEstadosAceptacion(nuevoFin);

            //definir estados clave para realizar la cerraduras
            Estado anteriorInicio = automataFN.getEstadoInicial();

            List<Estado> anteriorFin = automataFN.getEstadosAceptacion();

            // agregar transiciones desde el nuevo estado inicial
            nuevoInicio.getTransiciones().Add(new Transicion(nuevoInicio, anteriorInicio, Form1.EPSILON));
            nuevoInicio.getTransiciones().Add(new Transicion(nuevoInicio, nuevoFin, Form1.EPSILON));

            // agregar transiciones desde el anterior estado final
            for (int i = 0; i < anteriorFin.Count(); i++)
            {
                //anteriorFin.ElementAt(i).getTransiciones().Add(new Transicion(anteriorFin.ElementAt(i), anteriorInicio, Form1.EPSILON));
                anteriorFin.ElementAt(i).getTransiciones().Add(new Transicion(anteriorFin.ElementAt(i), nuevoFin, Form1.EPSILON));
            }
            afn_kleene.setAlfabeto(automataFN.getAlfabeto());
            afn_kleene.setLenguajeR(automataFN.getLenguajeR());
            return afn_kleene;
        }
        public Automata concatenacion(Automata AFN1, Automata AFN2)
        {

            Automata afn_concat = new Automata();

            //se utiliza como contador para los estados del nuevo automata
            int i = 0;
            //agregar los estados del primer automata
            for (i = 0; i < AFN2.getEstados().Count(); i++)
            {
                Estado tmp = (Estado)AFN2.getEstados().ElementAt(i);
                tmp.setId(i);
                //se define el estado inicial
                if (i == 0)
                {
                    afn_concat.setEstadoInicial(tmp);
                }
                //cuando llega al último, concatena el ultimo con el primero del otro automata con un epsilon
                if (i == AFN2.getEstados().Count() - 1)
                {
                    //se utiliza un ciclo porque los estados de aceptacion son un array
                    for (int k = 0; k < AFN2.getEstadosAceptacion().Count(); k++)
                    {
                        tmp.setTransiciones(new Transicion((Estado)AFN2.getEstadosAceptacion().ElementAt(k), AFN1.getEstadoInicial(), Form1.EPSILON));
                    }
                }
                afn_concat.addEstados(tmp);

            }
            //termina de agregar los estados y transiciones del segundo automata
            for (int j = 0; j < AFN1.getEstados().Count(); j++)
            {
                Estado tmp = (Estado)AFN1.getEstados().ElementAt(j);
                tmp.setId(i);

                //define el ultimo con estado de aceptacion
                if (AFN1.getEstados().Count() - 1 == j)
                    afn_concat.addEstadosAceptacion(tmp);
                afn_concat.addEstados(tmp);
                i++;
            }

            HashSet<object> alfabeto = new HashSet<object>();
            alfabeto.Add(AFN1.getAlfabeto());
            alfabeto.Add(AFN2.getAlfabeto());
            afn_concat.setAlfabeto(alfabeto);
            afn_concat.setLenguajeR(AFN1.getLenguajeR() + " " + AFN2.getLenguajeR());

            return afn_concat;
        }
        public Automata union(Automata AFN1, Automata AFN2)
        {
            Automata afn_union = new Automata();
            //se crea un nuevo estado inicial
            Estado nuevoInicio = new Estado(0);
            //se crea una transicion del nuevo estado inicial al primer automata
            nuevoInicio.setTransiciones(new Transicion(nuevoInicio, AFN2.getEstadoInicial(), Form1.EPSILON));

            afn_union.addEstados(nuevoInicio);
            afn_union.setEstadoInicial(nuevoInicio);
            int i = 0;//llevar el contador del identificador de estados
                      //agregar los estados del segundo automata
            for (i = 0; i < AFN1.getEstados().Count(); i++)
            {
                Estado tmp = (Estado)AFN1.getEstados().ElementAt(i);
                tmp.setId(i + 1);
                afn_union.addEstados(tmp);
            }
            //agregar los estados del primer automata
            for (int j = 0; j < AFN2.getEstados().Count(); j++)
            {
                Estado tmp = (Estado)AFN2.getEstados().ElementAt(j);
                tmp.setId(i + 1);
                afn_union.addEstados(tmp);
                i++;
            }

            //se crea un nuevo estado final
            Estado nuevoFin = new Estado(AFN1.getEstados().Count() + AFN2.getEstados().Count() + 1);
            afn_union.addEstados(nuevoFin);
            afn_union.addEstadosAceptacion(nuevoFin);


            Estado anteriorInicio = AFN1.getEstadoInicial();
            List<Estado> anteriorFin = AFN1.getEstadosAceptacion();
            List<Estado> anteriorFin2 = AFN2.getEstadosAceptacion();

            // agregar transiciones desde el nuevo estado inicial
            nuevoInicio.getTransiciones().Add(new Transicion(nuevoInicio, anteriorInicio, Form1.EPSILON));

            // agregar transiciones desde el anterior AFN 1 al estado final
            for (int k = 0; k < anteriorFin.Count(); k++)
                anteriorFin.ElementAt(k).getTransiciones().Add(new Transicion(anteriorFin.ElementAt(k), nuevoFin, Form1.EPSILON));
            // agregar transiciones desde el anterior AFN 2 al estado final
            for (int k = 0; k < anteriorFin.Count(); k++)
                anteriorFin2.ElementAt(k).getTransiciones().Add(new Transicion(anteriorFin2.ElementAt(k), nuevoFin, Form1.EPSILON));

            HashSet<object> alfabeto = new HashSet<object>();
            alfabeto.Add(AFN1.getAlfabeto());
            alfabeto.Add(AFN2.getAlfabeto());
            afn_union.setAlfabeto(alfabeto);
            afn_union.setLenguajeR(AFN1.getLenguajeR() + " " + AFN2.getLenguajeR());
            return afn_union;
        }

        public Automata afnSimple(string simboloRegex)
        {
            Automata automataFN = new Automata();
            //definir los nuevos estados
            Estado inicial = new Estado(0);
            Estado aceptacion = new Estado(1);
            //crear una transicion unica con el simbolo
            Transicion tran = new Transicion(inicial, aceptacion, simboloRegex);
            inicial.setTransiciones(tran);
            //agrega los estados creados
            automataFN.addEstados(inicial);
            automataFN.addEstados(aceptacion);
            //colocar los estados iniciales y de aceptacion
            automataFN.setEstadoInicial(inicial);
            automataFN.addEstadosAceptacion(aceptacion);
            automataFN.setLenguajeR(simboloRegex + "");
            return automataFN;

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
                texto += " \"" + automataFinito.getEstadosAceptacion().ElementAt(i).id+ "\"";
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
        public void generarArchivo() { 
            try {
                var command = string.Format("dot -Tjpg "+nombre+".dot -o " + nombre + ".jpg");
                var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/C " + command);
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                proc.WaitForExit();
                Thread.Sleep(2000);
                Process.Start(@""+nombre + ".jpg");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
