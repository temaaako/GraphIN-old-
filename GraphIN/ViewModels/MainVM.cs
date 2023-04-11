using GraphIN.ViewModels;
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
using System.Timers;

namespace GraphIN
{
    public class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ChartValues<MeasureModel> Values { get; set; }
        private Stopwatch _stopwatch;
        private bool _isRunning;

        public MainVM() {
            //used to generate random values
            

            //lets instead plot elapsed milliseconds and value
            var mapper = Mappers.Xy<MeasureModel>()
                .X(x => x.ElapsedMilliseconds)
                .Y(x => x.Value);
            


            //save the mapper globally         
            Charting.For<MeasureModel>(mapper);

            Values = new ChartValues<MeasureModel>();
            _stopwatch = new Stopwatch();
            _stopwatch.Start();

            
        }

        public void SwitchDrawing()
        {
            var random = new Random();
            var time = 0f;
            if (_isRunning)
            {
                _isRunning = false;
                _stopwatch.Stop();
            }
            else
            {
                _isRunning = true;
                _stopwatch.Start();
            }
            Task.Run(() =>
            {
                while (_isRunning)
                {
                    Thread.Sleep(500);

                    //we add the lecture based on our StopWatch instance
                    Values.Add(new MeasureModel
                    {
                        ElapsedMilliseconds = _stopwatch.ElapsedMilliseconds,
                        Value = time += random.Next(0, 10)
                    });
                }
            });
        }

    }
    
}
