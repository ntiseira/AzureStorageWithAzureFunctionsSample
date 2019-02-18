using ChatRoom.Commands;
using ChatRoom.Extensions;
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

        private string userLbl;     
        public string UserLbl
        {
            get
            {
                return userLbl;
            }
            set
            {
                if (userLbl != value)
                {
                    userLbl = value;
                    OnPropertyChanged("UserLbl");
                }
            }
        }



        private ICommand sendMessageCommand;

        private readonly IChatRoomService _ChatRoomService = new ChatRoomService();
        private string aliasUser;

        public ChatRoomMainViewModel(Window main , string aliasUser)
        {
            CurrentWindow = main;
            UserLbl = aliasUser;
            ListOfItems = new ObservableCollection<string>();
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
            //INSTANCIANDO EL TIMER CON LA CLASE DISPATCHERTIMER 
            DispatcherTimer dispathcer = new DispatcherTimer();

            //EL INTERVALO DEL TIMER ES DE 0 HORAS,0 MINUTOS Y 1 SEGUNDO 
            dispathcer.Interval = new TimeSpan(0, 0, 3);

            //EL EVENTO TICK SE SUBSCRIBE A UN CONTROLADOR DE EVENTOS UTILIZANDO LAMBDA 
            dispathcer.Tick += (s, a) =>
            {
                //AQUI VA LO QUE QUIERES QUE HAGA CADA 1 SEGUNDO 
                var backgroundWorker = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                backgroundWorker.DoWork += new DoWorkEventHandler(DoLongWork);
                backgroundWorker.RunWorkerAsync(Dispatcher.CurrentDispatcher);

                backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(
                    ProgressChanged);


            };
            dispathcer.Start();



        
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

        private ObservableCollection<string> listOfItems;

        public ObservableCollection<string> ListOfItems
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
      
                CurrentWindow.Dispatcher.BeginInvoke((Action)delegate () {
                    var list = ListOfItems;
                    var listUsers = this._ChatRoomService.GetNewsAsync(aliasUser).Result;


                    foreach (var item in listUsers)
                    {
                        ListOfItems.Add(item.UserName + ":" + item.Message);
                    }
                });
             
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
