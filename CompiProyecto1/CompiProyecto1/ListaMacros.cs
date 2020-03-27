using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiProyecto1
{
    public class ListaMacros
    {
        public String nombre;
        public List<Int32> permitidos = new List<Int32>();
        public ListaMacros siguiente;

        public ListaMacros(String nombre, List<Int32> permitidos)
        {
            this.nombre = nombre;
            this.permitidos = permitidos;
            this.siguiente = null;
        }
    }
}
