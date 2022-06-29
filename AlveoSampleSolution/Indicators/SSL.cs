// References to include
using System;
using System.ComponentModel;
using System.Windows.Media;
using Alveo.Interfaces.UserCode;
using Alveo.UserCode;
using Alveo.Common;
using Alveo.Common.Classes;

// namespace must be Alveo.UserCode
namespace Alveo.UserCode
{
    
    public class SSL : IndicatorBase
    {
        #region Properties


        private readonly Array<double> ssld = new Array<double>();
        private readonly Array<double> sslu = new Array<double>();
        private readonly Array<double> Hlv = new Array<double>();

        [Category("Settings")]
        [Description("Weight")]
        public int Lb { get; set; }

        #endregion


        public SSL()
        {
            // Basic indicator initialization. Don't use this constructor to calculate values
            indicator_chart_window = true;
            indicator_buffers = 2;
            indicator_color1 = Colors.Red;
            indicator_color2 = Colors.Green;
            indicator_width1 = 2;
            indicator_width2 = 2;
            Lb = 8;
            
            //fooBar = 0;
            copyright = "";
            link = "";
            
        }

        //+-------- EXTERNAL PARAMETERS HERE --------+
        //[Category("My Category")]
        //[DisplayName("My Display Name")]
        //public int fooBar { get; set; }

        //+------------------------------------------------------------------+");
        //| Custom indicator initialization function                         |");
        //+------------------------------------------------------------------+");
        protected override int Init()
        {
            /* MT4 Code
            IndicatorBuffers(3);
            SetIndexBuffer(0, ssld); SetIndexDrawBegin(0, Lb + 1);
            SetIndexBuffer(1, sslu); SetIndexDrawBegin(0, Lb + 1);
            SetIndexBuffer(2, Hlv);
            */
            //SetIndexBuffer(0, ssld); //Set the first buffer
            ////SetIndexStyle(0, DRAW_LINE); //Set the style for the buffer, this will draw the values as a line
            ////SetIndexLabel(0, "SSL Down"); //Set the label for the buffer
            //SetIndexDrawBegin(0, Lb + 1); //Set the start index for the buffer

            //SetIndexBuffer(1, sslu); 
            ////SetIndexStyle(1, DRAW_LINE);
            ////SetIndexLabel(1, "SSL Up");
            //SetIndexDrawBegin(0, Lb + 1);
            //SetIndexBuffer(2, Hlv);
            
            IndicatorBuffers(3);
            
   			SetIndexBuffer(0,ssld);
   			SetIndexDrawBegin(0,Lb+1);
   			SetIndexStyle(0, DRAW_LINE); //Set the style for the buffer, this will draw the values as a line
            SetIndexLabel(0, "SSL Down"); //Set the label for the buffer
            
   			SetIndexBuffer(1, sslu);
			SetIndexDrawBegin(0,Lb+1);   			
   			SetIndexStyle(1, DRAW_LINE);
            SetIndexLabel(1, "SSL Up");
   			
   			SetIndexBuffer(2, Hlv);
   			SetIndexStyle(3, DRAW_NONE);
   			SetIndexLabel(3, "Weight");
            // ENTER YOUR CODE HERE
            return 0;
        }

        //+------------------------------------------------------------------+");
        //| Custom indicator deinitialization function                       |");
        //+------------------------------------------------------------------+");
        protected override int Deinit()
        {
            // ENTER YOUR CODE HERE
            return 0;
        }

        //+------------------------------------------------------------------+");
        //| Custom indicator iteration function                              |");
        //+------------------------------------------------------------------+");
        protected override int Start()
        {
            int counted_bars = IndicatorCounted();
            // ENTER YOUR CODE HERE
            int i, limit;
            //----
            if (counted_bars < 0) return (-1);
            if (counted_bars > 0) counted_bars--;
            limit = Bars - counted_bars;
            //----
            for (i = limit; i >= 0; i--)
            {
                Hlv[i] = Hlv[i + 1];
                if (Close[i] > iMA(Symbol(), 0, Lb, 0, MODE_SMA, PRICE_HIGH, i + 1)) Hlv[i] = 1;
                if (Close[i] < iMA(Symbol(), 0, Lb, 0, MODE_SMA, PRICE_LOW, i + 1)) Hlv[i] = -1;
                if (Hlv[i] == -1)
                {
                    ssld[i] = iMA(Symbol(), 0, Lb, 0, MODE_SMA, PRICE_HIGH, i + 1);
                    sslu[i] = iMA(Symbol(), 0, Lb, 0, MODE_SMA, PRICE_LOW, i + 1);
                }
                else
                {
                    ssld[i] = iMA(Symbol(), 0, Lb, 0, MODE_SMA, PRICE_LOW, i + 1);
                    sslu[i] = iMA(Symbol(), 0, Lb, 0, MODE_SMA, PRICE_HIGH, i + 1);
                }
            }
            return 0;
        }


        //+------------------------------------------------------------------+
        //| AUTO GENERATED CODE. THIS METHODS USED FOR INDICATOR CACHING     |
        //+------------------------------------------------------------------+
        #region Auto Generated Code

        [Description("Parameters order Symbol, TimeFrame, param_1")]
        public override bool IsSameParameters(params object[] values)
        {
            if (values.Length != 3)
                return false;

            if (!CompareString(Symbol, (string)values[0]))
                return false;

            if (TimeFrame != (int)values[1])
                return false;



            return true;
        }

        [Description("Parameters order Symbol, TimeFrame, param_1")]
        public override void SetIndicatorParameters(params object[] values)
        {
            if (values.Length != 3)
                throw new ArgumentException("Invalid parameters number");

            Symbol = (string)values[0];
            TimeFrame = (int)values[1];
            

        }

        #endregion
    }
}

