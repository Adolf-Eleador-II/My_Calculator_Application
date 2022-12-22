using CommunityToolkit.Mvvm.Input;
using My_Calculator_Project;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using static My_Calculator_Project.Memory;

namespace My_Calculator_Application
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class mainViewModel : INotifyPropertyChanged
    {

        public string input_display = string.Empty;
        public string output_display = string.Empty;
        public string color_output_display = "#888";

        public string InputDisplay
        {
            get { return input_display; }
            set
            {
                input_display = value;
                OnPropertyChanged();
            }
        }
        public string OutputDisplay
        {
            get { return output_display; }
            set
            {
                output_display = value;
                OnPropertyChanged();
            }
        }
        public string ColorOutputTextBox
        {
            get { return color_output_display; }
            set
            {
                color_output_display = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<string> ButtonClickCommand { get; }
        public RelayCommand<StructMemory> GetExamCommand { get; }
        public RelayCommand<StructMemory> GetResCommand { get; }
        public RelayCommand<StructMemory> RemoveElementCommand { get; }

        public Parser parser = new();

        public mainViewModel()
        {
            ButtonClickCommand = new RelayCommand<string>(ButtonClick);
            GetExamCommand = new RelayCommand<StructMemory>(GetExam);
            GetResCommand = new RelayCommand<StructMemory>(ResExam);
            RemoveElementCommand = new RelayCommand<StructMemory>(RemoveElement);
        }
        private void ButtonClick(string? button)
        {
            switch (button)
            {
                case "C":
                    InputDisplay = string.Empty;
                    OutputDisplay = string.Empty;
                    break;
                case "=":
                    try
                    {
                        OutputDisplay = parser.ParserExperession(InputDisplay).ToString();
                        ColorOutputTextBox = "#000";
                        memory.AddMemory(InputDisplay, OutputDisplay);
                        OnPropertyChanged(nameof(ListMemory));
                    }
                    catch
                    {
                        ColorOutputTextBox = "#F00";
                        OutputDisplay = "ERROR";
                    }
                    break;
                default:
                    InputDisplay += button;
                    ColorOutputTextBox = "#888";
                    break;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public Memory memory = new();
        public ObservableCollection<StructMemory> ListMemory => new(memory.list_memory);
        public void GetExam(StructMemory id)
        {
            InputDisplay += memory.GetExample(id);
        }
        public void ResExam(StructMemory id)
        {
            InputDisplay += memory.GetResult(id);
        }
        public void RemoveElement(StructMemory _example)
        {
            memory.RemoveMemory(_example);
            memory.SaveMemory();
            OnPropertyChanged(nameof(ListMemory));
        }
    }
}
