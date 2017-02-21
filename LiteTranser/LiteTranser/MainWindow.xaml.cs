using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LiteTranser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private readonly TextBox _tbInput;
        private State _currState = State.Disactive;

        private NetworkHandler _netWorker;
        enum State { 
            Active,
            Disactive
        }

        public MainWindow()
        {
            InitializeComponent();
            _tbInput = (TextBox)FindName("tb_input");
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (null != _tbInput && _currState==State.Active)
            {
                string result = _netWorker.DoTranslate(_tbInput.Text);
                UpdateUiState(State.Disactive,true,result);
            }
        }

        private void UpdateUiState(State state,bool readOnly,string text)
        {
            _currState = state;
            _tbInput.IsReadOnly = readOnly;
            if (!readOnly)
            {
                _tbInput.Focus();
                _tbInput.TabIndex = 0;
            }
            _tbInput.Text = text;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _netWorker = new NetworkHandler();
            UpdateUiState(State.Active,false,"");
            _tbInput.Focus();
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (null != _tbInput) 
            {
                UpdateUiState(State.Active,false,"");
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                if (null != _tbInput && _currState == State.Active)
                {
                    string result = _netWorker.DoTranslate(_tbInput.Text);
                    UpdateUiState(State.Disactive, true, result);
                }
            }
        }
    }
}
