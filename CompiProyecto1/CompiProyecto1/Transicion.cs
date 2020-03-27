namespace CompiProyecto1
{
    public class Transicion
    {
        public Estado inicio;
        //estado final de la transiciones
        public Estado fin;
        //simbolo por el cual se realiza la transicion
        private string simbolo;

        /**
         * Constructor de una transicion
         * @param inicio Estado inicial
         * @param fin Estado final
         * @param simbolo simbolo string o character
         */
        public Transicion(Estado inicio, Estado fin, string simbolo)
        {
            this.inicio = inicio;
            this.fin = fin;
            this.simbolo = simbolo;
        }
        /**
         * Accesor del estado inicial de la transicion
         * @return Estado
         */
        public Estado getInicio()
        {
            return inicio;
        }
        /**
         * Mutador del estado inicial de la transicion
         * @param inicio 
         */
        public void setInicio(Estado inicio)
        {
            this.inicio = inicio;
        }

        /**
         * Accesor del estado final de la transiciones
         * @return Estado
         */
        public Estado getFin()
        {
            return fin;
        }

        /**
         * Mutadro del estado final de la transicion
         * @param fin estado de final o aceptaion
         */
        public void setFin(Estado fin)
        {
            this.fin = fin;
        }
        /**
         * Obtener el simbolo de la transicion
         * @return String
         */
        public string getSimbolo()
        {
            return simbolo;
        }

        /**
         * Mutador del simbolo
         * @param simbolo simbolor string o character
         */
        public void setSimbolo(string simbolo)
        {
            this.simbolo = simbolo;
        }
        /**
         * Mostrar la transicion
         * @return String toString
         */
    public string toString()
        {
            return "(" + inicio.getId() + "-" + simbolo + "-" + fin.getId() + ")";
        }
        public string DOT_String()
        {
            return ("\""+this.inicio.id + "\" -> \"" + this.fin.id + "\" [label=\"" + this.simbolo.Replace("\"","") + "\"];");
        }
    }
}