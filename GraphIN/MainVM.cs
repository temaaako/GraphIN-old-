using LiveCharts;
using LiveCharts.Configurations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GraphIN
{
    public class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ChartValues<MeasureModel> Values { get; set; }


        public MainVM() {
            //used to generate random values
            var random = new Random();
            var time = 0d;

            //lets instead plot elapsed milliseconds and value
            var mapper = Mappers.Xy<MeasureModel>()
                .X(x => x.ElapsedMilliseconds)
                .Y(x => x.Value);
            

            //save the mapper globally         
            Charting.For<MeasureModel>(mapper);

            Values = new ChartValues<MeasureModel>();
            var sw = new Stopwatch();
            sw.Start();

            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(500);

                    //we add the lecture based on our StopWatch instance
                    Values.Add(new MeasureModel
                    {
                        ElapsedMilliseconds = sw.ElapsedMilliseconds,
                        Value = time += random.Next(0, 10)
                    });
                }
            });
        }

    }
    public class MeasureModel
    {
        public double ElapsedMilliseconds { get; set; }
        public double Value { get; set; }
    }
}
