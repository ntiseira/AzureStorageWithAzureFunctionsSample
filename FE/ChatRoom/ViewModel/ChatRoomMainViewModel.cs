using ChatRoom.Commands;
using ChatRoom.Services;
using ChatRoom.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ChatRoom.ViewModel
{
    public class ChatRoomMainViewModel : ViewModelBase
    {
        public Window CurrentWindow { get; set; }

        private ICommand sendMessageCommand;

        private readonly IChatRoomService _ChatRoomService = new ChatRoomService();
        private string aliasUser;

        public ChatRoomMainViewModel(Window main , string aliasUser)
        {
            CurrentWindow = main;
            ListOfItems = new ObservableCollection<object>();
            this.aliasUser = aliasUser;
            GetMessages();
        }

        public ICommand SendMessageCommand
        {
            get
            {
                if (sendMessageCommand == null)
                {
                    sendMessageCommand = new RelayCommand(
                        param => this.SendGeneralMessage()
                    );
                }
                return sendMessageCommand;
            }
        }


        private void GetMessages()
        {            

            var backgroundWorker = new BackgroundWorker
            {
                 WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            backgroundWorker.DoWork += new DoWorkEventHandler(DoLongWork);
            backgroundWorker.RunWorkerAsync(Dispatcher.CurrentDispatcher);

            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(
                ProgressChanged);     

        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ListOfItems = ListOfItems;

        }

        public static void OperationCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //UPDATE LIST OF MESSAGE IF EXIST NEW MESSAGES
            Console.WriteLine(
                "Operation has either completed successfully or has been cancelled");
        }

        private ObservableCollection<object> listOfItems;

        public ObservableCollection<object> ListOfItems
        {
            get
            {
                return listOfItems;
            }
            set
            {
                if (listOfItems != value)
                {
                    listOfItems = value;
                   OnPropertyChanged("ListOfItems");
                }
            }
        }


        /// <summary>
        /// Call queqe Storage of azure to get new messages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DoLongWork(object sender, DoWorkEventArgs e)
        {
           // Dispatcher dispatcher = e.Argument as Dispatcher; // this is right ui dispatcher

            CurrentWindow.Dispatcher.BeginInvoke((Action)delegate () {
                var list = ListOfItems;
                 ListOfItems = list;
            });


            while (true)
            {
               // var worker = sender as BackgroundWorker;
                Console.WriteLine("Operation has started");

                //Call to get new messages
                Thread.Sleep(1000);

                CurrentWindow.Dispatcher.BeginInvoke((Action)delegate () {
                    var list = ListOfItems;
                     ListOfItems = list;
                });

             
            }
        }

        private string textInserted;
        public string TextInserted
        {
            get
            {
                return textInserted;
            }
            set
            {
                if (textInserted != value)
                {
                    textInserted = value;
                    OnPropertyChanged("TextInserted");
                }
            }
        }


        public void SendGeneralMessage()
        {
            string message =  aliasUser + ": " + TextInserted;
            var list = ListOfItems;
            list.Add(message);
            ListOfItems = list;

            message = "General:" + message;

            _ChatRoomService.AddMessageAsync(message);
 
            //clean textbox
            TextInserted = string.Empty;
        }

    }
}
