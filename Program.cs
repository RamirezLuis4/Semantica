//Luis Angel Ramirez Peña
using System;
using System.IO;

namespace Semantica
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                
                Lenguaje a = new Lenguaje();

                a.Programa();


                
                //while(!a.FinArchivo())
                //{
                  //  a.NextToken();
                //}
                a.cerrar();
               
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }


        }
    }
}