using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiProyecto1
{
    class ListadoNodosExpresion
    {
        public int id = 0;
        public String caracteres;
        public int noHijos = 0;
        public ListadoNodosExpresion siguiente;

        public ListadoNodosExpresion(int id,String caracteres,int noHijos)
        {
            this.id = id;
            this.caracteres = caracteres;
            this.noHijos = noHijos;
            this.siguiente = null;
        }
    }
}
