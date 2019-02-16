using ChatRoom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatRoom.Views
{
    /// <summary>
    /// Interaction logic for ChatRoom.xaml
    /// </summary>
    public partial class ChatRoomMain : Window
    {
        private string aliasUser;

        public ChatRoomMain(string aliasUser)
        {
            InitializeComponent();
            this.aliasUser = aliasUser;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChatRoomMainViewModel vm = new ChatRoomMainViewModel(this, aliasUser);
            this.DataContext = vm;
        }
    }
}
