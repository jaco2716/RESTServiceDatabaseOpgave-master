using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HotelModel;
using Newtonsoft.Json;

namespace RestConsumer
{
    class Worker
    {

        private const string URI = "http://localhost:58031/api/Facilitys";

        public Worker()
        {
        }

        public void Start()
        {
            List<Facility> Facilitys = GetAll();

            foreach (var Facility in Facilitys)
            {
                Console.WriteLine("Facility:: " + Facility);
            }

            Console.WriteLine("Henter nummer 55");
            Console.WriteLine("Facility :: " + GetOne(55));


            Console.WriteLine("Sletter nummer 55");
            Console.WriteLine("Resultat = " + Delete(55));

            Console.WriteLine("Opretter nyt Facility object id findes ");
            Console.WriteLine("Resultat = " + Post(new Facility(2,'f','f','f','f','f')));

            Console.WriteLine("Opretter nyt Facility object id findes ikke");
            Console.WriteLine("Resultat = " + Post(new Facility(49, "Findes ikke", "vej3")));

            Console.WriteLine("Opdaterer nr 50");
            Console.WriteLine("Resultat = " + Put(50, new Facility(50, "Pouls", "Hillerød")));
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
