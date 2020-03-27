using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiProyecto1
{
    class Automata
    {

        private Estado inicial;
        private List<Estado> aceptacion;
        private List<Estado> estados;
        private HashSet<Object> alfabeto;
        private String tipo;
        private String[] resultadoRegex;
        private String lenguajeR;

        public Automata()
        {
            this.estados = new List<Estado>();
            this.aceptacion = new List<Estado>();
            this.alfabeto = new HashSet<Object>();
            this.resultadoRegex = new String[3];
            this.alfabeto.Clear();
        }


        /**
         * Accesor del estado inicial del autómata
         * @return Estado
         */
        public Estado getEstadoInicial()
        {
            return inicial;
        }
        /**
         * Mutador del estado inicial del autómata
         * @param inicial Estado inicial
         */
        public void setEstadoInicial(Estado inicial)
        {
            this.inicial = inicial;
        }
        /**
         * Accesor del Estado<T> de aceptacion o final del autómata
         * @return Estado<T>
         */
        public List<Estado> getEstadosAceptacion()
        {
            return aceptacion;
        }
        /**
         * Mutador del estado final o aceptacion del autómata
         * @param fin Estado final
         */
        public void addEstadosAceptacion(Estado fin)
        {
            this.aceptacion.Add(fin);
        }

        /**
         * Obtener los Estado<T>s del autómata
         * @return Array de Estado<T>s
         */
        public List<Estado> getEstados()
        {
            return estados;
        }

        public Estado getEstados(int index)
        {
            return estados.ElementAt(index);
        }

        /**
         * Agregar un estado al autómata
         * @param estado estructura de estado
         */
        public void addEstados(Estado estado)
        {
            this.estados.Add(estado);
        }


        /**
         * Mostrar los atributos del autómata
         * @return String
         */
        public HashSet<Object> getAlfabeto()
        {
            return alfabeto;
        }

        /**
         * Metodo para definir el alfabeto del automata a partir 
         * de la expresion regular
         * @param regex 
         */
        public void createAlfabeto(List<String> regex)
        {
            for (int i=0; i<regex.Count();i++)
            {
                if (!regex.ElementAt(i).Equals("|") && !regex.ElementAt(i).Equals(".") && !regex.ElementAt(i).Equals("*") && !regex.ElementAt(i).Equals("+")  && !regex.ElementAt(i).Equals("?") && !regex.ElementAt(i).Equals(Form1.EPSILON_CHAR))
                {
                    this.alfabeto.Add(regex.ElementAt(i));
                }
            }
           
        }

        public void setAlfabeto(HashSet<Object> alfabeto)
        {
            this.alfabeto = alfabeto;
        }

        public void setTipo(String tipo)
        {
            this.tipo = tipo;
        }

        public String getTipo()
        {
            return this.tipo;
        }

        public Estado getInicial()
        {
            return inicial;
        }

        public void setInicial(Estado inicial)
        {
            this.inicial = inicial;
        }

        public String[] getResultadoRegex()
        {
            return resultadoRegex;
        }

        public void addResultadoRegex(int key, String value)
        {
            this.resultadoRegex[key] = value;
        }



        
    public String toString()
        {
            String res = "";
            res += "-------" + this.tipo + "---------\r\n";
            res += "Alfabeto " + this.alfabeto + "\r\n";
            res += "Estado inicial " + this.inicial + "\r\n";
            res += "Conjutos de estados de aceptacion " + this.aceptacion + "\r\n";
            res += "Conjunto de Estados " + this.estados.ToString() + "\r\n";
            res += "Conjunto de transiciones ";
            for (int i = 0; i < this.estados.Count(); i++)
            {
                Estado est = estados.ElementAt(i);
                res += est.getTransiciones() + "-";
            }
            res += "\r\n";
            res += "Lenguaje r: " + this.lenguajeR + "\r\n";
            res += "Cadena w ingresada: " + this.resultadoRegex[1] + "\r\n";
            res += "Resultado: " + this.resultadoRegex[2] + "\r\n";


            return res;
        }

        public String getLenguajeR()
        {
            return lenguajeR;
        }

        public void setLenguajeR(String lenguajeR)
        {
            this.lenguajeR = lenguajeR;
        }

    }

}
