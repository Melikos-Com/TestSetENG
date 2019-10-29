using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public class Orders
    {

        public KeyValuePair<string, OrderType> majorOrder { get; set; }
        public List<KeyValuePair<string, OrderType>> minorOrder { get; set; }

        public Orders()
        {
            this.majorOrder = new KeyValuePair<string, OrderType>();
            this.minorOrder = new List<KeyValuePair<string, OrderType>>();
        }


        public void Add(OrderType type,
                        string prop)
        {

            if (string.IsNullOrEmpty(prop))
                return;

            var keypair = new KeyValuePair<string, OrderType>(prop, type);

            if (object.Equals(this.majorOrder, default(KeyValuePair<string, OrderType>)))
            {
                this.majorOrder = keypair;
            }
            else
            {
                this.minorOrder.Add(keypair);
            }

        }



        public void Clear()
        {
            this.majorOrder = new KeyValuePair<string, OrderType>();
            this.minorOrder = new List<KeyValuePair<string, OrderType>>();
        }

        public KeyValuePair<string, OrderType> GetMajor() => this.majorOrder;

        public List<KeyValuePair<string, OrderType>> GetMinor() => this.minorOrder;



    }
}
