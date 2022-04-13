using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using Azure;
using MySql.Data.MySqlClient;

namespace blobService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer(); // name space(using System.Timers;)  
            
public Service1()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {

            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 1000; //number in milisecinds  
            timer.Enabled = true;
            //AddBlobs();
        

        }
        protected override void OnStop()
        {


        }



        //public static void AddBlobs()

        //{

        //    var sasURI = "https://ffinternalstorage.blob.core.windows.net/blob-deletion-test/testimg.png?sp=racwdl&st=2022-04-11T05:10:01Z&se=2022-04-17T13:10:01Z&skoid=01f055dd-cdcb-4630-a95a-c9d17710f093&sktid=4526bb6e-a7c5-42df-8b2a-9f6d23e2c9c4&skt=2022-04-11T05:10:01Z&ske=2022-04-17T13:10:01Z&sks=b&skv=2020-08-04&sv=2020-08-04&sr=c&sig=oPmfi8drFf0PywWRQIzu35ojYyJA9OcfMuhsFUysWd4%3D";



        //    byte[] bytes = System.IO.File.ReadAllBytes(@"D:\BLOB\sort.png");
        //    var client = new HttpClient();
        //    var content = new StringContent("Some content");
        //    content.Headers.Add("x-ms-blob-type", "BlockBlob");
        //    client.DefaultRequestHeaders.Add("x-ms-blob-type", "BlockBlob");
        //    var response = client.PutAsync(sasURI, new ByteArrayContent(bytes)).Result;

        //    response.EnsureSuccessStatusCode();

        //    Console.WriteLine(response);
        //    Console.WriteLine("File uploaded Successfully");


        //}
        
        public static void DeleteBlobs()
        {
            

            string connStr = "server=localhost;user=root;database=blob;port=3306;password=92727";

            MySqlConnection conn = new MySqlConnection(connStr);
            try
                
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                //SQL Query to execute
           
                string sql = "SELECT * FROM blob.testdb where isDeleted=1;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                //read the data
                while (rdr.Read())
                    
                {

                    Console.WriteLine(rdr[0] + " -- " + rdr[1] + " -- " + rdr[2]);
                    try
                    {

                        var clientB = new HttpClient();
                        var blobname = rdr[1];
                        var deleteuri = "https://ffinternalstorage.blob.core.windows.net/blob-deletion-test/" + blobname + "?sp=racwdl&st=2022-04-11T05:10:01Z&se=2022-04-17T13:10:01Z&skoid=01f055dd-cdcb-4630-a95a-c9d17710f093&sktid=4526bb6e-a7c5-42df-8b2a-9f6d23e2c9c4&skt=2022-04-11T05:10:01Z&ske=2022-04-17T13:10:01Z&sks=b&skv=2020-08-04&sv=2020-08-04&sr=c&sig=oPmfi8drFf0PywWRQIzu35ojYyJA9OcfMuhsFUysWd4%3D";
                        var resp = clientB.DeleteAsync(deleteuri).Result;
                        resp.EnsureSuccessStatusCode();
                        Console.WriteLine("deleted successfully \n " + resp);


                    }
                    catch (Exception ex)
                    {
                        
                        Console.WriteLine(ex.Message);

                    }
                } 
                
                rdr.Close();
            }
            

            catch (Exception err)
            {

                Console.WriteLine(err.ToString());
            }
            

            conn.Close();
            Console.Read();




        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            
            DeleteBlobs();

        }








    }
}



