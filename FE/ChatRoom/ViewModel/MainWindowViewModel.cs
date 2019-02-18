using ChatRoom.Commands;
using ChatRoom.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChatRoom.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public Window CurrentWindow { get; set; }

        private ICommand chatRoomCommand;

        public MainWindowViewModel(Window main)
        {
            CurrentWindow = main; 
        }
       
        public ICommand ChatRoomCommand
        {
            get
            {
                if (chatRoomCommand == null)
                {
                    chatRoomCommand = new RelayCommand(
                        param => this.OpenChatRoomClick()
                    );
                }
                return chatRoomCommand;
            }
        }

        private string aliasUser;
        public string AliasUser
        {
            get
            {
                return aliasUser;
            }
            set
            {
                if (aliasUser != value)
                {
                    aliasUser = value;
                    OnPropertyChanged("AliasUser");
                }
            }
        }

        public void OpenChatRoomClick()
        {
            this.CurrentWindow.Hide();
            ChatRoomMain chatRoomMain = new ChatRoomMain(AliasUser);
            chatRoomMain.ShowDialog();
         
        }



    }
}
