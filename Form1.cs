using BitMEX;
using BitMexBot.Trades;
using BitMexBot.Position;
using BitMexBot.Bots;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace BitMexBot
{
    public partial class MainForm : Form
    {
        Account A = new Account();
        Thread TrackPriceThread;
        double xBt = 0;

        public MainForm()
        {
           
            InitializeComponent();
        }

        private void button_GetRecentTrades_Click(object sender, EventArgs e)
        {
            RecentTrades trades = new RecentTrades();
            string Symbol = comboBox_RecentTradesSymbol.Text;
            trades.GetRecentTrades(A, Symbol);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Transaction s = new Transaction();

            string Symbol = comboBox_TransactionCryptoType.Text;
            Decimal Price = Convert.ToDecimal(numericUpDown_TransactionLimitPrice.Text);
            uint Quantity = Convert.ToUInt32(numericUpDown_TransactionQuantity.Text);
            double Leverage = Convert.ToDouble(numericUpDown_TransactionLeverage.Text);

            bool PostOnly = false;
           
            if(this.checkBox_TransactionPostOnly.Checked)
            {
                PostOnly = true;
            }

            s.CreateTransaction(A, PostOnly, Quantity, Transaction.OrderTypes.Limit, Price, Symbol, "Sell");
        }


        private void TrackPrice()
        {
            instrument I = new instrument();
            bool Tracking = true;
            double MarkPrice = I.GetInstrumentPrice(A, this.comboBox_RecentTradesSymbol.Text);
            int IncreaseCounter = 0;
            int DecreaseCounter = 0;

            Console.WriteLine("Starting Mark Price: " + MarkPrice + " - Getting price trend please wait 20-30s");

            while(Tracking)
            {
                string Symbol = this.comboBox_RecentTradesSymbol.Text; //change to combobox
             
                double OldMarkPrice = MarkPrice;
                MarkPrice = I.GetInstrumentPrice(A, Symbol);     
                if(MarkPrice > OldMarkPrice) //increase
                {
                    IncreaseCounter += 1;
                }

                if(MarkPrice < OldMarkPrice)
                {
                    DecreaseCounter += 1;
                }

                if (IncreaseCounter >= 20 || DecreaseCounter >= 20)
                {
                    if(IncreaseCounter > DecreaseCounter)
                    {
                        Console.WriteLine("Trend Increasing.");

                        //write to file: csv for input to AI program
                        using (System.IO.StreamWriter file =
                            new System.IO.StreamWriter(@"C:\Users\pello\OneDrive\Desktop\BTC.csv", true))
                        {
                            file.WriteLine("{0},{1},{2},{3}", DateTime.Now, MarkPrice, "UP", MarkPrice + 5);
                        }

                        DecreaseCounter = 0;
                        IncreaseCounter = 0;
                    }
                    else if (DecreaseCounter > IncreaseCounter)
                    {
             
                        using (System.IO.StreamWriter file =
                            new System.IO.StreamWriter(@"C:\Users\pello\OneDrive\Desktop\BTC.csv", true))
                        {
                            file.WriteLine("{0},{1},{2},{3}", DateTime.Now, MarkPrice, "DOWN", MarkPrice - 5);
                        }
                        Console.WriteLine("Trend Decreasing");
                        DecreaseCounter = 0;
                        IncreaseCounter = 0;
                    }
                }

                Thread.Sleep(1000);

                if(A.AutoTrading)
                {
                    //place shit order here
                    Transaction T = new Transaction();

                    if(IncreaseCounter > DecreaseCounter) //buy
                    {
                        //check open positions, we need to switch sides

                        if (T.CreateTransaction(A, true, 222, Transaction.OrderTypes.Limit, Convert.ToDecimal(MarkPrice), Symbol, "Buy"))
                            Console.WriteLine("Bought 222 contracts.");
                    }
                    else if(DecreaseCounter > IncreaseCounter) //sell
                    {
                        //check open positions, we need to switch sides


                        T.CreateTransaction(A, true, 222, Transaction.OrderTypes.Limit, Convert.ToDecimal(MarkPrice), Symbol, "Sell");
                            Console.WriteLine("Sold 222 contracts.");
                    }

                    DecreaseCounter = 0;
                    IncreaseCounter = 0;
                }


            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("INITIALIZING CRAB-17... DIALING LORD BOG FOR ACCESS CONTROL...\n\n");
            Console.WriteLine("BOY SMINEM CURRENTLY VISITING A MUSEUM WITH GRANDPARENTS. ACCESS APPROVED.\n");

            A.FillAPIKeys("./Keys.txt");
            Console.WriteLine(A.APIKey + " " +  A.SecretKey);
            double Satoshi = A.GetWalletBalance();
            Console.WriteLine(Satoshi);
            Console.WriteLine("CRAB-17 WELCOMES YOU. PLEASE USE WISELY. \nALL CREDITS TO XIANLABS LTD: DEVLEOPED FOR LORDS BOGDANOFF.\n\n");

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Satoshi: " + Satoshi);
            Console.WriteLine("XBT: " + (0.00000001 * Convert.ToDouble(Satoshi)));
            this.xBt = (0.00000001 * Convert.ToDouble(Satoshi));
            Console.WriteLine("Account: " + A.AccountId);
            Console.WriteLine("Email: " + A.Email + " Readable name: " + A.ReadableName);
            Console.ResetColor();

            Console.WriteLine("\nGlobal recommended leverage for BTC, ETH: 10x or less depending on wallet size.");
            Console.WriteLine("Shorting is usually recommended. Find a dumpy market (eth, btc, xrp)");
        }


        private void button_GetAccountOrders_Click(object sender, EventArgs e)
        {
            Order s = new Order();

            string Symbol = comboBox_RecentTradesSymbol.Text;

            s.GetOrders(A, Symbol);
        }

        private void button_Long_Click(object sender, EventArgs e)
        {
            Transaction L = new Transaction();

            string Symbol = comboBox_TransactionCryptoType.Text;
            Decimal Price = Convert.ToDecimal(numericUpDown_TransactionLimitPrice.Text);
            uint Quantity = Convert.ToUInt32(numericUpDown_TransactionQuantity.Text);
            double Leverage = Convert.ToDouble(numericUpDown_TransactionLeverage.Text);

            bool PostOnly = false;

            if (this.checkBox_TransactionPostOnly.Checked)
            {
                PostOnly = true;
            }

            L.CreateTransaction(A, PostOnly, Quantity, Transaction.OrderTypes.Limit, Price, Symbol, "Buy");
        }

        private void button_BuyMarket_Click(object sender, EventArgs e)
        {
        }

        private void button_FastSwap_Click(object sender, EventArgs e)
        {
            BitMexBot.Bots.Scalp FS = new BitMexBot.Bots.Scalp();

            string symbol = comboBox_FastSwapSymbol.Text;
            string side = comboBox_FastSwapSide.Text;
            uint Quantity = Convert.ToUInt32(numericUpDown_FastSwapQuantity.Text); //all converts needs try/catches

            bool result = FS.MarketTransaction(A, Quantity, side, symbol);

            if(result == true)
            {
                bool deltaResult = FS.MarketTransaction(A, Quantity, side, symbol); //need to flip this and add or remove cents based on sell/buy
            }

        }

        private void whoIsSnimenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("knowyourmeme.com");
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("All credits go to XIAN LABS INC. Credits for REST API to BitMex.");
        }

        private void contactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You wish nub. HA.");
            MessageBox.Show("But srs. Kingpin#1068, looking for experienced traders + TA's.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BitMEXApi api = new BitMEXApi(A.APIKey, A.SecretKey);
            string result =  api.GetOrders(this.comboBox_RecentTradesSymbol.Text);
            Console.WriteLine(result);
        }

        private void button_SetLeverage_Click(object sender, EventArgs e)
        {
            leverage L = new leverage();
            L.ChangeLeverage(A, this.comboBox_TransactionCryptoType.Text, 1.0);

        }

        private void button_GetTrollbox_Click(object sender, EventArgs e)
        {
            trollbox tb = new trollbox();
            tb.GetTrollboxData(A, 10, 1);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            instrument inst = new instrument();
            inst.GetInstrumentPrice(A, this.comboBox_RecentTradesSymbol.Text);

        }

        private void checkBox_TrackPrice_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_TrackPrice.Checked == true)
            {
                TrackPriceThread = new Thread(new ThreadStart(this.TrackPrice));
                TrackPriceThread.Start();
            }
            else
            {
                TrackPriceThread.Abort();
            }
        }

        private void button_PostTrollbox_Click(object sender, EventArgs e)
        {
            string Txt = textBox_TrollboxText.Text;
            trollbox tb = new trollbox();
            tb.PostMessage(A, Txt);
        }

        private void button_GetPositions_Click(object sender, EventArgs e)
        {
            //get current positions
            position OpenPos = new position();
            if (OpenPos.GetOpenPositions(A, this.comboBox_RecentTradesSymbol.Text, 1, "[\"liquidationPrice\"]") == false)
            {
                Console.WriteLine("GetOpenPositions failed.");
            }
            else
            {
                Console.WriteLine("GetOpenPositions succeeded.");
                Console.WriteLine(OpenPos.currency + " " + OpenPos.account + " " + OpenPos.bankruptPrice + " " + OpenPos.symbol + " " + OpenPos.currentCost);
            }
        }

        private void button_CancelAllOrders_Click(object sender, EventArgs e)
        {
            BitMEXApi api = new BitMEXApi(A.APIKey, A.SecretKey);
            api.DeleteOrder("test");
        }

        private void checkBox_AutoPurchase_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox_AutoPurchase.Checked == true)
            {
                A.AutoTrading = true;
            }
            else
            {
                A.AutoTrading = false;
            }

        }

        private void checkBox_AntiLiquidation_CheckedChanged(object sender, EventArgs e)
        {
            

            if (this.checkBox_AntiLiquidation.Checked == true)
            {
                //get current positions
                position OpenPos = new position();
                if (OpenPos.GetOpenPositions(A, this.comboBox_RecentTradesSymbol.Text, 1, "[\"liquidationPrice\"]") == false)
                {
                    Console.WriteLine("GetOpenPositions failed.");
                    
                    //PreventLiqidationsThread.Start();
                }
                else
                {
                    Console.WriteLine("GetOpenPositions succeeded.");
                    Console.WriteLine(OpenPos.currency + " " + OpenPos.account + " " + OpenPos.bankruptPrice + " " + OpenPos.symbol + " " + OpenPos.currentCost);
                    position.PreventLiquidations(this.A, this.comboBox_RecentTradesSymbol.Text);
                }
            }
            else
            {

            }

        }
    }
}
