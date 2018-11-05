using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge;
using System.Drawing;
using System.IO;
using MySql.Data.MySqlClient;
using System.Drawing.Imaging;

namespace Windows
{
    class Program
    {
       
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Mysql Photo Importer With Console Applicaon");
            Console.Write("Start YES/NO : ");
            string request = Console.ReadLine().ToUpper();
            if (request == "YES") take_photo();
            else return;            
            void take_photo(){
                MySqlConnection con = new MySqlConnection("Server=localhost;Database=example_webcam;Uid=root;Pwd=;");
                FilterInfoCollection webcam;
                VideoCaptureDevice cam;
                webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                cam = new VideoCaptureDevice(webcam[0].MonikerString);
                cam.Start();
                cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);
                void cam_NewFrame(object sender, NewFrameEventArgs eventArgs){
                    Bitmap bit = (Bitmap)eventArgs.Frame.Clone();
                    System.IO.MemoryStream ms = new MemoryStream();
                    bit.Save(ms, ImageFormat.Jpeg);
                    byte[] byteImage = ms.ToArray();
                    var SigBase64 = Convert.ToBase64String(byteImage);
                    Console.WriteLine(SigBase64);
                    MySqlCommand cmd = new MySqlCommand("insert into pictures(image) values('" + SigBase64 + "')", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine("Succesfull! ");
                    Console.WriteLine("--------------------------------------------------");
                    cam.Stop();                    
                }
            }
            Console.ReadLine();
        }
    }
}

