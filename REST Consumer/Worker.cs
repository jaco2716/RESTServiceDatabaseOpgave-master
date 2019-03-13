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
            List<Facility> Facilitys = GetAll();
            Delete(4);

                Console.WriteLine("t=true, f=false.");

            var table = new ConsoleTable("Hotel_No", "Bar", "PoolTable", "Restaurant", "SwimmingPool", "TableTennis");
            foreach (var Facili in Facilitys)
            {
//                Console.Write("Hotel Nr:  " + Facility.Hotel_No);
//                Console.Write(" | Bar:  " + Facility.Bar);
//                Console.Write(" | Pool Table: " + Facility.PoolTable);
//                Console.Write(" | Restaurant: " + Facility.Restaurant);
//                Console.Write(" | Swimming Pool: " + Facility.SwimmingPool);
//                Console.Write(" | Table Tennis: " + Facility.TableTennis);
//                Console.WriteLine();
                //Console.WriteLine(Facili.ToString());
                table.AddRow(Facili.Hotel_No, Facili.Bar, Facili.PoolTable, Facili.Restaurant, Facili.SwimmingPool,
                    Facili.TableTennis);
            }
            table.Write(Format.Alternative);

            Console.WriteLine("Henter nummer 4");
            Console.WriteLine("Hotel Nr: " + GetOne(4).Hotel_No );

            Console.WriteLine("Opretter nyt Facility object id findes ikke");
            Console.WriteLine("Resultat = " + Post(new Facility(4, 'f', 'f', 'f', 'f', 'f')));

            Console.WriteLine("Opdaterer nr 4");
            Console.WriteLine("Resultat = " + Put(4, new Facility(4, 't', 't', 't', 't', 't')));

            Console.WriteLine("Opretter nyt Facility object id findes ");
            Console.WriteLine("Resultat = " + Post(new Facility(4,'f','f','f','f','f')));

            Console.WriteLine("Sletter nummer 4");
            Console.WriteLine("Resultat = " + Delete(4));

            Console.WriteLine("Sletter nummer 4");
            Console.WriteLine("Resultat = " + Delete(4));


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
