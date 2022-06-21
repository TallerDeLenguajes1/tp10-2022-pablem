using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;

partial class Program
    {

        //https://json2csharp.com/

        static void Main(string[] args)
        {
            var civilizacion = GetCivilizaciones();
            Console.WriteLine("Lista de civilizaciones cargadas: ");
            foreach (var civil in civilizacion.ListaCivilizaciones) {
                Console.WriteLine("{0}- {1}",civil.Id,civil.Name);
            }
            int opcion;
            do {
                Console.WriteLine("Seleccione una sivilización (0-32): ");
                opcion = Convert.ToInt16(Console.ReadLine());
            } while (opcion < 0 || opcion > 32);
            foreach (var civil in  civilizacion.ListaCivilizaciones)
            {
                if(civil.Id == opcion) {
                    Console.WriteLine("Nombre: {0}",civil.Name);
                    Console.WriteLine("Pertenece a la expansión: {0}",civil.Expansion);
                    Console.WriteLine("Tipo de armada: {0}",civil.ArmyType);
                }
            }
            Console.WriteLine("");

        }


        private static Root GetCivilizaciones()
        {
            var url = @"https://age-of-empires-2-api.herokuapp.com/api/v1/civilizations";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            var civilizacion = new Root();
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader != null){
                            using (StreamReader objReader = new StreamReader(strReader)) {
                                string responseBody = objReader.ReadToEnd();
                                civilizacion = JsonSerializer.Deserialize<Root>(responseBody);
                            }
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Problemas de acceso a la API");
            }
            return civilizacion;
        }
    }
    //     private static void GetClima()
    //     {
    //        var url = $"https://ws.smn.gob.ar/map_items/weather/";
    //         var request = (HttpWebRequest)WebRequest.Create(url);
    //         request.Method = "GET";
    //         request.ContentType = "application/json";
    //         request.Accept = "application/json";
           
    //         try
    //         {
    //             using (WebResponse response = request.GetResponse())
    //             {
    //                 using (Stream strReader = response.GetResponseStream())
    //                 {
    //                     if (strReader == null) return;
    //                     using (StreamReader objReader = new StreamReader(strReader))
    //                     {
    //                         string responseBody = objReader.ReadToEnd();
    //                         List<Root> listclima = JsonSerializer.Deserialize<List<Root>>(responseBody);
    //                         foreach (var Prov in listclima)
    //                         {
    //                             Console.WriteLine("Nombre: " + Prov.name + " Temperatura: " + Prov.weather.temp);
    //                         }

    //                     }
    //                 }
    //             }
    //         }
    //         catch (WebException ex)
    //         {
    //             Console.WriteLine("Problemas de acceso a la API");
    //         }
    //     }
