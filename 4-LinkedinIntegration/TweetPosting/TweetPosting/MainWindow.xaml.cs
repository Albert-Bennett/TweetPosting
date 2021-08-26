using System.Windows;

namespace TweetPosting
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPostMessage.Text) && !string.IsNullOrEmpty(txtUrl.Text))
            {
                txtPostMessage.Text = string.Empty;
                txtUrl.Text = string.Empty;

                MessageBox.Show("Post shared successfully");
            }
            else
            {
                MessageBox.Show("Fill in something for each textbox");
            }
        }
    }
}