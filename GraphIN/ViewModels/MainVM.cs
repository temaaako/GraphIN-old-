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

        private string[] _time;
        private string[] _values;
        private int _counter;
        private int _step =1;

        public int Step
        {
            get { return _step; }
            set
            {
                if (value > 0)
                {
                    _step = value;
                }
                else
                {
                    _step = 1;
                }
            }
        }
        public MainVM()
        {
            //used to generate random values


            //lets instead plot elapsed milliseconds and value
            var mapper = Mappers.Xy<MeasureModel>()
                .X(x => x.Time)
                .Y(x => x.Value);



            //save the mapper globally         
            Charting.For<MeasureModel>(mapper);

            Values = new ChartValues<MeasureModel>();
            _stopwatch = new Stopwatch();
            _stopwatch.Start();

            _time = ExcelToArrayConverter.ConvertColumnToArray("C:\\Users\\MI\\Desktop\\данные.xlsx", 1);
            _values = ExcelToArrayConverter.ConvertColumnToArray("C:\\Users\\MI\\Desktop\\данные.xlsx", 18);
            _counter = 0;
        }

        public void SwitchDrawing()
        {


            var maxIndex = Math.Min(_time.Length, _values.Length);

            if (_isRunning)
            {
                _isRunning = false;

            }
            else
            {
                _isRunning = true;
            }


            Task.Run(() =>
            {
                while (_isRunning && _counter < maxIndex)
                {
                    Thread.Sleep(50);

                    float parsedValue;
                    if (float.TryParse(_values[_counter], out parsedValue) == false)
                    {
                        parsedValue = 0;
                    }

                    Values.Add(new MeasureModel
                    {
                        Time = float.Parse(_time[_counter]),
                        Value = parsedValue
                    });
                    _counter += _step;
                }
            });
        }

    }

}
