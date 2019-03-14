using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables.Core;
using HotelModel;
using Newtonsoft.Json;

namespace RestConsumer
{
    class Worker
    {

        private const string URI = "http://localhost:58031/api/Facility";

        public Worker()
        {
        }

        public void Start()
        {
            List<Facility> facilities = GetAll();

            Delete(7);
            Console.WriteLine("t=true, f=false.");

            var table = new ConsoleTable("Hotel_No", "Bar", "PoolTable", "Restaurant", "SwimmingPool", "TableTennis");
            foreach (var Facili in facilities)
            {
                table.AddRow(Facili.Hotel_No, Facili.Bar, Facili.PoolTable, Facili.Restaurant, Facili.SwimmingPool,
                    Facili.TableTennis);
            }
            table.Write(Format.Default);

            Console.WriteLine("Opretter nyt Facility | Resultat = " + Post(new Facility(7, 't', 'f', 't', 'f', 't')));
            Console.WriteLine();
            Console.WriteLine("Har det nye Facility Bar?");
            Console.WriteLine("Hotel Nr: " + GetOne(7).Hotel_No + ", Bar: " + ((GetOne(7).Bar == 't') ? "true" : "false"));
            Console.WriteLine();
            Console.WriteLine("Opdatere Facilty 7 til uden Bar | Resultat = " + Put(7, new Facility(7,'f','f','t','f','t')));
            Console.WriteLine("Printer alle Facilities igen");

            facilities = GetAll();
            table = new ConsoleTable("Hotel_No", "Bar", "PoolTable", "Restaurant", "SwimmingPool", "TableTennis");
            foreach (var Facili in facilities)
            {
                table.AddRow(Facili.Hotel_No, Facili.Bar, Facili.PoolTable, Facili.Restaurant, Facili.SwimmingPool,
                    Facili.TableTennis);
            }
            table.Write(Format.Default);

            Console.WriteLine();
            Console.WriteLine("Sletter Facility 7 | Resultat = "+ Delete(7));
        }


        private List<Facility> GetAll()
        {
            List<Facility> Facilityler = new List<Facility>();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI);
                String jsonStr = resTask.Result;

                Facilityler = JsonConvert.DeserializeObject<List<Facility>>(jsonStr);
            }


            return Facilityler;
        }



        private Facility GetOne(int id)
        {
            Facility Facility = new Facility();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI + "/" + id);
                String jsonStr = resTask.Result;

                Facility = JsonConvert.DeserializeObject<Facility>(jsonStr);
            }


            return Facility;
        }

        private bool Delete(int id)
        {
            bool ok = true;

            using (HttpClient client = new HttpClient())
            {
                Task<HttpResponseMessage> deleteAsync = client.DeleteAsync(URI + "/" + id);

                HttpResponseMessage resp = deleteAsync.Result;
                if (resp.IsSuccessStatusCode)
                {
                    String jsonStr = resp.Content.ReadAsStringAsync().Result;
                    ok = JsonConvert.DeserializeObject<bool>(jsonStr);
                }
                else
                {
                    ok = false;
                }
            }


            return ok;
        }

        private bool Post(Facility Facility)
        {
            bool ok = true;

            using (HttpClient client = new HttpClient())
            {
                String jsonStr = JsonConvert.SerializeObject(Facility);
                StringContent content = new StringContent(jsonStr, Encoding.ASCII, "application/json");

                Task<HttpResponseMessage> postAsync = client.PostAsync(URI, content);

                HttpResponseMessage resp = postAsync.Result;
                if (resp.IsSuccessStatusCode)
                {
                    String jsonResStr = resp.Content.ReadAsStringAsync().Result;
                    ok = JsonConvert.DeserializeObject<bool>(jsonResStr);
                }
                else
                {
                    ok = false;
                }
            }


            return ok;
        }

        private bool Put(int id, Facility Facility)
        {
            bool ok = true;

            using (HttpClient client = new HttpClient())
            {
                String jsonStr = JsonConvert.SerializeObject(Facility);
                StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

                Task<HttpResponseMessage> putAsync = client.PutAsync(URI + "/" + id, content);

                HttpResponseMessage resp = putAsync.Result;
                if (resp.IsSuccessStatusCode)
                {
                    String jsonResStr = resp.Content.ReadAsStringAsync().Result;
                    ok = JsonConvert.DeserializeObject<bool>(jsonResStr);
                }
                else
                {
                    ok = false;
                }
            }


            return ok;
        }



    }
}
