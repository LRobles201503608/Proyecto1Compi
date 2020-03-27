using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace CompiProyecto1
{
   public class Analizador
    {
        int pnombre = 0;
        Stack pila = new Stack();
        String cadena;
        List<Int32> asciimacros = new List<Int32>();
        List<String> listaExpresiones = new List<String>();
        List<Stack> listapilas = new List<Stack>();
        ListaMacros primero, ultimo;
        ListadoNodosExpresion prim, ult;
        List<String> regexInOrder = new List<String>();
        List<String> tokens = new List<String>();
        List<String> erroresLexicos = new List<String>();
        public Analizador(String cadena)
        {
            this.prim = null;
            this.ult = prim;
            this.cadena = cadena;
            this.primero = null;
            this.ultimo = this.primero;
        }

        public void AnalisisLexico(String cad)
        {
            int linea = 1;
            int contacomillas = 0;
            String cadconcat = "";
            int estado = 0;
            int caracter = 0;
            char caracteractual;
            int contaporcent = 0;
            String nombreExpre = "";
            String nombremacroaux = "";
            String expresionaux = "";

            for (int i = 0; i<cad.Length; i++)
            {
                caracteractual = cad.ElementAt(i);
                caracter = (int)caracteractual;
                switch (estado)
                {
//**************************************************Estado 0****************************************************************
                    case 0:
                        if (caracter == 47)
                        {
                            estado = 1;
                        }
                        else if (caracter == 60)
                        {
                            estado = 2;
                        }
                        else if ((caracter > 64 && caracter < 91) || (caracter > 96 && caracter < 123))
                        {
                            cadconcat += caracteractual;
                            estado = 3;
                        }
                        else if (caracteractual == '\n' || caracteractual == '\t' || caracter == 32)
                        {
                            estado = 0;
                            linea++;
                        }
                        else
                        {
                            estado = 90;
                        }
                        break;
//***************************************Estado 1 eliminacion de comentarios una linea***************************************                   
                    case 1:
                        if (caracter == 47)
                        {
                            estado = 1;
                        }
                        else if ((caracter > 64 && caracter < 91) || (caracter > 96 && caracter < 123))
                        {
                            estado = 1;
                        }
                        else if (caracteractual == '\n')
                        {
                            estado = 0;
                            linea++;
                        }
                        else
                        {
                            estado = 1;
                        }
                        break;
//***************************************Estado 2 Elimincacion de comentarios de multi linea *********************************
                    case 2:
                        if (caracter == 33 || caracter == 32)
                        {
                            estado = 2;
                        }
                        else if ((caracter > 64 && caracter < 91) || (caracter > 96 && caracter < 123))
                        {
                            estado = 2;
                        }
                        else if (caracteractual == '\n')
                        {
                            estado = 2;
                            linea++;
                        }
                        else if (caracter == 62)
                        {
                            estado = 0;
                        }
                        else
                        {
                            estado = 2;
                        }
                        break;
//************************************Estado 3 Concatenacion de caracteres(IDs)***********************************************
                    case 3:
                        if ((caracter > 64 && caracter < 91) || (caracter > 96 && caracter < 123) || (caracter > 47 && caracter < 58))
                        {
                            cadconcat += caracteractual;
                            estado = 3;
                        }
                        else if (caracter == 45)
                        {
                            estado = 4;
                            nombreExpre = cadconcat;
                            tokens.Add("Token: " + cadconcat + " en la linea: " + linea);
                            cadconcat = "";
                        }
                        if (caracter == 58 && cadconcat.Equals("CONJ"))
                        {
                            tokens.Add("Token: "+cadconcat+" en la linea: "+linea);
                            cadconcat = "";
                            estado = 5;
                        }
                        else if (caracter == 58 && !cadconcat.Equals("CONJ"))
                        {
                            tokens.Add("Token: " + cadconcat + " en la linea: " + linea);
                            cadconcat = "";
                            estado = 6;
                            nombreExpre = cadconcat;
                        }
                        break;
//*********************************Estado 4 inicio de creacion de expresiones ************************************************
                    case 4:
                        if (caracter == 46 || caracter == 124)
                        {
                            if (contacomillas==1)
                            {
                                String caractostring = "\""+Char.ToString(caracteractual)+ "\"";
                                pila.Push(caractostring);
                                expresionaux += caractostring;
                                estado = 4;
                            }
                            else
                            {
                                String caractostring = Char.ToString(caracteractual);
                                pila.Push(caractostring);
                                expresionaux += caractostring;
                                estado = 4;
                            }
                        }
                        else if (caracter == 42 || caracter == 43 || caracter == 63)
                        {
                            if (contacomillas==1)
                            {
                                String caractostring = "\""+Char.ToString(caracteractual)+ "\"";
                                pila.Push(caractostring);
                                expresionaux += caractostring;
                                estado = 4;
                            }
                            else
                            {
                                String caractostring = Char.ToString(caracteractual);
                                pila.Push(caractostring);
                                expresionaux += caractostring;
                                estado = 4;
                            }

                        }
                        else if (caracter == 34)
                        {
                            contacomillas++;
                            if (contacomillas == 2)
                            {
                                // añadir ingreso a pila
                                cadconcat += caracteractual;
                                tokens.Add("Token: " + cadconcat + " en la linea: " + linea);
                                pila.Push(cadconcat);
                                contacomillas = 0;
                                cadconcat = "";
                            }
                            else
                            {
                                cadconcat += caracteractual;
                                estado = 4;
                            }
                        }
                        else if ((caracter > 64 && caracter < 91) || (caracter > 96 && caracter < 123) || (caracter > 47 && caracter < 58))
                        {
                            cadconcat += caracteractual;
                            expresionaux += caracteractual;
                            estado = 4;
                        }
                        else if (caracter == 123)
                        {
                            cadconcat += caracteractual;
                            tokens.Add("Token: " + cadconcat + " en la linea: " + linea);
                            estado = 4;   
                        }
                        else if (caracter == 125)
                        {
                            cadconcat += caracteractual;
                            tokens.Add("Token: " + cadconcat + " en la linea: " + linea);
                            tokens.Add("Token: }" + " en la linea: " + linea);
                            pila.Push(cadconcat);
                            cadconcat = "";

                        }
                        else if (caracter == 59)
                        {
                            if (contacomillas == 1)
                            {
                                cadconcat += caracteractual;
                            }
                            else
                            {
                                tokens.Add("Token: " + cadconcat + " en la linea: " + linea);
                                tokens.Add("Token: ;" + " en la linea: " + linea);
                                estado = 10;
                            }
                        }
                        else if (caracter == 62)
                        {
                            if (contacomillas == 1)
                            {
                                cadconcat += caracteractual;
                                tokens.Add("Token: " + cadconcat + " en la linea: " + linea);
                            }
                            else
                            {
                                tokens.Add("Token: " + cadconcat + " en la linea: " + linea);
                                estado = 4;
                            }
                        }
                        else if (caracter == 32)
                        {
                            estado = 4;
                        }
                        else
                        {
                            if (contacomillas == 1)
                            {
                                tokens.Add("Token: " + cadconcat + " en la linea: " + linea);
                                cadconcat += caracteractual;
                            }
                            else
                            {
                                estado = 90;
                            }
                        }
                        break;

//***********************************Estado 5 inicio de comprovacion de macros************************************************                    
                    case 5:
                        if ((caracter > 64 && caracter < 91) || (caracter > 96 && caracter < 123) || (caracter > 47 && caracter < 58))
                        {
                            cadconcat += caracteractual;
                            estado = 5;
                        }
                        else if (caracter == 45)
                        {
                            nombremacroaux = cadconcat;
                            tokens.Add("Token: " + cadconcat + " en la linea: " + linea);
                            cadconcat = "";
                            estado = 7;
                        }
                        else if (caracter == 32)
                        {
                            estado = 5;
                        }
                        else
                        {
                            cadconcat += caracteractual;
                            estado = 5;
                        }
                        
                        break;
//*********************************Estado 7 aqui se calidará todo aquello que venga despues de la flechita en los macros******
                    case 7:
                        if (nombremacroaux.Equals("[:todo:]"))
                        {

                        }
                        if ((caracter > 64 && caracter < 91) || (caracter > 96 && caracter < 123) || (caracter > 47 && caracter < 58))
                        {
                            tokens.Add("Token: " + caracteractual + " en la linea: " + linea);
                            asciimacros.Add(caracter);
                            estado = 7;
                        }
                        else if (caracter == 44)
                        {
                            tokens.Add("Token: ," + " en la linea: " + linea);
                            estado = 7;
                        }
                        else if (caracter == 126)
                        {
                            tokens.Add("Token: " + caracteractual + " en la linea: " + linea);
                            asciimacros.Add(caracter);
                            estado = 7;
                        }
                        else if (caracter == 59)
                        {
                            tokens.Add("Token: " + caracteractual + " en la linea: " + linea);
                            ValidarMacros(nombremacroaux);
                            estado = 0;
                        }
                        else if (caracter == 62 || caracter == 32)
                        {
                            estado = 7;
                        }
                        break;
//***********************************Estado 10 aceptacion de las expresiones e ingreso de las mismas a las listas de expresiones********************
                    case 10:
                        listapilas.Add(pila);
                        expresionaux = "";
                        estado = 0;
                        ConvertirInFija(pila,nombreExpre);
                        int limite = pila.Count;
                        for (int a =0; a<pila.Count;a++)
                        {
                            pila.Pop();
                        }
                        break;
                    case 90:
                        MessageBox.Show("Error lexico detectado en linea: "+linea);
                        String carerror = cad.ElementAt(i - 1).ToString();
                        erroresLexicos.Add("Error lexico detectado en linea: " + linea + " con caracter: "+ carerror);
                        estado = 0;
                        break;
                }
            }
            try
            {
                if (pila.Count != 0)
                {
                    listapilas.Add(pila);
                    expresionaux = "";
                    estado = 0;
                    ConvertirInFija(pila,"ultima");
                    int limite = pila.Count;
                    for (int a = 0; a < pila.Count; a++)
                    {
                        pila.Pop();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Reportes rep = new Reportes();
            rep.GenerarHTMLTokens(tokens);
            rep.GenerarHTMLErrores(erroresLexicos);
            //GenerarThompson();
        }

        public void ConvertirInFija(Stack listapilas,String nombreexpre)
        {
            int limite = listapilas.Count;
            Stack auxiliar = new Stack();
            Stack auxiliar2 = new Stack();
            Stack auxiliar3 = new Stack();
            List<String> regeeex = new List<String>();
            auxiliar3 = listapilas;
            while (auxiliar3.Count!=0)
            {
                regeeex.Add((String)auxiliar3.Pop());
            }
            AFNConstructor auto = new AFNConstructor(regeeex, nombreexpre+pnombre);
            auto.construct();
            pnombre++;
            /*auxiliar = listapilas;
            AFNConstructor auto = new AFNConstructor(regeeex,nombreexpre);
            auto.construct();
            while (auxiliar.Count!=0)
                {
                    String valor = (String)auxiliar.Pop();
                    switch (valor)
                    {
                        case ".":
                            String val1 = (String)auxiliar2.Pop();
                            String val2 = (String)auxiliar2.Pop();
                            String mix = "("+val1 + "." + val2+")";
                            auxiliar2.Push(mix);
                            break;
                        case "|":
                            String v1 = (String)auxiliar2.Pop();
                            String v2 = (String)auxiliar2.Pop();
                            String mix2 = "("+v1 + "|" + v2+")";
                            auxiliar2.Push(mix2);
                            break;
                        case "*":
                            String va1 = (String)auxiliar2.Pop();
                            String mix3 = ""+va1 + "*";
                            auxiliar2.Push(mix3);
                            break;
                        case "+":
                            String valu1 = (String)auxiliar2.Pop();
                            String mix4 = "" + valu1 + "+";
                            auxiliar2.Push(mix4);
                            break;
                        case "?":
                            String value1 = (String)auxiliar2.Pop();
                            String mix5 = "" + value1 + "?";
                            auxiliar2.Push(mix5);
                            break;
                        default:
                            auxiliar2.Push(valor);
                            break;
                    }
                }
                String regex = (String)auxiliar2.Pop();
                listaExpresiones.Add(regex);
                SegundoAnalisis(regex);*/
        }
        private void SegundoAnalisis(String regex)
        {
            String cad = regex;
            String cadconcat = "";
            int estado = 0;
            int caracter = 0;
            char caracteractual;
            int contaporcent = 0;
            String nombreExpre = "";
            String nombremacroaux = "";
            String expresionaux = "";

            for (int i = 0; i < cad.Length; i++)
            {
                caracteractual = cad.ElementAt(i);
                caracter = (int)caracteractual;
                switch (estado)
                {
                    case 0:
                        if (caracter == 40 || caracter == 41)
                        {
                            estado = 0;
                        }else if (caracter == 34)
                        {
                            cadconcat += caracteractual;
                            estado = 1;
                        }else if (caracter == 123)
                        {
                            cadconcat += caracteractual;
                            estado = 2;
                        }
                        else
                        {
                            regexInOrder.Add(caracteractual.ToString());
                        }
                        break;
                    case 1:
                        if (caracter == 34)
                        {
                            cadconcat += caracteractual;
                            regexInOrder.Add(cadconcat);
                            cadconcat = "";
                            estado = 0;
                        }
                        else
                        {
                            cadconcat += caracteractual;
                            estado = 1;
                        }
                        break;
                    case 2:
                        if (caracter == 125)
                        {
                            cadconcat += caracteractual;
                            regexInOrder.Add(cadconcat);
                            cadconcat = "";
                            estado = 0;
                        }
                        else
                        {
                            cadconcat += caracteractual;
                            estado = 2;
                        }
                        break;
                }
            }
            //AFNConstructor auto = new AFNConstructor(regexInOrder);
            //auto.construct();
        }
        private void ValidarMacros(String nombremacroaux)
        {
            int asciiInf = 0;
            int asciiSup = 0;
            int conta = 0;
            List<Int32> aux = new List<Int32>();

            if (asciimacros.Count() == 3 && asciimacros.ElementAt(1) == 126)
            {
                asciiInf = asciimacros.ElementAt(0);
                asciiSup = asciimacros.ElementAt(2);
                conta = asciiSup - asciiInf;
                asciimacros.Clear();
                for (int a = 0; a <= conta; a++)
                {
                    aux.Add(asciiInf + a);
                }
            }
            else
            {
                for (int a = 0; a < asciimacros.Count(); a++)
                {
                    asciiInf = asciimacros.ElementAt(a);
                    aux.Add(asciiInf);
                }
                asciimacros.Clear();
            }
            if (primero == null)
            {
                ListaMacros nuevo = new ListaMacros(nombremacroaux, aux);
                nuevo.siguiente = null;
                primero = nuevo;
                ultimo = primero;
            }
            else
            {
                ListaMacros nuevo = new ListaMacros(nombremacroaux, aux);
                nuevo.siguiente = null;
                ultimo.siguiente = nuevo;
                ultimo = ultimo.siguiente;
            }
        }
        public void AgregaraLNE(int id,String caracteres)
        {
            ListadoNodosExpresion nuevo = new ListadoNodosExpresion(id,caracteres,0);
            if (prim==null)
            {
                prim = nuevo;
                nuevo.siguiente = null;
                ult = prim;
            }
            else
            {
                ult.siguiente = nuevo;
                nuevo.siguiente = null;
                ult = nuevo;
            }
        }
        public void GenerarThompson()
        {

        }
    }
}
