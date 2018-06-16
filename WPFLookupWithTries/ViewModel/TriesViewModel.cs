using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFLookupWithTries.Model;

namespace WPFLookupWithTries.ViewModel
{
    class TriesViewModel : INotifyPropertyChanged
    {
        Node triesRoot;
        Dictionary<int, string> map;
        public TriesViewModel()
        {
            LoadContacts();
          
        }

        async void LoadContacts()
        {
            await Task.Run(
                () =>
                {
                    triesRoot = new Node();
                    //uncomment below line to have few records to filter
                    //map = n.LoadDummyContacts();
                    // Read the file and add it line by line as a contact.  
                    System.IO.StreamReader file =
                        new System.IO.StreamReader("Model\\contacts.txt");
                    string line;
                    map = new Dictionary<int, string>();
                    int counter = 1;
                    while ((line = file.ReadLine()) != null)
                    {
                        map.Add(counter, line);
                        triesRoot.AddContact(line, counter++);
                    }

                    file.Close();
                    Contacts = new ObservableCollection<string>(map.Values.ToList());
                    RecordsCount = string.Format("Result count :{0}", Contacts.Count);
                }
            );
        }

        string contactPartial;
        public string ContactPartial
        {
            get
            {
                return contactPartial;
            }

            set
            {
                contactPartial = value;
                RaisePropertyChanged("ContactPartial");
                BindContacts();
            }
        }
        string recordsCount;
        public string RecordsCount
        {
            get
            {
                return recordsCount;
            }

            set
            {
                recordsCount = value;
                RaisePropertyChanged("RecordsCount");

            }
        }


        ObservableCollection<string> contacts = new ObservableCollection<string>();

        public ObservableCollection<string> Contacts
        {
            get
            {
                return contacts;
            }
            set
            {
                contacts = value;
                RaisePropertyChanged("Contacts");
            }
        }
        async void BindContacts()
        {
            await Task.Run(() =>
           {
               List<int> filtered = triesRoot.FindContactIds(ContactPartial);
               List<string> filteredRecords = new List<string>();
               foreach (var item in map)
               {
                   if (filtered.Contains(item.Key))
                   {
                       filteredRecords.Add(item.Value);
                   }
               }
               Contacts = new ObservableCollection<string>(filteredRecords);
               RecordsCount = string.Format("Result count :{0}", filteredRecords.Count);
           });

        }

        void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
