using System.Collections.Generic;

namespace WPFLookupWithTries.Model
{
    class Node
    {
        public Dictionary<char, Node> map;
        bool isCompleteString;
        int id;
        public Node()
        {
            map = new Dictionary<char, Node>();
            isCompleteString = false;
        }

        public void AddContact(string contact,int id)
        {
            Node temp = this;
            for (int i = 0; i < contact.Length; i++)
            {
                char c = contact[i];

                if (!temp.map.ContainsKey(c))
                {
                    Node newNode = new Node();
                    temp.map.Add(c, newNode);
                    temp = newNode;
                    if (i == contact.Length - 1)
                    {
                        temp.isCompleteString = true;
                        temp.id = id;
                    }
                }
                else
                {
                    temp = temp.map[c];
                }
            }
        }
        public List<int> FindContactIds(string partial)
        {
            Node temp = this;
            for (int i = 0; i < partial.Length; i++)
            {
                char c = partial[i];
                if (temp.map.ContainsKey(c))
                {
                    temp = temp.map[c];
                }
                else
                {
                    return new List<int>();
                }
            }
            return TraverseTreeForContacts(temp);
        }
        List<int> TraverseTreeForContacts(Node n)
        {
            if (n == null)
                return new List<int>();
            List<int> ids = new List<int>();
            if (n.isCompleteString)
                ids.Add(n.id);
            foreach (var kvmap in n.map)
            {
                var result = TraverseTreeForContacts(kvmap.Value);
                if (result.Count > 0)
                {
                    ids.AddRange(result);
                }
            }
            return ids;
        }

        public Dictionary<int, string> LoadDummyContacts()
        {
            Dictionary<int, string> contacts = new Dictionary<int, string>();

            contacts.Add(1,"chan");
            contacts.Add(2,"chandra");
            contacts.Add(3,"chandrash");
            contacts.Add(4,"chandrashekhar");
            contacts.Add(5,"she");
            contacts.Add(6,"shek");
            contacts.Add(7,"shekhar");
            contacts.Add(8,"avi");
            contacts.Add(9,"avish");
            contacts.Add(10,"avishka");
            contacts.Add(11, "mo");
            contacts.Add(12, "mon");
            contacts.Add(13, "moni");
            contacts.Add(14, "mona");
            contacts.Add(15, "monu");
            contacts.Add(16, "monal");
            contacts.Add(17, "monali");
            foreach (var item in contacts)
            {
                this.AddContact(item.Value,item.Key);

            }
            return contacts;
        }

    }

}
