//Luis Angel Ramirez Peña
//Requerimiento 1: Actualizar el dominante para variables en la expersion.
//                 Ejemplo: float x; char y; y=x
//Requerimiento 2: Actualizar el dominante para el casteo y el valor de la subexpression
//Requerimiento 3: Programar un metodo de conversion de un valor a un tipo de dato 
//                 private float convert(float valor, string TipoDato)
//                 deberan usar el residuo de la division %255, %65535
//Requerimiento 4: 
//Requerimiento 5: 
using System.Collections.Generic;

namespace Semantica
{
    public class Lenguaje : Sintaxis
    {
        List<Variable> listaVariables = new List<Variable>();
        Stack<float> stackOperandos = new Stack<float>();
        Variable.TipoDato dominante;
        public Lenguaje()
        {

        }
        public Lenguaje(string nombre) : base(nombre)
        {

        }
        private void addVariable(string name, Variable.TipoDato type)
        {
            listaVariables.Add(new Variable(name, type));
        }
        private void displayVariables()
        {
            log.WriteLine("\nVariables: ");
            foreach (Variable v in listaVariables)
            {
                log.WriteLine(v.getNombre() + " " + v.getTipo() + " " + v.getValor());
            }
        }
        private bool existeVariable(string name)
        {
            foreach (Variable v in listaVariables)
            {
                if (v.getNombre().Equals(name))
                    return true;
            }
            return false;
        }
        private void modValor(string name, float newValue)
        {
            //Requerimiento 3
            foreach (Variable v in listaVariables)
            {
                if (v.getNombre().Equals(name))
                {
                    v.setValor(newValue);
                }
            }
        }
        private float getValor(string nameVariable)
        {
            //Requerimiento 4.
            foreach (Variable v in listaVariables)
            {
                if (v.getNombre().Equals(nameVariable))
                {
                    return v.getValor();
                }
            }
            return 0;
        }
        private Variable.TipoDato getTipo(string nameVariable)
        {
            foreach (Variable v in listaVariables)
            {
                if (v.getNombre().Equals(nameVariable))
                {
                    return v.getTipo();
                }
            }
            return Variable.TipoDato.Char;
        }
        //Programa -> Librerias? Variables? Main
        public void Programa()
        {
            Librerias();
            Variables();
            Main();
            displayVariables();
        }
        // Librerias -> #include<identificador(.h)?> Librerias?
        private void Librerias()
        {
            if (getContenido() == "#")
            {
                match("#");
                match("include");
                match("<");
                match(Tipos.Identificador);
                if (getContenido() == ".")
                {
                    match(".");
                    match("h");
                }
                match(">");
                Librerias();
            }
        }
        // Variables -> tipoDato Lista_identificadores ; Variables?
        private void Variables()
        {
            if (getClasificacion() == Tipos.TipoDato)
            {
                Variable.TipoDato type = Variable.TipoDato.Char;
                switch (getContenido())
                {
                    case "int": type = Variable.TipoDato.Int; break;
                    case "float": type = Variable.TipoDato.Float; break;
                }
                match(Tipos.TipoDato);
                Lista_identificadores(type);
                match(Tipos.FinSentencia);
                Variables();
            }
        }
        // Lista_identificadores -> identificador (,Lista_identificadores)?
        private void Lista_identificadores(Variable.TipoDato type)
        {
            if (getClasificacion() == Token.Tipos.Identificador)
            {
                if (!existeVariable(getContenido()))
                    addVariable(getContenido(), type);
                else
                    throw new Error("Error de sintáxis. Variable duplicada \"" + getContenido() + "\" en la línea " + linea + ".", log);
            }
            match(Tipos.Identificador);
            if (getContenido() == ",")
            {
                match(",");
                Lista_identificadores(type);
            }
        }
        // Bloque_Instrucciones -> {Lista_Instrucciones?}
        private void Bloque_Instrucciones(bool evaluacion)
        {
            match("{");
            if (getContenido() != "}")
            {
                Lista_Instrucciones(evaluacion);
            }
            match("}");
        }
        // Lista_Instrucciones -> Instruccion Lista_Instrucciones?
        private void Lista_Instrucciones(bool evaluacion)
        {
            Instruccion(evaluacion);
            if (getContenido() != "}")
            {
                Lista_Instrucciones(evaluacion);
            }
        }
        // Instruccion -> Printf | Scanf | If | While | Do | For | Switch | Asignacion
        private void Instruccion(bool evaluacion)
        {
            if (getContenido() == "printf")
                Printf(evaluacion);
            else if (getContenido() == "scanf")
                Scanf(evaluacion);
            else if (getContenido() == "if")
                If(evaluacion);
            else if (getContenido() == "while")
                While(evaluacion);
            else if (getContenido() == "do")
                Do(evaluacion);
            else if (getContenido() == "for")
                For(evaluacion);
            else if (getContenido() == "switch")
                Switch(evaluacion);
            else
            {
                Asignacion(evaluacion);
                //Console.WriteLine("Error de sintaxis. No se reconoce la instruccion: " + getContenido());
                //nextToken();
            }
        }
        private Variable.TipoDato evaluanumero(float resultado)
        {
            if(resultado % 1 != 0)
            {
                return Variable.TipoDato.Float;
            }
            if(resultado <= 255)
            {
                return Variable.TipoDato.Char;
            }
            else if(resultado <= 65535)
            {
                return Variable.TipoDato.Int;
            }
            return Variable.TipoDato.Float;
        }
        private bool evaluasemantica(string variable, float resultado)
        {
            Variable.TipoDato tipoDato = getTipo(variable);
            return false;
        }
        // Asignacion -> identificador = cadena | Expresion ;
        private void Asignacion(bool evaluacion)
        {
            //Requerimiento 2. Si no existe la variable, se levanta la excepción.
            if (!existeVariable(getContenido()))
            {
                throw new Error("Error de sintáxis: Variable no existe \"" + getContenido() + "\" en la línea " + linea + ".", log);
            }
            log.WriteLine();
            log.Write(getContenido() + " = ");
            string name = getContenido();
            match(Tipos.Identificador);
            match(Tipos.Asignacion);
            dominante = Variable.TipoDato.Char;
            Expresion();
            match(";");
            float resultado = stackOperandos.Pop();
            log.Write("= " + resultado);
            log.WriteLine();
            if (dominante < evaluanumero(resultado))
            {
                dominante = evaluanumero(resultado);
            }
            if(dominante <= getTipo(name))
            {
                if (evaluacion)
                {
                    modValor(name, resultado);
                }
            }
            else 
            {
                 throw new Error("Error de semantica: no podemos asignar un: <" +dominante + "> a un <" + getTipo(name) +  "> en linea  " + linea, log);
            }
        }
        // Printf -> printf (string | Expresion);
        private void Printf(bool evaluacion)
        {
            match("printf");
            match("(");
            if (getClasificacion() == Tipos.Cadena)
            {
                string comilla = getContenido();
                if ((evaluacion))
                {
                    comilla = comilla.Replace("\\n" , "\n");
                    comilla = comilla.Replace("\\t" , "\t");
                    Console.Write(comilla.Substring(1, comilla.Length - 2));
                    //Console.Write(comilla);
                }
                match(Tipos.Cadena);
            }
            else
            {
                Expresion();
                float resultado = stackOperandos.Pop();
                if (evaluacion)
                {
                Console.Write(stackOperandos.Pop());
                }
            }
            match(")");
            match(";");
        }
        // Scanf -> scanf (string, &Identificador);
        private void Scanf()
        {
            match("scanf");
            match("(");
            match(Tipos.Cadena);
            match(",");
            match("&");
            //Requerimiento 2. Si no existe la variable, se levanta la excepción.
            if (!existeVariable(getContenido()))
            {
                throw new Error("Error de sintáxis: Variable no existe \"" + getContenido() + "\" en la línea " + linea + ".", log);
            }
            string value = "" + Console.ReadLine();
            float valor = float.Parse(value);
            //Requerimiento 5. Modificar el valor de la variable.
            modValor(getContenido(), valor);
            match(Tipos.Identificador);
            match(")");
            match(";");
        }
        // If -> if (Condicion) Bloque_Instrucciones (else Bloque_Instrucciones)?
        private void If(bool evaluacion)
        {
            match("if");
            match("(");
            bool validarif = Condicion();
            match(")");
            if (getContenido() == "{")
                Bloque_Instrucciones(validarif);
            else
                Instruccion(validarif);
            if (getContenido() == "else")
            {
                match("else");
                if (getContenido() == "{")
                    Bloque_Instrucciones(validarif);
                else
                    Instruccion(validarif);
            }
        }
        // While -> while(Condicion) Bloque_Instrucciones | Instruccion
        private void While( bool evaluacion)
        {
            match("while");
            match("(");
            Condicion();
            match(")");
            if (getContenido() == "{")
                Bloque_Instrucciones(evaluacion);
            else
                Instruccion(evaluacion);
        }
        // Do -> do Bloque_Instrucciones | Instruccion while(Condicion);
        private void Do(bool evaluacion)
        {
            match("do");
            if (getContenido() == "{")
                Bloque_Instrucciones(evaluacion);
            else
                Instruccion(evaluacion);
            match("while");
            match("(");
            Condicion();
            match(")");
            match(";");
        }
        // For -> for (Asignacion Condición ; Incremento) Bloque_Instrucciones | Instruccion
        private void For(bool evaluacion)
        {
            match("for");
            match("(");
            Asignacion(evaluacion);
            Condicion();
            match(";");
            Incremento(evaluacion);
            match(")");
            if (getContenido() == "{")
                Bloque_Instrucciones(evaluacion);
            else
                Instruccion(evaluacion);
        }
        // Incremento -> identificador ++ | --
        private void Incremento(bool evaluacion)
        {
            string variable = getContenido();
            //Requerimiento 2. Si no existe la variable, se levanta la excepción.
            if (!existeVariable(getContenido()))
            {
                throw new Error("Error de sintáxis: Variable no existe \"" + getContenido() + "\" en la línea " + linea + ".", log);
            }
            match(Tipos.Identificador);
            if (getClasificacion() == Tipos.IncrementoTermino)
            {
                if (getContenido()[0] == '+')
                {
                    match("++");
                    if (evaluacion)
                    {
                        modValor(variable, getValor(variable) + 1);
                    }
                }
                else
                {
                    match("--");
                    if (evaluacion)
                    {
                    modValor(variable, getValor(variable) - 1);
                    }
                }
            }
            else
                match(Tipos.IncrementoTermino);
        }
        // Switch -> switch (Expresion) { Lista_Casos (default: (Lista_Instrucciones_Case | Bloque_Instrucciones)? (break;)? )? }
        private void Switch(bool evaluacion)
        {
            match("switch");
            match("(");
            Expresion();
            stackOperandos.Pop();
            match(")");
            match("{");
            Lista_Casos(evaluacion);
            if (getContenido() == "default")
            {
                match("default");
                match(":");
                if (getContenido() != "}" && getContenido() != "{")
                    Lista_Instrucciones_Case(evaluacion);
                else if (getContenido() == "{")
                    Bloque_Instrucciones(evaluacion);
                if (getContenido() == "break")
                {
                    match("break");
                    match(";");
                }
            }
            match("}");
        }
        // Lista_Casos -> case Expresion: (Lista_Instrucciones_Case | Bloque_Instrucciones)? (break;)? (Lista_Casos)?
        private void Lista_Casos(bool evaluacion)
        {
            if (getContenido() != "}" && getContenido() != "default")
            {
                match("case");
                Expresion();
                stackOperandos.Pop();
                match(":");
                if (getContenido() != "case" && getContenido() != "{")
                    Lista_Instrucciones_Case(evaluacion);
                else if (getContenido() == "{")
                    Bloque_Instrucciones(evaluacion);
                if (getContenido() == "break")
                {
                    match("break");
                    match(";");
                }
                Lista_Casos(evaluacion);
            }
        }
        // Lista_Instrucciones_Case -> Instruccion Lista_Instrucciones_Case?
        private void Lista_Instrucciones_Case(bool evaluacion)
        {
            Instruccion(evaluacion);
            if (getContenido() != "break" && getContenido() != "case" && getContenido() != "default" && getContenido() != "}")
                Lista_Instrucciones_Case(evaluacion);
        }
        // Condicion -> Expresion operadorRelacional Expresion
        private bool Condicion()
        {
            Expresion();
            string operador = getContenido();
            match(Tipos.OperadorRelacional);
            Expresion();
            float e2 = stackOperandos.Pop();
            float e1 = stackOperandos.Pop();
            switch(operador)
            {
                case "==":
                    return e1 == e2;
                case ">":
                    return e1 > e2;
                case ">=":
                    return e1 >= e2;
                case "<":
                    return e1 < e2;
                case "<=":
                    return e1 <= e2;
                default: 
                return e1 != e2;
                    
            }   
        }
        // Main -> void main() Bloque_Instrucciones 
        private void Main(bool evaluacion)
        {
            match("void");
            match("main");
            match("(");
            match(")");
            Bloque_Instrucciones(evaluacion);
        }
        // Expresion -> Termino MasTermino
        private void Expresion()
        {
            Termino();
            MasTermino();
        }
        // MasTermino -> (operadorTermino Termino)?
        private void MasTermino()
        {
            if (getClasificacion() == Tipos.OperadorTermino)
            {
                string operador = getContenido();
                match(Tipos.OperadorTermino);
                Termino();
                log.Write(operador + " ");
                float n1 = stackOperandos.Pop();
                float n2 = stackOperandos.Pop();
                switch (operador)
                {
                    case "+":
                        stackOperandos.Push(n2 + n1);
                        break;
                    case "-":
                        stackOperandos.Push(n2 - n1);
                        break;
                }
            }
        }
        // Termino -> Factor PorFactor
        private void Termino()
        {
            Factor();
            PorFactor();
        }
        // PorFactor -> (operadorFactor Factor)?
        private void PorFactor()
        {
            if (getClasificacion() == Tipos.OperadorFactor)
            {
                string operador = getContenido();
                match(Tipos.OperadorFactor);
                Factor();
                log.Write(operador + " ");
                float n1 = stackOperandos.Pop();
                float n2 = stackOperandos.Pop();
                switch (operador)
                {
                    case "*":
                        stackOperandos.Push(n2 * n1);
                        break;
                    case "/":
                        stackOperandos.Push(n2 / n1);
                        break;
                }
            }
        }
        // Factor -> numero | identificador | (Expresion)
        private void Factor()
        {
            if (getClasificacion() == Tipos.Numero)
            {
                log.Write(getContenido() + " ");
                if(dominante < evaluanumero(float.Parse(getContenido())))
                {
                    dominante = evaluanumero(float.Parse(getContenido()));
                }
                stackOperandos.Push(float.Parse(getContenido()));
                match(Tipos.Numero);
            }
            else if (getClasificacion() == Tipos.Identificador)
            {
                //Requerimiento 2. Si no existe la variable, se levanta la excepción.
                if (!existeVariable(getContenido()))
                {
                    throw new Error("Error de sintáxis: Variable no existe \"" + getContenido() + "\" en la línea " + linea + ".", log);
                }
                log.Write(getContenido() + " ");
                //Requerimiento 1 : Es con un if como ese
                /*if(dominante < evaluanumero(float.Parse(getContenido())))
                {
                    dominante = evaluanumero(float.Parse(getContenido()));
                }*/
                stackOperandos.Push(getValor(getContenido()));
                match(Tipos.Identificador);
            }
            else
            {
                bool hubocasteo = false;
                Variable.TipoDato casteo = Variable.TipoDato.Char;
                match("(");
                if (getClasificacion() == Tipos.TipoDato)
                {
                    hubocasteo = true;
                    switch(getContenido())
                    {
                        case "char":
                            casteo = Variable.TipoDato.Char;
                            break;
                        case "int":
                            casteo = Variable.TipoDato.Int;
                            break;
                        case "float":
                            casteo = Variable.TipoDato.Float;
                            break;
                    }
                    match(Tipos.TipoDato);
                    match(")");
                    match("(");
                }
                Expresion();
                match(")");
                if (hubocasteo)
                {
                    //Requerimiento 2: Actualizar dominande en base a casteo
                    //Saco un elemnto del satck
                    //Convierto ese valor al equivalente en casteo
                    //Requerimiento 3:
                    //Ejemplo: si el casteo es char y el Pop regresa un 256
                    //el valor equivalente en casteo es 0

                }
            }
        }
    }
}