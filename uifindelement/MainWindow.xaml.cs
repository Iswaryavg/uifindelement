using System;
using System.Windows;
using System.Threading;
using System.Windows.Automation;
using Samples;
namespace uifindelement
{
    public partial class MainWindow : Window
    {

        Tracker reader = new Tracker();

        public MainWindow()
        {
            InitializeComponent();
            new Thread(Work).Start();
            var sampleWindow = new ShowcaseWindow();
            sampleWindow.ShowDialog();

        }

        void Work()
        {
            // Simulate time-consuming task
            Automation.AddAutomationFocusChangedEventHandler(reader.OnFocusChanged);
            Thread.Sleep(5000);
            UpdateMessage(controlNames);

        }

        public void UpdateMessage(string message)
        {
            Action action = () => txtBlock.Text = message;
            Dispatcher.Invoke(action);
            controlNames = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


        }


        static String controlNames;
        public static void callBack()
        {
            AutomationElement rootElement = AutomationElement.RootElement;
            System.Windows.Automation.Condition condition = new PropertyCondition
         (AutomationElement.NameProperty, "Program Manager");
            //}
            var winCollection = rootElement.FindAll(System.Windows.Automation.TreeScope.Children, condition);

            foreach (AutomationElement element in winCollection)
            {
                getChildren(element);
            }
        }
        private static void getChildren(AutomationElement element)
        {
            var children = element.FindAll(System.Windows.Automation.TreeScope.Children, System.Windows.Automation.Condition.TrueCondition);
            foreach (AutomationElement child in children)
            {
                controlNames += child.Current.Name + "\n";
                getChildren(child);
            }
        }

    }
}