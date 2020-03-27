using System.Collections.Generic;

namespace CompiProyecto1
{
    public class Estado
    {
        public int id;
        public List<Transicion> transiciones = new List<Transicion>();
        public Estado(int id, List<Transicion> transiciones)
        {
            this.id = id;
            this.transiciones = transiciones;
        }
        public Estado(int identificador)
        {
            this.id = identificador;
        }
        public int getId()
        {
            return id;
        }
        
        public void setId(int id)
        {
            this.id = id;
        }

        public List<Transicion> getTransiciones()
        {

            return transiciones;
        }

        public void setTransiciones(Transicion tran)
        {
            this.transiciones.Add(tran);
        }

        public string toString()
        {
            return this.id.ToString();
        }
    }
}