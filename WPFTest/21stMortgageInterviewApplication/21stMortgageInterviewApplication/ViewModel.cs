using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace _21stMortgageInterviewApplication
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        List<int> _inputNumberList;

        public ViewModel()
        {
            _processInputCmd = new RelayCommand(ProcessInput, CanProcessInput);
        }
         
        private readonly ICommand _processInputCmd;
        public ICommand ButtonClickCommand { get { return _processInputCmd; } }

        public void ProcessInput(object obj)
        {
            string _cmdName = (string)obj;
            if (_cmdName == "btnLargeValue")
            {
                TxtResult = GetResults("Large");
            }
            else if (_cmdName == "btnSumOdd")
            {
                TxtResult = GetResults("Odd");
            }
            else if (_cmdName == "btnSumEven")
            {
                TxtResult = GetResults("Even");
            }
            int _resultNumber = 0;
            int.TryParse(_txtResult, out _resultNumber);
            if (_resultNumber > 0)
            {
                TxtResultColor = "Green";
            }
            else if (_resultNumber < 0)
            {
                TxtResultColor = "Red";
            }
            else {
                TxtResultColor = "White";
            }
        }

        public bool CanProcessInput(object obj)
        {
            if (obj != null)
                return true;
            return false;
        }

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _txtInputString;
        public string TxtInputString
        {
            get {
                return _txtInputString;
            }
            set {
                _txtInputString = value;
                OnPropertyChanged("TxtInputString");
            } 
        }

        private string _txtResult;
        public string TxtResult { 
            get {
                return _txtResult;
            }
            set {
                _txtResult = value;
                OnPropertyChanged("TxtResult");
            }
        }

        private string _txtResultColor;
        public string TxtResultColor {
            get
            {
                return _txtResultColor;
            }
            set
            {
                _txtResultColor = value;
                OnPropertyChanged("TxtResultColor");
            }
        }

        private string GetResults(string operationType) {
            string _returnValue = string.Empty;
            try
            {
                int sum = 0;
                List<string> inputStringList = _txtInputString.Split(",").ToList();
                _inputNumberList = inputStringList.Select(int.Parse).ToList();
                if (operationType == "Large" && _inputNumberList.Count > 0)
                {
                    _inputNumberList.Sort();
                    _returnValue = _inputNumberList[_inputNumberList.Count - 1].ToString();
                }
                else if ((operationType == "Odd" || operationType == "Even" ) && _inputNumberList.Count > 0) {

                    foreach (int number in _inputNumberList)
                    {
                        if (number % 2 != 0 && operationType == "Odd")
                        {
                            sum = sum + number;
                        }
                        else if (number % 2 == 0 && operationType == "Even")
                        {
                            sum = sum + number;
                        }
                    }
                    _returnValue = sum.ToString();
                }
                return _returnValue;
            }
            catch (FormatException)
            {
                _returnValue = "invalid input.";
            }
            return _returnValue;
        }
    }
}
