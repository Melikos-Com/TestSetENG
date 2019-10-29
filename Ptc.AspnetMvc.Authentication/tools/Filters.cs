using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public class Filters<T>
    {
        private List<KeyValuePair<AppendPredicateWith, Expression<Func<T, bool>>>> filters;


        public Filters()
        {
            this.filters = new List<KeyValuePair<AppendPredicateWith, Expression<Func<T, bool>>>>();
        }


        public void Add(AppendPredicateWith linqType,
                        Expression<Func<T, bool>> exp)
        {

            var keypair = new KeyValuePair<AppendPredicateWith, Expression<Func<T, bool>>>(linqType, exp);

            filters.Add(keypair);

        }

        public void Clear()
            => this.filters = new List<KeyValuePair<AppendPredicateWith, Expression<Func<T, bool>>>>();
        
        public List<KeyValuePair<AppendPredicateWith, Expression<Func<T, bool>>>> Get()
            => this.filters ?? new List<KeyValuePair<AppendPredicateWith, Expression<Func<T, bool>>>>();



    }
}
