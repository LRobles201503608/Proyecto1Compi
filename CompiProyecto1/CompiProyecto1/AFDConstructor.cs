//using java.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompiProyecto1
{
    class AFDConstructor
    {
        private Automata afd;
        private Automata afdDirecto;
        private Automata afdMinimo;
        //private  AFNaAFD simulador;
        private java.util.HashMap resultadoFollowPos;
  
    
    public AFDConstructor()
        {
            this.resultadoFollowPos = new java.util.HashMap();
            //simulador = new AFNaAFD();
            afd = new Automata();
        }


        /**
         * Conversion de un automata AFN a uno AFD por el
         * metodo de subconjuntos
         * @param afn AFN
         */
        public void conversionAFN(Automata afn)
        {
            try
            {
                //se crea una estructura vacia
                Automata automata = new Automata();
                //se utiliza una cola como la estructura para guardar los subconjuntos a analizar
                List<HashSet<Estado>> cola = new List<HashSet<Estado>>();
                //se crea un nuevo estado inicial
                Estado inicial = new Estado(0);
                automata.setEstadoInicial(inicial);
                automata.addEstados(inicial);


                //el algoritmo empieza con el e-Closure del estado inicial del AFN
                HashSet<Estado> array_inicial = eClosure(afn.getEstadoInicial());
                //si el primer e-closure contiene estados de aceptacion hay que agregarlo
                foreach (Estado aceptacion in afn.getEstadosAceptacion())
                {
                    if (array_inicial.Contains(aceptacion))
                        automata.addEstadosAceptacion(inicial);
                }

                //lo agregamos a la pila
                cola.Add(array_inicial);
                //variable temporal para guardar el resultado todos los subconjuntos creados
                List<HashSet<Estado>> temporal = new List<HashSet<Estado>>();
                //se utilizan esetos indices para saber el estado actuales y anterior
                int indexEstadoInicio = 0;
                int counter = 0;
                while (counter <cola.Count())
                {
                    
                    //actual subconjunto
                    HashSet<Estado> actual = cola.ElementAt(counter);
                    //se recorre el subconjunto con cada simbolo del alfabeto del AFN
                    HashSet<Object> simbolo = new HashSet<Object>();
                    simbolo = (afn.getAlfabeto());
                    for (int i = 2; i < simbolo.Count(); i++)
                    {
                        //se realiza el move con el subconjunto
                        HashSet<Estado> move_result = move(actual, simbolo.ElementAt(i).ToString());

                        HashSet<Estado> resultado = new HashSet<Estado>();
                        //e-Closure con cada estado del resultado del move y 
                        //se guarda en un solo array (merge)
                        foreach (Estado e in move_result)
                        {
                            resultado = (eClosure(e));
                        }

                        Estado anterior = (Estado)automata.getEstados().ElementAt(indexEstadoInicio);
                        /*Si el subconjunto ya fue creado una vez, solo se agregan
                        transiciones al automata*/
                        if (temporal.Contains(resultado))
                        {
                            List<Estado> array_viejo = automata.getEstados();
                            Estado estado_viejo = anterior;
                            //se busca el estado correspondiente y se le suma el offset
                            Estado estado_siguiente = array_viejo.ElementAt(temporal.IndexOf(resultado) + 1);
                            estado_viejo.setTransiciones(new Transicion(estado_viejo, estado_siguiente, simbolo.ElementAt(i).ToString()));

                        }
                        //si el subconjunto no existe, se crea un nuevo estado
                        else
                        {
                            temporal.Add(resultado);
                            cola.Add(resultado);

                            Estado nuevo = new Estado(temporal.IndexOf(resultado) + 1);
                            anterior.setTransiciones(new Transicion(anterior, nuevo, simbolo.ElementAt(i).ToString()));
                            automata.addEstados(nuevo);
                            //se verifica si el estado tiene que ser de aceptacion
                            foreach (Estado aceptacion in afn.getEstadosAceptacion())
                            {
                                if (resultado.Contains(aceptacion))
                                    automata.addEstadosAceptacion(nuevo);
                            }
                        }


                    }
                    indexEstadoInicio++;
                    counter++;
                }

                this.afd = automata;
                this.afd.setTipo("AFD");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public HashSet<Estado> eClosure(Estado eClosureEstado)
        {
            Stack<Estado> pilaClosure = new Stack<Estado>();
            Estado actual = eClosureEstado;
            actual.getTransiciones();
            HashSet<Estado> resultado = new HashSet<Estado>();

            pilaClosure.Push(actual);
            while (pilaClosure.Count()!=0)
            {
                actual = pilaClosure.Pop();

                foreach (Transicion t in (List<Transicion>)actual.getTransiciones())
                {

                    if (t.getSimbolo().Equals(Form1.EPSILON) && !resultado.Contains(t.getFin()))
                    {
                        resultado.Add(t.getFin());
                        pilaClosure.Push(t.getFin());
                    }
                }
            }
            resultado.Add(eClosureEstado); //la operacion e-Closure debe tener el estado aplicado
            return resultado;
        }

        public HashSet<Estado> move(HashSet<Estado> estados, String simbolo)
        {

            HashSet<Estado> alcanzados = new HashSet<Estado>();
            List<Estado> estado = new List<Estado>();
            for (int i=0; i<estados.Count();i++)
            {
                estado.Add((estados.ElementAt(i)));
            }

            for (int i=0;(i+1)<estado.Count();i++)
            {
                foreach (Transicion t in (List<Transicion>)estado.ElementAt(i+1).getTransiciones())
                {
                    Estado siguiente = t.getFin();
                    String simb = (String)t.getSimbolo();
                    if (simb.Equals(simbolo))
                    {
                        alcanzados.Add(siguiente);
                    }
                }
            }
            /*while (iterador.hasNext())
            {

                foreach (Transicion t in (List<Transicion>)iterador.next().getTransiciones())
                {
                    Estado siguiente = t.getFin();
                    String simb = (String)t.getSimbolo();
                    if (simb.Equals(simbolo))
                    {
                        alcanzados.Add(siguiente);
                    }

                }

            }*/
            return alcanzados;

        }

    }
}
