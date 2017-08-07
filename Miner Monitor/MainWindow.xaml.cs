using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Timers;
using Newtonsoft.Json;

namespace Miner_Monitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string json = @"{ }";
        private string textInBlock;
        private string minerName;

        public MainWindow()
        {
            InitializeComponent();
            int timer = 10000;
        }

        public void Textbox_Focus (object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= Textbox_Focus;
        }

        private void Json_Click(object sender, RoutedEventArgs e)
        {
            String url = apiUrl.Text;
            textInBlock = "";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    json = wc.DownloadString(url);
                    
                }

                catch
                {
                    MessageBox.Show("Please enter a valid JSON URL");
                    return;
                }
            }
       
            dynamic convert = JsonConvert.DeserializeObject<dynamic>(json);

            foreach (dynamic a in convert.workers)
            {
                minerName = ((Newtonsoft.Json.Linq.JProperty)a).Name.ToString();                
                textInBlock = textInBlock + "Current hashrate for " + minerName + " is " + convert.workers.GetValue(minerName).hashrate + "\n";
            }
            jsonBlock.Text = textInBlock;
            //jsonBlock.Text = "Current hashrate for " +convert.workers.svettrig01.worker +" is " +convert.workers.svettrig01.hashrate;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            jsonBlock.Text = string.Empty;
        }

        private void Monitor_File()
        {
            string filePath = @"c:\temp\minerOutput.txt";
        }

        /* public class Svettrig01
        {
            public string worker { get; set; }
            public string hashrate { get; set; }
            public string reportedHashRate { get; set; }
            public int validShares { get; set; }
            public int invalidShares { get; set; }
            public int staleShares { get; set; }
            public int workerLastSubmitTime { get; set; }
            public int invalidShareRatio { get; set; }
        }

        public class Workers
        {
            public Svettrig01 svettrig01 { get; set; }
        }

        public class Round
        {
            public int id { get; set; }
            public string miner { get; set; }
            public int block { get; set; }
            public int work { get; set; }
            public object amount { get; set; }
            public int processed { get; set; }
        }

        public class Payout
        {
            public int id { get; set; }
            public string miner { get; set; }
            public int start { get; set; }
            public int end { get; set; }
            public object amount { get; set; }
            public string txHash { get; set; }
            public string paidOn { get; set; }
        }

        public class Settings
        {
            public string miner { get; set; }
            public string email { get; set; }
            public int monitor { get; set; }
            public long minPayout { get; set; }
        }

        public class MinerStats
        {
            public long time { get; set; }
            public long lastSeen { get; set; }
            public int reportedHashrate { get; set; }
            public double currentHashrate { get; set; }
            public int validShares { get; set; }
            public int invalidShares { get; set; }
            public int staleShares { get; set; }
            public double averageHashrate { get; set; }
            public int activeWorkers { get; set; }
        }

        public class Converters
        {
            public string address { get; set; }
            public Workers workers { get; set; }
            public long unpaid { get; set; }
            public List<Round> rounds { get; set; }
            public List<Payout> payouts { get; set; }
            public Settings settings { get; set; }
            public string hashRate { get; set; }
            public double avgHashrate { get; set; }
            public string reportedHashRate { get; set; }
            public MinerStats minerStats { get; set; }
            public double ethPerMin { get; set; }
            public double usdPerMin { get; set; }
            public double btcPerMin { get; set; }
        } */
    }
}
